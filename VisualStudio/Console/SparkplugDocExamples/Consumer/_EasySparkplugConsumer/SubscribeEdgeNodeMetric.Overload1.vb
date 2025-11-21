' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to all metrics of a given edge node and display the name and value of the metric with
' each notification.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugConsumer
    Partial Class SubscribeEdgeNodeMetric
        Public Shared Sub Overload1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the consumer object and hook events.
            Dim consumer = New EasySparkplugConsumer()
            AddHandler consumer.MetricNotification, AddressOf consumer_Overload1_MetricNotification

            Console.WriteLine("Subscribing...")
            ' In this example, we specify the precise Sparkplug group ID and edge node ID, but allow any metric name.
            consumer.SubscribeEdgeNodeMetric(hostDescriptor,
                "easyGroup", "easySparkplugDemo", "#")

            Console.WriteLine("Processing notifications for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllMetrics()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub consumer_Overload1_MetricNotification(ByVal sender As Object, ByVal eventArgs As EasySparkplugMetricNotificationEventArgs)
            ' Handle different types of notifications.
            Console.WriteLine()
            Select Case eventArgs.NotificationType
                Case SparkplugNotificationType.Connect
                    Console.WriteLine($"Connected to Sparkplug host, client ID: {eventArgs.ClientId}.")
                Case SparkplugNotificationType.Disconnect
                    Console.WriteLine("Disconnected from Sparkplug host.")
                Case SparkplugNotificationType.Data
                    Console.WriteLine("Received data from Sparkplug host.")
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}")
                    Console.WriteLine($"Value: {eventArgs.MetricData?.Value}")
                Case SparkplugNotificationType.Birth
                    Console.WriteLine("Received birth message from Sparkplug host.")
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}")
                    Console.WriteLine($"Value: {eventArgs.MetricData?.Value}")
                Case SparkplugNotificationType.Death
                    Console.WriteLine("Received death message from Sparkplug host.")
                    Console.WriteLine($"Metric name: {eventArgs.MetricName}")
            End Select
            If Not eventArgs.Succeeded Then
                Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace
#End Region
