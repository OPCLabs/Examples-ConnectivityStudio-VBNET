' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to allow browsing for an OPC Unified Architecture node by placing a browsing control on the form.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Forms.Browsing
Imports OpcLabs.BaseLib

Namespace UAFormsDocExamples._UABrowseControl
    Public Class UsageForm
        Private Sub getOutputsButton_Click(sender As Object, e As EventArgs) Handles getOutputsButton.Click
            ' Obtain the current node element.
            Dim currentNodeElement As UABrowseNodeElement = uaBrowseControl1.Outputs.CurrentNodeElement

            ' Display the present parts of the current node element in the outputs text text box.
            outputsTextBox.Text = ""
            If currentNodeElement.HostElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(UABrowseNodeElement.HostElement)}: {currentNodeElement.HostElement}" + Environment.NewLine
            End If
            If currentNodeElement.DiscoveryElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(UABrowseNodeElement.DiscoveryElement)}: {currentNodeElement.DiscoveryElement}" + Environment.NewLine
            End If
            If currentNodeElement.NodeElement IsNot Nothing Then
                outputsTextBox.Text += $"{NameOf(UABrowseNodeElement.NodeElement)}: {currentNodeElement.NodeElement}" + Environment.NewLine
            End If
        End Sub

        Private Sub uaBrowseControl1_BrowseFailure(sender As Object, e As FailureEventArgs) Handles uaBrowseControl1.BrowseFailure
            ' Append the event name and its arguments to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(UABrowseControl.BrowseFailure)}: {e}" + Environment.NewLine
        End Sub

        Private Sub uaBrowseControl1_CurrentNodeChanged(sender As Object, e As EventArgs) Handles uaBrowseControl1.CurrentNodeChanged
            ' Append the event name and the current node element to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(UABrowseControl.CurrentNodeChanged)}; {uaBrowseControl1.Outputs.CurrentNodeElement}" + Environment.NewLine
        End Sub

        Private Sub uaBrowseControl1_NodeDoubleClick(sender As Object, e As EventArgs) Handles uaBrowseControl1.NodeDoubleClick
            ' Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(UABrowseControl.NodeDoubleClick)}" + Environment.NewLine
        End Sub

        Private Sub uaBrowseControl1_SelectionChanged(sender As Object, e As EventArgs) Handles uaBrowseControl1.SelectionChanged
            ' Append the event name to the browsing events text box.
            browsingEventsTextBox.Text += $"{NameOf(UABrowseControl.SelectionChanged)}" + Environment.NewLine
        End Sub

        Private Sub setInputsButton_Click(sender As Object, e As EventArgs) Handles setInputsButton.Click
            ' Set the current node to our pre-defined OPC UA server.
            uaBrowseControl1.InputsOutputs.CurrentNodeDescriptor.EndpointDescriptor = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
        End Sub
    End Class
End Namespace
#End Region