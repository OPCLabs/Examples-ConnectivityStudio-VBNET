' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
' ReSharper disable UnusedVariable
#Region "Example"
' This example shows how to search for nodes in the server by their node Id.
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
    Partial Friend Class FindServerNode
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define some nodes in the server.
            Dim constantDataVariable As UADataVariable = UADataVariable.CreateIn(server.Objects, "Constant").ConstantValue("abc")
            Dim nestedConstantDataVariable As UADataVariable = UADataVariable.CreateIn(constantDataVariable, "NestedConstant").ConstantValue(42)

            ' Try to find the nested constant data variable. It will be found.
            Dim serverNode1 As UAServerNode = server.FindServerNode("nsu=http://opclabs.com/OpcUA/Custom/Objects ;s=Constant.NestedConstant")
            Console.WriteLine(serverNode1)

            ' Try to find an unknown server node. A null reference will be returned.
            Dim serverNode2 As UAServerNode = server.FindServerNode("nsu=http://opclabs.com/OpcUA/Custom/Objects ;s=Unknown")
            Console.WriteLine(serverNode2)
        End Sub
    End Class
End Namespace
#End Region

