' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to subscribe to events with specified event attributes, and obtain the attribute values in event 
' notifications.
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

Namespace Global.DocExamples.AlarmsAndEvents._EasyAENotificationEventArgs
    Friend Class AttributeValues
        Public Shared Sub Main1()
            Dim aeClient = New EasyAEClient()
            Dim daClient = New EasyDAClient()

            Dim eventHandler = New EasyAENotificationEventHandler(AddressOf aeClient_Notification)
            AddHandler aeClient.Notification, eventHandler

            ' Inactivate the event condition (we will later activate it and receive the notification)
            daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Inactivate", True)

            Dim subscriptionFilter As New AESubscriptionFilter
            subscriptionFilter.Sources = New AENodeDescriptor() {"Simulation.ConditionState1"}

            ' Prepare a dictionary holding requested event attributes for each event category
            ' The event category IDs and event attribute IDs are hard-coded here, but can be obtained from the OPC 
            ' server by querying as well.
            Dim returnedAttributesByCategory = New AEAttributeSetDictionary()
            returnedAttributesByCategory(&HECFF02) = New Long() {&HEB0003, &HEB0008}

            Console.WriteLine("Subscribing to events...")
            Dim handle As Integer = aeClient.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000, Nothing, subscriptionFilter, returnedAttributesByCategory)

            ' Give the refresh operation time to complete
            Thread.Sleep(5 * 1000)

            ' Trigger an event carrying specified attributes (activate the condition)
            Try
                daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.AttributeValues.15400963", 123456)
                daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.AttributeValues.15400968", "Some string value")
                daClient.WriteItemValue("", "OPCLabs.KitServer.2", "SimulateEvents.ConditionState1.Activate", True)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            Console.WriteLine("Processing event notifications for 10 seconds...")
            Thread.Sleep(10 * 1000)

            aeClient.UnsubscribeEvents(handle)
        End Sub

        ' Notification event handler
        Private Shared Sub aeClient_Notification(ByVal sender As Object, ByVal e As EasyAENotificationEventArgs)
            If Not e.Succeeded Then
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
                Exit Sub
            End If

            If (Not e.Refresh) AndAlso (e.EventData IsNot Nothing) Then
                ' Display all received event attribute IDs and their corresponding values
                Console.WriteLine("Event attribute count: {0}", e.EventData.AttributeValues.Count)
                For Each pair As KeyValuePair(Of Long, Object) In e.EventData.AttributeValues
                    Console.WriteLine("    {0}: {1}", pair.Key, pair.Value)
                Next pair
            End If
        End Sub
    End Class
End Namespace
#End Region
