' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example demonstrates the loggable entries originating in the OPC-UA client engine and the EasyUAClient component.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Instrumentation
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class LogEntry
        Public Shared Sub Main1()

            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Hook static events
            AddHandler EasyUAClient.LogEntry, AddressOf EasyUAClientOnLogEntry

            Try
                ' Do something - invoke an OPC read, to trigger some loggable entries.
                Dim client = New EasyUAClient()
                Try
                    client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853")
                Catch uaException As UAException
                    Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                    Exit Sub
                End Try

                Console.WriteLine("Processing log entry events for 1 minute...")
                Threading.Thread.Sleep(60 * 1000)

                Console.WriteLine("Finished.")
            Finally
                ' Unhook static events
                RemoveHandler EasyUAClient.LogEntry, AddressOf EasyUAClientOnLogEntry
            End Try

        End Sub


        ' Event handler for the LogEntry event. It simply prints out the event.
        Private Shared Sub EasyUAClientOnLogEntry(ByVal sender As Object, ByVal logEntryEventArgs As LogEntryEventArgs)
            Console.WriteLine(logEntryEventArgs)
        End Sub
    End Class
End Namespace

#End Region
