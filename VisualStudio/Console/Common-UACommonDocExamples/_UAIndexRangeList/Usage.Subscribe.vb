' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to range of values from an array.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples._UAIndexRangeList
    Partial Friend Class Usage
        Public Shared Sub Subscribe()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            Console.WriteLine("Subscribing to range...")
            Dim attributeArguments = New UAAttributeArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10933") With
                {
                    .IndexRangeList = UAIndexRangeList.OneDimension(2, 4)
                }
            Dim monitoredItemArguments = New UAMonitoredItemArguments(attributeArguments, monitoringParameters:=1000)
            ' The callback is a lambda expression the displays the value
            client.SubscribeMonitoredItem(monitoredItemArguments,
                Sub(sender, eventArgs)
                    If eventArgs.Succeeded Then
                        Dim arrayValue = CType(eventArgs.AttributeData.Value, Int32())
                        If (arrayValue IsNot Nothing) Then
                            Console.WriteLine($"Value: {{{String.Join(",", arrayValue)}}}")
                        End If
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
