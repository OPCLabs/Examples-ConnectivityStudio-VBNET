' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
' ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#Region "Example"
' This example shows how subscribe to changes of multiple items and display each change, identifying the different
' subscriptions by an object.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class SubscribeMultipleItems

        Private Class CustomObject
            Sub New(name As String)
                Me.Name = name
            End Sub

            Public ReadOnly Property Name As String
        End Class

        Public Shared Sub StateAsObject()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_StateAsObject_ItemChanged

                Console.WriteLine("Subscribing...")
                Dim handleArray() As Integer = client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Random", 1000,
                        New CustomObject("First")), ' A custom object that corresponds to the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000,
                        New CustomObject("Second")), ' A custom object that corresponds to the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Sine (1 min)", 1000,
                        New CustomObject("Third")), ' A custom object that corresponds to the subscription
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_I4", 1000,
                        New CustomObject("Fourth")) ' A custom object that corresponds to the subscription
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
        Private Shared Sub client_StateAsObject_ItemChanged(ByVal sender As Object, ByVal eventArgs As EasyDAItemChangedEventArgs)
            ' Obtain the custom object we have passed in.
            Dim stateAsObject As CustomObject = CType(eventArgs.Arguments.State, CustomObject)

            If eventArgs.Succeeded Then
                Console.WriteLine($"{stateAsObject.Name}: {eventArgs.Vtq}")
            Else
                Console.WriteLine($"{stateAsObject.Name} *** Failure: {eventArgs.ErrorMessageBrief}")
            End If
        End Sub
    End Class
End Namespace
#End Region
