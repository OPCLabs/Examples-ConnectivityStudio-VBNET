' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain information about OPC UA servers available on the network.
' The result is flat, i.e. each discovery URL is returned in separate element, with possible repetition of the servers.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class DiscoverNetworkServers
        Public Shared Sub Main1()
            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain collection of application elements
            Dim discoveryElementCollection As UADiscoveryElementCollection
            Try
                discoveryElementCollection = client.DiscoverNetworkServers("opcua.demo-this.com")
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each discoveryElement As UADiscoveryElement In discoveryElementCollection
                Console.WriteLine()
                Console.WriteLine("Server name: {0}", discoveryElement.ServerName)
                Console.WriteLine("Discovery URI string: {0}", discoveryElement.DiscoveryUriString)
                Console.WriteLine("Server capabilities: {0}", discoveryElement.ServerCapabilities)
                Console.WriteLine("Application URI string: {0}", discoveryElement.ApplicationUriString)
                Console.WriteLine("Product URI string: {0}", discoveryElement.ProductUriString)
            Next discoveryElement
        End Sub
    End Class
End Namespace

#End Region
