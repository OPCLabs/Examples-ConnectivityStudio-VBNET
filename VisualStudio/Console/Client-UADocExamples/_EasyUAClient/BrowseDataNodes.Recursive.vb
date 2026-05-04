' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain "data nodes" under the "Objects" node, recursively.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class BrowseDataNodes
        Public Shared Sub Recursive()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()
            Try
                BrowseFromNode(client, endpointDescriptor, UAObjectIds.ObjectsFolder, level:=0)
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}")
                Exit Sub
            End Try
        End Sub

        Private Shared Sub BrowseFromNode(client As EasyUAClient, endPointDescriptor As UAEndpointDescriptor, parentNodeDescriptor As UANodeDescriptor, level As Integer)
            Debug.Assert(client IsNot Nothing)
            Debug.Assert(endPointDescriptor IsNot Nothing)
            Debug.Assert(parentNodeDescriptor IsNot Nothing)

            ' Obtain all node elements under parentNodeDescriptor
            Dim nodeElementCollection As UANodeElementCollection =
                client.BrowseDataNodes(endPointDescriptor, parentNodeDescriptor)
            ' Remark: BrowseDataNodes(...) may throw UAException; we handle it in the calling method.

            For Each nodeElement As UANodeElement In nodeElementCollection
                Debug.Assert(nodeElement IsNot Nothing)

                Console.Write(New String(" "c, level * 2))
                Console.WriteLine(nodeElement)

                ' Browse recursively into the node.
                ' The UANodeElement has an implicit conversion to UANodeDescriptor.
                BrowseFromNode(client, endPointDescriptor, nodeElement, level + 1)

                ' Note that the number of nodes you obtain through recursive browsing may be very large, or even infinite.
                ' Production code should contain appropriate safeguards for these cases.
            Next nodeElement
        End Sub
    End Class
End Namespace

#End Region
