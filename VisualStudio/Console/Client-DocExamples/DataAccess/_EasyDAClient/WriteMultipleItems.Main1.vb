' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable StringLiteralTypo
#Region "Example"
' This example shows how to write values, timestamps and qualities into 3 items at once.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteMultipleItems
        Public Shared Sub Main1()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Console.WriteLine("Writing multiple items...")
            Dim resultArray() As OperationResult = client.WriteMultipleItems(New DAItemVtqArguments() {
                New DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_I4",
                    New DAVtq(23456, DateTime.UtcNow, DAQualities.GoodNonspecific)),
                New DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_R8",
                    New DAVtq(2.3456789, DateTime.UtcNow, DAQualities.GoodNonspecific)),
                New DAItemVtqArguments("OPCLabs.KitServer.2", "Simulation.Register_BSTR",
                    New DAVtq("ABC", DateTime.UtcNow, DAQualities.GoodNonspecific))
            })

            For i = 0 To resultArray.Length - 1
                Debug.Assert(resultArray(i) IsNot Nothing)

                If resultArray(i).Succeeded Then
                    Console.WriteLine("Result {0}: success", i)
                Else
                    Debug.Assert(resultArray(i).Exception IsNot Nothing)
                    Console.WriteLine("Result {0} *** Failure: {1}", i, resultArray(i).ErrorMessageBrief)
                End If
            Next i
        End Sub
    End Class
End Namespace
#End Region
