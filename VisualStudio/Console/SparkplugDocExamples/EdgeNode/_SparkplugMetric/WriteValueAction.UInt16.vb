' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' This example shows how to define a metric of Sparkplug data type UInt16 and use an action to its write behavior.
' This is an example of the push data consumption model.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data. SparkplugCmd, or other capable Sparkplug application, can be used to write
' data into the metric.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugMetric
    Partial Class WriteValueAction
        Public Shared Sub UInt16()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Create a writable metric and add an action that will be executed when the metric is written to.
            ' We explicitly specify the Sparkplug data type of the variable to be UInt16, but use .NET Int32 in the write value
            ' function. EasySparkplug will attempt to convert the value being written to the specified .NET type. This is
            ' helpful in languages like VB.NET that do not have full support for some types (such as unsigned integers).
            edgeNode.Add(New SparkplugMetric("WriteToThisMetric").WriteValueAction(Of Integer)(
                GetType(UInt16),
                Sub(value) Console.WriteLine($"Value written: {value}")))

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
