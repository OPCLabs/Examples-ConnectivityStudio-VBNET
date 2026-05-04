' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' This example shows how to monitor birth, death, and rebirth of a Sparkplug edge node and its devices.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.Services

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugProducerMonitoring
    Class EdgeNodeAndDevices
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

            ' Define a metric providing random integers.
            Dim random = New Random()
            SparkplugMetric.CreateIn(edgeNode, "MyMetric").ReadValueFunction(Function() random.Next())

            ' Define two devices, each with a single metric providing random integers.
            Dim myDevice1 As SparkplugDevice = SparkplugDevice.CreateIn(edgeNode, "MyDevice1")
            SparkplugMetric.CreateIn(myDevice1, "MyMetric1").ReadValueFunction(Function() random.Next())
            Dim myDevice2 As SparkplugDevice = SparkplugDevice.CreateIn(edgeNode, "MyDevice2")
            SparkplugMetric.CreateIn(myDevice2, "MyMetric2").ReadValueFunction(Function() random.Next())

            ' Obtain monitoring services and hook events to them.
            Dim edgeNodeMonitoring As ISparkplugProducerMonitoring = edgeNode.GetService(Of ISparkplugProducerMonitoring)()
            If Not edgeNodeMonitoring Is Nothing Then
                ' Monitor the edge node itself.
                AddHandler edgeNodeMonitoring.Birth, Sub(sender, EventArgs) _
                    Console.WriteLine($"{sender}.{NameOf(edgeNodeMonitoring.Birth)}")
                AddHandler edgeNodeMonitoring.Death, Sub(sender, EventArgs) _
                    Console.WriteLine($"{sender}.{NameOf(edgeNodeMonitoring.Death)}")
                AddHandler edgeNodeMonitoring.Rebirth, Sub(sender, EventArgs) _
                    Console.WriteLine($"{sender}.{NameOf(edgeNodeMonitoring.Rebirth)}")

                ' Monitor all devices in the edge node.
                For Each device As SparkplugDevice In edgeNode.Devices
                    Dim deviceMonitoring As ISparkplugProducerMonitoring = device.GetService(Of ISparkplugProducerMonitoring)()
                    If Not deviceMonitoring Is Nothing Then
                        AddHandler deviceMonitoring.Birth, Sub(sender, eventArgs) _
                            Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Birth)}")
                        AddHandler deviceMonitoring.Death, Sub(sender, eventArgs) _
                            Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Death)}")
                        AddHandler deviceMonitoring.Rebirth, Sub(sender, eventArgs) _
                            Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Rebirth)}")
                    End If
                Next
            End If

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
