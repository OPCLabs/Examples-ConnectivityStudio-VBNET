' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to filter the events by their category.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel
Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.AlarmsAndEvents._EasyAEClient

    Partial Friend Class SubscribeEvents
        _
        Private Shared ReadOnly AEClient As New EasyAEClient()
        _
        Private Shared ReadOnly DAClient As New EasyDAClient()

        Public Shared Sub FilterByCategories()
            Dim eventHandler = New EasyAENotificationEventHandler(AddressOf AEClient_Notification_FilterByCategories)
            AddHandler AEClient.Notification, eventHandler

            Console.WriteLine("Processing event notifications...")
            Dim subscriptionFilter As New AESubscriptionFilter() With _
                {.Categories = New Long() {15531778}}
            ' You can also filter using event types, severity, areas, and sources.
            Dim handle As Integer = AEClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", Nothing, subscriptionFilter)

            ' Allow time for initial refresh
            Thread.Sleep(5 * 1000)

            ' Set some events to active state.
            DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", True)
            DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState2.Activate", True)

            Thread.Sleep(10 * 1000)

            AEClient.UnsubscribeEvents(handle)
            RemoveHandler AEClient.Notification, eventHandler
        End Sub

        ' Notification event handler
        Private Shared Sub AEClient_Notification_FilterByCategories(ByVal sender As Object, ByVal e As EasyAENotificationEventArgs)
            Console.WriteLine()
            Console.WriteLine(e)
            If Not e.Succeeded Then
                Exit Sub
            End If

            Console.WriteLine("Refresh: {0}", e.Refresh)
            Console.WriteLine("RefreshComplete: {0}", e.RefreshComplete)
            Dim eventData As AEEventData = e.EventData
            If e.EventData IsNot Nothing Then
                Console.WriteLine("Event.CategoryId: {0}", eventData.CategoryId)
                Console.WriteLine("Event.QualifiedSourceName: {0}", eventData.QualifiedSourceName)
                Console.WriteLine("Event.Message: {0}", eventData.Message)
                Console.WriteLine("Event.Active: {0}", eventData.Active)
                Console.WriteLine("Event.Acknowledged: {0}", eventData.Acknowledged)
            End If
        End Sub
    End Class
End Namespace
#End Region
