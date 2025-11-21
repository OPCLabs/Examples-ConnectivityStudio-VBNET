' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToStaticMemberViaDerivedType
' ReSharper disable AccessToDisposedClosure
' ReSharper disable ConvertToUsingDeclaration
' ReSharper disable PossibleNullReferenceException
' ReSharper disable UnusedParameter.Local
#Region "Example"
' A fully functional OPC UA demo server running in a console host.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.BaseLib
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Services
Imports UAServerDemoLibrary

Namespace Global.UAServerConsoleDemo
    Module Program
        Sub Main(args As String())
            Console.WriteLine("OPC Wizard Server Console Demo")
            Console.WriteLine("")

            ' Enable the console interaction by the server.
            EasyUAServer.SharedParameters.PluginSetups.FindName("UAConsoleInteraction").Enabled = True

            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Using server = New EasyUAServer()
                ' Define various nodes.
                ConsoleNodes.AddToParent(server.Objects)
                DataNodes.AddToParent(server.Objects)
                DemoNodes.AddToParent(server.Objects)

                ' Hook events to the server object.
                AddHandler server.EndpointStateChanged, Sub(sender, EventArgs) _
                                                           Console.WriteLine($"{NameOf(server.EndpointStateChanged)}: {EventArgs}")
                AddHandler server.Starting, Sub(sender, EventArgs) Console.WriteLine(NameOf(server.Starting))
                AddHandler server.Stopped, Sub(sender, EventArgs) Console.WriteLine(NameOf(server.Stopped))

                ' Obtain the server connection monitoring service.
                Dim serverConnectionMonitoring As IEasyUAServerConnectionMonitoring = server.GetService(Of IEasyUAServerConnectionMonitoring)()
                If Not (serverConnectionMonitoring Is Nothing) Then
                    ' Hook events to the connection monitoring service.
                    AddHandler serverConnectionMonitoring.ClientSessionConnected, Sub(sender, EventArgs) _
                        Console.WriteLine($"{NameOf(serverConnectionMonitoring.ClientSessionConnected)}: {EventArgs}")
                    AddHandler serverConnectionMonitoring.ClientSessionDisconnected, Sub(sender, EventArgs) _
                        Console.WriteLine($"{NameOf(serverConnectionMonitoring.ClientSessionDisconnected)}: {EventArgs}")
                End If

                ' Start the server.
                server.Start()

                ' Let the user decide when to stop.
                Dim cancelled = New ManualResetEvent(initialState:=False)
                AddHandler Console.CancelKeyPress, Sub(sender, EventArgs) _
                                                       ' Signal the main thread to exit.
                                                       cancelled.Set()

                                                       ' Prevent the process from terminating immediately.
                                                       EventArgs.Cancel = True
                                                   End Sub

                Console.WriteLine("Press Ctrl+C to stop the server...")
                cancelled.WaitOne()

                ' Stop the server.
                server.Stop()
            End Using
        End Sub
    End Module
End Namespace
#End Region

