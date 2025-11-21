' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to configure the namespace URI of the custom nodes, using a Uri object.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class ObjectsNamespaceUri
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Set custom value for the namespace URI of our nodes under the Objects folder, using a Uri object.
            server.ObjectsNamespaceUri = New Uri("http://mynamespace.example")

            ' Create some data variable and a folder in the Objects folder.
            Dim dataVariable1 = UADataVariable.CreateIn(server.Objects, "DataVariable1")
            Dim folder1 = UAFolder.CreateIn(server.Objects, "Folder1")

            ' Display the node Ids (including the namespace URI).
            Console.WriteLine(server.Objects.EffectiveNodeDescriptor.NodeId)
            Console.WriteLine(dataVariable1.EffectiveNodeDescriptor.NodeId)
            Console.WriteLine(folder1.EffectiveNodeDescriptor.NodeId)
        End Sub
    End Class
End Namespace
#End Region

