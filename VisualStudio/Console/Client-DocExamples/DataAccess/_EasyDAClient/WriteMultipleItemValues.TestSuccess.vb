' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to write values into 3 items at once, test for success of each write and display the exception 
' message in case of failure.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteMultipleItemValues
        Public Shared Sub TestSuccess()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Console.WriteLine("Writing multiple item values...")
            Dim resultArray As OperationResult() = client.WriteMultipleItemValues(New DAItemValueArguments() {
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 23456),
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R8",
                        "This string cannot be converted to VT_R8"),
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "UnknownItem", "ABC")
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

        ' Example output
        '
        'Writing multiple item values...
        'Result 0: success
        'Result 1 *** Failure: Type mismatch.  [...]
        'Result 2 *** Failure: The item Is no longer available In the server address space. [...]
    End Class
End Namespace
#End Region
