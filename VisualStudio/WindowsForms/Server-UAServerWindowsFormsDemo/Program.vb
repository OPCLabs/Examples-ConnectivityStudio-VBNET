' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable AccessToStaticMemberViaDerivedType
' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' A fully functional OPC UA demo server running in Windows Forms host.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Windows.Forms
Imports OpcLabs.EasyOpc.UA

Namespace UAServerWindowsFormsDemo
    Module Program
        Sub Main(args As String())
#If (NET6_0_OR_GREATER) Then
            ' To customize application configuration such as set high DPI settings or default font,
            ' see https://aka.ms/applicationconfiguration.
            'ApplicationConfiguration.Initialize()
#End If

            ' Enable the Windows Forms interaction by the server.
            EasyUAServer.SharedParameters.PluginSetups.FindName("UAWindowsFormsInteraction").Enabled = True

            Application.Run(New MainForm())
        End Sub
    End Module
End Namespace
#End Region

