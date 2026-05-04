' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToStaticMemberViaDerivedType
' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to set the OPC Wizard parameters for best OPC compliance.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class _Parameterization
        Shared Sub OpcCompliance()
            ' You need to set both the shared parameters and instance parameters of the EasyUAServer to the values preset
            ' for OPC compliance, as shown in the code below. The main difference from the default ("Interoperability")
            ' settings is that the OPC compliance settings do not allow insecure connections, but there are other
            ' differences as well.
            ' 
            ' You will need to establish mutual trust between the OPC UA server and the client in order to successfully
            ' establish a secure connection.

            ' Set the shared parameters for OPC compliance.
            EasyUAServer.SharedParameters = EasyUAServerSharedParameters.OpcCompliance

            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Hook event handler for the EndpointStateChanged event. It simply prints out the event.
            AddHandler server.EndpointStateChanged, Sub(sender, args) Console.WriteLine(args)

            ' Set the instance parameters for OPC compliance.
            server.InstanceParameters = EasyUAServerInstanceParameters.OpcCompliance

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
        End Sub
    End Class
End Namespace
#End Region

