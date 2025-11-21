' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports System.Web.UI.WebControls
Imports OpcLabs.EasyOpc.DataAccess.Extensions
Imports OpcLabs.EasyOpc.OperationModel

' ReSharper disable InconsistentNaming
' ReSharper once UnusedMember.Global

Partial Public Class _Default
    Inherits UI.Page

    Shared Sub New()
        ' Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read requests.
        Client.TryEnableAutoSubscribingOptimization()
    End Sub

    ' Use a shared client instance to allow for better optimization.
    Shared ReadOnly Client As New EasyDAClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Read(TextBox1, "Simulation.Ramp (10 s)")
        Read(TextBox2, "Simulation.Random")
        Read(TextBox3, "Simulation.Incrementing (1 s)")
    End Sub

    Protected Sub Read(ByVal textBox As TextBox, ByVal itemId As String)
        Try
            textBox.Text = Client.ReadItemValue("", "OPCLabs.KitServer.2", itemId).ToString()
        Catch e As OpcException
            textBox.Text = "***" & e.GetBaseException().Message
        End Try
    End Sub
End Class