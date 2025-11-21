' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example obtains and prints out information about PubSub connections, writer groups, and dataset writers in the
' OPC UA PubSub configuration.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
'Imports OpcLabs.EasyOpc.UA.PubSub.Configuration.Extensions
Imports OpcLabs.EasyOpc.UA.PubSub.InformationModel
Imports OpcLabs.EasyOpc.UA.PubSub.InformationModel.Extensions

Namespace Global.UASubscriberDocExamples.PubSub._IUAReadOnlyPubSubConfiguration
    Partial Friend Class GetMethods
        Public Shared Sub PubSubComponents()

            ' Instantiate the publish-subscribe client object.
            Dim publishSubscribeClient = New EasyUAPublishSubscribeClient()

            Try
                Console.WriteLine("Loading the configuration...")
                ' Load the PubSub configuration from a file. The file itself is at the root of the project, and we have
                ' specified that it has to be copied to the project's output directory.
                Dim pubSubConfiguration As IUAReadOnlyPubSubConfiguration =
                    publishSubscribeClient.LoadReadOnlyConfiguration("UADemoPublisher-Default.uabinary")

                ' Alternatively, using the statement below, you can access a live configuration residing in an OPC UA
                ' Server with appropriate information model.
                'Dim pubSubConfiguration As IUAReadOnlyPubSubConfiguration =
                '    publishSubscribeClient.AccessReadOnlyConfiguration("opc.tcp://localhost:48010")

                ' Get the names of PubSub connections in the configuration, regardless of the folder they reside in.
                Dim pubSubConnectionNames = pubSubConfiguration.ListConnectionNames()
                For Each pubSubConnectionName As String In pubSubConnectionNames
                    Console.WriteLine($"PubSub connection: {pubSubConnectionName}")

                    ' You can use the statement below to obtain parameters of the PubSub connection.
                    'Dim connectionElement As UAPubSubConnectionElement =
                    '    pubSubConfiguration.GetConnectionElement(pubSubConnectionName)

                    ' Get names of the writer groups on this PubSub connection.
                    Dim writerGroupNames = pubSubConfiguration.ListWriterGroupNames(pubSubConnectionName)
                    For Each writerGroupName As String In writerGroupNames
                        Console.WriteLine($"  Writer group: {writerGroupName}")

                        ' You can use the statement below to obtain parameters of the writer group.
                        'Dim writerGroupElement As UAWriterGroupElement =
                        '    pubSubConfiguration.GetWriterGroupElement(pubSubConnectionName, writerGroupName)

                        ' Get names of the dataset writers on this writer group.
                        Dim dataSetWriterNames =
                            pubSubConfiguration.ListDataSetWriterNames(pubSubConnectionName, writerGroupName)
                        For Each dataSetWriterName As String In dataSetWriterNames
                            Console.WriteLine($"    Dataset writer: {dataSetWriterName}")

                            ' You can use the statement below to obtain parameters of the dataset writer.
                            'Dim dataSetWriterElement As UADataSetWriterElement = pubSubConfiguration.GetDataSetWriterElement(
                            '    pubSubConnectionName, writerGroupName, dataSetWriterName)
                        Next dataSetWriterName
                    Next writerGroupName
                Next pubSubConnectionName
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.InnerException.Message}")
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class

    ' Example output
    '
    'Loading the configuration...
    'PubSub connection FixedLayoutConnection
    '  Writer group: FixedLayoutGroup
    '    Dataset writer: SimpleWriter
    '    Dataset writer: AllTypesWriter
    '    Dataset writer: MassTestWriter
    'PubSub connection: DynamicLayoutConnection
    '  Writer group: DynamicLayoutGroup
    '    Dataset writer: SimpleWriter
    '    Dataset writer: MassTestWriter
    '    Dataset writer: AllTypes-DynamicWriter
    '    Dataset writer: EventSimpleWriter
    'PubSub connection: FlexibleLayoutConnection
    '  Writer group: FlexibleLayoutGroup
    '    Dataset writer: SimpleWriter
    '    Dataset writer: MassTestWriter
    '    Dataset writer: AllTypes-DynamicWriter
    'Finished.

End Namespace

#End Region


