' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Public Class Form1

    ' ReSharper disable InconsistentNaming
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        ' ReSharper restore InconsistentNaming

        Try
            TextBox1.Text = EasyDAClient1.ReadItemValue("", "OPCLabs.KitServer.2", "Demo.Single")
        Catch opcException As OpcException
            TextBox1.Text = $"*** {opcException.Message}"
        End Try
    End Sub
End Class
