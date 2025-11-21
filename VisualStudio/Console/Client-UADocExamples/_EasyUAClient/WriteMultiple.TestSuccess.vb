' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to write data (a value, timestamps and status code) into 3 nodes at once, test for success of each 
' write and display the exception message in case of failure.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class WriteMultiple
        Public Shared Sub TestSuccess()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Modify data of nodes' attributes
            Dim operationResultArray() As OperationResult = client.WriteMultiple(New UAWriteArguments() _
              {
                  New UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10221",
                                       New UAAttributeData(23456, UASeverity.GoodOrSuccess, Date.UtcNow)),
                  New UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10226",
                                       New UAAttributeData(2.3456789, UASeverity.GoodOrSuccess, Date.UtcNow)),
                  New UAWriteArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10227",
                                       New UAAttributeData("ABC", UASeverity.GoodOrSuccess, Date.UtcNow))
              }
            )

            ' The target server may not support this, and in such case failures will occur.

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
