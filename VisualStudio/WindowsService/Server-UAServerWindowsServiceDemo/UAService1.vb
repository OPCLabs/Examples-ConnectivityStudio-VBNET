' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToStaticMemberViaDerivedType
' ReSharper disable AccessToDisposedClosure
' ReSharper disable ConvertToUsingDeclaration
' ReSharper disable PossibleNullReferenceException
' ReSharper disable UnusedParameter.Local
#Region "Example"
' UAServerWindowsServiceDemo: Shows how to use the component to create an OPC UA server hosted in a Windows service. It
' provides readable and writable nodes of various types.
'
' Install the service by running:
'      C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /i UAServerWindowsServiceDemo.exe
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.ServiceProcess
Imports OpcLabs.EasyOpc.UA
Imports UAServerDemoLibrary

Namespace Global.UAServerWindowsServiceDemo
    Partial Public Class UAService1
        Inherits ServiceBase

        Public Sub New()
            InitializeComponent()

            ' Define various nodes.
            ConsoleNodes.AddToParent(_server.Objects)
            DataNodes.AddToParent(_server.Objects)
            DemoNodes.AddToParent(_server.Objects)
        End Sub

        Protected Overrides Sub OnStart(ByVal args() As String)
            ' Start the server.
            _server.Start()
        End Sub

        Protected Overrides Sub OnStop()
            ' Stop the server.
            _server.Stop()
        End Sub

        Private ReadOnly _server As EasyUAServer = New EasyUAServer()
    End Class
End Namespace
#End Region