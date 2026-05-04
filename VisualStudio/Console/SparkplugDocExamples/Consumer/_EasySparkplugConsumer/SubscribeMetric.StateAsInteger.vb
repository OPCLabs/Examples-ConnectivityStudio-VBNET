' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to changes of multiple metrics and display each change, identifying the different
' subscriptions by an integer.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugConsumer
    Class SubscribeMetric
        Public Shared Sub StateAsInteger()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the consumer object and hook events.
            Dim consumer = New EasySparkplugConsumer()
            AddHandler consumer.MetricNotification, AddressOf consumer_StateAsInteger_MetricNotification

            Console.WriteLine("Subscribing...")
            consumer.SubscribeMetric(
                New EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor,
                    "easyGroup", "easySparkplugDemo", "", "Random", Nothing) With
                    {.State = 1}) ' An integer we have chosen to identify the subscription
            consumer.SubscribeMetric(
                New EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor,
                    "easyGroup", "easySparkplugDemo", "", "Simple", Nothing) With
                    {.State = 2}) ' An integer we have chosen to identify the subscription
            consumer.SubscribeMetric(
                New EasySparkplugMetricSubscriptionArguments(SparkplugComponentTypes.EdgeNode, hostDescriptor,
                    "easyGroup", "easySparkplugDemo", "", "Ramp", Nothing) With
                    {.State = 3}) ' An integer we have chosen to identify the subscription

            Console.WriteLine("Processing notifications for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllMetrics()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub consumer_StateAsInteger_MetricNotification(ByVal sender As Object, ByVal eventArgs As EasySparkplugMetricNotificationEventArgs)
            ' Obtain the integer state we have passed in.
            ' Note that the metric name also comes with the notification and can be used to determine which metric the
            ' notification relates to. The reason we are using the state is that it allows to pass in the information that
            ' your application understands immediately, and is thus more efficient.
            Dim stateAsInteger = CInt(eventArgs.Arguments.State)

            ' Display the data.
            If (eventArgs.Succeeded) Then
                If (eventArgs.HasData) Then
                    Console.WriteLine($"{stateAsInteger}: {eventArgs.MetricData}")
                End If
            Else
                Console.WriteLine($"{stateAsInteger} *** Failure: {eventArgs.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace
#End Region
