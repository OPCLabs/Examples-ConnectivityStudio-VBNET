' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable UnusedVariable
#Region "Example"
' This example shows different ways of constructing the EasySparkplugEdgeNode object.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.EdgeNode._EasySparkplugEdgeNode
    Class Construction
        Public Shared Sub Main1()
            ' The toolkit provides a ready-made shared instance of the edge node object which you can use without even
            ' having to construct it. Not recommended for use in library code, because it is a shared instance, and its
            ' usage may therefore conflict with other code using the same instance.
            Dim edgeNode0 = EasySparkplugEdgeNode.SharedInstance


            ' The simplest way to construct the edge node object is to use the default constructor. The edge node will
            ' connect to the default broker URL "mqtt://localhost". Group ID is "easyGroup", edge node ID and primary host
            ' ID will be auto-generated.
            Dim edgeNode1 = New EasySparkplugEdgeNode()


            ' The edge node object can be constructed with a specific broker URL string passed as an argument to the
            ' constructor. This relies on the implicit conversion from string to SparkplugBrokerDescriptor.
            Dim edgeNode2 = New EasySparkplugEdgeNode("mqtt://localhost:1883")


            ' The broker URL can also be specified using the Uri object.
            Dim edgeNode3 = New EasySparkplugEdgeNode(New Uri("mqtt://localhost:1883"))


            ' You can construct the edge node object with a specific broker descriptor, which allows you to set all its
            ' parameters;
            Dim edgeNode4 = New EasySparkplugEdgeNode(
                New SparkplugBrokerDescriptor With
                {
                    .Host = "localhost",
                    .Password = "password",
                    .Port = 1883,
                    .UserName = "admin"
                })


            ' The sparkplug group ID and edge node ID can be specified as additional arguments to the constructor.
            Dim edgeNode5 = New EasySparkplugEdgeNode("mqtt://localhost:1883", "myGroup", "myEdgeNode")


            ' The primary host ID of the application can also be specified, using a different constructor overload (when
            ' not specified, i.e. left empty, the component will not use the primary host application logic).
            Dim edgeNode6 = New EasySparkplugEdgeNode("mqtt://localhost:1883", "myPrimaryHost", "myGroup", "myEdgeNode")


            ' You do not have to specify everything in the constructor. The basic properties can be set later - but before
            ' the edge node is started.
            Dim edgeNode7 = New EasySparkplugEdgeNode()
            edgeNode7.SystemDescriptor = New SparkplugSystemDescriptor("mqtt://localhost:1883")
            edgeNode7.GroupId = "myGroup"
            edgeNode7.EdgeNodeId = "myEdgeNode"


            ' If the language supports property initializers (such as C# or VB.NET), the above code can be written more
            ' concisely.
            Dim edgeNode8 = New EasySparkplugEdgeNode With
            {
                .GroupId = "myGroup",
                .EdgeNodeId = "myEdgeNode",
                .SystemDescriptor = New SparkplugSystemDescriptor("mqtt://localhost:1883")
            }


            ' For more advanced scenarios, a SparkplugSystemDescriptor can be passed to the constructor instead of the 
            ' SparkplugBrokerDescriptor. In the example below, this allows you to specify the Sparkplug version.
            Dim edgeNode9 = New EasySparkplugEdgeNode(
                New SparkplugSystemDescriptor("mqtt://localhost:1883", SparkplugVersions.PayloadA),
                "myPrimaryHost",
                "myGroup",
                "myEdgeNode")


            ' If the language supports collection initializers (such as C# or VB.NET), the edge node object can be
            ' constructed with its metrics (the contents of the Metrics collection), in a single statement.
            Dim edgeNode10 = New EasySparkplugEdgeNode("myPrimaryHost", "myGroup", "myEdgeNode") From
            {
                New SparkplugMetric("Constant1").ConstantValue(42),
                New SparkplugMetric("Constant2").ConstantValue("abc")
            }
        End Sub
    End Class
End Namespace
#End Region
