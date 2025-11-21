' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' Shows how different data types can be subscribed to, including rare types and arrays of values.
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
        Private Shared Sub client_ItemChanged(sender As Object, e As EasyDAItemChangedEventArgs)

            Console.WriteLine()
            Console.WriteLine("ItemDescriptor.Arguments.ItemId: {0}", e.Arguments.ItemDescriptor.ItemId)
            If e.Succeeded Then
                Debug.Assert(e.Vtq IsNot Nothing)
                Console.WriteLine("Vtq: {0}", e.Vtq)
            Else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
            End If
        End Sub

        Shared Sub DataTypes()
            Dim arguments As IEnumerable(Of DAItemGroupArguments) = New String() _
            { _
                "Simulation.Register_EMPTY",
                "Simulation.Register_NULL",
                "Simulation.Register_DISPATCH",
 _
                "Simulation.ReadValue_I2",
                "Simulation.ReadValue_I4",
                "Simulation.ReadValue_R4",
                "Simulation.ReadValue_R8",
                "Simulation.ReadValue_CY",
                "Simulation.ReadValue_DATE",
                "Simulation.ReadValue_BSTR",
                "Simulation.ReadValue_BOOL",
                "Simulation.ReadValue_DECIMAL",
                "Simulation.ReadValue_I1",
                "Simulation.ReadValue_UI1",
                "Simulation.ReadValue_UI2",
                "Simulation.ReadValue_UI4",
                "Simulation.ReadValue_INT",
                "Simulation.ReadValue_UINT",
 _
                "Simulation.ReadValue_ArrayOfI2",
                "Simulation.ReadValue_ArrayOfI4",
                "Simulation.ReadValue_ArrayOfR4",
                "Simulation.ReadValue_ArrayOfR8",
                "Simulation.ReadValue_ArrayOfCY",
                "Simulation.ReadValue_ArrayOfDATE",
                "Simulation.ReadValue_ArrayOfBSTR",
                "Simulation.ReadValue_ArrayOfBOOL",
                "Simulation.ReadValue_ArrayOfI1",
                "Simulation.ReadValue_ArrayOfUI1",
                "Simulation.ReadValue_ArrayOfUI2",
                "Simulation.ReadValue_ArrayOfUI4",
                "Simulation.ReadValue_ArrayOfINT",
                "Simulation.ReadValue_ArrayOfUINT"
            } _
            .Select(Function(itemId) New DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 3 * 1000, Nothing))

            Console.WriteLine()

            Dim client As New EasyDAClient()
            Dim eventHandler = New EasyDAItemChangedEventHandler(AddressOf client_ItemChanged)
            AddHandler client.ItemChanged, eventHandler

            Console.WriteLine("Subscribing items...")
            client.SubscribeMultipleItems(arguments.ToArray())
            Thread.Sleep(30 * 1000)
            client.UnsubscribeAllItems()
            RemoveHandler client.ItemChanged, eventHandler
        End Sub
    End Class
End Namespace
#End Region
