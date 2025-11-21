' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to create a Sparkplug edge node with a single metric, start and stop it, using WebSocket.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
' The MQTT broker may need additional configuration to support WebSocket connections, such as enabling the WebSocket port
' and setting up the path.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Partial Class Start_Stop
        Public Shared Sub WebSocket()
            ' The WebSocket protocol can be specified in the broker URL using the "ws" scheme, as below.
            ' Note: Use the "wss" scheme for WebSocket Secure. Default port for "ws" is 80, and for "wss" is 443.
            Dim hostDescriptor = New SparkplugHostDescriptor("ws://localhost:8080/mqtt")

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor,
                                                     "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged, AddressOf edgeNode_WebSocket_SystemConnectionStateChanged

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

        Private Shared Sub edgeNode_WebSocket_SystemConnectionStateChanged _
            (ByVal sender As Object, ByVal eventArgs As SparkplugConnectionStateChangedEventArgs)
            ' Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
        End Sub
    End Class
End Namespace
#End Region
