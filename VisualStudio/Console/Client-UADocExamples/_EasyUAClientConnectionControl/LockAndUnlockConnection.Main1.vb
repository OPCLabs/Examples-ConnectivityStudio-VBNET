' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to lock and unlock connections to an OPC UA server. The component attempts to keep the locked
' connections open, until unlocked.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Services

Namespace Global.UADocExamples._EasyUAClientConnectionControl
    Partial Friend Class LockAndUnlockConnection
        Public Shared Sub Main1()
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Using client = New EasyUAClient()
                ' Obtain the client connection monitoring service.
                Dim clientConnectionMonitoring As IEasyUAClientConnectionMonitoring = client.GetService(Of IEasyUAClientConnectionMonitoring)
                If clientConnectionMonitoring Is Nothing Then
                    Console.WriteLine("The client connection monitoring service is not available.")
                    Exit Sub
                End If

                ' Obtain the client connection control service.
                Dim clientConnectionControl As IEasyUAClientConnectionControl = client.GetService(Of IEasyUAClientConnectionControl)
                If clientConnectionControl Is Nothing Then
                    Console.WriteLine("The client connection control service is not available.")
                    Exit Sub
                End If

                ' Display the server condition changed events.
                AddHandler clientConnectionMonitoring.ServerConditionChanged, Sub(sender, args)
                                                                                  Console.WriteLine(args)
                                                                              End Sub

                Dim lockHandle As Integer = 0
                Dim locked As Boolean = False
                Try
                    Console.WriteLine("Reading (1)")
                    ' The first read will cause a connection to the server.
                    Dim attributeData1 As UAAttributeData =
                        client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                    Console.WriteLine(attributeData1)

                    Console.WriteLine("Waiting for 10 seconds...")
                    ' Since the connection is now not used for some time, and it is not locked, it will be closed.
                    System.Threading.Thread.Sleep(10 * 1000)

                    Console.WriteLine("Locking")
                    ' Locking the connection causes it to open, if possible.
                    lockHandle = clientConnectionControl.LockConnection(endpointDescriptor)
                    locked = True

                    Console.WriteLine("Waiting for 10 seconds...")
                    ' The connection is locked, it will not be closed now.
                    System.Threading.Thread.Sleep(10 * 1000)

                    Console.WriteLine("Reading (2)")
                    Dim attributeData2 As UAAttributeData =
                        client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                    Console.WriteLine(attributeData2)

                    Console.WriteLine("Waiting for 10 seconds...")
                    ' The connection is still locked, it will not be closed now.
                    System.Threading.Thread.Sleep(10 * 1000)
                Catch uaException As UAException
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException().Message)
                Finally
                    If locked Then
                        Console.WriteLine("Unlocking")
                        clientConnectionControl.UnlockConnection(lockHandle)
                    End If
                End Try

                Console.WriteLine("Waiting for 10 seconds...")
                ' After some delay, the connection will be closed, because there are no subscriptions to the server and no
                ' connection locks.
                System.Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Finished.")
            End Using
        End Sub

        ' Example output
        '
        'Reading (1)
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success; Attempt #1
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        '-1.034588E+18 {Single} @2021-11-15T15:26:39.169 @@2021-11-15T15:26:39.169; Good
        'Waiting for 10 seconds...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(RetrialDelay=Infinite); Success
        'Locking
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connecting; Success; Attempt #1
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Connected; Success
        'Waiting for 10 seconds...
        'Reading (2)
        '2.288872E+21 {Single} @2021-11-15T15:26:59.836 @@2021-11-15T15:26:59.836; Good
        'Waiting for 10 seconds...
        'Unlocking
        'Waiting for 10 seconds...
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnecting; Success
        '"opc.tcp://opcua.demo-this.com:51210/UA/SampleServer" Disconnected(RetrialDelay=Infinite); Success
        'Finished.
    End Class
End Namespace

#End Region
