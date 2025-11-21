' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to read different sections from an OPC UA file stream.
' Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.IO
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.FileTransfer
Imports OpcLabs.EasyOpc.UA.IO.Extensions

Namespace Global.UADocExamples.FileTransfer._UAFileStream

    Friend Class ReadAndSeek

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor = "opc.tcp://localhost:48030"

            ' A node that represents an instance of OPC UA FileType object.
            Dim fileNodeDescriptor As UANodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile"

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            Try
                ' Get a stream object that corresponds to an OPC UA file.
                Console.WriteLine("Opening the file for reading...")
                Using stream As Stream = fileTransferClient.OpenStream(endpointDescriptor, fileNodeDescriptor)
                    ' Get a stream object that corresponds to an OPC UA file.
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
                ' OPC UA errors encountered during opening of an UA file stream And operations on such stream are transformed
                ' to IOException-s.
            Catch ioException As IOException
                Console.WriteLine("*** Failure: {0}", ioException.GetBaseException.Message)
                Exit Sub
            End Try

            Console.WriteLine()
            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
