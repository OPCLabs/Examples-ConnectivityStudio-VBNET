' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to copy an OPC UA file, using the file transfer client.
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

    Friend Class Copy

        Public Shared Sub File1()

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

            ' Create a file, and a directory. Then, copy the file into the directory.
            Try
                ' The file system node is a root directory of the file system.
                Console.WriteLine("Getting file system...")
                Dim fileSystemNodeDescriptor As UANodeDescriptor = fileTransferClient.GetFileSystem(endpointDescriptor, objectDescriptor)

                Dim fileName As String = "MyFile-" & random.Next()
                Console.WriteLine($"Creating file, '{fileName}'...")
                Dim fileNodeId As UANodeId = fileTransferClient.CreateFile(endpointDescriptor, fileSystemNodeDescriptor, fileName)
                Console.WriteLine($"Node Id of the file: {fileNodeId}")

                Dim directoryName As String = "MyDirectory-" & random.Next()
                Console.WriteLine($"Creating directory, '{directoryName}'...")
                Dim directoryNodeId As UANodeId = fileTransferClient.CreateDirectory(endpointDescriptor, fileSystemNodeDescriptor, directoryName)
                Console.WriteLine($"Node Id of the directory: {directoryNodeId}")

                Console.WriteLine("Copying the file...")
                fileTransferClient.CopyFile(endpointDescriptor, fileSystemNodeDescriptor, fileNodeId, directoryNodeId)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
