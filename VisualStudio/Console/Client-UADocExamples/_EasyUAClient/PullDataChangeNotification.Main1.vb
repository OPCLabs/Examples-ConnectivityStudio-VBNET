' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to subscribe to changes of a single monitored item, pull events, and display each change.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class PullDataChangeNotification
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()
            ' In order to use event pull, you must set a non-zero queue capacity upfront.
            client.PullDataChangeNotificationQueueCapacity = 1000

            Console.WriteLine("Subscribing...")
            client.SubscribeDataChange(
                endpointDescriptor,
                "nsu=http://test.org/UA/Data/ ;i=10853",
                1000)   ' or "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"

            Console.WriteLine("Processing data change events for 1 minute...")
            Dim endTick As Integer = Environment.TickCount + 60 * 1000
            Do
                Dim eventArgs As EasyUADataChangeNotificationEventArgs = client.PullDataChangeNotification(2 * 1000)
                If Not eventArgs Is Nothing Then
                    ' Handle the notification event
                    Console.WriteLine(eventArgs)
                End If
            Loop While Environment.TickCount < endTick
        End Sub
    End Class
End Namespace

#End Region
