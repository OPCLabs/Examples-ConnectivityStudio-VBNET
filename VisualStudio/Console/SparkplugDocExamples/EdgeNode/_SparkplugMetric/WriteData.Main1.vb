' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' This example shows how to retrieve the metric data in the pull data consumption model. In this model, the data that
' Sparkplug applications send to the edge node or device is pulled and processed by your code when needed.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data. SparkplugCmd, or other capable Sparkplug application, can be used to write
' data into the metric.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports Timer = System.Timers.Timer

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugMetric
    Class WriteData
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

            ' Create a read-write data variable with an initial value.
            Dim metric = SparkplugMetric.CreateIn(edgeNode, "WriteToThisMetric").ReadWriteValue(0)

            ' Create a timer for pulling the data from OPC writes. In a real server the activity may also come from other
            ' sources.
            Dim timer = New Timer With
            {
                .Interval = 1000, ' 1 second
                .AutoReset = True
            }

            ' Periodically display the value of the metric on the console.
            AddHandler timer.Elapsed, Sub(s, a) Console.Write($"  {metric.WriteData.Value}")
            timer.Start()
            Console.WriteLine("Values of the example metric are displayed on the console periodically.")

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

            ' Stop the timer.
            timer.Stop()
        End Sub
    End Class
End Namespace
#End Region
