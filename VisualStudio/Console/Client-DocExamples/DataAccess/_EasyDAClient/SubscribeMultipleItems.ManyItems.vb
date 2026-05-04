' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how subscribe to large number of items.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeMultipleItems
        Public Shared Sub ManyItems()
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_ItemChanged_ManyItems

                Const numberOfItems As Integer = 1000

                Console.WriteLine("Preparing arguments...")
                Dim argumentArray = New DAItemGroupArguments(numberOfItems - 1) {}
                For i As Integer = 0 To numberOfItems - 1
                    Dim copy As Integer = (i \ 100) + 1
                    Dim phase As Integer = (i Mod 100) + 1
                    Dim itemId As String = String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", copy, phase)
                    argumentArray(i) = New DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 50, Nothing)
                Next i

                Console.WriteLine("Subscribing to {0} items...", numberOfItems)
                client.SubscribeMultipleItems(argumentArray)

                Console.WriteLine("Processing item changed events for 1 minute...")
                Thread.Sleep(60 * 1000)
            End Using
        End Sub

        ' Item changed event handler
        Private Shared Sub client_ItemChanged_ManyItems(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
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
