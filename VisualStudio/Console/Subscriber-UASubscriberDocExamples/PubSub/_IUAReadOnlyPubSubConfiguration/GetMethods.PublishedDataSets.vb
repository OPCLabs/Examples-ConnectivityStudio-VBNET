' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example obtains and prints out information about all published datasets in the OPC UA PubSub configuration.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration
Imports OpcLabs.EasyOpc.UA.PubSub.Configuration.Extensions
Imports OpcLabs.EasyOpc.UA.PubSub.InformationModel
Imports OpcLabs.EasyOpc.UA.PubSub.InformationModel.Extensions

Namespace Global.UASubscriberDocExamples.PubSub._IUAReadOnlyPubSubConfiguration
    Partial Friend Class GetMethods
        Public Shared Sub PublishedDataSets()

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

                ' Get the names of all published datasets in the PubSub configuration.
                Dim publishedDataSetNames = pubSubConfiguration.ListAllPublishedDataSetNames()

                For Each publishedDataSetName As String In publishedDataSetNames
                    Console.WriteLine($"Published dataset: {publishedDataSetName}")

                    ' You can use the statement below to obtain parameters of the published dataset.
                    'Dim publishedDataSetElement As UAPublishedDataSetElement =
                    '    pubSubConfiguration.GetPublishedDataSetElement(publishedDataSetName)
                Next publishedDataSetName
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.InnerException.Message}")
            End Try

            Console.WriteLine("Finished...")
        End Sub
    End Class

    ' Example output
    '
    'Loading the configuration...
    'Published dataset Simple
    'Published dataset: AllTypes
    'Published dataset: MassTest
    'Published dataset: AllTypes-Dynamic
    'Finished.

End Namespace

#End Region

