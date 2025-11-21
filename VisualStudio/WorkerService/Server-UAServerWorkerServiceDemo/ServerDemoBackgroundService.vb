' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
#Region "Example"
' UAServerWorkerServiceDemo: Shows how to use the component to create an OPC UA server hosted in a worker service. It
' provides readable and writable nodes of various types.
' See also: https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports UAServerDemoLibrary
Public Class ServerDemoBackgroundService
    Inherits BackgroundService

    Private ReadOnly _logger As ILogger(Of ServerDemoBackgroundService)

    ' The server object.
    ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
    Private ReadOnly _server As EasyUAServer = New EasyUAServer()

    Public Sub New(logger As ILogger(Of ServerDemoBackgroundService))
        _logger = logger

        ' Define various nodes.
        ConsoleNodes.AddToParent(_server.Objects)
        DataNodes.AddToParent(_server.Objects)
        DemoNodes.AddToParent(_server.Objects)
    End Sub

    Protected Overrides Async Function ExecuteAsync(stoppingToken As Threading.CancellationToken) As Task
        Try
            ' Start the server.
            _server.Start()

            While (Not stoppingToken.IsCancellationRequested)
                Await Task.Delay(1000, stoppingToken)
            End While

            ' Stop the server.
            _server.Stop()
        Catch __ As OperationCanceledException
            ' When the stopping token is canceled, for example, a call made from services.msc,
            ' we shouldn't exit with a non-zero exit code. In other words, this is expected...
        Catch ex As Exception
            _logger.LogError(ex, "{Message}", ex.Message)

            ' Terminates this process and returns an exit code to the operating system.
            ' This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            ' performs one of two scenarios:
            ' 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            ' 2. When set to "StopHost": will cleanly stop the host, and log errors.
            ' 
            ' In order for the Windows Service Management system to leverage configured
            ' recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1)
        End Try
    End Function
End Class
#End Region