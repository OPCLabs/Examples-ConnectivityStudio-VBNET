' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to implement custom data source connection and disconnection handling for a Sparkplug device.
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
Imports Timer = System.Timers.Timer

Namespace Global.SparkplugDocExamples.EdgeNode._SparkplugDevice
    Class ConnectDataSource
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the edge node object.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, "easyGroup", "easySparkplugDemo")

            ' Hook the SystemConnectionStateChanged event to handle system connection state changes.
            AddHandler edgeNode.SystemConnectionStateChanged,
                Sub(sender, eventArgs)
                    ' Display the new connection state (such as when the connection to the broker succeeds or fails).
                    Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
                End Sub

            ' Define a metric providing random integers.
            Dim random = New Random()
            edgeNode.Metrics.Add(New SparkplugMetric("MyMetric").ReadValueFunction(Function() random.Next()))

            ' Create a device.
            Dim device As SparkplugDevice = SparkplugDevice.CreateIn(edgeNode, "Device")

            ' Hook the DataSourceConditionChanged event to handle data source connection state changes.
            AddHandler device.DataSourceConditionChanged,
                Sub(sender, eventArgs)
                    ' Display the new data source condition.
                    Console.WriteLine($"{NameOf(SparkplugDevice.DataSourceConditionChanged)}: {eventArgs}")
                End Sub

            ' Hook the events to connect and disconnect the device data source.
            AddHandler device.ConnectDataSource, AddressOf DeviceOnConnectDataSource
            AddHandler device.DisconnectDataSource, AddressOf DeviceOnDisconnectDataSource

            ' Define a metric on the device providing random integers.
            device.Metrics.Add(New SparkplugMetric("MyMetric2").ReadValueFunction(Function() random.Next()))

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
        ''' Handles the <see cref="SparkplugDevice.ConnectDataSource"/> event.
        ''' </summary>
        Private Shared Sub DeviceOnConnectDataSource(ByVal sender As Object, ByVal eventArgs As SparkplugProducerProcessedEventArgs)
            ' The event sender is the device.
            Dim device = CType(sender, SparkplugDevice)
            Console.WriteLine(NameOf(device.ConnectDataSource))

            ' Simulate a connection to the data source that takes 5 seconds to either succeed or fail.
            ' In a real application, you would connect to the actual data source here.
            Dim timer = New Timer(5 * 1000) With {.AutoReset = False}
            AddHandler timer.Elapsed,
                Sub(s, e)
                    If Random.Next(0, 3) = 0 Then
                        ' Simulate a successful connection to the data source. Reconnect after 10 seconds.
                        device.DataSourceConnectionSuccess()
                    Else
                        ' Simulate a failure to connect to the data source.
                        device.DataSourceConnectionFailure(
                            "Simulated connection failure.",
                            10 * 1000)
                    End If
                End Sub
            timer.Start()

            ' Indicate that the event is handled. This is necessary to do, otherwise the default behavior would kick in,
            ' and the data source would be considered immediately successfully connected.
            eventArgs.Handled = True
        End Sub

        ''' <summary>
        ''' Handles the <see cref="SparkplugDevice.DisconnectDataSource"/> event.
        ''' </summary>
        Private Shared Sub DeviceOnDisconnectDataSource(ByVal sender As Object, ByVal eventArgs As SparkplugProducerProcessedEventArgs)
            ' The event sender is the device.
            Dim device = CType(sender, SparkplugDevice)
            Console.WriteLine(NameOf(device.DisconnectDataSource))

            ' Simulate a disconnection from the data source that takes 5 seconds.
            ' In a real application, you would disconnect from the actual data source here.
            Dim timer = New Timer(5 * 1000) With {.AutoReset = False}
            AddHandler timer.Elapsed, Sub(s, e) device.DataSourceDisconnectionSuccess()
            timer.Start()

            ' Indicate that the event is handled. This is necessary to do, otherwise the default behavior would kick in,
            ' and the data source would be considered immediately successfully disconnected.
            eventArgs.Handled = True
        End Sub

        Private Shared ReadOnly Random As Random = New Random()
    End Class
End Namespace
#End Region
