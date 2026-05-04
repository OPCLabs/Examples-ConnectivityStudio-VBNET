' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example subscribes to changes of 2 items separately, and displays rich information available with each item changed
' event notification.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAItemChangedEventArgs
    Partial Friend Class General
        Shared Sub Main1()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                Dim eventHandler = New EasyDAItemChangedEventHandler(AddressOf client_ItemChanged)
                AddHandler client.ItemChanged, eventHandler

                Console.WriteLine("Subscribing items...")
                client.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Random", 5 * 1000)
                client.SubscribeItem("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 5 * 1000)

                Console.WriteLine("Processing item changed events for 1 minute...")
                Thread.Sleep(60 * 1000)
            End Using
        End Sub

        Private Shared Sub client_ItemChanged(sender As Object, e As EasyDAItemChangedEventArgs)
            Console.WriteLine()
            Console.WriteLine($"e.Arguments.State: {e.Arguments.State}")
            Console.WriteLine($"e.Arguments.ServerDescriptor.MachineName: {e.Arguments.ServerDescriptor.MachineName}")
            Console.WriteLine($"e.Arguments.ServerDescriptor.ServerClass: {e.Arguments.ServerDescriptor.ServerClass}")
            Console.WriteLine($"e.Arguments.ItemDescriptor.ItemId: {e.Arguments.ItemDescriptor.ItemId}")
            Console.WriteLine($"e.Arguments.ItemDescriptor.AccessPath: {e.Arguments.ItemDescriptor.AccessPath}")
            Console.WriteLine($"e.Arguments.ItemDescriptor.RequestedDataType: {e.Arguments.ItemDescriptor.RequestedDataType}")
            Console.WriteLine($"e.Arguments.GroupParameters.Locale: {e.Arguments.GroupParameters.Locale}")
            Console.WriteLine($"e.Arguments.GroupParameters.RequestedUpdateRate: {e.Arguments.GroupParameters.RequestedUpdateRate}")
            Console.WriteLine($"e.Arguments.GroupParameters.PercentDeadband: {e.Arguments.GroupParameters.PercentDeadband}")
            If e.Succeeded Then
                Debug.Assert(e.Vtq IsNot Nothing)
                Console.WriteLine($"e.Vtq.Value: {e.Vtq.Value}")
                Console.WriteLine($"e.Vtq.Timestamp: {e.Vtq.Timestamp}")
                Console.WriteLine($"e.Vtq.TimestampLocal: {e.Vtq.TimestampLocal}")
                Console.WriteLine($"e.Vtq.Quality: {e.Vtq.Quality}")
            Else
                Debug.Assert(e.Exception IsNot Nothing)
                Console.WriteLine($"e.Exception.Message: {e.Exception.Message}")
                Console.WriteLine($"e.Exception.Source: {e.Exception.Source}")
            End If
        End Sub

        ' Example output
        '
        'Processing item changed events for 1 minute...
        '
        'e.Arguments.State: 
        'e.Arguments.ServerDescriptor.MachineName 
        'e.Arguments.ServerDescriptor.ServerClass OPCLabs.KitServer.2
        'e.Arguments.ItemDescriptor.ItemId: Simulation.Random
        'e.Arguments.ItemDescriptor.AccessPath: 
        'e.Arguments.ItemDescriptor.RequestedDataType Empty
        'e.Arguments.GroupParameters.Locale: 0
        'e.Arguments.GroupParameters.RequestedUpdateRate: 5000
        'e.Arguments.GroupParameters.PercentDeadband: 0
        'e.Vtq.Value: 0.00125125888851588
        'e.Vtq.Timestamp: 4/10/2020 4:35:25 PM
        'e.Vtq.TimestampLocal: 4/10/2020 6:35:25 PM
        'e.Vtq.Quality: GoodNonspecific (192)
        '
        'e.Arguments.State: 
        'e.Arguments.ServerDescriptor.MachineName 
        'e.Arguments.ServerDescriptor.ServerClass OPCLabs.KitServer.2
        'e.Arguments.ItemDescriptor.ItemId: Trends.Ramp(1 min)
        'e.Arguments.ItemDescriptor.AccessPath: 
        'e.Arguments.ItemDescriptor.RequestedDataType Empty
        'e.Arguments.GroupParameters.Locale: 0
        'e.Arguments.GroupParameters.RequestedUpdateRate: 5000
        'e.Arguments.GroupParameters.PercentDeadband: 0
        'e.Vtq.Value: 0.431881904602051
        'e.Vtq.Timestamp: 4/10/2020 4:35:25 PM
        'e.Vtq.TimestampLocal: 4/10/2020 6:35:25 PM
        'e.Vtq.Quality: GoodNonspecific (192)
        '
        '...
    End Class
End Namespace
#End Region
