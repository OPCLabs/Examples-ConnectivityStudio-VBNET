' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to create and delete OPC UA files, using the file transfer client.
' Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.Extensions
Imports OpcLabs.EasyOpc.UA.FileTransfer
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.FileTransfer._EasyUAFileTransferClient

    Friend Class CreateFileAndDelete

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor =
                New UAEndpointDescriptor("opc.tcp://localhost:48030") _
                .WithUserNameIdentity("john", "master")

            ' An object that aggregates an OPC UA file system.
            Dim objectDescriptor As UANodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files"

            ' Create a random number generator - will be used for file/directory names.
            Dim random = New Random

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            ' Create two files, and delete the first one.
            Try
                ' The file system node is a root directory of the file system.
                Console.WriteLine("Getting file system...")
                Dim fileSystemNodeDescriptor As UANodeDescriptor = fileTransferClient.GetFileSystem(endpointDescriptor, objectDescriptor)

                Dim fileName1 As String = "MyFile1-" & random.Next()
                Console.WriteLine($"Creating first file, '{fileName1}'...")
                Dim fileNodeId1 As UANodeId = fileTransferClient.CreateFile(endpointDescriptor, fileSystemNodeDescriptor, fileName1)
                Console.WriteLine($"Node Id of the first file: {fileNodeId1}")

                Dim fileName2 As String = "MyFile2-" & random.Next()
                Console.WriteLine($"Creating second file, '{fileName2}'...")
                Dim fileNodeId2 As UANodeId = fileTransferClient.CreateFile(endpointDescriptor, fileSystemNodeDescriptor, fileName2)
                Console.WriteLine($"Node Id of the second file: {fileNodeId2}")

                Console.WriteLine("Deleting the first file...")
                fileTransferClient.DeleteFile(endpointDescriptor, fileSystemNodeDescriptor, fileName1)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
