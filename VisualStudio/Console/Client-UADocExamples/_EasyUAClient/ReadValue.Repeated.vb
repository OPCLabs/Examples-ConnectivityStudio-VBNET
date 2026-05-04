' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to repeatedly read value of a single node, and display it.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class ReadValue
        Public Shared Sub Repeated()

            ' Define which server we will work with.
            Const endpointDescriptorUrlString As String =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Const nodeIdExpandedText As String = "nsu=http://test.org/UA/Data/ ;i=10221"
            ' Example settings with Softing dataFEED OPC Suite:
            '  endpointDescriptorUrlString = "opc.tcp://localhost:4980/Softing_dataFEED_OPC_Suite_Configuration1";
            '  nodeIdExpandedText = "nsu=Local%20Items ;s=Local Items.EAK_Test1.EAK_Testwert1_I4";

            ' Instantiate the client object.
            Dim client = New EasyUAClient()

            For i = 1 To 60
                Console.Write($"@{DateTime.Now}: ")

                ' Obtain value of a node
                Dim value As Object
                Try
                    value = client.ReadValue(endpointDescriptorUrlString, nodeIdExpandedText)
                Catch uaException As UAException
                    Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}")
                    Exit Sub
                End Try

                ' Display results
                Console.WriteLine($"Read {value}")

                '
                Thread.Sleep(1000)
            Next i
        End Sub
    End Class
End Namespace

#End Region
