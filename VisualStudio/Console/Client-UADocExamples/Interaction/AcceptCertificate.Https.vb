' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how in a console application, the user is asked to accept a server HTTPS certificate.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.Interaction
    Partial Friend Class AcceptCertificate
        Public Shared Sub Https()

            ' Do not implicitly trust any endpoint URLs. We want the user be asked explicitly.
            EasyUAClient.SharedParameters.EngineParameters.CertificateAcceptancePolicy.TrustedEndpointUrlStrings.Clear()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor = "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object.
            Dim client = New EasyUAClient()

            Dim attributeData As UAAttributeData
            Try
                ' Obtain attribute data.
                ' The component automatically triggers the necessary user interaction during the first operation.
                attributeData = client.Read(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results.
            Console.WriteLine("Value: {0}", attributeData.Value)
            Console.WriteLine("ServerTimestamp: {0}", attributeData.ServerTimestamp)
            Console.WriteLine("SourceTimestamp: {0}", attributeData.SourceTimestamp)
            Console.WriteLine("StatusCode: {0}", attributeData.StatusCode)
        End Sub
    End Class
End Namespace

#End Region
