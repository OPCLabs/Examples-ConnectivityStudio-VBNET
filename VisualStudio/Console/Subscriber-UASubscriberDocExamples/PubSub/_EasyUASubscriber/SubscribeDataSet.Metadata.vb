' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to dataset messages with RawData field encoding, specifying the metadata necessary
' for their decoding directly in the code.
'
' In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
' https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub Metadata()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://239.0.0.1"
            ' In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            ' the statement below. Your actual interface name may differ, of course.
            ' pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet"

            ' Define the filter. Publisher Id (unsigned 16-bits) is 30, and the writer group Id is 101.
            ' The dataset writer Id (1) must not be specified in the filter, because it does not appear in the message.
            Dim filter = New UASubscribeDataSetFilter(UAPublisherId.CreateUInt16(30))
            filter.WriterGroupDescriptor.WriterGroupId = 101

            ' Define the metadata, with the use of collection initializer for its fields.
            Dim metaData = New UADataSetMetaData From {
                New UAFieldMetaData("BoolToggle", UABuiltInType.Boolean),
                New UAFieldMetaData("Int32", UABuiltInType.Int32),
                New UAFieldMetaData("Int32Fast", UABuiltInType.Int32),
                New UAFieldMetaData("DateTime", UABuiltInType.DateTime)
            }

            ' Define the dataset subscription, with specific communication parameters.
            ' The dataset offset is needed with messages that do not contain dataset writer Ids and use RawData field
            ' encoding. An exception to this rule is when the dataset is the only or first in the dataset message payload,
            ' which is also the case here, but we are specifying the dataset offset anyway, for illustration.
            Dim dataSetSubscriptionDescriptor = New UADataSetSubscriptionDescriptor(
                pubSubConnectionDescriptor, filter, metaData)
            dataSetSubscriptionDescriptor.CommunicationParameters.UadpDataSetReaderMessageParameters.DataSetOffset = 15

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_Metadata

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(dataSetSubscriptionDescriptor)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub

        Private Shared Sub subscriber_DataSetMessage_Metadata(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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

    ' Example output
    '
    'Subscribing...
    'Processing dataset message events for 20 seconds...
    '
    'Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 3072 {System.Int32}; Good]
    '[Int32Fast, 894 {System.Int32}; Good]
    '[DateTime, 10/1/2019 12:21:14 PM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 3072 {System.Int32}; Good]
    '[Int32Fast, 920 {System.Int32}; Good]
    '[DateTime, 10/1/2019 12:21:14 PM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 3073 {System.Int32}; Good]
    '[Int32Fast, 1003 {System.Int32}; Good]
    '[DateTime, 10/1/2019 12:21:15 PM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 3073 {System.Int32}; Good]
    '[Int32Fast, 1074 {System.Int32}; Good]
    '[DateTime, 10/1/2019 12:21:15 PM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt16)30, group=101, fields: 4
    '[BoolToggle, True {System.Boolean}; Good]
    '[Int32, 3074 {System.Int32}; Good]
    '[Int32Fast, 1140 {System.Int32}; Good]
    '[DateTime, 10/1/2019 12:21:16 PM {System.DateTime}; Good]
    '
    '...

End Namespace

#End Region
