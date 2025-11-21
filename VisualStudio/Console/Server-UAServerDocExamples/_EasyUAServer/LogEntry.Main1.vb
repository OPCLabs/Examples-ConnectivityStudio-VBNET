' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToStaticMemberViaDerivedType
' ReSharper disable ArrangeModifiersOrder
#Region "Example"
' This example demonstrates the loggable entries originating in the OPC-UA server engine and the EasyUAServer component.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.BaseLib.Instrumentation
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class LogEntry
        Shared Sub Main1()
            ' Hook static events.
            AddHandler EasyUAServer.LogEntry, AddressOf EasyUAServerOnLogEntry

            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define a data variable providing random integers.
            Dim random = New Random()
            server.Add(New UADataVariable("MyDataVariable").ReadValueFunction(Function() random.Next()))

            ' Start the server.
            Console.WriteLine("The server is starting...")
            server.Start()

            Console.WriteLine("The server is started.")
            Console.WriteLine()

            ' Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...")
            Console.ReadLine()

            ' Stop the server.
            Console.WriteLine("The server is stopping...")
            server.Stop()

            Console.WriteLine("The server is stopped.")

            ' Give the remaining events chance to be handled.
            Thread.Sleep(2 * 1000)

            ' Unhook static events.
            RemoveHandler EasyUAServer.LogEntry, AddressOf EasyUAServerOnLogEntry
        End Sub

        ' Event handler for the LogEntry event. It simply prints out the event.
        Private Shared Sub EasyUAServerOnLogEntry(ByVal sender As Object, ByVal logEntryEventArgs As LogEntryEventArgs)
            Console.WriteLine(logEntryEventArgs)
        End Sub
    End Class
End Namespace
#End Region

