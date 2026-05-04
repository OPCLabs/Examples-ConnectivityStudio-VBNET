' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to subscribe to events and display the event message with each notification, using a callback method
' specified using lambda expression.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.AlarmsAndEvents

Namespace Global.DocExamples.AlarmsAndEvents._EasyAEClient
    Partial Friend Class SubscribeEvents
        Public Shared Sub CallbackLambda()
            ' Instantiate the client object
            Dim client = New EasyAEClient()

            Console.WriteLine("Subscribing...")
            ' The callback is a lambda expression the displays the event message
            client.SubscribeEvents("", "OPCLabs.KitEventServer.2", 1000,
                    Sub(sender, eventArgs)
                        Debug.Assert(eventArgs IsNot Nothing)
                        If Not eventArgs.Succeeded Then
                            Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                            Exit Sub
                        End If
                        If eventArgs.EventData IsNot Nothing Then
                            Console.WriteLine(eventArgs.EventData.Message)
                        End If
                    End Sub)

            Console.WriteLine("Processing event notifications for 20 seconds...")
            Threading.Thread.Sleep(20 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllEvents()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace
#End Region
