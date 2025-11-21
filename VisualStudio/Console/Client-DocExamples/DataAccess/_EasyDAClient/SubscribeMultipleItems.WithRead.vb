' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to read items while subscribed.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeMultipleItems
        Public Shared Sub WithRead()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_Read_ItemChanged

                Console.WriteLine("Subscribing item changes...")
                Dim handleArray() As Integer = client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, Nothing)
                })

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Reading items...")
                Dim vtqResults() As DAVtqResult = client.ReadMultipleItems("OPCLabs.KitServer.2",
                    New DAItemDescriptor() {"Trends.Ramp (1 min)", "Trends.Sine (1 min)"})

                For i = 0 To vtqResults.Length - 1
                    Debug.Assert(vtqResults(i) IsNot Nothing)

                    If vtqResults(i).Succeeded Then
                        Console.WriteLine("vtqResult[{0}].Vtq: {1}", i, vtqResults(i).Vtq)
                    Else
                        Console.WriteLine("vtqResult[{0}] *** Failure: {1}", i, vtqResults(i).ErrorMessageBrief)
                    End If
                Next i

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing item changes...")
            End Using

            Console.WriteLine("Finished.")
        End Sub

        ' Item changed event handler
        Private Shared Sub client_Read_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
            If e.Succeeded Then
                Console.WriteLine($"< {e.Arguments.ItemDescriptor.ItemId}: {e.Vtq}")
            Else
                Console.WriteLine($"< {e.Arguments.ItemDescriptor.ItemId} *** Failure: {e.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace
#End Region
