
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

' ReSharper disable InconsistentNaming

Partial Public Class _Default
    Inherits Page

    ' Use a shared client instance to allow for better optimization.
    Shared ReadOnly Client As New EasyDAClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim branchElements = Client.BrowseBranches("", "OPCLabs.KitServer.2")

            For Each branchElement In branchElements
                ListBox1.Items.Add(branchElement.ItemId)
            Next
            ListBox1.Visible = True

        Catch ex As OpcException
            TextBox1.Text = ex.GetBaseException.Message
            TextBox1.Visible = True
        End Try

    End Sub
End Class