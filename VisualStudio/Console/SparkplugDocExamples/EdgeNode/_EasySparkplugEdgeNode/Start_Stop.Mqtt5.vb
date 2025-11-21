' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
' ReSharper disable UnusedVariable
#Region "Example"
' This example shows how to create a Sparkplug edge node with a single metric, start and stop it, using MQTT version 5.0.
'
' You can use any Sparkplug application, including our SparkplugCmd utility and the SparkplugApplicationConsoleDemo
' program, to subscribe to the edge node data.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Partial Class Start_Stop
        Public Shared Sub Mqtt5()
            ' The MQTT protocol version can be specified in the broker URL using the "version" query parameter, as below.
            ' Possible values are 310, 311, and 500, which correspond to MQTT 3.1, MQTT 3.1.1, and MQTT 5.0, respectively.
            ' The default is MQTT 3.1.1.
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost?version=500")

            ' Alternatively, if you cannot or do not want to manipulate the broker URL, set a custom property in the
            ' broker descriptor, as below. This method is subject to change with the implementation of the Sparkplug
            ' component provider. Use value 3 for MQTT 3.1, 4 for MQTT 3.1.1, and 5 for MQTT 5.0.
            Dim brokerDescriptor = New SparkplugBrokerDescriptor("mqtt://localhost")
            brokerDescriptor.CustomPropertyValueDictionary.Add("NetSparkplugComponentProvider.MqttClientOptions.ProtocolVersion", 5)

            ' Create the host descriptor for the alternative method.
            Dim hostDescriptor2 = New SparkplugHostDescriptor(brokerDescriptor)

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, ' or hostDescriptor2, if you prefer the alternative method
                                                     "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged, AddressOf edgeNode_Mqtt5_SystemConnectionStateChanged

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

        Private Shared Sub edgeNode_Mqtt5_SystemConnectionStateChanged _
            (ByVal sender As Object, ByVal eventArgs As SparkplugConnectionStateChangedEventArgs)
            ' Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
        End Sub
    End Class
End Namespace
#End Region
