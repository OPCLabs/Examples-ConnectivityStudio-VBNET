' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to read different sections from an OPC UA file, using the file transfer client.
' Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.FileTransfer
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.FileTransfer._EasyUAFileTransferClient

    Friend Class ReadAndSetFilePosition

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor = "opc.tcp://localhost:48030"

            ' A node that represents an instance of OPC UA FileType object.
            Dim fileNodeDescriptor As UANodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile"

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            ' Open the file, read two separate sections of it, and close it.
            Try
                Console.WriteLine("Opening file...")
                Using fileHandle As UAFileHandle = fileTransferClient.OpenFile(endpointDescriptor, fileNodeDescriptor, UAOpenFileModes.Read)
                    Console.WriteLine("Reading first file section...")
                    Dim bytes1 As Byte() = fileTransferClient.ReadFile(endpointDescriptor, fileNodeDescriptor, fileHandle, 16)
                    Console.WriteLine($"First section: {BitConverter.ToString(bytes1)}")

                    Console.WriteLine("Reading second file section...")
                    Dim bytes2 As Byte() = fileTransferClient.ReadFile(endpointDescriptor, fileNodeDescriptor, fileHandle, 10)
                    Console.WriteLine($"Second section: {BitConverter.ToString(bytes2)}")

                    Console.WriteLine("Setting file position...")
                    fileTransferClient.SetFilePosition(endpointDescriptor, fileNodeDescriptor, fileHandle, 100)

                    Console.WriteLine("Reading third file section...")
                    Dim bytes3 As Byte() = fileTransferClient.ReadFile(endpointDescriptor, fileNodeDescriptor, fileHandle, 20)
                    Console.WriteLine($"Third section: {BitConverter.ToString(bytes3)}")

                    Console.WriteLine("Closing file...")
                    fileTransferClient.CloseFile(endpointDescriptor, fileNodeDescriptor, fileHandle)
                End Using
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
