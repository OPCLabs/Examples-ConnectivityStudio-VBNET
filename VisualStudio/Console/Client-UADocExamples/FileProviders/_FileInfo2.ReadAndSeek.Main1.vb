' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to read different sections from an OPC UA file, using the file provider model.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.IO
Imports OpcLabs.BaseLib.Extensions.FileProviders
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.FileTransfer

Namespace Global.UADocExamples.FileProviders._FileInfo2

    Friend Class ReadAndSeek

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor = "opc.tcp://localhost:48030"

            ' A node that represents an instance of OPC UA FileType object.
            Dim fileNodeDescriptor As UANodeDescriptor =
                "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile"

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            Console.WriteLine("Getting file info...")
            Dim fileInfo2 As IFileInfo2 =
                fileTransferClient.GetFileInfo2(endpointDescriptor, fileNodeDescriptor)
            ' From this point onwards, the code is independent of the concrete realization of the file provider, and would
            ' be identical e.g. for files in the physical file system, if the corresponding file provider was used.

            ' Open the file, read two separate sections of it, and close it.
            Try
                Console.WriteLine("Opening file...")
                Using stream As Stream = fileInfo2.CreateReadStream()
                    Console.WriteLine("Reading first section of a stream...")
                    Dim buffer1(16 - 1) As Byte
                    Dim bytesRead1 As Integer = stream.Read(buffer1, 0, buffer1.Length)
                    Console.WriteLine($"{bytesRead1} bytes read, buffer: {BitConverter.ToString(buffer1)}")

                    Console.WriteLine("Reading second section of a stream...")
                    Dim buffer2(10 - 1) As Byte
                    Dim bytesRead2 As Integer = stream.Read(buffer2, 0, buffer2.Length)
                    Console.WriteLine($"{bytesRead2} bytes read, buffer: {BitConverter.ToString(buffer2)}")

                    Console.WriteLine("Seeking...")
                    stream.Seek(100, SeekOrigin.Begin)

                    Console.WriteLine("Reading third section of a stream...")
                    Dim buffer3(20 - 1) As Byte
                    Dim bytesRead3 As Integer = stream.Read(buffer3, 0, buffer3.Length)
                    Console.WriteLine($"{bytesRead3} bytes read, buffer: {BitConverter.ToString(buffer3)}")
                End Using

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
