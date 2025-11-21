' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how subscribe to changes of a single item with percent deadband.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeItem
        Shared Sub PercentDeadband()
            ' Instantiate the client object
            Dim client = New EasyDAClient()

            Const percentDeadband As Single = 5.0F
            Console.WriteLine("Subscribing with {0}% deadband...", percentDeadband)
            ' The callback is a lambda expression the displays the value
            client.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Ramp 0:100 (10 s)",
                VarTypes.Empty, requestedUpdateRate:=100, percentDeadband:=percentDeadband,
                    Sub(sender, eventArgs)
                        Debug.Assert(eventArgs IsNot Nothing)
                        If eventArgs.Succeeded Then
                            Debug.Assert(eventArgs.Vtq IsNot Nothing)
                            Console.WriteLine(eventArgs.Vtq.ToString())
                        Else
                            Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                        End If
                    End Sub,
                    state:=Nothing)

            Console.WriteLine("Processing item changed events for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace
#End Region
