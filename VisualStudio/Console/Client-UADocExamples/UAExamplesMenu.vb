' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UADocExamples
    Public Class UAExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                    {
                        AddressOf _EasyUAClient.Browse.All,
                        AddressOf _EasyUAClient.Browse.Main1,
                        AddressOf _EasyUAClient.BrowseDataNodes.Overload1,
                        AddressOf _EasyUAClient.BrowseDataNodes.Recursive,
                        AddressOf _EasyUAClient.BrowseDataVariables.Overload2,
                        AddressOf _EasyUAClient.BrowseMethods.Overload2,
                        AddressOf _EasyUAClient.BrowseObjects.Overload2,
                        AddressOf _EasyUAClient.BrowseProperties.Overload2,
                        AddressOf _EasyUAClient.CallMethod.Main1,
                        AddressOf _EasyUAClient.CallMultipleMethods.Main1,
                        AddressOf _EasyUAClient.ChangeMonitoredItemSubscription.Overload1,
                        AddressOf _EasyUAClient.ChangeMultipleMonitoredItemSubscriptions.Overload2,
                        AddressOf _EasyUAClient.DiscoverGlobalServers.Hierarchical,
                        AddressOf _EasyUAClient.DiscoverGlobalServers.Main1,
                        AddressOf _EasyUAClient.DiscoverLocalServers.Overload1,
                        AddressOf _EasyUAClient.DiscoverNetworkServers.Hierarchical,
                        AddressOf _EasyUAClient.DiscoverNetworkServers.Main1,
                        AddressOf _EasyUAClient.FindLocalApplications.Main1,
                        AddressOf _EasyUAClient.GetMonitoredItemArguments.Main1,
                        AddressOf _EasyUAClient.GetMonitoredItemArgumentsDictionary.Main1,
                        AddressOf _EasyUAClient.Isolated.Main1,
                        AddressOf _EasyUAClient.LogEntry.Main1,
                        AddressOf _EasyUAClient.PullDataChangeNotification.Main1,
                        AddressOf _EasyUAClient.Read.FromDevice,
                        AddressOf _EasyUAClient.Read.Main1,
                        AddressOf _EasyUAClient.ReadMultiple.BrowsePath,
                        AddressOf _EasyUAClient.ReadMultiple.FromDevice,
                        AddressOf _EasyUAClient.ReadMultiple.Main1,
                        AddressOf _EasyUAClient.ReadMultipleValues.DataType,
                        AddressOf _EasyUAClient.ReadMultipleValues.Main1,
                        AddressOf _EasyUAClient.ReadValue.ArrayOfUInt16,
                        AddressOf _EasyUAClient.ReadValue.MultipleServers,
                        AddressOf _EasyUAClient.ReadValue.NamespaceArray,
                        AddressOf _EasyUAClient.ReadValue.Overload1,
                        AddressOf _EasyUAClient.ReadValue.Overload2,
                        AddressOf _EasyUAClient.ReadValue.Repeated,
                        AddressOf _EasyUAClient.RetrieveAllDataTypes.Main1,
                        AddressOf _EasyUAClient.SubscribeDataChange.AbsoluteDeadband,
                        AddressOf _EasyUAClient.SubscribeDataChange.CallbackLambda,
                        AddressOf _EasyUAClient.SubscribeDataChange.Filter,
                        AddressOf _EasyUAClient.SubscribeDataChange.Overload1,
                        AddressOf _EasyUAClient.SubscribeDataChange.PercentDeadband,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.AbsoluteDeadband,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.AllInObject,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.Filter,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.Main1,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.PercentDeadband,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.StateAsInteger,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.StateAsObject,
                        AddressOf _EasyUAClient.SubscribeMultipleMonitoredItems.StoreInDictionary,
                        AddressOf _EasyUAClient.UnsubscribeAllMonitoredItems.Main1,
                        AddressOf _EasyUAClient.UnsubscribeMonitoredItem.Main1,
                        AddressOf _EasyUAClient.UnsubscribeMultipleMonitoredItems.Main1,
                        AddressOf _EasyUAClient.UnsubscribeMultipleMonitoredItems.Some,
                        AddressOf _EasyUAClient.Write.Main1,
                        AddressOf _EasyUAClient.WriteMultiple.TestSuccess,
                        AddressOf _EasyUAClient.WriteMultipleValues.Main1,
                        AddressOf _EasyUAClient.WriteMultipleValues.TestSuccess,
                        AddressOf _EasyUAClient.WriteMultipleValues.ValueType,
                        AddressOf _EasyUAClient.WriteMultipleValues.ValueTypeCode,
                        AddressOf _EasyUAClient.WriteMultipleValues.ValueTypeFullName,
                        AddressOf _EasyUAClient.WriteValue.ArrayOfInt32,
                        AddressOf _EasyUAClient.WriteValue.ByteString,
                        AddressOf _EasyUAClient.WriteValue.Incrementing,
                        AddressOf _EasyUAClient.WriteValue.Main1,
                        AddressOf _EasyUAClient.WriteValue.Type,
                        AddressOf _EasyUAClient.WriteValue.TypeCode,
                                                                    _
                        AddressOf _EasyUAClientConnectionControl.DisposableLockConnection.Main1,
                        AddressOf _EasyUAClientConnectionControl.LockAndUnlockConnection.Main1,
                                                                                               _
                        AddressOf _EasyUAClientConnectionMonitoring.ServerConditionChanged.Main1,
                                                                                                 _
                        AddressOf _EasyUAClientExtension.WaitForValue.Main1,
                                                                            _
                        AddressOf _EasyUAClientNodeRegistration.RegisterAndUnregisterMultipleNodes.Main1,
                                                                                                         _
                        AddressOf _UAClientMapper.DefineMapping.Main1,
                                                                      _
                        AddressOf _UAClientSessionParameters.Timeouts.Isolated
                    }

            Dim actionList = New List(Of Action)(actionArray)

            Dim originalSharedParameters = CType(EasyUAClient.SharedParameters.Clone(), EasyUAClientSharedParameters)
            Do
                Console.WriteLine()
                If Not ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList) Then
                    Exit Do
                End If

                Console.WriteLine("Press Enter to continue...")
                Console.ReadLine()

                If EasyUAClient.SharedParameters <> originalSharedParameters Then
                    Using ConsoleUtilities.WithForegroundColor(ConsoleColor.Yellow)
                        Console.WriteLine(
                            "This example has changed some global parameters that can influence how other examples work. " +
                            "For this reason, the application will now exit. Start it again to continue.")
                        Console.WriteLine("Press Enter to continue...")
                        Console.ReadLine()
                        Environment.Exit(0)
                    End Using
                End If
            Loop While True
        End Sub
    End Class
End Namespace
' ReSharper restore CheckNamespace
