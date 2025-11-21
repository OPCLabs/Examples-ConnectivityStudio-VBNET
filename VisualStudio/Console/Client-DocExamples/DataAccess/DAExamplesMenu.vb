' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.DocExamples.DataAccess

    Public Class DAExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                {
                    AddressOf _DAClientMapper.DefineMapping.Main1,
                    AddressOf _DAClientMapper.DefineMapping.MappingKinds,
                    AddressOf _DAClientMapper.DefineMapping.Subscribe,
                                                                      _
                    AddressOf _EasyDAClient.BrowseAccessPaths.Main1,
                    AddressOf _EasyDAClient.BrowseBranches.Main1,
                    AddressOf _EasyDAClient.BrowseLeaves.Main1,
                    AddressOf _EasyDAClient.BrowseNodes.DataTypes,
                    AddressOf _EasyDAClient.BrowseNodes.Main1,
                    AddressOf _EasyDAClient.BrowseNodes.Recursive,
                    AddressOf _EasyDAClient.BrowseNodes.RecursiveWithRead,
                    AddressOf _EasyDAClient.BrowseProperties.Main1,
                    AddressOf _EasyDAClient.BrowseServers.Main1,
                    AddressOf _EasyDAClient.ChangeItemSubscription.PercentDeadband,
                    AddressOf _EasyDAClient.ChangeItemSubscription.Main1,
                    AddressOf _EasyDAClient.ChangeMultipleItemSubscriptions.Main1,
                    AddressOf _EasyDAClient.GetMultiplePropertyValues.DataType,
                    AddressOf _EasyDAClient.GetMultiplePropertyValues.Main1,
                    AddressOf _EasyDAClient.GetPropertyValue.DataType,
                    AddressOf _EasyDAClient.GetPropertyValue.Main1,
                    AddressOf _EasyDAClient.Isolated.Main1,
                    AddressOf _EasyDAClient.PullItemChanged.Main1,
                    AddressOf _EasyDAClient.PullItemChanged.MultipleItems,
                    AddressOf _EasyDAClient.ReadItem.BrowsePath,
                    AddressOf _EasyDAClient.ReadItem.DataTypes,
                    AddressOf _EasyDAClient.ReadItem.DeviceSource,
                    AddressOf _EasyDAClient.ReadItem.GetTypeCode,
                    AddressOf _EasyDAClient.ReadItem.Main1,
                    AddressOf _EasyDAClient.ReadItem.Synchronous,
                    AddressOf _EasyDAClient.ReadItemValue.Array,
                    AddressOf _EasyDAClient.ReadItemValue.CLSID,
                    AddressOf _EasyDAClient.ReadItemValue.DeviceSource,
                    AddressOf _EasyDAClient.ReadItemValue.Main1,
                    AddressOf _EasyDAClient.ReadMultipleItems.DeviceSource,
                    AddressOf _EasyDAClient.ReadMultipleItems.Main1,
                    AddressOf _EasyDAClient.ReadMultipleItems.ManyRepeat,
                    AddressOf _EasyDAClient.ReadMultipleItems.Synchronous,
                    AddressOf _EasyDAClient.ReadMultipleItems.TimeMeasurements,
                    AddressOf _EasyDAClient.ReadMultipleItemValues.Main1,
                    AddressOf _EasyDAClient.SubscribeItem.CallbackLambda,
                    AddressOf _EasyDAClient.SubscribeItem.Main1,
                    AddressOf _EasyDAClient.SubscribeItem.PercentDeadband,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.DataTypes,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.Main1,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.ManyItems,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.PercentDeadband,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.StateAsInteger,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.StateAsObject,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.StoreInDictionary,
                    AddressOf _EasyDAClient.SubscribeMultipleItems.WithRead,
                    AddressOf _EasyDAClient.UnsubscribeAllItems.Main1,
                    AddressOf _EasyDAClient.UnsubscribeItem.Main1,
                    AddressOf _EasyDAClient.UnsubscribeMultipleItems.Main1,
                    AddressOf _EasyDAClient.WriteItem.Main1,
                    AddressOf _EasyDAClient.WriteItemValue.Array,
                    AddressOf _EasyDAClient.WriteItemValue.Main1,
                    AddressOf _EasyDAClient.WriteItemValue.RequestedDataType,
                    AddressOf _EasyDAClient.WriteMultipleItems.Main1,
                    AddressOf _EasyDAClient.WriteMultipleItemValues.Main1,
                    AddressOf _EasyDAClient.WriteMultipleItemValues.RequestedDataType,
                    AddressOf _EasyDAClient.WriteMultipleItemValues.TestSuccess,
                    AddressOf _EasyDAClient.WriteMultipleItemValues.TimeMeasurements,
                                                                                     _
                    AddressOf _EasyDAClientExtension.GetDataTypePropertyValue.Main1,
                    AddressOf _EasyDAClientExtension.GetItemPropertyRecord.Main1,
                    AddressOf _EasyDAClientExtension.GetPropertyValueDictionary.Main1,
                    AddressOf _EasyDAClientExtension.WaitForItemValue.Main1,
                                                                            _
                    AddressOf _EasyDAClientHoldPeriods.TopicWrite.Main1,
                                                                        _
                    AddressOf _EasyDAItemChangedEventArgs.General.Main1}

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
