' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to set parameters specific to JSON message mapping.
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

Imports Opc.Ua
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
Imports OpcLabs.EasyOpc.UA.PubSub.Engine
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub MappingParameters()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            ' Default port with MQTT is 1883.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com"
            ' Specify the transport protocol mapping.
            ' The statement below isn't actually necessary, due to automatic message mapping recognition feature; see
            ' https://kb.opclabs.com/OPC_UA_PubSub_Automatic_Message_Mapping_Recognition for more details.
            pubSubConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson

            ' Set a custom property on the PubSub connection that influences how the JSON parsing works. 
            ' We are instructing the message parser to turn off the automatic recognition of message format.
            ' For more details, see https://kb.opclabs.com/OPC_UA_PubSub_JSON_mapping_component .
            pubSubConnectionDescriptor.CustomPropertyValueDictionary(New UAQualifiedName("{OpcLabs}",
                "OpcLabs.EasyOpc.UA.Toolkit.PubSub.Sdk.JsonReceiveMessageMapping.MessageParsingParameters.AutoRecognizeMessageFormat")) =
                False

            ' Define the arguments for subscribing to the dataset.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/json"
            ' We must set the DataSetFieldContentMask when the format auto-recognition is turned off.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.DataSetFieldContentMask = UADataSetFieldContentMask.RawData
            ' We must set the DataSetMessageContentMask when the format auto-recognition is turned off.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.JsonDataSetReaderMessageParameters.DataSetMessageContentMask =
                UAJsonDataSetMessageContentMask.DataSetWriterId Or
                UAJsonDataSetMessageContentMask.SequenceNumber Or
                UAJsonDataSetMessageContentMask.Status
            ' We must set the NetworkMessageContentMask when the format auto-recognition is turned off.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.JsonDataSetReaderMessageParameters.NetworkMessageContentMask =
                UAJsonNetworkMessageContentMask.NetworkMessageHeader Or
                UAJsonNetworkMessageContentMask.DataSetMessageHeader Or
                UAJsonNetworkMessageContentMask.PublisherId
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = 1

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_MappingParameters

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

        Private Shared Sub subscriber_DataSetMessage_MappingParameters(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
                Console.WriteLine($"*** Failure: {e.ErrorMessage}")
            End If
        End Sub

        ' Example output:
        '
        'Subscribing...
        'Processing dataset message events for 20 seconds...
        '
        'Dataset data: Good; Data; publisher=[String]30, writer=1, fields: 4
        '[BoolToggle, True {System.Boolean}; Good]
        '[Int32, 9034 {System.Int64}; Good]
        '[Int32Fast, 1751 {System.Int64}; Good]
        '[DateTime, 1/30/2020 5:23:11 PM {System.DateTime}; Good]
        '
        'Dataset data: Good; Data; publisher=[String]30, writer=1, fields: 4
        '[BoolToggle, True {System.Boolean}; Good]
        '[Int32, 9036 {System.Int64}; Good]
        '[Int32Fast, 2526 {System.Int64}; Good]
        '[DateTime, 1/30/2020 5:23:13 PM {System.DateTime}; Good]
        '...
    End Class
End Namespace

#End Region
