' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to let the user browse for OPC Alarms&Events areas or sources. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace
Imports OpcLabs.EasyOpc.AlarmsAndEvents.Forms.Browsing

Namespace FormsDocExamples._AEAreaOrSourceDialog
    Friend Class ShowDialog
        Shared Sub Main1(owner As IWin32Window)
            Dim areaOrSourceDialog = New AEAreaOrSourceDialog() With {
                .ServerDescriptor = New ServerDescriptor() With {
                    .ServerClass = "OPCLabs.KitEventServer.2"
                }
            }

            Dim dialogResult As DialogResult = areaOrSourceDialog.ShowDialog(owner)
            If dialogResult <> DialogResult.OK Then
                Return
            End If

            ' Display results
            Dim nodeElementsString As String = String.Join("\n", areaOrSourceDialog.NodeElements.Select(Function(element As AENodeElement) element))
            MessageBox.Show(owner, nodeElementsString)
        End Sub
    End Class
End Namespace
#End Region