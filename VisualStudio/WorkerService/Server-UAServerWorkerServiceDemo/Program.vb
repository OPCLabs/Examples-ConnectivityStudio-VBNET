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

Imports Microsoft.Extensions.Logging.Configuration
Imports Microsoft.Extensions.Logging.EventLog

Module Program
    Sub Main(args As String())
        Dim builder As HostApplicationBuilder = Host.CreateApplicationBuilder(args)
        builder.Services.AddWindowsService(Sub(options)
                                               options.ServiceName = "OpcWizardDemo"
                                           End Sub
        )

        LoggerProviderOptions.RegisterProviderOptions(Of EventLogSettings, EventLogLoggerProvider) _
                (builder.Services)

        builder.Services.AddHostedService(Of ServerDemoBackgroundService)()

        Dim host0 As IHost = builder.Build()
        host0.Run()
    End Sub
End Module
#End Region