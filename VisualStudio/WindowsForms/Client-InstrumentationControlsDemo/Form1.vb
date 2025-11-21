
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel ' ReSharper disable CheckNamespace

Namespace InstrumentationControlsDemo
    ' ReSharper restore CheckNamespace

    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' ReSharper restore InconsistentNaming
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Ramp 0:100 (10 s)", 100, Nothing, 0)
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Sine -100:100 (10 s)", 100, Nothing, 1)
            easyDAClient1.SubscribeItem("", "OPCLabs.KitServer.2", "Simulation.Register_R8", 100, Nothing, 2)
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub easyDAClient1_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs) Handles easyDAClient1.ItemChanged
            ' ReSharper restore InconsistentNaming
            Dim index = CInt(Fix(e.Arguments.State))
            If (e.Vtq IsNot Nothing) AndAlso (e.Vtq.HasValue()) Then
                plot1.Channels(index).AddXY(e.Vtq.Timestamp, CDbl(e.Vtq.Value))
            End If
        End Sub

        ' ReSharper disable once MemberCanBeMadeStatic.Local
        ' ReSharper disable InconsistentNaming
        Private Sub richTextBox1_LinkClicked(ByVal sender As Object, ByVal e As LinkClickedEventArgs) Handles richTextBox1.LinkClicked
            ' ReSharper restore InconsistentNaming
            Process.Start(e.LinkText)
        End Sub
    End Class
End Namespace
