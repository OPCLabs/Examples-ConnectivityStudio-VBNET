' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable RedundantAssignment
#Region "Example"
' This example measures the time needed to write 2000 item values all at once, and in 20 groups by 100 items.
' Note that the writes will currently all fail, as we do not have the appropriate writeable items available.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteMultipleItemValues
        Private Const NumberOfGroups As Integer = 100
        Private Const ItemsInGroup As Integer = 20
        Private Const TotalItems As Integer = NumberOfGroups * ItemsInGroup

        ' Main method
        Public Shared Sub TimeMeasurements()
            ' Make the measurements 10 times; note that first time the times might be longer.
            For i As Integer = 1 To 10
                ' Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000)

                '  Write all item values at once, and measure the time
                Dim stopwatch1 = New Stopwatch()
                stopwatch1.Start()
                WriteAllAtOnce()
                stopwatch1.Stop()
                Console.WriteLine("WriteAllAtOnce has taken (milliseconds): {0}", stopwatch1.ElapsedMilliseconds)

                ' Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000)

                ' Write item values in groups, and measure the time
                Dim stopwatch2 = New Stopwatch()
                stopwatch2.Start()
                WriteInGroups()
                stopwatch2.Stop()
                Console.WriteLine("WriteInGroups has taken (milliseconds): {0}", stopwatch2.ElapsedMilliseconds)
            Next i
        End Sub

        '  Write all item values at once
        Private Shared Sub WriteAllAtOnce()
            Dim client = New EasyDAClient()

            ' Create an array of arguments for all items
            Dim arguments = New DAItemValueArguments(TotalItems - 1) {}
            Dim index As Integer = 0
            For iLoop As Integer = 0 To NumberOfGroups - 1
                For iItem As Integer = 0 To ItemsInGroup - 1
                    arguments(index) = New DAItemValueArguments(
                        "OPCLabs.KitServer.2",
                            String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1),
                            0)
                    index += 1
                Next iItem
            Next iLoop

            ' Perform the OPC write
            Dim operationResults() As OperationResult = client.WriteMultipleItemValues(arguments)

            ' Count successful results
            Dim successCount As Integer = 0
            For iItem As Integer = 0 To TotalItems - 1
                Debug.Assert(operationResults(iItem) IsNot Nothing)
                If operationResults(iItem).Succeeded Then
                    successCount += 1
                End If
            Next iItem

            If successCount <> TotalItems Then
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount)
            End If
        End Sub

        ' Write item values in groups
        Private Shared Sub WriteInGroups()
            Dim client = New EasyDAClient()

            Dim successCount As Integer = 0
            For iLoop As Integer = 0 To NumberOfGroups - 1
                ' Create an array of item arguments for items in one group
                Dim arguments = New DAItemValueArguments(ItemsInGroup - 1) {}
                For iItem As Integer = 0 To ItemsInGroup - 1
                    arguments(iItem) = New DAItemValueArguments(
                            "OPCLabs.KitServer.2",
                            String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1),
                            0)
                Next iItem

                ' Perform the OPC write
                Dim operationResults() As OperationResult = client.WriteMultipleItemValues(arguments)

                ' Count successful results (totalling to previous value)
                For iItem As Integer = 0 To ItemsInGroup - 1
                    Debug.Assert(operationResults(iItem) IsNot Nothing)
                    If operationResults(iItem).Succeeded Then
                        successCount += 1
                    End If
                Next iItem
            Next iLoop

            If successCount <> TotalItems Then
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount)
            End If
        End Sub
    End Class
End Namespace
#End Region
