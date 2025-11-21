' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to let the user browse for an OPC Alarms&Events condition available on a specified event category.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.AlarmsAndEvents.Forms.Browsing

Namespace FormsDocExamples._AECategoryConditionDialog
    Friend Class ShowDialog
        Shared Sub Main1(owner As IWin32Window)
            Dim categoryConditionDialog = New AECategoryConditionDialog() With {
                .ServerDescriptor = New ServerDescriptor() With {
                    .ServerClass = "OPCLabs.KitEventServer.2"
                },
                .CategoryId = &HEC0002 ' Deviation
            }

            Dim dialogResult As DialogResult = categoryConditionDialog.ShowDialog(owner)
            If dialogResult <> DialogResult.OK Then
                Return
            End If

            ' Display results
            MessageBox.Show(owner, $"ConditionElement: {categoryConditionDialog.ConditionElement}")
        End Sub
    End Class
End Namespace
#End Region