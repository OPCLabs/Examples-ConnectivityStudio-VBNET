' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows different ways of constructing OPC UA node IDs.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Parsing
Imports OpcLabs.EasyOpc.UA.AddressSpace.Parsing.Extensions
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard

Namespace Global.UACommonDocExamples._UANodeId
    Friend Class _Construction
        Public Shared Sub Main1()

            ' A node ID can be specified in string form (so-called expanded text). 
            ' The code below specifies a namespace URI (nsu=...), and an integer identifier (i=...).
            Dim nodeId1 = New UANodeId("nsu=http://test.org/UA/Data/ ;i=10853")
            Console.WriteLine(nodeId1)


            ' Similarly, with a string identifier (s=...).
            Dim nodeId2 = New UANodeId("nsu=http://test.org/UA/Data/ ;s=someIdentifier")
            Console.WriteLine(nodeId2)


            ' Actually, "s=" can be omitted (not recommended, though)
            Dim nodeId3 = New UANodeId("nsu=http://test.org/UA/Data/ ;someIdentifier")
            Console.WriteLine(nodeId3)


            ' Similarly, with a GUID identifier (g=...)
            Dim nodeId4 = New UANodeId("nsu=http://test.org/UA/Data/ ;g=BAEAF004-1E43-4A06-9EF0-E52010D5CD10")
            Console.WriteLine(nodeId4)


            ' Similarly, with an opaque identifier (b=..., in Base64 encoding).
            Dim nodeId5 = New UANodeId("nsu=http://test.org/UA/Data/ ;b=AP8=")
            Console.WriteLine(nodeId5)


            ' Namespace index can be used instead of namespace URI. The server is allowed to change the namespace 
            ' indices between sessions, and for this reason, you should avoid the use of namespace indices, and
            ' rather use the namespace URIs whenever possible.
            Dim nodeId6 = New UANodeId("ns=2;i=10853")
            Console.WriteLine(nodeId6)


            ' Namespace index can be also specified together with namespace URI. This is still safe, but may be 
            ' a bit quicker to perform, because the client can just verify the namespace URI instead of looking 
            ' it up.
            Dim nodeId7 = New UANodeId("nsu=http://test.org/UA/Data/ ;ns=2;i=10853")
            Console.WriteLine(nodeId7)


            ' When neither namespace URI nor namespace index are given, the node ID is assumed to be in namespace
            ' with index 0 and URI "http://opcfoundation.org/UA/", which is reserved by OPC UA standard. There are 
            ' many standard nodes that live in this reserved namespace, but no nodes specific to your servers will 
            ' be in the reserved namespace, and hence the need to specify the namespace with server-specific nodes.
            Dim nodeId8 = New UANodeId("i=2254")
            Console.WriteLine(nodeId8)


            ' If you attempt to pass in a string that does not conform to the syntax rules, 
            ' a UANodeIdFormatException is thrown.
            Try
                Dim nodeId9 = New UANodeId("nsu=http://test.org/UA/Data/ ;i=notAnInteger")
                Console.WriteLine(nodeId9)
            Catch nodeIdFormatException As UANodeIdFormatException
                Console.WriteLine(nodeIdFormatException.Message)
            End Try


            ' There is a parser object that can be used to parse the expanded textx of node IDs. 
            Dim nodeIdParser10 = New UANodeIdParser()
            Dim nodeId10 = nodeIdParser10.Parse("nsu=http://test.org/UA/Data/ ;i=10853")
            Console.WriteLine(nodeId10)


            ' The parser can be used if you want to parse the expanded text of the node ID but do not want 
            ' exceptions be thrown.
            Dim nodeIdParser11 = New UANodeIdParser()
            Dim nodeId11 As UANodeId = Nothing
            Dim stringParsingError As IStringParsingError =
                    nodeIdParser11.TryParse("nsu=http://test.org/UA/Data/ ;i=notAnInteger", nodeId11)
            If stringParsingError Is Nothing Then
                Console.WriteLine(nodeId11)
            Else
                Console.WriteLine(stringParsingError.Message)
            End If


            ' You can also use the parser if you have node IDs where you want the default namespace be different 
            ' from the standard "http://opcfoundation.org/UA/".
            Dim nodeIdParser12 = New UANodeIdParser("http://test.org/UA/Data/")
            Dim nodeId12 = nodeIdParser12.Parse("i=10853")
            Console.WriteLine(nodeId12)


            ' The namespace URI string (or the namespace index, or both) and the identifier can be passed to the
            ' constructor separately.
            Dim nodeId13 = New UANodeId("http://test.org/UA/Data/", 10853)
            Console.WriteLine(nodeId13)


            ' You can create a "null" node ID. Such node ID does not actually identify any valid node in OPC UA, but 
            ' is useful as a placeholder or as a starting point for further modifications of its properties.
            Dim nodeId14 = New UANodeId()
            Console.WriteLine(nodeId14)


            ' Properties of a node ID can be modified individually. The advantage of this approach is that you do 
            ' not have to care about syntax of the node ID expanded text.
            Dim nodeId15 = New UANodeId()
            nodeId15.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId15.Identifier = 10853
            Console.WriteLine(nodeId15)


            ' The same as above, but using an object initializer list.
            Dim nodeId16 = New UANodeId With
            {
                .NamespaceUriString = "http://test.org/UA/Data/",
                .Identifier = 10853
            }
            Console.WriteLine(nodeId16)


            ' If you know the type of the identifier upfront, it is safer to use typed properties that correspond 
            ' to specific types of identifier. Here, with an integer identifier.
            Dim nodeId17 = New UANodeId()
            nodeId17.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId17.NumericIdentifier = 10853
            Console.WriteLine(nodeId17)


            ' Similarly, with a string identifier.
            Dim nodeId18 = New UANodeId()
            nodeId18.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId18.StringIdentifier = "someIdentifier"
            Console.WriteLine(nodeId18)


            ' Similarly, with a GUID identifier.
            Dim nodeId19 = New UANodeId()
            nodeId19.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId19.GuidIdentifier = Guid.Parse("BAEAF004-1E43-4A06-9EF0-E52010D5CD10")
            Console.WriteLine(nodeId19)


            ' If you have GUID in its string form, the node ID object can parse it for you.
            Dim nodeId20 = New UANodeId()
            nodeId20.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId20.GuidIdentifierString = "BAEAF004-1E43-4A06-9EF0-E52010D5CD10"
            Console.WriteLine(nodeId20)


            ' And, with an opaque identifier.
            Dim nodeId21 = New UANodeId()
            nodeId21.NamespaceUriString = "http://test.org/UA/Data/"
            nodeId21.OpaqueIdentifier = {&H0, &HFF}
            Console.WriteLine(nodeId21)


            ' Assigning an expanded text to a node ID parses the value being assigned and sets all corresponding
            ' properties accordingly.
            Dim nodeId22 = New UANodeId()
            nodeId22.ExpandedText = "nsu=http://test.org/UA/Data/ ;i=10853"
            Console.WriteLine(nodeId22)


            ' There is an implicit conversion from a string (representing the expanded text) to a node ID.
            ' You can therefore use the expanded text (string) in place of any node ID object directly.
            Dim nodeId23 = "nsu=http://test.org/UA/Data/ ;i=10853"
            Console.WriteLine(nodeId23)


            ' There is a copy constructor as well, creating a clone of an existing node ID.
            Dim nodeId24a = New UANodeId("nsu=http://test.org/UA/Data/ ;i=10853")
            Console.WriteLine(nodeId24a)
            Dim nodeId24b = New UANodeId(nodeId24a)
            Console.WriteLine(nodeId24b)


            ' We have provided static classes with properties that correspond to all standard nodes specified by 
            ' OPC UA. You can simply refer to these node IDs in your code.
            ' The class names are UADataTypeIds, UAMethodIds, UAObjectIds, UAObjectTypeIds, UAReferenceTypeIds, 
            ' UAVariableIds and UAVariableTypeIds.
            Dim nodeId25 = UAObjectIds.TypesFolder
            Console.WriteLine(nodeId25)


            ' You can also refer to any standard node using its name (in a string form).
            ' Note that assigning a non-existing standard name is not allowed, and throws ArgumentException.
            Dim nodeId26 = New UANodeId()
            nodeId26.StandardName = "TypesFolder"
            Console.WriteLine(nodeId26)


            ' When you browse for nodes in the OPC UA server, every returned node element contains a node ID that
            ' you can use further.
            Dim client27 = New EasyUAClient()
            Dim nodeElementCollection27 = client27.Browse(
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                UAObjectIds.Server,
                New UABrowseParameters(UANodeClass.All, {UAReferenceTypeIds.References}))
            Dim nodeId27 = nodeElementCollection27(0).NodeId
            Console.WriteLine(nodeId27)


            ' As above, but using a constructor that takes a node element as an input.
            Dim client28 = New EasyUAClient()
            Dim nodeElementCollection28 = client28.Browse(
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                UAObjectIds.Server,
                New UABrowseParameters(UANodeClass.All, {UAReferenceTypeIds.References}))
            Dim nodeId28 = New UANodeId(nodeElementCollection28(0))
            Console.WriteLine(nodeId28)


            ' Or, there is an explicit conversion from a node element as well.
            Dim client29 = New EasyUAClient()
            Dim nodeElementCollection29 = client29.Browse(
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer",
                UAObjectIds.Server,
                New UABrowseParameters(UANodeClass.All, {UAReferenceTypeIds.References}))
            Dim nodeId29 = CType(nodeElementCollection29(0), UANodeId)
            Console.WriteLine(nodeId29)
        End Sub
    End Class
End Namespace

#End Region
