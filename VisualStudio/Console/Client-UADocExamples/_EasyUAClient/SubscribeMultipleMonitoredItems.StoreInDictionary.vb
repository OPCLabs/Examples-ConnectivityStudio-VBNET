' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to store current state of the subscribed monitored items in a dictionary.
' each change.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class SubscribeMultipleMonitoredItems
        Public Shared Sub StoreInDictionary()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object.
            Using client = New EasyUAClient()
                AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification_StoreInDictionary

                Console.WriteLine("Subscribing...")
                client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() {
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000),
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000),
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000)
                })

                Console.WriteLine("Processing monitored item changed events for 1 minute...")
                Dim startTickCount As Integer = Environment.TickCount
                Do
                    Thread.Sleep(5 * 1000)

                    ' Each 5 seconds, display the current state of the monitored items we have subscribed to.
                    SyncLock _serialize
                        Console.WriteLine()
                        For Each pair As KeyValuePair(Of UANodeDescriptor, UAAttributeDataResult) In _attributeDataResultDictionary
                            Dim nodeDescriptor As UANodeDescriptor = pair.Key
                            Dim attributeDataResult As UAAttributeDataResult = pair.Value
                            Console.WriteLine($"{nodeDescriptor}: {attributeDataResult}")
                        Next

                        ' The code above shows how you can process the complete contents of the dictionary. In other
                        ' scenarios, you may want to access just a specific entry in the dictionary. You can achieve that
                        ' by indexing the dictionary by the node descriptor of the monitored item you are interested in.
                    End SyncLock
                Loop While Environment.TickCount < startTickCount + 60 * 1000

                Console.WriteLine("Unsubscribing...")
            End Using

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub client_DataChangeNotification_StoreInDictionary(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs)
            SyncLock _serialize
                ' Convert the event arguments to a UAAttributeData result object, and store it in the dictionary under the
                ' key which is the node descriptor of the monitored item this data change notification is for.
                _attributeDataResultDictionary(e.Arguments.NodeDescriptor) = CType(e, UAAttributeDataResult)
            End SyncLock

            ' Display value
            If e.Succeeded Then
                Console.WriteLine("{0}: {1}", e.Arguments.NodeDescriptor, e.AttributeData.Value)
            Else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.NodeDescriptor, e.ErrorMessageBrief)
            End If
        End Sub

        ' Holds last known state of each subscribed monitores item.
        Private Shared ReadOnly _attributeDataResultDictionary As New Dictionary(Of UANodeDescriptor, UAAttributeDataResult)

        ' Synchronization object used to prevent simultaneous access to the dictionary.
        Private Shared ReadOnly _serialize As New Object
    End Class
End Namespace

#End Region
