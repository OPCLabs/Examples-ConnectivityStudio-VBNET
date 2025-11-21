' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to monitor connections to and disconnections from the OPC UA server.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Services

Namespace Global.UADocExamples._EasyUAClientConnectionMonitoring
    Partial Friend Class ServerConditionChanged
        Public Shared Sub Main1()
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object.
            Using client = New EasyUAClient()
                ' Obtain the client connection monitoring service.
                Dim clientConnectionMonitoring As IEasyUAClientConnectionMonitoring = client.GetService(Of IEasyUAClientConnectionMonitoring)
                If clientConnectionMonitoring Is Nothing Then
                    Console.WriteLine("The client connection monitoring service is not available.")
                    Exit Sub
                End If

                ' Hook events.
                AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification
                AddHandler clientConnectionMonitoring.ServerConditionChanged, AddressOf clientConnectionMonitoring_OnServerConditionChanged

                Try
                    Console.WriteLine("Reading (1)")
                    ' The first read will cause a connection to the server.
                    Dim attributeData1 As UAAttributeData = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                    Console.WriteLine(attributeData1)

                    Console.WriteLine("Reading (2)")
                    ' The second read, because it closely follows the first one, will reuse the connection that is already
                    ' open.
                    Dim attributeData2 As UAAttributeData = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                    Console.WriteLine(attributeData2)
                Catch uaException As UAException
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message)
                End Try

                Console.WriteLine("Waiting for 10 seconds...")
                ' Since the connection is now Not used for some time, it will be closed.
                System.Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Subscribing...")
                ' Subscribing to a monitored item will cause a connection to the server, if one is not yet open.
                client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000)

                Console.WriteLine("Waiting for 10 seconds...")
                ' The connection will not be closed as long as there are any subscriptions to the server.
                System.Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing...")
                client.UnsubscribeAllMonitoredItems()

                Console.WriteLine("Waiting for 10 seconds...")
                ' After some delay, the connection will be closed, because there are no subscriptions to the server.
                System.Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Finished.")
            End Using
        End Sub

        Private Shared Sub client_DataChangeNotification(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            ' Display value
            If e.Succeeded Then
                Console.WriteLine($"Value: {e.AttributeData.Value}")
            Else
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}")
            End If
        End Sub

        Private Shared Sub clientConnectionMonitoring_OnServerConditionChanged(ByVal sender As Object, ByVal e As EasyUAServerConditionChangedEventArgs)
            ' Display the event
            Console.WriteLine(e)
        End Sub


        ' Example output
        '
        'Reading(1)
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        '-8.095801E+32 {System.Single} @2021-06-24T07:09:46.062 @@2021-06-24T07:09:46.062; Good
        'Reading(2)
        '-3.11389E-18 {System.Single} @2021 - 06 - 24T07: 09:46.125 @@2021 - 06 - 24T07: 09:46.125; Good
        'Waiting for 10 seconds...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(Infinite); Success
        'Subscribing...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success
        'Waiting for 10 seconds...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        'Value: 3.293034E-31
        'Value: 6.838126E+37
        'Value: 5.837702E-29
        'Value: 0.02940081
        'Value: -9.653872E-17
        'Value: -1.012749E+29
        'Value: -1.422391E+20
        'Value: -3.56571E-22
        'Unsubscribing...
        'Waiting for 10 seconds...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(Infinite); Success
        'Finished.
    End Class
End Namespace

#End Region
