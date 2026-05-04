' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to configure connection parameters of the Sparkplug host application.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel
Imports OpcLabs.EasySparkplug.System

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugHostApplication
    Class SystemConnectionParameters
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Pre-create the host application, so that we can control it.
            Dim hostApplication As EasySparkplugHostApplication = EasySparkplugInfrastructure.Instance.FindOrCreateHostApplication(hostDescriptor)

            ' Configure various connection parameters of the host application.
            hostApplication.SystemConnectionParameters.MqttKeepAliveInterval = 2 * 1000 ' 2 seconds
            hostApplication.SystemConnectionParameters.MqttKeepAliveIntervalDebug = 2 * 1000 ' 2 seconds
            hostApplication.SystemConnectionParameters.PublishConnectionTimeout = 10 * 1000 ' 10 seconds
            hostApplication.SystemConnectionParameters.ReconnectInterval = 5 * 1000 ' 5 seconds

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            Console.WriteLine("Subscribing...")
            ' Thanks to implicit conversion, EasySparkplugHostApplication can be used in place of SparkplugHostDescriptor.
            consumer.SubscribeEdgeNodePayload(hostApplication, "easyGroup", "easySparkplugDemo",
                Sub(sender, eventArgs)
                    ' Handle different types of notifications.
                    Console.WriteLine()
                    Select Case eventArgs.NotificationType
                        Case SparkplugNotificationType.Connect
                            Console.WriteLine($"Connected to Sparkplug host, client ID: {eventArgs.ClientId}.")
                        Case SparkplugNotificationType.Disconnect
                            Console.WriteLine("Disconnected from Sparkplug host.")
                        Case SparkplugNotificationType.Data,
                            SparkplugNotificationType.Birth
                            Console.WriteLine("Received birth or data message from Sparkplug host.")
                            ' Display the metrics name and data for each metric delivered in the payload.
                            For Each pair As KeyValuePair(Of String, SparkplugMetricElement) In eventArgs.Payload
                                Console.WriteLine($"{pair.Key}: {pair.Value.MetricData}")
                            Next
                        Case SparkplugNotificationType.Death
                            Console.WriteLine("Received death message from Sparkplug host.")
                    End Select
                    If Not eventArgs.Succeeded Then
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}")
                    End If
                End Sub)

            Console.WriteLine("Processing notifications for 60 seconds...")
            System.Threading.Thread.Sleep(60 * 1000)

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllPayloads()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
