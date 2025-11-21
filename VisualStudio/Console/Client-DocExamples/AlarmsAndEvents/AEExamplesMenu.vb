' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.DocExamples.AlarmsAndEvents

    Public Class AEExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
              {
                    AddressOf _AEAttributeElement.Properties.Main1,
                                                                   _
                    AddressOf _AECategoryElement.Properties.Main1,
                                                                  _
                    AddressOf _AEConditionElement.Properties.Main1,
                                                                   _
                    AddressOf _AESubscriptionFilter.Properties.Main1,
                                                                     _
                    AddressOf _EasyAEClient.AcknowledgeCondition.Main1,
                    AddressOf _EasyAEClient.BrowseAreas.Main1,
                    AddressOf _EasyAEClient.BrowseServers.Main1,
                    AddressOf _EasyAEClient.BrowseSources.Main1,
                    AddressOf _EasyAEClient.ChangeEventSubscription.Main1,
                    AddressOf _EasyAEClient.GetConditionState.Main1,
                    AddressOf _EasyAEClient.PullNotification.Main1,
                    AddressOf _EasyAEClient.QueryEventCategories.Main1,
                    AddressOf _EasyAEClient.QuerySourceConditions.Main1,
                    AddressOf _EasyAEClient.RefreshEventSubscription.Main1,
                    AddressOf _EasyAEClient.SubscribeEvents.CallbackLambda,
                    AddressOf _EasyAEClient.SubscribeEvents.FilterByCategories,
                    AddressOf _EasyAEClient.SubscribeEvents.Main1,
                    AddressOf _EasyAEClient.UnsubscribeAllEvents.Main1,
                    AddressOf _EasyAEClient.UnsubscribeEvents.Main1,
                                                                    _
                    AddressOf _EasyAENotificationEventArgs.AttributeValues.Main1,
                                                                                 _
                    AddressOf SWToolbox.TOPServer_AE.Main1}

            Dim actionList = New List(Of Action)(actionArray)
            Dim cont As Boolean
            Do
                Console.WriteLine()
                cont = ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList)
                If cont Then
                    Console.WriteLine("Press Enter to continue...")
                    Console.ReadLine()
                End If
            Loop While cont
        End Sub
    End Class

End Namespace
