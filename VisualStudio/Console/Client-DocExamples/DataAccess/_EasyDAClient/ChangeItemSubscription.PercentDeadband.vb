' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
Imports OpcLabs.EasyOpc.DataAccess.OperationModel ' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how change the percent deadband of an existing subscription.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.DataAccess._EasyDAClient

    Partial Friend Class ChangeItemSubscription
        Public Shared Sub PercentDeadband()
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_ItemChanged_PercentDeadband

                Console.WriteLine("Subscribing with 10% deadband...")
                Dim handle As Integer = client.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Ramp 0:100 (10 s)", VarTypes.Empty, 100, 10.0F, Nothing)

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Changing subscription to 0% deadband...")
                client.ChangeItemSubscription(handle, New DAGroupParameters(100, 0.0F))

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing...")
                client.UnsubscribeAllItems()

                Console.WriteLine("Waiting for 10 seconds...")
                Thread.Sleep(10 * 1000)
            End Using
        End Sub

        ' Item changed event handler
        Private Shared Sub client_ItemChanged_PercentDeadband(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
            If e.Succeeded Then
                Console.WriteLine(e.Vtq)
            Else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace
#End Region
