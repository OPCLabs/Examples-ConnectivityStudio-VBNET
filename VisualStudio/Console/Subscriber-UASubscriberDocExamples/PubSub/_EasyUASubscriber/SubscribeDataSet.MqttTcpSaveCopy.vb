' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with MQTT (JSON or UADP
' mapping) using TCP, and store a copy of all received messages into a file system.
'
' A related example (SubscribeDataSet.MqttFromFileStorage) shows hot the captured messages can be replayed from the file
' system, e.g. for troubleshooting.
'
' The following package needs to be referenced in your project (or otherwise made available) for the MQTT transport to 
' work.
' - OpcLabs.MqttNet
' Refer to the documentation for more information.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub MqttTcpSaveCopy()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            ' Default port with MQTT is 1883.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com"
            ' Configure the PubSub connection so that a copy of the received messages is stored into the file system, under
            ' the specified "root" directory.
            pubSubConnectionDescriptor.CustomPropertyValueDictionary(
                New UAQualifiedName("{OpcLabs}", "MqttFileCopyReceivedRoot")) = "C:\MqttReceived"

            ' Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/#"

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_MqttTcpSaveCopy

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(subscribeDataSetArguments)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub

        Private Shared Sub subscriber_DataSetMessage_MqttTcpSaveCopy(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
            ' Display the dataset.
            If e.Succeeded Then
                ' An event with null DataSetData just indicates a successful connection.
                If e.DataSetData IsNot Nothing Then
                    Console.WriteLine()
                    Console.WriteLine($"Dataset data: {e.DataSetData}")
                    For Each pair As KeyValuePair(Of String, UADataSetFieldData) In e.DataSetData.FieldDataDictionary
                        Console.WriteLine(pair)
                    Next
                End If
            Else
                Console.WriteLine()
                Console.WriteLine($"*** Failure: {e.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace

#End Region
