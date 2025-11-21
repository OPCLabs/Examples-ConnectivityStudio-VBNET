' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to get OPC UA file properties (such as its size or writable status), using the file transfer client.
' Note: Consider using a higher-level abstraction, OPC UA file provider, instead.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.FileTransfer
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.FileTransfer._EasyUAFileTransferClient

    Friend Class GetFileProperties

        Public Shared Sub Main1()

            ' Unified Automation .NET based demo server (UaNETServer/UaServerNET.exe)
            Dim endpointDescriptor As UAEndpointDescriptor = "opc.tcp://localhost:48030"

            ' A node that represents an instance of OPC UA FileType object.
            Dim fileNodeDescriptor As UANodeDescriptor = "nsu=http://www.unifiedautomation.com/DemoServer/ ;s=Demo.Files.TextFile"

            ' Instantiate the file transfer client object
            Dim fileTransferClient = New EasyUAFileTransferClient

            ' Get properties of a specified file.
            Dim fileProperties As UAFileProperties
            Console.WriteLine("Getting file properties...")
            Try
                fileProperties = fileTransferClient.GetFileProperties(endpointDescriptor, fileNodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display result
            Console.WriteLine()
            Console.WriteLine($"MimeType: {fileProperties.MimeType}")
            Console.WriteLine($"OpenCount: {fileProperties.OpenCount}")
            Console.WriteLine($"Size: {fileProperties.Size}")
            Console.WriteLine($"UserWritable: {fileProperties.UserWritable}")
            Console.WriteLine($"Writable: {fileProperties.Writable}")
            Console.WriteLine($"Timestamp: {fileProperties.Timestamp}")

            Console.WriteLine()
            Console.WriteLine("Finished...")
        End Sub
    End Class
End Namespace

#End Region
