' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to implement reading from edge node metrics using a single overriden method.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class OnRead
        ''' <summary>
        ''' A sparkplug edge node, with specialized read behavior for its metrics.
        ''' </summary>
        Class EdgeNodeWithOnRead
            Inherits EasySparkplugEdgeNode

            ''' <summary>
            ''' Obtains the data for Sparkplug read.
            ''' </summary>
            ''' <param name="eventArgs">The event arguments.</param>
            Protected Overrides Sub OnRead(eventArgs As SparkplugMetricReadEventArgs)
                ' Obtain the state associated with the metric that is being read.
                Dim state As Object = eventArgs.Metric.State

                ' Use the state as the offset for the random value, so that each metric generates values in a unique range.
                Dim offset As Integer = CInt(state * 100)

                ' Generate a random value, indicate that the read has been handled, and return the generated value.
                eventArgs.HandleAndReturn(Random.Next(offset, offset + 100))
            End Sub

            Private Shared ReadOnly Random As Random = New Random()
        End Class

        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate our derived edge node object and hook events.
            Dim edgeNode = New EdgeNodeWithOnRead With
            {
                .EdgeNodeId = "easySparkplugDemo",
                .GroupId = "easyGroup",
                .SystemDescriptor = hostDescriptor
            }
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Create metrics in the folder. Distinguish them by their state.
            edgeNode.Add(New SparkplugMetric("MyMetric1").ValueType(Of Integer)().SetState(1))
            edgeNode.Add(New SparkplugMetric("MyMetric2").ValueType(Of Integer)().SetState(2))
            edgeNode.Add(New SparkplugMetric("MyMetric3").ValueType(Of Integer)().SetState(3))
            edgeNode.Add(New SparkplugMetric("MyMetric4").ValueType(Of Integer)().SetState(4))
            edgeNode.Add(New SparkplugMetric("MyMetric5").ValueType(Of Integer)().SetState(5))

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
