' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to consume Sparkplug data while specifying own Sparkplug host application ID.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.Consumer._SparkplugHostDescriptor
    Class HostId
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            ' The second parameter is the host ID of this Sparkplug host application. Other Sparkplug components can use
            ' this host ID to detect whether the application is online or offline.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost", "easyApplication")

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            Console.WriteLine("Subscribing...")
            consumer.SubscribeEdgeNodePayload(hostDescriptor, "easyGroup", "easySparkplugDemo",
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

            Console.WriteLine("Processing notifications for 20 seconds...")
            System.Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllPayloads()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
