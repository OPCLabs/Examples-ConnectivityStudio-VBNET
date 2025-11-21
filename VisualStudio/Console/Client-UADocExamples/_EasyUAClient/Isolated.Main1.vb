' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to create and use two isolated client objects, resulting in two separate connections to the target
' OPC UA server.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Windows.Forms.AxHost
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Class Isolated
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client objects and make them isolated
            Dim client1 = New EasyUAClient() With {.Isolated = True}
            Dim client2 = New EasyUAClient() With {.Isolated = True}

            ' The callback is a local method the displays the value
            Dim dataChangeCallback = Sub(ByVal sender As Object, ByVal eventArgs As EasyUADataChangeNotificationEventArgs)
                                         Debug.Assert(eventArgs IsNot Nothing)

                                         Dim displayPrefix As String = $"[{eventArgs.Arguments.State}]"
                                         If eventArgs.Succeeded Then
                                             Debug.Assert(eventArgs.AttributeData IsNot Nothing)
                                             Console.WriteLine($"{displayPrefix} {eventArgs.AttributeData}")
                                         Else
                                             Console.WriteLine($"{displayPrefix} *** Failure: {eventArgs.ErrorMessageBrief}")
                                         End If
                                     End Sub

            Console.WriteLine("Subscribing...")
            client1.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000,
                dataChangeCallback, state:=1)
            client2.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000,
                dataChangeCallback, state:=2)

            Console.WriteLine("Processing data change events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client1.UnsubscribeAllMonitoredItems()
            client2.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace

#End Region
