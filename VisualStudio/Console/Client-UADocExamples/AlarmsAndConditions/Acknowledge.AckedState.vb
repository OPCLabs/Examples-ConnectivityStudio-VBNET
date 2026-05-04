' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain acknowledged state of events, and acknowledge an event that is not acknowledged yet.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.BaseLib
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.AlarmsAndConditions
Imports OpcLabs.EasyOpc.UA.Filtering
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Partial Friend Class Acknowledge
        Public Shared Sub AckedState()
            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer"

            ' Instantiate the client object
            Dim client = New EasyUAClient()
            Dim alarmsAndConditionsClient As IEasyUAAlarmsAndConditionsClient = client.AsAlarmsAndConditionsClient()

            Dim nodeId As UANodeId = Nothing
            Dim eventId As Byte() = Nothing
            Dim anEvent = New ManualResetEvent(initialState:=False)

            ' Prepare the Select clauses.
            Dim selectClauses As UAAttributeFieldCollection = UABaseEventObject.AllFields
            Dim ackedStateIdOperand As UASimpleAttributeOperand = UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/AckedState/Id")
            selectClauses.Add(ackedStateIdOperand)

            Console.WriteLine("Subscribing...")
            client.SubscribeEvent(
                endpointDescriptor,
                UAObjectIds.Server,
                1000,
                New UAEventFilterBuilder(UAFilterElements.LessThan(UABaseEventObject.Operands.Severity, 200),
                    selectClauses), ' We will auto-acknowledge an event with severity less than 200.
                Sub(sender, eventArgs)
                    If Not eventArgs.Succeeded Then
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}")
                        Return
                    End If

                    Dim eventData As UAEventData = eventArgs.EventData
                    If eventData IsNot Nothing Then
                        Dim baseEventObject = eventArgs.EventData.BaseEvent
                        Console.WriteLine(baseEventObject)

                        ' Obtain the acknowledge state of the event.
                        Dim ackedStateIdResult As ValueResult = eventData.FieldResults(ackedStateIdOperand)
                        Debug.Assert(ackedStateIdResult IsNot Nothing)
                        If Not ackedStateIdResult.Succeeded Then
                            Return
                        End If
                        Dim ackedStateId As Boolean? = If((TypeOf ackedStateIdResult.Value Is Boolean), CType(ackedStateIdResult.Value, Boolean?), Nothing)
                        Console.WriteLine($"AckedState/Id: {ackedStateId}")

                        ' Only attempt to acknowledge when Not acknowledged yet.
                        If Not ackedStateId = False Then
                            Return
                        End If


                        ' Make sure we do not catch the event more than once
                        If anEvent.WaitOne(0) Then
                            Return
                        End If

                        nodeId = baseEventObject.NodeId
                        eventId = baseEventObject.EventId

                        anEvent.Set()
                    End If
                End Sub,
                state:=Nothing)

            Console.WriteLine("Waiting for an acknowledgeable event for 10 minutes...")
            If Not anEvent.WaitOne(10 * 60 * 1000) Then
                Console.WriteLine("Event not received")
                Return
            End If

            Console.WriteLine()
            Console.WriteLine("Acknowledging an event...")
            Try
                alarmsAndConditionsClient.Acknowledge(
                    endpointDescriptor,
                    nodeId,
                    eventId,
                    "Acknowledged by an automated example code.")
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}")
            End Try

            Console.WriteLine("Waiting for 5 seconds...")
            Thread.Sleep(5 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace

#End Region
