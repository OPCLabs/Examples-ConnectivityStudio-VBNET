' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to find client or server applications that meet the specified filters, using the global discovery client.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.Gds
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.Gds._EasyUAGlobalDiscoveryClient
    Friend Class QueryApplications
        Public Shared Sub Main1()

            ' Instantiate the global discovery client object
            Dim globalDiscoveryClient = New EasyUAGlobalDiscoveryClient()

            ' Find all (client or server) applications registered in the GDS.
            Dim applicationDescriptionArray() As UAApplicationDescription = Nothing
            Try
                Dim lastCounterResetTime As DateTime
                Dim nextRecordId As Long
                globalDiscoveryClient.QueryApplications(
                    gdsEndpointDescriptor:="opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer",
                    startingRecordId:=0,
                    maximumRecordsToReturn:=0,
                    applicationName:="",
                    applicationUriString:="",
                    applicationTypes:=UAApplicationTypes.All,
                    productUriString:="",
                    serverCapabilities:=New String() {},
                    lastCounterResetTime:=lastCounterResetTime,
                    nextRecordId:=nextRecordId,
                    applications:=applicationDescriptionArray)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each applicationDescription As UAApplicationDescription In applicationDescriptionArray
                Console.WriteLine()
                Console.WriteLine("Application name: {0}", applicationDescription.ApplicationName)
                Console.WriteLine("Application type: {0}", applicationDescription.ApplicationType)
                Console.WriteLine("Application URI string: {0}", applicationDescription.ApplicationUriString)
                Console.WriteLine("Discovery URI strings: {0}", applicationDescription.DiscoveryUriStrings)
            Next applicationDescription
        End Sub
    End Class
End Namespace

#End Region
