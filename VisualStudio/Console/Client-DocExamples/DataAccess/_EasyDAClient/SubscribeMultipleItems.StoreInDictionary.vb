' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to store current state of the subscribed items in a dictionary.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeMultipleItems
        Public Shared Sub StoreInDictionary()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_ItemChanged_StoreInDictionary

                Console.WriteLine("Subscribing item changes...")
                client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000, Nothing)
                })

                Console.WriteLine("Processing item changed events for 1 minute...")
                Dim startTickCount As Integer = Environment.TickCount
                Do
                    Thread.Sleep(5 * 1000)

                    ' Each 5 seconds, display the current state of the items we have subscribed to.
                    SyncLock _serialize
                        Console.WriteLine()
                        For Each pair As KeyValuePair(Of DAItemDescriptor, DAVtqResult) In _vtqResultDictionary
                            Dim itemDescriptor As DAItemDescriptor = pair.Key
                            Dim vtqResult = pair.Value
                            Console.WriteLine($"{itemDescriptor}: {vtqResult}")
                        Next

                        ' The code above shows how you can process the complete contents of the dictionary. In other
                        ' scenarios, you may want to access just a specific entry in the dictionary. You can achieve that
                        ' by indexing the dictionary by the item descriptor of the item you are interested in.
                    End SyncLock
                Loop While Environment.TickCount < startTickCount + 60 * 1000

                Console.WriteLine("Unsubscribing item changes...")
            End Using

            Console.WriteLine("Finished.")
        End Sub

        ' Item changed event handler
        Private Shared Sub client_ItemChanged_StoreInDictionary(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
            SyncLock _serialize
                ' Convert the event arguments to a DAVtq result object, and store it in the dictionary under the key which
                ' is the item descriptor of the item this item changed event is for.
                _vtqResultDictionary(e.Arguments.ItemDescriptor) = CType(e, DAVtqResult)
            End SyncLock
        End Sub

        ' Holds last known state of each subscribed item.
        Private Shared ReadOnly _vtqResultDictionary As New Dictionary(Of DAItemDescriptor, DAVtqResult)

        ' Synchronization object used to prevent simultaneous access to the dictionary.
        Private Shared ReadOnly _serialize As New Object
    End Class
End Namespace
#End Region
