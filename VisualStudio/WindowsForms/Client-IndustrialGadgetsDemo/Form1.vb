
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

' ReSharper disable CheckNamespace
Namespace IndustrialGadgetsDemo
    ' ReSharper restore CheckNamespace

    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub richTextBox1_LinkClicked(ByVal sender As Object, ByVal e As LinkClickedEventArgs) Handles richTextBox1.LinkClicked
            ' ReSharper restore InconsistentNaming
            Process.Start(e.LinkText)
        End Sub
    End Class
End Namespace
