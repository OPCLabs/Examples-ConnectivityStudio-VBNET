' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read values of 4 items at once, and display them.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadMultipleItemValues
        Public Shared Sub Main1()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Dim valueResults() As ValueResult = client.ReadMultipleItemValues("OPCLabs.KitServer.2",
                New DAItemDescriptor() {"Simulation.Random", "Trends.Ramp (1 min)", "Trends.Sine (1 min)", "Simulation.Register_I4"})

            For i = 0 To valueResults.Length - 1
                Dim valueResult As ValueResult = valueResults(i)
                Debug.Assert(valueResult IsNot Nothing)

                If valueResult.Succeeded Then
                    Console.WriteLine($"valueResults[{i}].Value: {valueResult.Value}")
                Else
                    Console.WriteLine($"valueResults[{i}] *** Failure: {valueResult.ErrorMessageBrief}")
                End If
            Next i
        End Sub

        ' Example output:
        '
        'valueResults[0].Value: 0.00125125888851588
        'valueResults[1].Value: 0.732510924339294
        'valueResults[2].Value: -0.993968485238202
        'valueResults[3].Value: 0
    End Class
End Namespace
#End Region
