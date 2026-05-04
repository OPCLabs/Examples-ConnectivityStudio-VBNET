' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to changes of multiple monitored items
' and display each change, identifying the different subscriptions by an
' object.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class SubscribeMultipleMonitoredItems
        Class CustomObject
            Public Sub New(ByVal name As String)
                _Name = name
            End Sub
            Public ReadOnly Property Name As String
                Get
                    Return _Name
                End Get
            End Property
            Private ReadOnly _Name As String
        End Class

        Public Shared Sub StateAsObject()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.DataChangeNotification, AddressOf ClientOnDataChangeNotification_StateAsObject

            Console.WriteLine("Subscribing...")
            Dim handleArray() As Integer = client.SubscribeMultipleMonitoredItems(New EasyUAMonitoredItemArguments() _
                { _
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, _
                        "nsu=http://test.org/UA/Data/ ;i=10845", 1000) _
                         With {.State = New CustomObject("First")}, _
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, _
                        "nsu=http://test.org/UA/Data/ ;i=10853", 1000) _
                         With {.State = New CustomObject("Second")}, _
                    New EasyUAMonitoredItemArguments(Nothing, endpointDescriptor, _
                        "nsu=http://test.org/UA/Data/ ;i=10855", 1000) _
                        With {.State = New CustomObject("Third")} _
                } _
            ) ' A custom object that corresponds to the subscription

            For i As Integer = 0 To handleArray.Length - 1
                Console.WriteLine("handleArray[{0}]: {1}", i, handleArray(i))
            Next i

            Console.WriteLine("Processing monitored item changed events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub ClientOnDataChangeNotification_StateAsObject(ByVal sender As Object, ByVal eventArgs As EasyUADataChangeNotificationEventArgs)
            ' Obtain the custom object we have passed in.
            Dim stateAsObject As CustomObject = CType(eventArgs.Arguments.State, CustomObject)

            ' Display the data
            If eventArgs.Succeeded Then
                Console.WriteLine("{0}: {1}", stateAsObject.Name, eventArgs.AttributeData)
            Else
                Console.WriteLine("{0} *** Failure: {1}", stateAsObject.Name, eventArgs.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace

#End Region
