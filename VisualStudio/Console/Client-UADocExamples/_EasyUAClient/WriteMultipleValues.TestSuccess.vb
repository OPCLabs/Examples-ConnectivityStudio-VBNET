' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to write values into 3 nodes at once, test for success of each write and display the exception 
' message in case of failure.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class WriteMultipleValues
        Public Shared Sub TestSuccess()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Modify value of a node
            Dim operationResultArray() As OperationResult = client.WriteMultipleValues(New UAWriteValueArguments() _
                { _
                    New UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10221", 23456), _
                    New UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10226", "This string cannot be converted to Double"), _
                    New UAWriteValueArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;s=UnknownNode", "ABC") _
                } _
             )

            For i As Integer = 0 To operationResultArray.Length - 1
                If operationResultArray(i).Succeeded Then
                    Console.WriteLine("Result {0}: success", i)
                Else
                    Console.WriteLine("Result {0}: {1}", i, operationResultArray(i).Exception.GetBaseException().Message)
                End If
            Next i
        End Sub
    End Class
End Namespace

#End Region
