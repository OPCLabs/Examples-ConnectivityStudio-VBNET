' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' A fully functional OPC UA demo server running in Windows service host.
'
' See also:
' https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-add-installers-to-your-service-application
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.ComponentModel

Namespace Global.UAServerWindowsServiceDemo
    <RunInstaller(True)>
    Public Class ProjectInstaller
        Inherits System.Configuration.Install.Installer
        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

    End Class
End Namespace
#End Region
