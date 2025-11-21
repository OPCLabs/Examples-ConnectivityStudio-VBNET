' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
' ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#Region "Example"
' This example shows how subscribe to changes of multiple items and display each change, identifying the different
' subscriptions by an integer.
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
        Public Shared Sub StateAsInteger()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_StateAsInteger_ItemChanged

                Console.WriteLine("Subscribing...")
                Dim handleArray() As Integer = client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000,
                        state:=1), ' An integer we have chosen to identify the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000,
                        state:=2), ' An integer we have chosen to identify the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000,
                        state:=3), ' An integer we have chosen to identify the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000,
                        state:=4) ' An integer we have chosen to identify the subscription
                    })

                For i As Integer = 0 To handleArray.Length - 1
                    Console.WriteLine($"handleArray[{i}]: {handleArray(i)}")
                Next i

                Console.WriteLine("Processing item changed events for 10 seconds...")
                Thread.Sleep(10 * 1000)

                Console.WriteLine("Unsubscribing...")
            End Using

            Console.WriteLine("Waiting for 5 seconds...")
            Thread.Sleep(5 * 1000)

            Console.WriteLine("Finished.")
        End Sub

        ' Item changed event handler
        Private Shared Sub client_StateAsInteger_ItemChanged(ByVal sender As Object, ByVal eventArgs As EasyDAItemChangedEventArgs)
            ' Obtain the integer state we have passed in.
            Dim stateAsInteger As Integer = CInt(eventArgs.Arguments.State)

            If eventArgs.Succeeded Then
                Console.WriteLine($"{stateAsInteger}: {eventArgs.Vtq}")
            Else
                Console.WriteLine($"{stateAsInteger} *** Failure: {eventArgs.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace
#End Region
