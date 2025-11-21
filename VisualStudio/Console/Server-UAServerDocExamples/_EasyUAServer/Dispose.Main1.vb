' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to use the IDisposable interface to automatically stop the OPC UA server.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class Dispose
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            ' 
            ' The "Using" statement ensures disposal of the resource it acquires.
            Using server = New EasyUAServer()
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

                ' The IDisposable.Dispose call (automatically made at the end of the "using" statement) stops the
                ' EasyUAServer if it is started.
                Console.WriteLine("The server is stopping...")
            End Using
        End Sub
    End Class
End Namespace

#End Region

