' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to configure connection parameters of the Sparkplug edge node.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class SystemConnectionParameters
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

            ' Configure various connection parameters of the edge node.
            edgeNode.SystemConnectionParameters.MqttKeepAliveInterval = 2 * 1000 ' 2 seconds
            edgeNode.SystemConnectionParameters.MqttKeepAliveIntervalDebug = 2 * 1000 ' 2 seconds
            edgeNode.SystemConnectionParameters.PublishConnectionTimeout = 10 * 1000 ' 10 seconds
            edgeNode.SystemConnectionParameters.ReconnectInterval = 5 * 1000 ' 5 seconds

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
