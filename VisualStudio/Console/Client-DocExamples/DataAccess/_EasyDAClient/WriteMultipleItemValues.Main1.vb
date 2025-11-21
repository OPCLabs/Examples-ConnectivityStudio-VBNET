' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to write values into multiple items.
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
        Public Shared Sub Main1()
            Dim client = New EasyDAClient()

            Dim argumentsArray = New DAItemValueArguments() { _
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 12345), _
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BOOL", True), _
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R4", 234.56) _
            }

            Dim resultArray As OperationResult() = client.WriteMultipleItemValues(argumentsArray)

            For i = 0 To resultArray.Length - 1
                Debug.Assert(resultArray(i) IsNot Nothing)

                If resultArray(i).Succeeded Then
                    Console.WriteLine("Results[{0}]: success", i)
                Else
                    Console.WriteLine("Results[{0}] *** Failure: {1}", i, resultArray(i).ErrorMessageBrief)
                End If
            Next i

            Console.WriteLine("Reading multiple item values...")
            Dim valueResultArray() As ValueResult = client.ReadMultipleItemValues("OPCLabs.KitServer.2", _
                New DAItemDescriptor() { _
                    "Simulation.Register_I4",
                    "Simulation.Register_BOOL",
                    "Simulation.Register_R4"})

            For i = 0 To valueResultArray.Length - 1
                Debug.Assert(valueResultArray(i) IsNot Nothing)
                Console.WriteLine("valueResultArray[{0}]: {1}", i, valueResultArray(i))
            Next i

        End Sub
    End Class
End Namespace
#End Region
