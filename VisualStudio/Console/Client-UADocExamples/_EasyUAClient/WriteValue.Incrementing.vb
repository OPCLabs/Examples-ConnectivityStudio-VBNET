' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to write an ever-incrementing value to an OPC UA variable.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports Opc.Ua
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class WriteValue
        Public Shared Sub Incrementing()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            Dim nodeId As UANodeId = "nsu=http://test.org/UA/Data/ ;i=10221"
            ' Example settings with Softing dataFEED OPC Suite: 
            'Dim endpointDescriptor As UAEndpointDescriptor =
            '    "opc.tcp://localhost:4980/Softing_dataFEED_OPC_Suite_Configuration1"
            'Dim nodeId As UANodeId = "nsu=Local%20Items ;s=Local Items.EAK_Test1.EAK_Testwert1_I4"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            '
            Dim i As Integer = 0

            Do
                Console.WriteLine($"@{DateTime.Now}: Writing {i}")
                Try
                    client.WriteValue(endpointDescriptor, nodeId, i)
                Catch uaException As UAException
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                    Exit Sub
                End Try
                i = CInt((CLng(i) + 1) And &H7FFFFFFF)
                Thread.Sleep(2 * 1000)
            Loop Until Console.KeyAvailable
        End Sub
    End Class
End Namespace

#End Region
