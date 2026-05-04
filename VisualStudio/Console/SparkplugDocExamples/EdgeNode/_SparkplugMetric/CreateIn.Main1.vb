' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable UnusedVariable
#Region "Example"
' This example show to add metrics to the Sparkplug edge node and/or device using the CreateIn method. This method combines
' creation of the metric with adding it to the parent component.
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

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugMetric
    Class CreateIn
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged, AddressOf edgeNode_Main1_SystemConnectionStateChanged

            ' Create a constant metric in the edge node.
            Dim constantMetric As SparkplugMetric = SparkplugMetric.CreateIn(edgeNode, "Constant").ConstantValue("abc")

            ' Create a device.
            Dim device As SparkplugDevice = SparkplugDevice.CreateIn(edgeNode, "Device")

            ' Create a read/write metric ("register") in the device.
            Dim readWriteMetric As SparkplugMetric = SparkplugMetric.CreateIn(device, "ReadWrite").ReadWriteValue(0)

            ' Note: You can, of course, also create the SparkplugMetric using its constructor, and add it to the Metrics
            ' collection then on the edge node or device then. The CreateIn method is just a convenience shorthand for
            ' this; in addition, it returns the created metric, so you can easily configure it further, and/or store it.

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

        Private Shared Sub edgeNode_Main1_SystemConnectionStateChanged _
            (ByVal sender As Object, ByVal eventArgs As SparkplugConnectionStateChangedEventArgs)
            ' Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
        End Sub

    End Class
End Namespace
#End Region
