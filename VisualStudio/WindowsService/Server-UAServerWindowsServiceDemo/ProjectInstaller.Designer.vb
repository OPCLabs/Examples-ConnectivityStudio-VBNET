' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' A fully functional OPC UA demo server running in Windows service host.
'
' See also:
' https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-add-installers-to-your-service-application
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Namespace Global.UAServerWindowsServiceDemo
    Partial Class ProjectInstaller
        Inherits System.Configuration.Install.Installer

        'Installer overrides dispose to clean up the component list.
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

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.serviceProcessInstaller1 = New ServiceProcess.ServiceProcessInstaller()
            Me.serviceInstaller1 = New ServiceProcess.ServiceInstaller()
            '
            ' serviceProcessInstaller1
            '
            Me.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem
            Me.serviceProcessInstaller1.Password = Nothing
            Me.serviceProcessInstaller1.Username = Nothing
            '
            ' serviceInstaller1
            '
            Me.serviceInstaller1.Description = "This server demo is written in .NET, and it shows how to use the component to cre" +
        "ate an OPC UA server hosted in a Windows service. It provides readable and writa" +
        "ble nodes of various types."
            Me.serviceInstaller1.DisplayName = "OPC Wizard .NET Windows Service Demo"
            Me.serviceInstaller1.ServiceName = "OpcWizardDemo"
            '
            ' ProjectInstaller
            '
            Me.Installers.AddRange(New System.Configuration.Install.Installer() {
                Me.serviceProcessInstaller1,
                Me.serviceInstaller1})
        End Sub

        Private serviceProcessInstaller1 As System.ServiceProcess.ServiceProcessInstaller
        Private serviceInstaller1 As System.ServiceProcess.ServiceInstaller
    End Class
End Namespace
#End Region