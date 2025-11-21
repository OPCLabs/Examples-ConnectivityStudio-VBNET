' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example repeatedly reads a large number of items.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadMultipleItems

        Private Const RepeatCount As Integer = 10
        Private Const NumberOfItems As Integer = 1000

        Public Shared Sub ManyRepeat()
            Console.WriteLine("Creating array of arguments...")
            Dim arguments = New DAReadItemArguments(NumberOfItems - 1) {}
            For i As Integer = 0 To NumberOfItems - 1
                Dim copy As Integer = (i \ 100) + 1
                Dim phase As Integer = (i Mod 100)
                Dim itemId As String = String.Format($"Simulation.Incrementing.Copy_{copy}.Phase_{phase}")
                Console.WriteLine(itemId)

                Dim readItemArguments As DAReadItemArguments = New DAReadItemArguments("OPCLabs.KitServer.2", itemId)
                arguments(i) = readItemArguments
            Next i

            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            For iRepeat = 1 To RepeatCount
                Console.WriteLine("Reading items...")
                Dim vtqResults() As DAVtqResult = client.ReadMultipleItems(arguments)

                Dim successCount As Integer = 0
                For Each vtqResult In vtqResults
                    If vtqResult.Succeeded Then
                        successCount += 1
                    End If
                Next vtqResult

                Console.WriteLine($"Success count: {successCount}")
            Next iRepeat
        End Sub
    End Class
End Namespace
#End Region
