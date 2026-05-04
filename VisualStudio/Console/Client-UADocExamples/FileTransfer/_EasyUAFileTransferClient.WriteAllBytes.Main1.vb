' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to write the full contents of an OPC UA file at once, using the file transfer client.
' Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.IO
Imports System.Text
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.FileTransfer
Imports OpcLabs.EasyOpc.UA.IO.Extensions

Namespace Global.UADocExamples.FileTransfer._EasyUAFileTransferClient

    Friend Class WriteAllBytes

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor = "opc.tcp://localhost:48030"

            ' A node that represents an instance of OPC UA FileType object.
            Dim fileNodeDescriptor As UANodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile"

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            ' Write all contents into a specified file node.
            Dim bytes As Byte() = Encoding.UTF8.GetBytes("TEXT FROM FILE TRANSFER CLIENT EXAMPLE. Demonstrates writing the whole contents of a file at once.")
            Try
                Console.WriteLine("Writing the whole file...")
                fileTransferClient.WriteAllBytes(endpointDescriptor, fileNodeDescriptor, bytes)

                ' Due to an issue in the server, the file might Not be readable now, without server restart.
                'Console.WriteLine("Reading the data back...")
                'Dim data As Byte() = fileTransferClient.ReadAllFileBytes(endpointDescriptor, fileNodeDescriptor)
                'Console.WriteLine(Encoding.UTF8.GetString(data))

                ' Beware that WriteAllFileBytes throws IOException And Not UAException.
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
