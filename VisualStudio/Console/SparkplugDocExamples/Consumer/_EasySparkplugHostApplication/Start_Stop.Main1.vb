' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to manually start and stop the host application.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel
Imports OpcLabs.EasySparkplug.System

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugHostApplication
    Class Start_Stop
        Public Shared Sub Main1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            ' The second parameter is the host ID of this Sparkplug host application. Other Sparkplug components can use
            ' this host ID to detect whether the application is online or offline.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost", "easyApplication")

            ' Pre-create the host application, so that we can control it.
            Dim hostApplication As EasySparkplugHostApplication = EasySparkplugInfrastructure.Instance.FindOrCreateHostApplication(hostDescriptor)

            ' Configure the host application so that we can manually start and stop it.
            hostApplication.AutoStartStop = False

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            ' The lifetime of subscriptions is independent of the state of the host application. We can subscribe before
            ' the application is started.
            Console.WriteLine("Subscribing...")
            ' Thanks to implicit conversion, EasySparkplugHostApplication can be used in place of SparkplugHostDescriptor.
            consumer.SubscribeEdgeNodePayload(hostApplication, "easyGroup", "easySparkplugDemo",
                Sub(sender, eventArgs)
                    ' Handle different types of notifications.
                    Select Case eventArgs.NotificationType
                        Case SparkplugNotificationType.Connect
                            Console.WriteLine($"Connected to Sparkplug host, client ID: {eventArgs.ClientId}.")
                        Case SparkplugNotificationType.Disconnect
                            Console.WriteLine("Disconnected from Sparkplug host.")
                        Case SparkplugNotificationType.Data
                            Console.Write("Data; ")
                        Case SparkplugNotificationType.Birth
                            Console.Write("Birth; ")
                        Case SparkplugNotificationType.Death
                            Console.Write("Death; ")
                    End Select
                    If Not eventArgs.Succeeded Then
                        Console.WriteLine($"*** Failure: {eventArgs.ErrorMessageBrief}")
                    End If
                End Sub)

            Console.WriteLine()
            Console.WriteLine("Press Enter to start the application...")
            Console.ReadLine()
            hostApplication.Start()

            Console.WriteLine()
            Console.WriteLine("Press Enter to stop the application...")
            Console.ReadLine()
            hostApplication.Stop()

            Console.WriteLine()
            Console.WriteLine("Press Enter to unsubscribe...")
            Console.ReadLine()

            Console.WriteLine("Unsubscribing...")
            consumer.UnsubscribeAllPayloads()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
