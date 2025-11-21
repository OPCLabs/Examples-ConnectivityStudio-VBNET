' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to changes of multiple monitored items with absolute deadband.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class SubscribeMultipleMonitoredItems
        Public Shared Sub AbsoluteDeadband()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification_AbsoluteDeadband

            Console.WriteLine("Subscribing with different absolute deadbands...")
            ' The UADataChangeFilter has an implicit conversion from Double, which creates a filter with the specified
            ' absolute deadband.
            client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() _
                {
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=11194",    ' /Data.Dynamic.AnalogScalar.Int32Value
                        New UAMonitoringParameters(samplingInterval:=100, dataChangeFilter:=50)),
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=11218",    ' /Data.Dynamic.AnalogScalar.FloatValue
                        New UAMonitoringParameters(samplingInterval:=100, dataChangeFilter:=20))
                }
             )

            Console.WriteLine("Processing monitored item changed events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)
        End Sub

        Private Shared Sub client_DataChangeNotification_AbsoluteDeadband(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            ' Display value
            If e.Succeeded Then
                Console.WriteLine("{0}: {1}", e.Arguments.NodeDescriptor, e.AttributeData.Value)
            Else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.NodeDescriptor, e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace

#End Region
