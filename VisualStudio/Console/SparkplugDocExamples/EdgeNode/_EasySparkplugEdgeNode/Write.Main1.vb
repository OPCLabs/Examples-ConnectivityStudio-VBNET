' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to handle the write event on the edge node, providing a way to implement writing of different
' metrics using a single handler.
'
' This example shows how to implement reading of multiple edge node metrics using a single handler.
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data. SparkplugCmd, or other capable Sparkplug application, can be used to write
' data into the metric.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class Write
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

            ' Create metrics in the folder. Distinguish them by their state.
            edgeNode.Add(New SparkplugMetric("MyMetric1").ValueType(Of Integer)().SetState(1))
            edgeNode.Add(New SparkplugMetric("MyMetric2").ValueType(Of Integer)().SetState(2))
            edgeNode.Add(New SparkplugMetric("MyMetric3").ValueType(Of Integer)().SetState(3))
            edgeNode.Add(New SparkplugMetric("MyMetric4").ValueType(Of Integer)().SetState(4))
            edgeNode.Add(New SparkplugMetric("MyMetric5").ValueType(Of Integer)().SetState(5))

            ' Handle the read event for the edge node.
            AddHandler edgeNode.Write, AddressOf EdgeNodeOnWrite

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

        ''' <summary>
        ''' Event handler for the write event on the edge node. 
        ''' </summary>
        ''' <param name="sender">The edge node object that sends the event.</param>
        ''' <param name="eventArgs">Data for the metric write event.</param>
        Private Shared Sub EdgeNodeOnWrite(ByVal sender As Object, ByVal eventArgs As SparkplugMetricWriteEventArgs)
            ' Obtain the state associated with the metric that is being written.
            Dim state As Object = eventArgs.Metric.State

            ' The state is null in metrics that we have not created, such as the "node rebirth" metric.
            If state Is Nothing Then
                Return
            End If

            ' Use the state as the offset for the random value, so that each metric generates values in a unique range.
            Dim offset As Integer = CInt(state * 100)

            ' Display the state on the console together with the new value.
            Console.WriteLine($"Metric {eventArgs.Metric.State}, value written: {eventArgs.Data.Value}")
        End Sub
    End Class
End Namespace
#End Region
