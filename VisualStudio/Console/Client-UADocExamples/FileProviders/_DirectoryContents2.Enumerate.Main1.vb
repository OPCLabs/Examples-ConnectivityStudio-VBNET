' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to browse for OPC UA files and directories, using the file provider model.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Extensions.FileProviders
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Extensions
Imports OpcLabs.EasyOpc.UA.FileTransfer

Namespace Global.UADocExamples.FileProviders._DirectoryContents2

    Friend Class Enumerate

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor =
                New UAEndpointDescriptor("opc.tcp://localhost:48030") _
                .WithUserNameIdentity("john", "master")

            ' A node that represents an OPC UA file system (a root directory).
            Dim fileSystemNodeDescriptor As UANodeDescriptor =
                "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.FileSystem"

            ' Create a random number generator - will be used for file/directory names.
            Dim random = New Random

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            Console.WriteLine("Getting writable file provider...")
            Dim writableFileProvider As IWritableFileProvider =
                fileTransferClient.GetWritableFileProvider(endpointDescriptor, fileSystemNodeDescriptor)
            ' From this point onwards, the code is independent of the concrete realization of the file provider, and would
            ' be identical e.g. for files in the physical file system, if the corresponding file provider was used.

            ' Create two files, and then browse the directory that contains them.
            Try
                Dim fileName1 As String = "MyFile1-" & random.Next()
                Console.WriteLine($"Creating first file, '{fileName1}'...")
                Dim writableFileInfo1 As IWritableFileInfo = writableFileProvider.GetWritableFileInfo(fileName1)
                writableFileInfo1.WriteAllBytes(Array.Empty(Of Byte))

                Dim fileName2 As String = "MyFile2-" & random.Next()
                Console.WriteLine($"Creating second file, '{fileName2}'...")
                Dim writableFileInfo2 As IWritableFileInfo = writableFileProvider.GetWritableFileInfo(fileName2)
                writableFileInfo2.WriteAllBytes(Array.Empty(Of Byte))

                Console.WriteLine("Browsing for files...")
                Dim directoryContents2 As IDirectoryContents2 = writableFileProvider.GetDirectoryContents2(Nothing)

                For Each fileInfo2 As IFileInfo2 In directoryContents2
                    Console.WriteLine(fileInfo2)
                Next fileInfo2
                ' You can distinguish between files and directories using the IFileInfo.IsDirectory property.

                ' Methods in the file provider model throw IOException and other exceptions, but not UAException.
            Catch exception As Exception
                Console.WriteLine("*** Failure: {0}", exception.GetBaseException.Message)
                Exit Sub
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
