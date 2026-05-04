' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain acknowledge an event.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.AlarmsAndConditions
Imports OpcLabs.EasyOpc.UA.Filtering
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Friend Class Acknowledge
        Public Shared Sub Main1()

            ' Instantiate the client object
            Dim client = New EasyUAClient()
            Dim alarmsAndConditionsClient As IEasyUAAlarmsAndConditionsClient = client.AsAlarmsAndConditionsClient()

            Dim nodeId As UANodeId = Nothing
            Dim eventId As Byte() = Nothing
            Dim anEvent = New ManualResetEvent(initialState:=False)

            Console.WriteLine("Subscribing...")
            client.SubscribeEvent(
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer",
                UAObjectIds.Server,
                1000,
                New UAEventFilterBuilder(
                    UAFilterElements.Equals(
                        UABaseEventObject.Operands.NodeId,
                        New UANodeId(nodeIdExpandedText:="nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Colours/EastTank?Yellow")),
                        UABaseEventObject.AllFields),
                Sub(sender, eventArgs)
                    If Not eventArgs.Succeeded Then
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                        Return
                    End If
                    If eventArgs.EventData IsNot Nothing Then
                        Dim baseEventObject = eventArgs.EventData.BaseEvent
                        Console.WriteLine(baseEventObject)

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

            Console.WriteLine("Waiting for an event for 30 seconds...")
            If Not anEvent.WaitOne(30 * 1000) Then
                Console.WriteLine("Event not received")
                Return
            End If

            Console.WriteLine("Acknowledging an event...")
            Try
                alarmsAndConditionsClient.Acknowledge(
                    "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer",
                    nodeId,
                    eventId,
                    "Acknowledged by an automated example code")
            Catch uaException As UAException
                Console.WriteLine("Failure: {0}", uaException.GetBaseException().Message)
            End Try

            Console.WriteLine("Waiting for 5 seconds...")
            Thread.Sleep(5 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Thread.Sleep(5 * 1000)
        End Sub
    End Class
End Namespace

#End Region
