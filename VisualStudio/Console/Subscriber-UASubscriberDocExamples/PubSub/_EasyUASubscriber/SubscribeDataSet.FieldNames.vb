' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to dataset messages and specify field names, without having the full metadata.
'
' In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
' https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub FieldNames()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://239.0.0.1"
            ' In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            ' the statement below. Your actual interface name may differ, of course.
            ' pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet"

            ' Define the filter. Publisher Id (unsigned 64-bits) is 31, and the dataset writer Id is 1.
            Dim filter = New UASubscribeDataSetFilter(UAPublisherId.CreateUInt64(31), UAWriterGroupDescriptor.Null, 1)

            ' Define the metadata, with the use of collection initializer for its fields. For UADP, the order of field
            ' metadata must correspond to the order of fields in the dataset message.
            ' Since the encoding is not RawData, we do not have to specify the type information for the fields.
            Dim metaData = New UADataSetMetaData From {
                New UAFieldMetaData("BoolToggle"),
                New UAFieldMetaData("Int32"),
                New UAFieldMetaData("Int32Fast"),
                New UAFieldMetaData("DateTime")
            }

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_FieldNames

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(pubSubConnectionDescriptor, filter, metaData)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub

        Private Shared Sub subscriber_DataSetMessage_FieldNames(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[BoolToggle, True {System.Boolean}; Good]
    '[Int32, 25 {System.Int32}; Good]
    '[Int32Fast, 928 {System.Int32}; Good]
    '[DateTime, 10/3/2019 10:43:01 AM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[Int32, 26 {System.Int32}; Good]
    '[Int32Fast, 1007 {System.Int32}; Good]
    '[DateTime, 10/3/2019 10:43:02 AM {System.DateTime}; Good]
    '[BoolToggle, True {System.Boolean}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[Int32Fast, 1113 {System.Int32}; Good]
    '[DateTime, 10/3/2019 10:43:02 AM {System.DateTime}; Good]
    '[BoolToggle, True {System.Boolean}; Good]
    '[Int32, 26 {System.Int32}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 27 {System.Int32}; Good]
    '[Int32Fast, 1201 {System.Int32}; Good]
    '[DateTime, 10/3/2019 10:43:03 AM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[Int32Fast, 1260 {System.Int32}; Good]
    '[DateTime, 10/3/2019 10:43:03 AM {System.DateTime}; Good]
    '[BoolToggle, False {System.Boolean}; Good]
    '[Int32, 27 {System.Int32}; Good]
    '
    '...

End Namespace

#End Region


