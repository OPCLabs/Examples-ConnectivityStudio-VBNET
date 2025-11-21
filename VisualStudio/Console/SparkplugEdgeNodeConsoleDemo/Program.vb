' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToDisposedClosure
' ReSharper disable ArrangeModifiersOrder
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' A fully functional Sparkplug edge node running in a console host.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.BaseLib
Imports OpcLabs.BaseLib.Collections.Generic.Extensions
Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.Services
Imports OpcLabs.EasySparkplug.System
Imports SparkplugEdgeNodeDemoLibrary

Namespace Global.SparkplugEdgeNodeConsoleDemo
    Friend Class Program
        Shared Sub Main(args As String())
            Console.WriteLine("EasySparkplug Edge Node Console Demo")
            Console.WriteLine()

            ' Parse command line arguments for broker URL, group ID, edge node ID, and primary host ID.

            Dim brokerUrlString As String = "mqtt://localhost"
            If args.Length >= 1 Then
                brokerUrlString = args(0)
            End If

            Dim groupId As String = "easyGroup"
            If args.Length >= 2 Then
                groupId = args(1)
            End If

            Dim edgeNodeId As String = "easySparkplugDemo"
            If args.Length >= 3 Then
                edgeNodeId = args(2)
            End If

            Dim primaryHostId As String = "" ' we use "easyApplication" in some examples
            If args.Length >= 4 Then
                primaryHostId = args(3)
            End If

            Console.WriteLine($"Broker URL: {brokerUrlString}")
            Console.WriteLine($"Group ID: {groupId}")
            Console.WriteLine($"Edge node ID: {edgeNodeId}")
            Console.WriteLine($"Primary host ID: {primaryHostId}")
            Console.WriteLine()

            ' Enable the console interaction by the component. The interactive user will then be able to validate remote
            ' certificates and/or specify local certificate(s) to use.
            Dim componentParameters As ComponentParameters = EasySparkplugInfrastructure.Instance.Parameters
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = True

            ' Instantiate the edge node object.
            Using edgeNode As New EasySparkplugEdgeNode(brokerUrlString, groupId, edgeNodeId)
                ' Configure the primary host ID the edge node will use.
                ' Leave the primary host ID empty if the edge node is not serving a primary host.
                edgeNode.PrimaryHostId = primaryHostId

                ' Add metrics from the demo library to the edge node.
                edgeNode.Metrics.AddRange(DemoMetrics.Create())

                ' Add devices to the edge node, with metrics from the demo library.
                edgeNode.Devices.Add(New SparkplugDevice("data", DataMetrics.Create()))
                edgeNode.Devices.Add(New SparkplugDevice("console", ConsoleMetrics.Create()))
                edgeNode.Devices.Add(New SparkplugDevice("demo", DemoMetrics.Create()))

                ' Hook events to the edge node object.
                AddHandler edgeNode.ApplicationOnlineChanged, Sub(sender, EventArgs) _
                    Console.WriteLine($"{NameOf(edgeNode.ApplicationOnlineChanged)}: {edgeNode.ApplicationOnline}")
                AddHandler edgeNode.MetricNotification, Sub(sender, EventArgs) _
                    Console.WriteLine($"{NameOf(edgeNode.MetricNotification)}: {EventArgs}")
                AddHandler edgeNode.PublishingError, Sub(sender, EventArgs) _
                    Console.WriteLine($"{NameOf(edgeNode.PublishingError)}: {EventArgs}")
                AddHandler edgeNode.Starting, Sub(sender, EventArgs) Console.WriteLine(NameOf(edgeNode.Starting))
                AddHandler edgeNode.Stopped, Sub(sender, EventArgs) Console.WriteLine(NameOf(edgeNode.Stopped))
                AddHandler edgeNode.SystemConnectionStateChanged, Sub(sender, EventArgs) _
                    Console.WriteLine($"{NameOf(edgeNode.SystemConnectionStateChanged)}: {EventArgs}")

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
                            AddHandler deviceMonitoring.Birth, Sub(sender, EventArgs) _
                                Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Birth)}")
                            AddHandler deviceMonitoring.Death, Sub(sender, EventArgs) _
                                Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Death)}")
                            AddHandler deviceMonitoring.Rebirth, Sub(sender, EventArgs) _
                                Console.WriteLine($"{sender}.{NameOf(deviceMonitoring.Rebirth)}")
                        End If
                    Next
                End If

                ' Start the edge node.
                edgeNode.Start()

                ' Let the user decide when to stop.
                Dim cancelled = New ManualResetEvent(initialState:=False)
                AddHandler Console.CancelKeyPress,
                    Sub(sender, EventArgs) _
                        ' Signal the main thread to exit.
                        cancelled.Set()

                        ' Prevent the process from terminating immediately.
                        EventArgs.Cancel = True
                    End Sub

                Console.WriteLine("Press Ctrl+C to stop the edge node...")
                Console.WriteLine()
                cancelled.WaitOne()

                ' Stop the edge node.
                edgeNode.Stop()
            End Using
        End Sub
    End Class
End Namespace
#End Region
