' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' A fully functional Sparkplug edge node running in a console host.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.BaseLib
Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.System

Namespace Global.SparkplugApplicationConsoleDemo
    Friend Class Program
        Shared Sub Main(args As String())
            Console.WriteLine("EasySparkplug Application Console Demo")
            Console.WriteLine()

            ' Parse command line arguments for broker URL, group ID, edge node ID, and host ID.

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

            Dim hostId As String = "" ' we use "easyApplication" in some examples
            If args.Length >= 4 Then
                hostId = args(3)
            End If

            Console.WriteLine($"Broker URL: {brokerUrlString}")
            Console.WriteLine($"Group ID: {groupId}")
            Console.WriteLine($"Edge node ID: {edgeNodeId}")
            Console.WriteLine($"Host ID: {hostId}")
            Console.WriteLine()

            ' Enable the console interaction by the component. The interactive user will then be able to validate remote
            ' certificates and/or specify local certificate(s) to use.
            Dim componentParameters As ComponentParameters = EasySparkplugInfrastructure.Instance.Parameters
            componentParameters.PluginSetups.FindName("ConsoleInteraction").Enabled = True

            ' Instantiate the consumer object.
            Dim hostDescriptor = New SparkplugHostDescriptor(brokerUrlString, hostId)
            Using consumer As New EasySparkplugConsumer(hostDescriptor)
                ' Subscribe to all metrics of the specified edge node(s).
                consumer.SubscribeEdgeNodeMetric(groupId, edgeNodeId, "#",
                    Sub(sender, EventArgs) Console.WriteLine($"{NameOf(consumer.MetricNotification)}: {EventArgs}"))

                ' Subscribe to all device metrics of the specified edge node(s).
                consumer.SubscribeDeviceMetric(groupId, edgeNodeId, "#", "#",
                    Sub(sender, EventArgs) Console.WriteLine($"{NameOf(consumer.MetricNotification)}: {EventArgs}"))

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
            End Using
        End Sub
    End Class
End Namespace
#End Region
