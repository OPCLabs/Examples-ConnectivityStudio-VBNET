' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to changes of a single metric, and display the value of the metric with each change
' using a callback method that is provided as lambda expression.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugConsumer
    Partial Class SubscribeEdgeNodeMetric
        Public Shared Sub CallbackLambda()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            Console.WriteLine("Subscribing...")
            ' The callback is a lambda expression the displays the value
            ' In this example, we specify the precise Sparkplug group ID, edge node ID, and metric name.

            consumer.SubscribeEdgeNodeMetric(hostDescriptor,
                "easyGroup", "easySparkplugDemo", "Random",
                Sub(sender, eventArgs)
                    If (eventArgs.Succeeded) Then
                        If (eventArgs.HasData) Then
                            Console.WriteLine("Value: {0}", eventArgs.MetricData?.Value)
                        End If
                    Else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                    End If
                End Sub)

            Console.WriteLine("Processing notifications for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllMetrics()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
