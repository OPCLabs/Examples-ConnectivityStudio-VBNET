' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Engine

Namespace Global.UASubscriberDocExamples.PubSub
    Public Class PubSubExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _EasyUASubscriber.PullDataSetMessage.Main1,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Callback,
                AddressOf _EasyUASubscriber.SubscribeDataSet.CaptureFile,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Ethernet,
                AddressOf _EasyUASubscriber.SubscribeDataSet.ExtractField,
                AddressOf _EasyUASubscriber.SubscribeDataSet.FieldNames,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Filter,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Main1,
                AddressOf _EasyUASubscriber.SubscribeDataSet.MappingParameters,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Metadata,
                AddressOf _EasyUASubscriber.SubscribeDataSet.MqttFromFileStorage,
                AddressOf _EasyUASubscriber.SubscribeDataSet.MqttJsonTcp,
                AddressOf _EasyUASubscriber.SubscribeDataSet.MqttTcpSaveCopy,
                AddressOf _EasyUASubscriber.SubscribeDataSet.MqttUadpTcp,
                AddressOf _EasyUASubscriber.SubscribeDataSet.PublishedDataSet,
                AddressOf _EasyUASubscriber.SubscribeDataSet.PublisherId,
                AddressOf _EasyUASubscriber.SubscribeDataSet.ResolveFromFile,
                AddressOf _EasyUASubscriber.SubscribeDataSet.Secure,
                AddressOf _EasyUASubscriber.SubscribeDataSetField.Main1,
                AddressOf _EasyUASubscriber.UnsubscribeDataSet.Main1,
                AddressOf _IUAReadOnlyPubSubConfiguration.GetMethods.PublishedDataSets,
                AddressOf _IUAReadOnlyPubSubConfiguration.GetMethods.PubSubComponents
            }

            Dim actionList = New List(Of Action)(actionArray)

            Dim originalSharedParameters = CType(EasyUASubscriber.SharedParameters.Clone(), EasyUASubscriberSharedParameters)
            Do
                Console.WriteLine()
                If Not ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList) Then
                    Exit Do
                End If

                Console.WriteLine("Press Enter to continue...")
                Console.ReadLine()

                If EasyUASubscriber.SharedParameters <> originalSharedParameters Then
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
