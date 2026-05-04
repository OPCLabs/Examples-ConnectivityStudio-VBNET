' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain information about OPC UA servers from the Global Discovery Server (GDS).
' The result is hierarchical, i.e. each server is returned in one element, and the element contains all its discovery URLs.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class DiscoverGlobalServers
        Public Shared Sub Hierarchical()
            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain collection of application elements
            Dim discoveryElementCollection As UADiscoveryElementCollection
            Try
                discoveryElementCollection = client.DiscoverGlobalServers( _
                    "opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer",
                    flat:=False)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each discoveryElement As UADiscoveryElement In discoveryElementCollection
                Console.WriteLine()
                Console.WriteLine("Server name: {0}", discoveryElement.ServerName)
                Console.WriteLine("Discovery URI strings:")
                For Each discoveryUriString As String In discoveryElement.DiscoveryUriStrings
                    Console.WriteLine("  {0}", discoveryUriString)
                Next discoveryUriString
                Console.WriteLine("Server capabilities: {0}", discoveryElement.ServerCapabilities)
                Console.WriteLine("Application URI string: {0}", discoveryElement.ApplicationUriString)
                Console.WriteLine("Product URI string: {0}", discoveryElement.ProductUriString)
            Next discoveryElement
        End Sub
    End Class
End Namespace

#End Region
