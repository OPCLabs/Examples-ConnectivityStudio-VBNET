' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain application URLs of all OPC Unified Architecture servers, using specified discovery URI strings.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Discovery
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class FindLocalApplications
        Public Shared Sub Main1()
            Dim discoveryUriStrings() As String =
            { _
                "opc.tcp://opcua.demo-this.com:4840/UADiscovery", _
                "http://opcua.demo-this.com/UADiscovery/Default.svc", _
                "http://opcua.demo-this.com:52601/UADiscovery" _
            }

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain collection of application elements
            Dim discoveryElementCollection As UADiscoveryElementCollection
            Try
                discoveryElementCollection = client.FindLocalApplications(discoveryUriStrings, UAApplicationTypes.Server)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display results
            For Each discoveryElement As UADiscoveryElement In discoveryElementCollection
                Console.WriteLine("discoveryElementCollection[""{0}""].ApplicationUriString: {1}", _
                    discoveryElement.DiscoveryUriString, discoveryElement.ApplicationUriString)
            Next discoveryElement

            ' Example output:
            'discoveryElementCollection["http://opcua.demo-this.com:62543/Quickstarts/AlarmConditionServer"].ApplicationUriString: urn:opcua.demo-this.com:Quickstart Alarm Condition Server
            'discoveryElementCollection["opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer"].ApplicationUriString: urn:opcua.demo-this.com:Quickstart Alarm Condition Server
            'discoveryElementCollection["opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"].ApplicationUriString: urn:opcua.demo-this.com:UA Sample Server
            'discoveryElementCollection["http://opcua.demo-this.com:51211/UA/SampleServer"].ApplicationUriString: urn:opcua.demo-this.com:UA Sample Server
            'discoveryElementCollection["https://opcua.demo-this.com:51212/UA/SampleServer/"].ApplicationUriString: urn:opcua.demo-this.com:UA Sample Server
            'discoveryElementCollection["opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"].ApplicationUriString: urn:Test-PC:UA Sample Server
        End Sub
    End Class
End Namespace

#End Region
