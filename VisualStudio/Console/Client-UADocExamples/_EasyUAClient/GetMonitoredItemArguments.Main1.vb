' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to obtain parameters of certain monitored item subscription.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class GetMonitoredItemArguments
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification

            Console.WriteLine("Subscribing...")
            Dim handleArray() As Integer = client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() _
               { _
                   New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845", 1000), _
                   New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000), _
                   New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10855", 1000) _
               } _
            )

            Console.WriteLine("Getting monitored item arguments...")
            Dim monitoredItemArguments As EasyUAMonitoredItemArguments = client.GetMonitoredItemArguments(handleArray(2))
            Console.WriteLine("NodeDescriptor: {0}", monitoredItemArguments.NodeDescriptor)
            Console.WriteLine("SamplingInterval: {0}", monitoredItemArguments.MonitoringParameters.SamplingInterval)
            Console.WriteLine("PublishingInterval: {0}", monitoredItemArguments.SubscriptionParameters.PublishingInterval)

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)
        End Sub

        Private Shared Sub client_DataChangeNotification(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            ' Your code would do the processing here
        End Sub
    End Class
End Namespace

#End Region
