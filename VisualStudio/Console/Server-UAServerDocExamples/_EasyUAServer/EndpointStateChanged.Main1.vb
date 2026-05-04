' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain notifications about the changes in the state of the endpoints that server exposes.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class EndpointStateChanged
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define a data variable providing random integers.
            Dim random = New Random()
            server.Add(New UADataVariable("MyDataVariable").ReadValueFunction(Function() random.Next()))

            ' Hook events.
            AddHandler server.EndpointStateChanged, AddressOf ServerOnEndpointStateChanged

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

        ' Event handler for the EndpointStateChanged event. It simply prints out the event.
        Private Shared Sub ServerOnEndpointStateChanged(ByVal sender As Object, ByVal e As EasyUAServerEndpointStateChangedEventArgs)
            Console.WriteLine(e)

            ' Following are some useful properties in the event notification:
            '   e.EndpointUrlString
            '   e.ConnectionState
            '   e.Exception
            '   e.Succeeded
        End Sub
    End Class
End Namespace
#End Region

