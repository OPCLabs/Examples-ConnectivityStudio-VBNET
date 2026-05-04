' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' This example shows how to react to events in order to initiate and finalize data collection in the push data provision
' model.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports Timer = System.Timers.Timer

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugMetric
    Class Starting_Stopped
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Create a read-only data metric.
            Dim metric = SparkplugMetric.CreateIn(edgeNode, "ReadThisMetric") _
                .ValueType(Of Integer)() _
                .Writable(False)

            AddHandler metric.Starting,
                Sub(sender, args)
                    ' Create a timer for pushing the data to the metric. In a real edge node or device, the activity may also come
                    ' from other sources.
                    Dim timer = New Timer With
                    {
                        .Interval = 1000, ' 1 second
                        .AutoReset = True
                    }

                    ' Set the read data of the metric to a random value whenever the timer interval elapses.
                    ' Note that this example shows the basic concept, however there is also an UpdateReadData method that
                    ' can be used in most cases to achieve slightly more concise code.
                    Dim random = New Random()
                    AddHandler timer.Elapsed,
                        Sub(s, a)
                            metric.ReadData = New SparkplugData(random.Next(), DateTime.UtcNow)
                        End Sub

                    ' Associate the timer with the data variable.
                    metric.State = timer

                    timer.Start()
                End Sub
            AddHandler metric.Stopped,
                Sub(sender, args)
                    ' Obtain the timer associated with the metric.
                    Dim timer = CType(CType(sender, SparkplugMetric).State, Timer)

                    ' Stop the timer.
                    timer.Stop()
                End Sub


            ' Start the edge node.
            Console.WriteLine("The edge node is starting...")
            edgeNode.Start()

            Console.WriteLine("The edge node is started.")
            Console.WriteLine()

            ' Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the edge node...")
            Console.ReadLine()

            ' Stop the edge node.
            Console.WriteLine("The edge node is stopping...")
            edgeNode.Stop()

            Console.WriteLine("The edge node is stopped.")
        End Sub
    End Class
End Namespace
#End Region
