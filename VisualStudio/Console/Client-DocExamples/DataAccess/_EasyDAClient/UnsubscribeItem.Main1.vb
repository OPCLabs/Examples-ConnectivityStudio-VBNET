' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how subscribe to changes of multiple items, and unsubscribe from one of them.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class UnsubscribeItem
        Shared Sub Main1()
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_ItemChanged

                Dim handleArray = client.SubscribeMultipleItems(
                        New DAItemGroupArguments() _
                        { _
                            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, Nothing),
                            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, Nothing),
                            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, Nothing),
                            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, Nothing)
                        })

                Console.WriteLine("Processing item changed events for 30 seconds...")
                Thread.Sleep(30 * 1000)

                Console.WriteLine("Unsubscribing from the first item...")
                client.UnsubscribeItem(handleArray(0))

                Console.WriteLine()

                Console.WriteLine("Processing item changed events for 30 seconds...")
                Thread.Sleep(30 * 1000)
            End Using
        End Sub

        ' Item changed event handler
        Private Shared Sub client_ItemChanged(sender As Object, e As EasyDAItemChangedEventArgs)
            If e.Succeeded Then
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq)
            Else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace
#End Region
