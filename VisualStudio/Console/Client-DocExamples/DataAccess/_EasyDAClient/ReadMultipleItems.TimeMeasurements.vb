' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable RedundantAssignment
#Region "Example"
' This example measures the time needed to read 2000 items all at once, and in 20 groups by 100 items.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadMultipleItems
        Private Const NumberOfGroups As Integer = 100
        Private Const ItemsInGroup As Integer = 20
        Private Const TotalItems As Integer = NumberOfGroups * ItemsInGroup

        ' Main method
        Public Shared Sub TimeMeasurements()
            ' Make the measurements 10 times; note that first time the times might be longer.
            For i As Integer = 1 To 10
                ' Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000)

                ' Read all items at once, and measure the time
                Dim stopwatch1 = New Stopwatch()
                stopwatch1.Start()
                ReadAllAtOnce()
                stopwatch1.Stop()
                Console.WriteLine("ReadAllAtOnce has taken (milliseconds): {0}", stopwatch1.ElapsedMilliseconds)

                ' Pause - we do not want the component to use the values it has in memory
                Thread.Sleep(2 * 1000)

                ' Read items in groups, and measure the time
                Dim stopwatch2 = New Stopwatch()
                stopwatch2.Start()
                ReadInGroups()
                stopwatch2.Stop()
                Console.WriteLine("ReadInGroups has taken (milliseconds): {0}", stopwatch2.ElapsedMilliseconds)
            Next i

            ' Example output (measured with Version 5.20, Release configuration):
            '                
            '                    ReadAllAtOnce has taken (milliseconds): 3432
            '                    ReadInGroups has taken (milliseconds): 1563
            '                    ReadAllAtOnce has taken (milliseconds): 539
            '                    ReadInGroups has taken (milliseconds): 1625
            '                    ReadAllAtOnce has taken (milliseconds): 579
            '                    ReadInGroups has taken (milliseconds): 1594
            '                    ReadAllAtOnce has taken (milliseconds): 638
            '                    ReadInGroups has taken (milliseconds): 1610   
            '                    ...
            '                

            ' Note that Version 5.12 and earlier were yielding much larger penalty to repeated reads.
            ' Example output (measured with Version 5.12, Release configuration):
            '                
            '                    ReadAllAtOnce has taken (milliseconds): 4241
            '                    ReadInGroups has taken (milliseconds): 8094
            '                    ReadAllAtOnce has taken (milliseconds): 269
            '                    ReadInGroups has taken (milliseconds): 7813
            '                    ReadAllAtOnce has taken (milliseconds): 285
            '                    ReadInGroups has taken (milliseconds): 7813
            '                    ReadAllAtOnce has taken (milliseconds): 283
            '                    ReadInGroups has taken (milliseconds): 7844                    
            '                    ...
            '                
        End Sub

        ' Read all items at once
        Private Shared Sub ReadAllAtOnce()
            Dim client = New EasyDAClient()

            ' Create an array of item descriptors for all items
            Dim itemDescriptors = New DAItemDescriptor(TotalItems - 1) {}
            Dim index As Integer = 0
            For iLoop As Integer = 0 To NumberOfGroups - 1
                For iItem As Integer = 0 To ItemsInGroup - 1
                    itemDescriptors(index) = New DAItemDescriptor(String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1))
                    index += 1
                Next iItem
            Next iLoop

            ' Perform the OPC read
            Dim vtqResults() As DAVtqResult = client.ReadMultipleItems("OPCLabs.KitServer.2", itemDescriptors)

            ' Count successful results
            Dim successCount As Integer = 0
            For iItem As Integer = 0 To TotalItems - 1
                Debug.Assert(vtqResults(iItem) IsNot Nothing)
                If vtqResults(iItem).Succeeded Then
                    successCount += 1
                End If
            Next iItem

            If successCount <> TotalItems Then
                Console.WriteLine("Warning: There were some failures, success count is {0}", successCount)
            End If
        End Sub

        ' Read items in groups
        Private Shared Sub ReadInGroups()
            Dim client = New EasyDAClient()

            Dim successCount As Integer = 0
            For iLoop As Integer = 0 To NumberOfGroups - 1
                ' Create an array of item descriptors for items in one group
                Dim itemDescriptors = New DAItemDescriptor(ItemsInGroup - 1) {}
                For iItem As Integer = 0 To ItemsInGroup - 1
                    itemDescriptors(iItem) = New DAItemDescriptor(String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", iLoop + 1, iItem + 1))
                Next iItem

                ' Perform the OPC read
                Dim vtqResults() As DAVtqResult = client.ReadMultipleItems("OPCLabs.KitServer.2", itemDescriptors)

                ' Count successful results (totalling to previous value)
                For iItem As Integer = 0 To ItemsInGroup - 1
                    Debug.Assert(vtqResults(iItem) IsNot Nothing)
                    If vtqResults(iItem).Succeeded Then
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
