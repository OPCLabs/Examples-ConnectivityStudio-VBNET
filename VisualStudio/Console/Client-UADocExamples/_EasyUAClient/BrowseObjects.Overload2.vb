' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain objects under the "Server" node in the address space.
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
    Friend Class BrowseObjects
        Public Shared Sub Overload2()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain objects under "Server" node
            Dim nodeElementCollection As UANodeElementCollection
            Try
                nodeElementCollection = client.BrowseObjects(endpointDescriptor, UAObjectIds.Server)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each nodeElement As UANodeElement In nodeElementCollection
                Console.WriteLine()
                Console.WriteLine("nodeElement.NodeId: {0}", nodeElement.NodeId)
                Console.WriteLine("nodeElement.DisplayName: {0}", nodeElement.DisplayName)
            Next nodeElement

            ' Example output:
            '
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/;i=2268
            'nodeElement.DisplayName: ServerCapabilities
            '
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/;i=2274
            'nodeElement.DisplayName: ServerDiagnostics
            '
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/;i=2295
            'nodeElement.DisplayName: VendorServerInfo
            '
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/;i=2296
            'nodeElement.DisplayName: ServerRedundancy
        End Sub
    End Class
End Namespace

#End Region
