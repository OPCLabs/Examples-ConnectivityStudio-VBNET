' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.SparkplugDocExamples.Consumer
    Public Class ConsumerExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _EasySparkplugConsumer.DeliverCompleteDataSet.Main1,
                AddressOf _EasySparkplugConsumer.ImplicitNodeDescriptor.Main1,
                AddressOf _EasySparkplugConsumer.PublishDeviceMetric.Bytes,
                AddressOf _EasySparkplugConsumer.PublishDeviceMetric.DataType,
                AddressOf _EasySparkplugConsumer.PublishDeviceMetric.Int32Array,
                AddressOf _EasySparkplugConsumer.PublishDeviceMetric.Overload1,
                AddressOf _EasySparkplugConsumer.PublishEdgeNodeMetric.Incrementing,
                AddressOf _EasySparkplugConsumer.PublishEdgeNodeMetric.Overload1,
                AddressOf _EasySparkplugConsumer.PublishEdgeNodeMetric.Timestamp,
                AddressOf _EasySparkplugConsumer.PublishEdgeNodePayload.Overload1,
                AddressOf _EasySparkplugConsumer.SubscribeDeviceMetric.Overload1,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Authentication,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.CallbackLambda,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.ClientId,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Mqtt5,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Overload1,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.Tls,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodeMetric.WebSocket,
                AddressOf _EasySparkplugConsumer.SubscribeEdgeNodePayload.Overload1,
                AddressOf _EasySparkplugConsumer.SubscribeMetric.StateAsInteger,
                AddressOf _EasySparkplugConsumer.UnsubscribeMetric.Main1, _
                                                                          _
                AddressOf _EasySparkplugHostApplication.Start_Stop.Main1,
                AddressOf _EasySparkplugHostApplication.SystemConnectionParameters.Main1, _
                                                                                          _
                AddressOf _SparkplugHostDescriptor.HostId.Main1
            }

            Dim actionList = New List(Of Action)(actionArray)

            Do
                Console.WriteLine()
                If Not ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList) Then
                    Exit Do
                End If

                Console.WriteLine("Press Enter to continue...")
                Console.ReadLine()
            Loop While True

        End Sub
    End Class
End Namespace
