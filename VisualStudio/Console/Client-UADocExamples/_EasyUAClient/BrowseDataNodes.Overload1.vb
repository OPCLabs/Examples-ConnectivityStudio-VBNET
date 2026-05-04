' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain "data nodes" (objects, variables and properties) under the "Objects" node in the address
' space.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class BrowseDataNodes
        Public Shared Sub Overload1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain data nodes under "Objects" node
            Dim nodeElementCollection As UANodeElementCollection
            Try
                nodeElementCollection = client.BrowseDataNodes(endpointDescriptor)
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
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/;i=2253
            'nodeElement.DisplayName: Server
            '
            'nodeElement.NodeId: nsu=http://test.org/UA/Data/ ;i=10157
            'nodeElement.DisplayName: Data
            '
            'nodeElement.NodeId: nsu=http://opcfoundation.org/UA/Boiler/;i=1240
            'nodeElement.DisplayName: Boilers
            '
            'nodeElement.NodeId: nsu=http://samples.org/UA/memorybuffer;i=1025
            'nodeElement.DisplayName: MemoryBuffers            
        End Sub
    End Class
End Namespace

#End Region
