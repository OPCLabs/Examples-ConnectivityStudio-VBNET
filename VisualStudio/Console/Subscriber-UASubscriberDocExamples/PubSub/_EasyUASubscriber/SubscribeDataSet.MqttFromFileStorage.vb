' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to MQTT dataset messages stored in a file system. This can be used e.g. for
' troubleshooting.
'
' A related example (SubscribeDataSet.MqttTcpSaveCopy) shows how to capture the MQTT messages into the file system.
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
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub MqttFromFileStorage()

            ' Define the PubSub connection we will work with. By specifying a file (file URI) with the directory path, MQTT
            ' messages will be provided from the file storage.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "C:\MqttReceived"

            ' Define the arguments for subscribing to the dataset, specifying the MQTT topic name.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/json/#"

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_MqttFromFileStorage

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

        Private Shared Sub subscriber_DataSetMessage_MqttFromFileStorage(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
    End Class
End Namespace

#End Region
