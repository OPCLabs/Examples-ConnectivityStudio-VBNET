' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to turn off the polling by the component, and instead manually publish the data by reporting when
' they have changed.
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
    Class ReportByException
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

            ' Define a metric providing random integers.
            Dim random = New Random()
            Dim myMetric1 As SparkplugMetric = SparkplugMetric.CreateIn(edgeNode, "MyMetric1").ValueType(Of Integer)()
            Dim myMetric2 As SparkplugMetric = SparkplugMetric.CreateIn(edgeNode, "MyMetric2").ValueType(Of Integer)()
            Dim myMetric3 As SparkplugMetric = SparkplugMetric.CreateIn(edgeNode, "MyMetric3").ValueType(Of Integer)()

            ' Start the edge node.
            Console.WriteLine("The edge node is starting...")
            edgeNode.Start()

            Console.WriteLine("The edge node is started.")
            Console.WriteLine()

            ' Create a timer for publishing the data, and start it.
            Dim timer = New Timer With {.AutoReset = True}
            AddHandler timer.Elapsed,
                Sub(sender, EventArgs)
                    ' Do not publish individual updates, but rather lock the publishing so that we can make multiple updates.
                    edgeNode.LockPublishing()
                    Try
                        ' Update some of the metrics (in this example, with random data).
                        If random.Next(2) <> 0 Then
                            myMetric1.UpdateReadData(random.Next())
                        End If
                        If random.Next(2) <> 0 Then
                            myMetric2.UpdateReadData(random.Next())
                        End If
                        If random.Next(2) <> 0 Then
                            myMetric3.UpdateReadData(random.Next())
                        End If
                    Finally
                        ' At this point, the edge node will publish the data for all metrics that have been updated.
                        edgeNode.UnlockPublishing()
                    End Try

                    ' Set the next interval to a random value between 0 and 3 seconds.
                    timer.Interval = random.Next(3 * 1000)
                End Sub
            timer.Start()

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
