' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to feed the packet capture file into the PubSub subscriber, instead of connecting to the message
' oriented middleware (receiving the messages from the network).
'
' The OpcLabs.Pcap assembly needs to be referenced in your project (Or otherwise made available, together with its
' dependencies) for the capture files to work. Refer to the documentation for more information.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Extensions
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub CaptureFile()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.eth://FF-FF-FF-FF-FF-FF"
            ' Use packets from the specified Ethernet capture file. The file itself is at the root of the project, and we
            ' have specified that it has to be copied to the project's output directory.
            ' Note that .pcap is the default file name extension, and can thus be omitted.
            pubSubConnectionDescriptor.UseEthernetCaptureFile("UADemoPublisher-Ethernet.pcap")

            ' Alternative setup for Ethernet with VLAN tagging
            'Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.eth://FF-FF-FF-FF-FF-FF:2"
            'pubSubConnectionDescriptor.UseEthernetCaptureFile("UADemoPublisher-EthernetVlan.pcap")

            ' Alternative setup for UDP over IPv4
            'Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://239.0.0.1"
            'pubSubConnectionDescriptor.UseUdpCaptureFile("UADemoPublisher-UDP.pcap")

            ' Alternative setup for UDP over IPv6
            'Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://[ff02::1]"
            'pubSubConnectionDescriptor.UseUdpCaptureFile("UADemoPublisher-UDP6.pcap")

            ' Instantiate the subscriber object.
            Dim subscriber = New EasyUASubscriber()

            ' Define the arguments for subscribing to the dataset, where the filter is (unsigned 64-bit) publisher Id 31.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(
                pubSubConnectionDescriptor, UAPublisherId.CreateUInt64(31))

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(subscribeDataSetArguments,
                Sub(sender, eventArgs)
                    '  Display the dataset.
                    If eventArgs.Succeeded Then
                        ' An event with null DataSetData just indicates a successful connection.
                        If Not eventArgs.DataSetData Is Nothing Then
                            Console.WriteLine()
                            Console.WriteLine("Dataset data: {0}", eventArgs.DataSetData)
                            For Each pair As KeyValuePair(Of String, UADataSetFieldData) In eventArgs.DataSetData.FieldDataDictionary
                                Console.WriteLine(pair)
                            Next
                        End If
                    Else
                        Console.WriteLine()
                        Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                    End If
                End Sub)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub
    End Class

    ' Example output
    '
    'Subscribing...
    'Processing dataset message events for 20 seconds...
    '
    'Dataset data: 2019-10-31T16:04:59.145,266,700,00; Good; Data; publisher=(UInt64)31, writer=1, fields: 4
    '[#0, True {System.Boolean} @2019-10-31T1604:59.145,266,700,00; Good]
    '[#1, 0 {System.Int32} @2019-10-31T16:04:59.145,266,700,00; Good]
    '[#2, 767 {System.Int32} @2019-10-31T16:04:59.145,266,700,00; Good]
    '[#3, 10/31/2019 4:04:59 PM {System.DateTime} @2019-10-31T16:04:59.145,266,700,00; Good]
    '
    'Dataset data: 2019-10-31T16:04:59.170,047,500,00; Good; Data; publisher=(UInt64)31, writer=3, fields: 100
    '[#0, 0 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#1, 100 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#2, 200 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#3, 300 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#4, 400 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#5, 500 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#6, 600 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#7, 700 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#8, 800 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#9, 900 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '[#10, 1000 {System.Int64} @2019-10-31T16:04:59.170,047,500,00; Good]
    '...

End Namespace

#End Region
