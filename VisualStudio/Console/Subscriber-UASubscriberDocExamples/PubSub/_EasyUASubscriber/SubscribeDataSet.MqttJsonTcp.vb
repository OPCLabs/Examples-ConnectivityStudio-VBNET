' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to all dataset messages on an OPC-UA PubSub connection with MQTT JSON mapping using
' TCP.
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

Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Engine
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub MqttJsonTcp()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            ' Default port with MQTT is 1883.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "mqtt://opcua-pubsub.demo-this.com"

            ' Specify the transport protocol mapping.
            ' The statement below isn't actually necessary, due to automatic message mapping recognition feature; see
            ' https://kb.opclabs.com/OPC_UA_PubSub_Automatic_Message_Mapping_Recognition for more details.
            pubSubConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson

            ' Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/json"

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_MqttJsonTcp

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

        Private Shared Sub subscriber_DataSetMessage_MqttJsonTcp(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
        'Dataset data: 2020-01-21T17:07:19.778,836,700,00; Good; Data; publisher=[String]31, class=eae79794-1af7-4f96-8401-4096cd1d8908, fields: 4
        '[BoolToggle, True {System.Boolean} @2020-01-21T16:07:19.778,836,700,00; Good]
        '[Int32, 482 {System.Int64} @2020-01-21T16:07:19.778,836,700,00; Good]
        '[Int32Fast, 2287 {System.Int64} @2020-01-21T16:07:19.778,836,700,00; Good]
        '[DateTime, 1/21/2020 5:07:19 PM {System.DateTime} @2020-01-21T16:07:19.778,836,700,00; Good]
        '
        'Dataset data: Good; Data; publisher=[String]32, fields: 4
        '[BoolToggle, True {System.Boolean}; Good]
        '[Int32, 482 {System.Int32}; Good]
        '[Int32Fast, 2287 {System.Int32}; Good]
        '[DateTime, 1/21/2020 5:07:19 PM {System.DateTime}; Good]
        '
        'Dataset data: Good; Data; publisher=[String]32, fields: 100
        '[Mass_0, 82 {System.Int64}; Good]
        '[Mass_1, 182 {System.Int64}; Good]
        '[Mass_2, 282 {System.Int64}; Good]
        '[Mass_3, 382 {System.Int64}; Good]
        '[Mass_4, 482 {System.Int64}; Good]
        '[Mass_5, 582 {System.Int64}; Good]
        '[Mass_6, 682 {System.Int64}; Good]
        '[Mass_7, 782 {System.Int64}; Good]
        '[Mass_8, 882 {System.Int64}; Good]
        '[Mass_9, 982 {System.Int64}; Good]
        '[Mass_10, 1082 {System.Int64}; Good]
        '...
    End Class
End Namespace

#End Region
