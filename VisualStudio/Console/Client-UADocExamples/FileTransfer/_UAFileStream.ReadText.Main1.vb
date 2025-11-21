' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to open an OPC UA file stream for reading, and read its content using a text reader object.
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

    Friend Class ReadText

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

                ' We know that the file contains text, so we read it using a stream reader. If the file content was
                ' binary, you would process the stream according to the data format.
                Using streamReader As StreamReader = fileTransferClient.OpenStreamReader(endpointDescriptor, fileNodeDescriptor)
                    ' The OPC UA stream reader object behaves like any other stream reader in .NET.

                    ' Read in the text from the file and display it line by line.
                    Console.WriteLine()
                    Console.WriteLine("Reading text lines:")
                    Dim i As Integer = 0
                    While Not streamReader.EndOfStream
                        Dim line As String = streamReader.ReadLine()
                        Console.WriteLine($"[{i}] {line}")
                        i += 1
                    End While
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
