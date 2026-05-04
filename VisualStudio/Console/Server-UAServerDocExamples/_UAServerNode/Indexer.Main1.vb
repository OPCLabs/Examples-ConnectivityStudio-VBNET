' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
' ReSharper disable UnusedVariable
#Region "Example"
' This example shows how sub-nodes of server nodes can be accessed by their node name using an indexer.
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
    Partial Friend Class Indexer
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define some nodes in the server.
            Dim constantDataVariable As UADataVariable = UADataVariable.CreateIn(server.Objects, "Constant").ConstantValue("abc")
            Dim nestedConstantDataVariable As UADataVariable = UADataVariable.CreateIn(constantDataVariable, "NestedConstant").ConstantValue(42)

            ' Get the nested constant data variable.
            Dim serverNode1 As UAServerNode = server.Objects("Constant")("NestedConstant")
            Console.WriteLine(serverNode1)
        End Sub
    End Class
End Namespace
#End Region

