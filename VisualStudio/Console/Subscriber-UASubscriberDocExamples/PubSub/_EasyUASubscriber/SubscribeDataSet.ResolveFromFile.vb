' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to subscribe to dataset messages and specify a filter, resolving logical parameters to physical
' from an OPC-UA PubSub configuration file in binary format. The metadata obtained through the resolution is used to decode
' fixed layout messages with RawData field encoding.
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
    Partial Friend Class SubscribeDataSet
        Public Shared Sub ResolveFromFile()

            ' Define the PubSub resolver. We want the information be resolved from a PubSub binary configuration file that
            ' we have. The file itself is at the root of the project, and we have specified that it has to be copied to the
            ' project's output directory.
            Dim pubSubResolverDescriptor = UAPubSubResolverDescriptor.File("UADemoPublisher-Default.uabinary")

            ' Define the PubSub connection we will work with, using its logical name in the PubSub configuration.
            Dim pubSubConnectionDescriptor = New UAPubSubConnectionDescriptor
            pubSubConnectionDescriptor.Name = "FixedLayoutConnection"
            ' In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            ' the statement below. Your actual interface name may differ, of course.
            ' pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet"

            ' Define the filter. The writer group and the dataset writer are specified using their logical names in the
            ' PubSub configuration. The publisher Id in the filter will be taken from the logical PubSub connection.
            Dim filter = New UASubscribeDataSetFilter("FixedLayoutGroup", "SimpleWriter")

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_ResolveFromFile
            AddHandler subscriber.ResolverAccess, AddressOf subscriber_ResolverAccess_ResolveFromFile

            Console.WriteLine("Subscribing...")
            subscriber.SubscribeDataSet(pubSubResolverDescriptor, pubSubConnectionDescriptor, filter)

            Console.WriteLine("Processing dataset message events for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            subscriber.UnsubscribeAllDataSets()

            Console.WriteLine("Waiting for 1 second...")
            ' Unsubscribe operation is asynchronous, messages may still come for a short while.
            Threading.Thread.Sleep(1 * 1000)

            Console.WriteLine("Finished...")
        End Sub

        Private Shared Sub subscriber_DataSetMessage_ResolveFromFile(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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

        Private Shared Sub subscriber_ResolverAccess_ResolveFromFile(ByVal sender As Object, ByVal e As EasyUAResolverAccessEventArgs)
            ' Display resolution information.
            Console.WriteLine(e)
        End Sub
    End Class



    ' Example output
    '
    'Subscribing...
    'Processing dataset message events for 20 seconds...
    '
    '[PublisherFile: UADemoPublisher-Default.uabinary] (no exception)
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
    '...

End Namespace

#End Region

