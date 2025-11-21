' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read 4 items from an OPC XML-DA server at once, and display their values, timestamps 
' and qualities.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class ReadMultipleItems
        Public Shared Sub Main1Xml()
            Dim client = New EasyDAClient()

            Dim vtqResults() As DAVtqResult = client.ReadMultipleItems(
                    New ServerDescriptor() With {.UrlString = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx"},
                    New DAItemDescriptor() _
                    { _
                        "Dynamic/Analog Types/Double",
                        "Dynamic/Analog Types/Double[]",
                        "Dynamic/Analog Types/Int",
                        "SomeUnknownItem"
                    })

            For i = 0 To vtqResults.Length - 1
                Debug.Assert(vtqResults(i) IsNot Nothing)

                If vtqResults(i).Exception IsNot Nothing Then
                    Console.WriteLine("vtqResult[{0}] *** Failure: {1}", i, vtqResults(i).ErrorMessageBrief)
                    Continue For
                End If
                Console.WriteLine("vtqResult[{0}].Vtq: {1}", i, vtqResults(i).Vtq)
            Next i
        End Sub
    End Class
End Namespace
#End Region
