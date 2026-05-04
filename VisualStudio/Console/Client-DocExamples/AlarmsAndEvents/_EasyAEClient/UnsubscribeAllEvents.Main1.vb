' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to unsubscribe from all event notifications.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents._EasyAEClient

    Friend Class UnsubscribeAllEvents
        Public Shared Sub Main1()
            Using client = New EasyAEClient()
                Dim eventHandler = New EasyAENotificationEventHandler(AddressOf client_Notification)
                AddHandler client.Notification, eventHandler

                Console.WriteLine("Subscribing...")
                client.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000)

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing...")
                client.UnsubscribeAllEvents()

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)
            End Using
        End Sub

        ' Notification event handler
        Private Shared Sub client_Notification(ByVal sender As Object, ByVal e As EasyAENotificationEventArgs)
            If Not e.Succeeded Then
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
                Exit Sub
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine(e.EventData.Message)
            End If
        End Sub
    End Class
End Namespace
#End Region
