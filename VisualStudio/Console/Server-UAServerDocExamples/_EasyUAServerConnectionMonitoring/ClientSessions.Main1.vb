' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to monitor OPC UA client connections to the server.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Services

Namespace Global.UAServerDocExamples._EasyUAServerConnectionMonitoring
    Partial Friend Class ClientSessions
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define a data variable providing random integers.
            Dim random = New Random()
            server.Add(New UADataVariable("MyDataVariable").ReadValueFunction(Function() random.Next()))

            ' Obtain the server connection monitoring service.
            Dim serverConnectionMonitoring As IEasyUAServerConnectionMonitoring = server.GetService(Of IEasyUAServerConnectionMonitoring)
            If serverConnectionMonitoring Is Nothing Then
                Console.WriteLine("The server connection monitoring service is not available.")
                Exit Sub
            End If


            ' Hook events.
            AddHandler serverConnectionMonitoring.ClientSessionConnected, AddressOf ServerConnectionMonitoringOnClientSessionConnected
            AddHandler serverConnectionMonitoring.ClientSessionDisconnected, AddressOf ServerConnectionMonitoringOnClientSessionDisconnected

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
        End Sub

        ' Event handler for the ClientSessionConnected event.
        Private Shared Sub ServerConnectionMonitoringOnClientSessionConnected(ByVal sender As Object, ByVal e As EasyUAClientSessionConnectionEventArgs)
            Dim serverConnectionMonitoring As IEasyUAServerConnectionMonitoring = sender
            Console.WriteLine(
                $"A client session has connected to endpoint ""{e.EndpointUrlString}"". " +
                $"There is now {serverConnectionMonitoring.ClientSessionCount} client session(s).")
        End Sub

        ' Event handler for the ClientSessionDisconnected event.
        Private Shared Sub ServerConnectionMonitoringOnClientSessionDisconnected(ByVal sender As Object, ByVal e As EasyUAClientSessionConnectionEventArgs)
            Dim serverConnectionMonitoring As IEasyUAServerConnectionMonitoring = sender
            Console.WriteLine(
                $"A client session has disconnected from endpoint ""{e.EndpointUrlString}"". " +
                $"There is now {serverConnectionMonitoring.ClientSessionCount} client session(s).")
        End Sub
    End Class
End Namespace
#End Region

