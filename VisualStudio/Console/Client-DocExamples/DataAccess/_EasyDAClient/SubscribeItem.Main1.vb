' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' Hooking up events and receiving OPC item changes.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeItem
        Shared Sub Main1()
            Using client = New EasyDAClient()
                Dim eventHandler = New EasyDAItemChangedEventHandler(AddressOf client_ItemChanged)
                AddHandler client.ItemChanged, eventHandler

                Console.WriteLine("Subscribing item...")
                client.SubscribeItem("", "OPCLabs.KitServer.2", "Demo.Ramp", 200)
                Thread.Sleep(30 * 1000)
                client.UnsubscribeAllItems()
                RemoveHandler client.ItemChanged, eventHandler
            End Using
        End Sub

        Private Shared Sub client_ItemChanged(sender As Object, e As EasyDAItemChangedEventArgs)
            If e.Succeeded Then
                Console.WriteLine(e.Vtq)
            Else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace
#End Region
