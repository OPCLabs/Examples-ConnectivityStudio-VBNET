' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example demonstrates how to set the application name for the application instance certificate.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Instrumentation
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Application
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples._UAApplicationManifest
    Friend Class ApplicationName
        Public Shared Sub Main1()

            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Hook static events
            AddHandler EasyUAClient.LogEntry, AddressOf EasyUAClientOnLogEntry

            Try
                ' Set the application name, which determines the subject of the client certificate.
                ' Note that this only works once in each host process.
                EasyUAApplication.Instance.ApplicationParameters.ApplicationManifest.ApplicationName =
                    "QuickOPC - VBNet example application"

                ' Do something - invoke an OPC read, to trigger some loggable entries.
                ' If you are doing server development: Instantiate and start the server here, instead of invoking the client.
                Dim client = New EasyUAClient()
                Try
                    client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                Catch uaException As UAException
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                    Exit Sub
                End Try

                ' The certificate will be located or created in a directory similar to:
                ' C:\ProgramData\OPC Foundation\CertificateStores\MachineDefault\certs
                ' and its subject will be as given by the application name.

                Console.WriteLine("Processing log entry events for 10 seconds...")
                Threading.Thread.Sleep(10 * 1000)

                Console.WriteLine("Finished.")
            Finally
                ' Unhook static events
                RemoveHandler EasyUAClient.LogEntry, AddressOf EasyUAClientOnLogEntry
            End Try

        End Sub


        ' Event handler for the LogEntry event.
        ' Print the loggable entry containing client certificate parameters.
        Private Shared Sub EasyUAClientOnLogEntry(ByVal sender As Object, ByVal logEntryEventArgs As LogEntryEventArgs)
            If (logEntryEventArgs.EventId = 161) Then
                Console.WriteLine(logEntryEventArgs)
            End If
        End Sub
    End Class
End Namespace

#End Region
