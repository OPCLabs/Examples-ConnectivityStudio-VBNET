' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to acknowledge an event condition in the OPC server.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents._EasyAEClient

    Friend Class AcknowledgeCondition

        Private Shared ReadOnly AEClient As New EasyAEClient()

        Private Shared ReadOnly DAClient As New EasyDAClient()

        Private Shared _done As Boolean ' volatile

        Public Shared Sub Main1()
            Dim eventHandler = New EasyAENotificationEventHandler(AddressOf AEClient_Notification)
            AddHandler AEClient.Notification, eventHandler

            Console.WriteLine("Processing event notifications for 1 minute...")
            Dim subscriptionFilter As New AESubscriptionFilter
            subscriptionFilter.Sources = New AENodeDescriptor() {"Simulation.ConditionState1"}
            Dim handle As Integer = AEClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, Nothing, subscriptionFilter)

            ' Give the refresh operation time to complete
            Thread.Sleep(5 * 1000)

            ' Trigger an acknowledgeable event
            Try
                DAClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", True)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            _done = False
            Dim endTime As Date = Date.Now + New TimeSpan(0, 0, 5)
            Do While ((Not _done)) AndAlso (Date.Now < endTime)
                Thread.Sleep(1000)
            Loop

            ' Give some time to also receive the acknowledgement notification
            Thread.Sleep(5 * 1000)

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
                Console.WriteLine("EventData.QualifiedSourceName: {0}", eventData.QualifiedSourceName)
                Console.WriteLine("EventData.Message: {0}", eventData.Message)
                Console.WriteLine("EventData.Active: {0}", eventData.Active)
                Console.WriteLine("EventData.Acknowledged: {0}", eventData.Acknowledged)
                Console.WriteLine("EventData.AcknowledgeRequired: {0}", eventData.AcknowledgeRequired)

                If eventData.AcknowledgeRequired Then
                    Console.WriteLine(">>>>> ACKNOWLEDGING THIS EVENT")
                    Try
                        AEClient.AcknowledgeCondition("", "OPCLabs.KitEventServer.2", "Simulation.ConditionState1", "Simulated", eventData.ActiveTime, eventData.Cookie)
                    Catch opcException As OpcException
                        Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                        Exit Sub
                    End Try
                    Console.WriteLine(">>>>> EVENT ACKNOWLEDGED")
                    _done = True
                End If
            End If
        End Sub
    End Class
End Namespace
#End Region
