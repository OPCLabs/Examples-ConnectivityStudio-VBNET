' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to unsubscribe from changes of all items.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class UnsubscribeAllItems
        Public Shared Sub Main1()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_ItemChanged

                Console.WriteLine("Subscribing...")
                client.SubscribeMultipleItems(New DAItemGroupArguments() {
                 New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, Nothing),
                 New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, Nothing),
                 New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, Nothing),
                 New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, Nothing)
                })

                Console.WriteLine("Processing item changed events for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing from all items...")
                client.UnsubscribeAllItems()

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)
            End Using
        End Sub

        ' Item changed event handler
        Private Shared Sub client_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
            ' Display the data
            If e.Succeeded Then
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq)
            Else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace
#End Region
