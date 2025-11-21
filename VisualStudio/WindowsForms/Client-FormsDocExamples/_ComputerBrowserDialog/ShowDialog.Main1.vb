' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to let the user browse for computers on the network.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Forms.Browsing.Specialized

Namespace FormsDocExamples._ComputerBrowserDialog
    Friend Class ShowDialog
        Shared Sub Main1(owner As IWin32Window)
            Dim computerBrowserDialog = New ComputerBrowserDialog()

            Dim dialogResult As DialogResult = computerBrowserDialog.ShowDialog(owner)
            If dialogResult <> DialogResult.OK Then
                Return
            End If

            ' Display results
            MessageBox.Show(owner, computerBrowserDialog.SelectedName)
        End Sub
    End Class
End Namespace
#End Region