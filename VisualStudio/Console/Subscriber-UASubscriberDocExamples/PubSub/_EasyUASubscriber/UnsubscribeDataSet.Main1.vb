' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to dataset messages on an OPC-UA PubSub connection, and then unsubscribe from that
' dataset.
'
' In order to produce network messages for this example, run the UADemoPublisher tool. For documentation, see
' https://kb.opclabs.com/UADemoPublisher_Basics . In some cases, you may have to specify the interface name to be used.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Friend Class UnsubscribeDataSet
        Public Shared Sub Main1()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://239.0.0.1"
            ' In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            ' the statement below. Your actual interface name may differ, of course.
            ' pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet"

            ' Define the filter. Publisher Id (unsigned 64-bits) is 31, and the dataset writer Id is 1.
            Dim filter = New UASubscribeDataSetFilter(UAPublisherId.CreateUInt64(31), UAWriterGroupDescriptor.Null, 1)

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(pubSubConnectionDescriptor, filter)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub

        Private Shared Sub subscriber_DataSetMessage(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
    '[#0, True {System.Boolean}; Good]
    '[#1, 7134 {System.Int32}; Good]
    '[#2, 7364 {System.Int32}; Good]
    '[#3, 10/1/2019 10:42:16 AM {System.DateTime}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[#1, 7135 {System.Int32}; Good]
    '[#2, 7429 {System.Int32}; Good]
    '[#3, 10/1/2019 10:42:17 AM {System.DateTime}; Good]
    '[#0, True {System.Boolean}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[#2, 7495 {System.Int32}; Good]
    '[#3, 10/1/2019 10:42:17 AM {System.DateTime}; Good]
    '[#0, True {System.Boolean}; Good]
    '[#1, 7135 {System.Int32}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[#1, 7136 {System.Int32}; Good]
    '[#2, 7560 {System.Int32}; Good]
    '[#3, 10/1/2019 10:42:18 AM {System.DateTime}; Good]
    '[#0, True {System.Boolean}; Good]
    '
    'Dataset data: Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[#2, 7626 {System.Int32}; Good]
    '[#3, 10/1/2019 10:42:18 AM {System.DateTime}; Good]
    '[#0, True {System.Boolean}; Good]
    '[#1, 7136 {System.Int32}; Good]
    '
    '...

End Namespace

#End Region

