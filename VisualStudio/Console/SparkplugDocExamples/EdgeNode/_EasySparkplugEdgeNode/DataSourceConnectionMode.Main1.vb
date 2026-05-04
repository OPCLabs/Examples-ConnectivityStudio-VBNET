' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to connect to the data source only when the producer is online.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class DataSourceConnectionMode
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")

            ' Configure the edge node to connect to the data source only when the producer is online. You can test it out
            ' e.g. by stopping or disconnecting from the MQTT broker.
            ' 
            ' You can compare the output of this example with and without the statement below. With the statement, the
            ' Poll event will not be raised unless the producer is online. Without the statement, the Poll event will
            ' always be raised while the edge node is started, regardless of the MQTT broker connection state.
            edgeNode.DataSourceConnectionMode = SparkplugDataSourceConnectionMode.WhenProducerOnline

            ' Hook the SystemConnectionStateChanged event to handle system connection state changes.
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Hook the ProducerOnlineChanged event to handle changes in the online state of the producer.
            AddHandler edgeNode.ProducerOnlineChanged,
                Sub(sender, eventArgs)
                    ' Display the new producer online state.
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.ProducerOnlineChanged)}: {edgeNode.ProducerOnline}")
                End Sub

            ' Hook the Poll event (but do not mark the polling as processed by ourselves).
            AddHandler edgeNode.Poll,
                Sub(sender, eventArgs)
                    ' Display when the component Is polling for New data.
                    Console.WriteLine(NameOf(EasySparkplugEdgeNode.Poll))
                End Sub

            ' Define a metric providing random integers.
            Dim random = New Random()
            edgeNode.Metrics.Add(New SparkplugMetric("MyMetric").ReadValueFunction(Function() random.Next()))

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
