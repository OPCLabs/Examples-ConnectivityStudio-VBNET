' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' This example shows how to use the IDisposable interface to automatically stop the Sparkplug edge node.
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
    Class Dispose
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object.
            ' The "using" statement ensures disposal of the resource it acquires.
            Using edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")
                ' Hook events.
                AddHandler edgeNode.SystemConnectionStateChanged, AddressOf edgeNode_Main1_SystemConnectionStateChanged

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

                ' The IDisposable.Dispose call (automatically made at the end of the "using" statement) stops the
                ' EasySparkplugEdgeNode if it is started.
                Console.WriteLine("The edge node is stopping...")
            End Using
            Console.WriteLine("The edge node is stopped.")
        End Sub

        Private Shared Sub edgeNode_Main1_SystemConnectionStateChanged(ByVal sender As Object, ByVal eventArgs As SparkplugConnectionStateChangedEventArgs)
            ' Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
        End Sub
    End Class
End Namespace
#End Region
