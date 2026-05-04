' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to find all application's registrations in the GDS.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.Application
Imports OpcLabs.EasyOpc.UA.Application.Extensions
Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.Extensions
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples.Application._IEasyUAClientServerApplication
    Friend Class FindGdsRegistrations
        Public Shared Sub Main1()

            ' Define which GDS we will work with.
            Dim gdsEndpointDescriptor As UAEndpointDescriptor =
                New UAEndpointDescriptor("opc.tcp://opcua.demo-this.com:58810/GlobalDiscoveryServer") _
                .WithUserNameIdentity("appadmin", "demo")

            ' Obtain the application interface.
            Dim application As EasyUAApplication = EasyUAApplication.Instance

            ' Display which application we are about to work with.
            Console.WriteLine("Application URI string: {0}",
                application.GetApplicationElement().ApplicationUriString)

            ' Find all application's registrations in the GDS.
            Dim applicationElementDictionary As IReadOnlyDictionary(Of UANodeId, UAApplicationElement)
            Try
                applicationElementDictionary = application.FindGdsRegistrations(gdsEndpointDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each pair In applicationElementDictionary
                Console.WriteLine()
                Console.WriteLine("Application ID: {0}", pair.Key)

                Dim applicationElement = pair.Value
                Console.WriteLine("Application element: {0}", applicationElement)
                Console.WriteLine("  Application URI string: {0}", applicationElement.ApplicationUriString)
                Console.WriteLine("  Discovery URI strings: {0}", applicationElement.DiscoveryUriStrings)
                Console.WriteLine("  Product URI string: {0}", applicationElement.ProductUriString)
            Next pair
        End Sub
    End Class
End Namespace

#End Region
