' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to allow browsing for an OPC Data Access node by placing a browsing control on the form.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.Forms.Browsing
Imports OpcLabs.BaseLib

Namespace FormsDocExamples._OpcBrowseControl
    Public Class UsageForm
        Private Sub getOutputsButton_Click(sender As Object, e As EventArgs) Handles getOutputsButton.Click
            ' Obtain the current node element.
            Dim currentNodeElement As OpcBrowseNodeElement = opcBrowseControl1.Outputs.CurrentNodeElement

            ' Display the present parts of the current node element in the outputs text text box.
            outputsTextBox.Text = ""
            If currentNodeElement.ComputerElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(OpcBrowseNodeElement.ComputerElement)}: {currentNodeElement.ComputerElement}" + Environment.NewLine
            End If
            If currentNodeElement.ServerElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(OpcBrowseNodeElement.ServerElement)}: {currentNodeElement.ServerElement}" + Environment.NewLine
            End If
            If currentNodeElement.DANodeElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(OpcBrowseNodeElement.DANodeElement)}: {currentNodeElement.DANodeElement}" + Environment.NewLine
            End If
        End Sub

        Private Sub opcBrowseControl1_BrowseFailure(sender As Object, e As FailureEventArgs) Handles opcBrowseControl1.BrowseFailure
            ' Append the event name and its arguments to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(OpcBrowseControl.BrowseFailure)}: {e}" + Environment.NewLine
        End Sub

        Private Sub opcBrowseControl1_CurrentNodeChanged(sender As Object, e As EventArgs) Handles opcBrowseControl1.CurrentNodeChanged
            ' Append the event name and the current node element to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(OpcBrowseControl.CurrentNodeChanged)}; {opcBrowseControl1.Outputs.CurrentNodeElement}" + Environment.NewLine
        End Sub

        Private Sub opcBrowseControl1_NodeDoubleClick(sender As Object, e As EventArgs) Handles opcBrowseControl1.NodeDoubleClick
            ' Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(OpcBrowseControl.NodeDoubleClick)}" + Environment.NewLine
        End Sub

        Private Sub opcBrowseControl1_SelectionChanged(sender As Object, e As EventArgs) Handles opcBrowseControl1.SelectionChanged
            ' Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(OpcBrowseControl.SelectionChanged)}" + Environment.NewLine
        End Sub

        Private Sub setInputsButton_Click(sender As Object, e As EventArgs) Handles setInputsButton.Click
            ' Set the current node to a pre-defined OPC DA item on our server.
            opcBrowseControl1.InputsOutputs.CurrentNodeDescriptor.ServerDescriptor = "OPCLabs.KitServer.2"
            opcBrowseControl1.InputsOutputs.CurrentNodeDescriptor.DANodeDescriptor = "Demo.Ramp"
        End Sub
    End Class
End Namespace
#End Region