' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to unsubscribe from changes of just some monitored
' items.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class UnsubscribeMultipleMonitoredItems
        Public Shared Sub Some()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification2

            Console.WriteLine()
            Console.WriteLine("Subscribing...")
            Dim handleArray() As Integer = client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() _
               {
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000),
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000),
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
               }
            )

            For i As Integer = 0 To handleArray.Length - 1
                Console.WriteLine($"handleArray[{i}]: {handleArray(i)}")
            Next i

            Console.WriteLine()
            Console.WriteLine("Processing monitored item changed events for 10 seconds...")
            System.Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine()
            Console.WriteLine("Unsubscribing from 2 monitored items...")
            ' We will unsubscribe from the first and third monitored item we have
            ' previously subscribed to.
            client.UnsubscribeMultipleMonitoredItems(New Integer() {handleArray(0), handleArray(2)})

            Console.WriteLine()
            Console.WriteLine("Processing monitored item changed events for 10 seconds...")
            System.Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine()
            Console.WriteLine("Unsubscribing from all remaining monitored items...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            System.Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub client_DataChangeNotification2(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            ' Display value
            If e.Succeeded Then
                Console.WriteLine($"{e.Arguments.NodeDescriptor}: {e.AttributeData.Value}")
            Else
                Console.WriteLine($"{e.Arguments.NodeDescriptor} *** Failure: {e.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace

#End Region
