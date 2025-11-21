' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to get notified when the Sparkplug edge encounters a failure during message publishing.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasySparkplug
Imports Timer = System.Timers.Timer

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class PublishingError
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")

            ' Configure the edge node so that we will publish data fully manually.
            edgeNode.PublishingInterval = Timeout.Infinite
            edgeNode.ReportByException = True

            ' Hook the SystemConnectionStateChanged event to handle system connection state changes.
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Hook the PublishingError event to handle errors that occur during publishing.
            ' application.
            AddHandler edgeNode.PublishingError,
                Sub(sender, eventArgs)
                    ' Display the error that occurred.
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.PublishingError)}: {eventArgs}")
                End Sub

            ' Define a metric providing random integers.
            Dim random = New Random()
            Dim myMetric As SparkplugMetric = SparkplugMetric.CreateIn(edgeNode, "MyMetric").ValueType(Of Integer)()

            ' Start the edge node.
            Console.WriteLine("The edge node is starting...")
            edgeNode.Start()

            Console.WriteLine("The edge node is started.")
            Console.WriteLine()

            ' Create a timer for publishing the data, and start it.
            Dim timer = New Timer With
            {
                .Interval = 2 * 1000, ' 2 seconds
                .AutoReset = True
            }
            AddHandler timer.Elapsed,
                Sub(sender, EventArgs)
                    edgeNode.PublishDataPayload(New SparkplugPayload(myMetric.Name, New SparkplugMetricData(random.Next())))
                End Sub
            timer.Start()

            ' You can simulate a publishing error e.g. by stopping the MQTT broker or disconnecting the network cable.
            ' Note that without the manual publishing, triggering the error would not be easy, as the edge node
            ' automatically pauses its own publishing attempts when it detects a connection failure.

            ' Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the edge node...")
            Console.ReadLine()

            ' Stop the timer.
            timer.Stop()

            ' Stop the edge node.
            Console.WriteLine("The edge node is stopping...")
            edgeNode.Stop()

            Console.WriteLine("The edge node is stopped.")
        End Sub
    End Class
End Namespace
#End Region
