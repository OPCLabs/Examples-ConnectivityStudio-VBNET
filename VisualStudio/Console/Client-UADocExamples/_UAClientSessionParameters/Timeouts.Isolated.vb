' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to set fairly short OPC UA timeouts (failures can thus occur).
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA

Namespace Global.UADocExamples._UAClientSessionParameters
    Partial Friend Class Timeouts
        Public Shared Sub Isolated()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and set timeouts
            Dim client = New EasyUAClient() With
                {
                    .Isolated = True,
                    .IsolatedParameters = New Engine.EasyUAClientAdaptableParameters() With
                        {
                            .SessionParameters = New Engine.UASmartClientSessionParameters() With
                                {
                                    .EndpointSelectionTimeout = 1500,
                                    .SessionConnectTimeout = 3000,
                                    .SessionTimeout = 3000,
                                    .SessionTimeoutDebug = 3000
                                }
                        }
                }

            Console.WriteLine("Subscribing...")
            ' The callback is a lambda expression the displays the value
            client.SubscribeDataChange(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853", 1000,
                Sub(sender, eventArgs)
                    If eventArgs.Succeeded Then
                        Console.WriteLine("Value: {0}", eventArgs.AttributeData.Value)
                    Else
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                    End If
                End Sub)

            Console.WriteLine("Processing data change events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace

#End Region
