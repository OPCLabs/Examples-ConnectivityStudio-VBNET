' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable InconsistentNaming
' ReSharper disable UnusedVariable
#Region "Example"
' This example shows how to create a Sparkplug edge node with a single metric, start and stop it, with authentication.
' to the broker. The username and password must match the ones used by the MQTT broker.
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
    Partial Class Start_Stop
        Public Shared Sub Authentication()
            ' The username and password can be specified in the broker descriptor, as below.
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim brokerDescriptor = New SparkplugBrokerDescriptor("mqtt://localhost") With
            {
                .UserName = "admin",
                .Password = "password"
            }
            Dim hostDescriptor = New SparkplugHostDescriptor(brokerDescriptor)

            ' Alternatively, if you do not mind, the MQTT broker URL can directly include the username and password,
            ' as below.
            ' Note that regardless of how the broker descriptor is constructed, the username and password are transferred
            ' in plain text on the wire, unless encryption is used (e.g., using TLS or WSS - see other examples).
            Dim hostDescriptor2 = New SparkplugHostDescriptor("mqtt://admin:password@localhost")

            ' Instantiate the edge node object and hook events.
            Dim edgeNode = New EasySparkplugEdgeNode(hostDescriptor, ' or hostDescriptor2, if you prefer the alternative method
                                                     "easyGroup", "easySparkplugDemo")
            AddHandler edgeNode.SystemConnectionStateChanged, AddressOf edgeNode_Authentication_SystemConnectionStateChanged

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

        Private Shared Sub edgeNode_Authentication_SystemConnectionStateChanged _
            (ByVal sender As Object, ByVal eventArgs As SparkplugConnectionStateChangedEventArgs)
            ' Display the new connection state (such as when the connection to the broker succeeds or fails).
            Console.WriteLine($"{NameOf(EasySparkplugEdgeNode.SystemConnectionStateChanged)}: {eventArgs}")
        End Sub
    End Class
End Namespace
#End Region
