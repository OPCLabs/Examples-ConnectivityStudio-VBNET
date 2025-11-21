' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to register and unregister multiple nodes in an OPC UA server, and use this approach together with
' connection locking.
'
' Node registration (with OPC UA servers that support it) can improve performance with repeatedly accessed nodes.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Services
Imports OpcLabs.EasyOpc.UA.Services.Extensions

Namespace Global.UADocExamples._EasyUAClientNodeRegistration
    Friend Class RegisterAndUnregisterMultipleNodes
        Public Shared Sub Main1()
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client.
            Using client = New EasyUAClient()
                ' Obtain the client node registration service.
                Dim clientNodeRegistration As IEasyUAClientNodeRegistration = client.GetRequiredService(Of IEasyUAClientNodeRegistration)

                ' Obtain the client connection control service.
                Dim clientConnectionControl As IEasyUAClientConnectionControl = client.GetRequiredService(Of IEasyUAClientConnectionControl)

                Dim nodeDescriptorArray = New UANodeDescriptor() {
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[0]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[4]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[8]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[12]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[16]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[20]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[24]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[28]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[32]",
                    "nsu=http://samples.org/UA/memorybuffer/Instance ;ns=8;s=UInt32[36]"
                }

                Console.WriteLine("Registering nodes")
                Dim registrationHandleArray() As Integer = clientNodeRegistration.RegisterMultipleNodes(endpointDescriptor, nodeDescriptorArray)

                Console.WriteLine("Locking the connection")
                ' Locking the connection will attempt to open it, and when successful, the nodes will be registered with
                ' the server at that time. The use of locking is not necessary, but it may bring benefits together with the
                ' node registration. See the conceptual documentation for more information.
                Dim lockHandle As Integer = clientConnectionControl.LockConnection(endpointDescriptor)

                Console.WriteLine("Waiting for 10 seconds...")
                ' The example uses this delay to demonstrate the fact that your code might have other tasks to do, before
                ' it accesses the previously registered nodes.
                System.Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Reading (1)")
                Dim resultArray1() As UAAttributeDataResult = client.ReadMultiple(endpointDescriptor, nodeDescriptorArray)
                For Each result As UAAttributeDataResult In resultArray1
                    Console.WriteLine(result)
                Next result

                Console.WriteLine("Reading (2)")
                Dim resultArray2() As UAAttributeDataResult = client.ReadMultiple(endpointDescriptor, nodeDescriptorArray)
                For Each result As UAAttributeDataResult In resultArray2
                    Console.WriteLine(result)
                Next result

                Console.WriteLine("Unlocking the connection")
                clientConnectionControl.UnlockConnection(lockHandle)

                Console.WriteLine("Unregistering nodes")
                clientNodeRegistration.UnregisterMultipleNodes(registrationHandleArray)

                Console.WriteLine("Finished.")
            End Using
        End Sub
    End Class
End Namespace

#End Region
