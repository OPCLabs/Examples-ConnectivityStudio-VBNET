
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

' ReSharper disable CheckNamespace
Namespace LiveBindingDemo
    ' ReSharper restore CheckNamespace

    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub tabPage3_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles tabPage3.Enter
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = ""
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub tabPage2_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles tabPage2.Enter
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = ""
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub tabPage1_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles tabPage1.Enter
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = ""
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub tabPage4_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles tabPage4.Enter
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = ""
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub getOnEventLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles getOnEventLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_getOnEventLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub automaticGetLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles automaticGetLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_automaticGetLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub automaticSubscriptionLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles automaticSubscriptionLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_automaticSubscriptionLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub automaticReadLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles automaticReadLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_automaticReadLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub readOnCustomEventLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles readOnCustomEventLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_readOnCustomEventLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub errorProviderLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles errorProviderLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_errorProviderLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub stringFormattingLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles stringFormattingLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_stringFormattingLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub changeBackgroundLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles changeBackgroundLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_changeBackgroundLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub bindingKindsLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles bindingKindsLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_bindingKindsLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub extendersLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles extendersLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_extendersLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub subscribeAndWriteLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles subscribeAndWriteLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_subscribeAndWriteLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub writeSingleLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles writeSingleLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_writeSingleLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub writeGroupLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles writeGroupLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_writeGroupLinkLabel_LinkClicked_HelpText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub displayWriteErrorsLinkLabel_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles displayWriteErrorsLinkLabel.LinkClicked
            ' ReSharper restore InconsistentNaming
            helpRichTextBox.Text = My.Resources.Form1_displayWriteErrorsLinkLabel_LinkClicked_HelpText
        End Sub
    End Class
End Namespace
