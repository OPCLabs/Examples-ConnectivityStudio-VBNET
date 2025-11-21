' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to create and use two isolated client objects, resulting in two separate connections to the target
' OPC DA server.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class Isolated
        Shared Sub Main1()
            ' Instantiate the client objects and make them isolated
            Dim client1 As New EasyDAClient() With {.Isolated = True}
            Dim client2 As New EasyDAClient() With {.Isolated = True}

            ' The callback is a local method the displays the value
            Dim ItemChagedCallback = Sub(sender As Object, eventArgs As EasyDAItemChangedEventArgs)
                                         Debug.Assert(eventArgs IsNot Nothing)

                                         Dim displayPrefix As String = $"[{eventArgs.Arguments.State}]"
                                         If eventArgs.Succeeded Then
                                             Debug.Assert(eventArgs.Vtq IsNot Nothing)
                                             Console.WriteLine($"{displayPrefix} {eventArgs.Vtq}")
                                         Else
                                             Console.WriteLine($"{displayPrefix} *** Failure: {eventArgs.ErrorMessageBrief}")
                                         End If
                                     End Sub

            Console.WriteLine("Subscribing...")
            client1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, ItemChagedCallback, state:=1)
            client2.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, ItemChagedCallback, state:=2)

            Console.WriteLine("Processing item changed events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client1.UnsubscribeAllItems()
            client2.UnsubscribeAllItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace
#End Region
