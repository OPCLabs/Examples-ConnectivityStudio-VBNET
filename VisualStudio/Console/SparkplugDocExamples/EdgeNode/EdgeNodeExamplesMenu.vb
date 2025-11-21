' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.SparkplugDocExamples.EdgeNode
    Public Class EdgeNodeExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _EasySparkplugEdgeNode.AutoConnectSystem.Main1,
                AddressOf _EasySparkplugEdgeNode.Construction.Main1,
                AddressOf _EasySparkplugEdgeNode.DataSourceConnectionMode.Main1,
                AddressOf _EasySparkplugEdgeNode.Dispose.Main1,
                AddressOf _EasySparkplugEdgeNode.DisposableLockPublishing.Main1,
                AddressOf _EasySparkplugEdgeNode.OnRead.Main1,
                AddressOf _EasySparkplugEdgeNode.OnWrite.Main1,
                AddressOf _EasySparkplugEdgeNode.PrimaryHostId.Main1,
                AddressOf _EasySparkplugEdgeNode.PublishingError.Main1,
                AddressOf _EasySparkplugEdgeNode.PublishingInterval.Main1,
                AddressOf _EasySparkplugEdgeNode.Read.Main1,
                AddressOf _EasySparkplugEdgeNode.ReportByException.Main1,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.Authentication,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.ClientId,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.Main1,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.Mqtt5,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.Tls,
                AddressOf _EasySparkplugEdgeNode.Start_Stop.WebSocket,
                AddressOf _EasySparkplugEdgeNode.SystemConnectionParameters.Main1,
                AddressOf _EasySparkplugEdgeNode.Write.Main1,
                                                             _
                AddressOf _SparkplugDevice.ConnectDataSource.Main1,
                                                                   _
                AddressOf _SparkplugMetric.ConstantValue.Main1,
                AddressOf _SparkplugMetric.CreateIn.Main1,
                AddressOf _SparkplugMetric.ReadData.Main1,
                AddressOf _SparkplugMetric.ReadFunction.Main1,
                AddressOf _SparkplugMetric.ReadValueFunction.Array,
                AddressOf _SparkplugMetric.ReadValueFunction.Bytes,
                AddressOf _SparkplugMetric.ReadValueFunction.Main1,
                AddressOf _SparkplugMetric.ReadValueFunction.UInt16,
                AddressOf _SparkplugMetric.ReadWrite.Main1,
                AddressOf _SparkplugMetric.ReadWriteValue.Array,
                AddressOf _SparkplugMetric.ReadWriteValue.Bytes,
                AddressOf _SparkplugMetric.ReadWriteValue.FullyWritable,
                AddressOf _SparkplugMetric.ReadWriteValue.Main1,
                AddressOf _SparkplugMetric.Starting_Stopped.Main1,
                AddressOf _SparkplugMetric.UpdateReadData.Main1,
                AddressOf _SparkplugMetric.WriteData.Main1,
                AddressOf _SparkplugMetric.WriteFunction.Main1,
                AddressOf _SparkplugMetric.WriteValueAction.Bytes,
                AddressOf _SparkplugMetric.WriteValueAction.Main1,
                AddressOf _SparkplugMetric.WriteValueAction.UInt16,
                AddressOf _SparkplugMetric.WriteValueAction.WriteOnly1,
                AddressOf _SparkplugMetric.WriteValueFunction.Main1,
                                                                    _
                AddressOf _SparkplugProducerMonitoring.EdgeNodeAndDevices.Main1
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
