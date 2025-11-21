' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to let the user browse for multiple OPC-UA nodes.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Forms.Browsing

Namespace UAFormsDocExamples._UABrowseDialog
    Partial Friend Class ShowDialog
        Shared Sub MultiSelect(owner As IWin32Window)
            Dim browseDialog = New UABrowseDialog()
            browseDialog.InputsOutputs.CurrentNodeDescriptor.EndpointDescriptor.Host = "opcua.demo-this.com"
            browseDialog.Mode.AnchorElementType = UAElementType.Host
            browseDialog.Mode.MultiSelect = True

            Dim dialogResult As DialogResult = browseDialog.ShowDialog(owner)
            If dialogResult <> DialogResult.OK Then
                Return
            End If

            ' Display results
            Dim selectionElements As UABrowseNodeElementCollection = browseDialog.Outputs.SelectionElements
            Dim text As String = ""

            For i = 0 To selectionElements.Count - 1
                Dim selectionElement As UABrowseNodeElement = selectionElements(i)
                text += $"SelectionElements({i}): {selectionElement.NodeElement}" + Environment.NewLine
            Next i

            MessageBox.Show(owner, text)
        End Sub
    End Class
End Namespace
#End Region