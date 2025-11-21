' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to for a refresh for all active conditions and inactive, unacknowledged conditions.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents._EasyAEClient

    Friend Class RefreshEventSubscription
        _
        Private Shared ReadOnly AEClient As New EasyAEClient()
        _
        Private Shared ReadOnly DAClient As New EasyDAClient()

        Public Shared Sub Main1()
            Dim eventHandler = New EasyAENotificationEventHandler(AddressOf AEClient_Notification)
            AddHandler AEClient.Notification, eventHandler

            Console.WriteLine("Processing event notifications...")
            Dim subscriptionFilter As New AESubscriptionFilter
            subscriptionFilter.Sources = New AENodeDescriptor() {"Simulation.ConditionState1", "Simulation.ConditionState2", "Simulation.ConditionState3"}
            Dim handle As Integer = AEClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, Nothing, subscriptionFilter)

            ' The component will perform auto-refresh at this point, give it time to happen
            Console.WriteLine("Waiting for 10 seconds...")
            Thread.Sleep(10 * 1000)

            ' Set some events to active state, which will cause them to appear in refresh
            Console.WriteLine("Activating conditions and waiting for 10 seconds...")
            Try
                DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", True)
                DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState2.Activate", True)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try
            Thread.Sleep(10 * 1000)

            Console.WriteLine("Refreshing subscription and waiting for 10 seconds...")
            AEClient.RefreshEventSubscription(handle)
            Thread.Sleep(10 * 1000)

            AEClient.UnsubscribeEvents(handle)
            RemoveHandler AEClient.Notification, eventHandler
        End Sub

        ' Notification event handler
        Private Shared Sub AEClient_Notification(ByVal sender As Object, ByVal e As EasyAENotificationEventArgs)
            Console.WriteLine()
            If Not e.Succeeded Then
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
                Exit Sub
            End If

            Console.WriteLine("Refresh: {0}", e.Refresh)
            Console.WriteLine("RefreshComplete: {0}", e.RefreshComplete)
            If e.EventData IsNot Nothing Then
                Dim eventData As AEEventData = e.EventData
                Console.WriteLine("Event.QualifiedSourceName: {0}", eventData.QualifiedSourceName)
                Console.WriteLine("Event.Message: {0}", eventData.Message)
                Console.WriteLine("Event.Active: {0}", eventData.Active)
                Console.WriteLine("Event.Acknowledged: {0}", eventData.Acknowledged)
            End If
        End Sub
    End Class
End Namespace
#End Region
