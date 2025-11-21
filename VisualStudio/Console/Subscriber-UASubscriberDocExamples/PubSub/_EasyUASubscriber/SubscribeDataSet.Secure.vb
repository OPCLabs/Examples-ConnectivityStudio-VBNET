' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to securely subscribe to signed and encrypted dataset messages.
' An external Security Key Service (SKS) is needed (not a part of QuickOPC).
'
' The network messages for this example can be published e.g. using the UADemoPublisher tool - see
' https://kb.opclabs.com/How_to_publish_or_subscribe_to_secure_OPC_UA_PubSub_messages .
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Engine
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

Namespace Global.UASubscriberDocExamples.PubSub._EasyUASubscriber
    Partial Friend Class SubscribeDataSet
        Public Shared Sub Secure()

            ' Define the PubSub connection we will work with. Uses implicit conversion from a string.
            Dim pubSubConnectionDescriptor As UAPubSubConnectionDescriptor = "opc.udp://239.0.0.1"
            ' In some cases you may have to set the interface (network adapter) name that needs to be used, similarly to
            ' the statement below. Your actual interface name may differ, of course.
            'pubSubConnectionDescriptor.ResourceAddress.InterfaceName = "Ethernet";

            ' Define the arguments for subscribing to the dataset.
            Dim subscribeDataSetArguments = New UASubscribeDataSetArguments(pubSubConnectionDescriptor)
            ' Specifies the security mode for the PubSub network messages received. 
            ' This is a minimum security mode that you want to accept.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityMode = UAMessageSecurityModes.SecuritySignAndEncrypt
            ' Specifies the URL of the SKS (Security Key Service) endpoint.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityKeyServiceTemplate.UrlString = "opc.tcp://localhost:48010"
            ' Specifies the security mode that will be used to connect to the SKS.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityKeyServiceTemplate.EndpointSelectionPolicy = UAMessageSecurityModes.SecuritySignAndEncrypt
            ' Specifies the user name and password used for "logging in" to the SKS.
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityKeyServiceTemplate.UserIdentity.UserNameTokenInfo.UserName = "root"
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityKeyServiceTemplate.UserIdentity.UserNameTokenInfo.Password = "secret"
            ' Specifies the Id of the security group in the SKS that will be used (the security group in the
            ' SKS is configured to use certain security policy, and has other parameters detailing how the
            ' security keys are generated).
            subscribeDataSetArguments.DataSetSubscriptionDescriptor.CommunicationParameters.SecurityGroupId = "TestGroup"

            ' Instantiate the subscriber object and hook events.
            Dim subscriber = New EasyUASubscriber()
            AddHandler subscriber.DataSetMessage, AddressOf subscriber_DataSetMessage_Secure

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

        Private Shared Sub subscriber_DataSetMessage_Secure(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs)
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
End Namespace

#End Region
