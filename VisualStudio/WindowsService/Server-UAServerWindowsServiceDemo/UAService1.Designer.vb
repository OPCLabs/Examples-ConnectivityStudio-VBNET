' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

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

Namespace Global.UAServerWindowsServiceDemo
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UAService1
        Inherits ServiceBase

        'UserService overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        ' NOTE: The following procedure is required by the Component Designer
        ' It can be modified using the Component Designer.  Do not modify it
        ' using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
            easyUAServer1 = New OpcLabs.EasyOpc.UA.EasyUAServer(Me.components)
            '
            ' easyUAServer1
            '
            ' UAService1
            '
            Me.ServiceName = "OpcWizardDemo"
        End Sub

        Private easyUAServer1 As OpcLabs.EasyOpc.UA.EasyUAServer
    End Class
End Namespace
#End Region