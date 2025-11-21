' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to obtain a new application certificate pack from the certificate manager (GDS), and store it for subsequent
' usage, with progress reporting.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Security.Cryptography.PkiCertificates
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.Application
Imports OpcLabs.EasyOpc.UA.Application.Extensions
Imports OpcLabs.EasyOpc.UA.Extensions
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples.Application._IEasyUAClientServerApplication
    Partial Friend Class ObtainNewCertificatePack
        Public Shared Sub Progress()

            ' Define which GDS we will work with.
            Dim gdsEndpointDescriptor As UAEndpointDescriptor =
                New UAEndpointDescriptor("opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer") _
                .WithUserNameIdentity("appadmin", "demo")

            ' Obtain the application interface.
            Dim application As EasyUAApplication = EasyUAApplication.Instance

            ' Display which application we are about to work with.
            Console.WriteLine("Application URI string: {0}",
                application.GetApplicationElement().ApplicationUriString)

            ' Obtain a new application certificate pack from the certificate manager (GDS), and store it for subsequent
            ' usage.
            Dim certificateDictionary As UANodeIdPkiCertificateDictionary
            Try
                certificateDictionary = application.ObtainNewCertificatePack(gdsEndpointDescriptor,
                    New Progress(Of String)(Sub(s) Console.WriteLine("Progress: {0}", s)))
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each pair As KeyValuePair(Of UANodeId, IPkiCertificate) In certificateDictionary
                Console.WriteLine()
                Console.WriteLine($"Certificate type Id: {pair.Key}")
                Console.WriteLine($"Certificate: {pair.Value}")
            Next
        End Sub
    End Class
End Namespace

#End Region
