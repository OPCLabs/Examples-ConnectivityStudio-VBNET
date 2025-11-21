' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to write into multiple OPC items using a single method call, specifying their requested data types.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteMultipleItemValues
        Public Shared Sub RequestedDataType()
            Dim client = New EasyDAClient()

            Console.WriteLine("Writing multiple item values...")
            Dim resultArray = client.WriteMultipleItemValues(New DAItemValueArguments() { _
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I2", 12345) With _
                {.ItemDescriptor = New DAItemDescriptor() With {.RequestedDataType = VarTypes.I2}}, _
                New DAItemValueArguments("", "OPCLabs.KitServer.2", "Simulation.Register_R4", 234.56) With _
                {.ItemDescriptor = New DAItemDescriptor() With {.RequestedDataType = VarTypes.R4}} _
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

            Console.WriteLine("Reading multiple item values...")
            Dim valueResultArray = client.ReadMultipleItemValues("OPCLabs.KitServer.2",
                    New DAItemDescriptor() {"Simulation.Register_I2", "Simulation.Register_R4"})

            For i = 0 To valueResultArray.Length - 1
                Debug.Assert(valueResultArray(i) IsNot Nothing)
                Console.WriteLine("valueResultArray[{0}]: {1}", i, valueResultArray(i))
            Next i
        End Sub
    End Class
End Namespace
#End Region
