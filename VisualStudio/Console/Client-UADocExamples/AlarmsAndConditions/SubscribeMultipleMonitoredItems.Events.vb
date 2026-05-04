' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to multiple events.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.AlarmsAndConditions
Imports OpcLabs.EasyOpc.UA.Filtering
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Partial Friend Class SubscribeMultipleMonitoredItems
        Public Shared Sub Events()

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.EventNotification, AddressOf client_EventNotification

            Console.WriteLine("Subscribing...")

            client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() _
                { _
                    New EasyUAMonitoredItemArguments("firstState", _
                        "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                        UAObjectIds.Server, _
                        New UAMonitoringParameters(1000, New UAEventFilterBuilder( _
                            UAFilterElements.GreaterThanOrEqual(UABaseEventObject.Operands.Severity, 500), _
                            UABaseEventObject.AllFields))) _
                        With {.AttributeId = UAAttributeId.EventNotifier}, _
                    New EasyUAMonitoredItemArguments("secondState", _
                        "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                        UAObjectIds.Server, _
                        New UAMonitoringParameters(2000, New UAEventFilterBuilder( _
                            UAFilterElements.Equals( _
                                UABaseEventObject.Operands.SourceNode, _
                                New UANodeId("nsu=http://opcfoundation.org/Quickstarts/AlarmCondition ;ns=2;s=1:Metals/SouthMotor")), _
                            UABaseEventObject.AllFields))) _
                        With {.AttributeId = UAAttributeId.EventNotifier} _
                } _
             )

            Console.WriteLine("Processing event notifications for 30 seconds...")
            Threading.Thread.Sleep(30 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)
        End Sub

        Private Shared Sub client_EventNotification(ByVal sender As Object, ByVal e As EasyUAEventNotificationEventArgs)
            ' Display the event
            Console.WriteLine(e)
        End Sub
    End Class
End Namespace

#End Region
