' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain the effective node descriptors of server nodes (i.e. their node IDs and browse paths).
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._UAServerNode
    Partial Friend Class EffectiveNodeDescriptor
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define some nodes in the server.
            Dim dataVariable1 = UADataVariable.CreateIn(server.Objects, "DataVariable1")
            Dim folder1 = UAFolder.CreateIn(server.Objects, "Folder1")
            Dim dataVariable2 = UADataVariable.CreateIn(folder1, "DataVariable2")

            ' Display the node Ids (including the namespace URI).
            Console.WriteLine()
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.NodeId)
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.NodeId)
            Console.WriteLine(folder1.EffectiveNodeDescriptor.NodeId)
            Console.WriteLine(dataVariable2.EffectiveNodeDescriptor.NodeId)

            ' Display the browse paths.
            Console.WriteLine()
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.BrowsePath)
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.BrowsePath)
            Console.WriteLine(folder1.EffectiveNodeDescriptor.BrowsePath)
            Console.WriteLine(dataVariable2.EffectiveNodeDescriptor.BrowsePath)
        End Sub
    End Class
End Namespace
#End Region

