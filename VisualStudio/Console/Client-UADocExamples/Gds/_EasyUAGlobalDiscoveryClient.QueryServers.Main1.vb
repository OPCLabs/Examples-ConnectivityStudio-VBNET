' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to find server applications that meet the specified filters, using the global discovery client.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.Gds
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.Gds._EasyUAGlobalDiscoveryClient
    Friend Class QueryServers
        Public Shared Sub Main1()

            ' Instantiate the global discovery client object
            Dim globalDiscoveryClient = New EasyUAGlobalDiscoveryClient()

            ' Find all servers registered in the GDS.
            Dim serverOnNetworkArray() As UAServerOnNetwork = Nothing
            Try
                Dim lastCounterResetTime As DateTime
                globalDiscoveryClient.QueryServers(
                    gdsEndpointDescriptor:="opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer",
                    startingRecordId:=0,
                    maximumRecordsToReturn:=0,
                    applicationName:="",
                    applicationUriString:="",
                    productUriString:="",
                    serverCapabilities:=New String() {},
                    lastCounterResetTime:=lastCounterResetTime,
                    serverOnNetworkArray:=serverOnNetworkArray)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each serverOnNetwork As UAServerOnNetwork In serverOnNetworkArray
                Console.WriteLine()
                Console.WriteLine("Server name: {0}", serverOnNetwork.ServerName)
                Console.WriteLine("Discovery URL string: {0}", serverOnNetwork.DiscoveryUrlString)
                Console.WriteLine("Server capabilities: {0}", serverOnNetwork.ServerCapabilities)
            Next serverOnNetwork
        End Sub
    End Class
End Namespace

#End Region
