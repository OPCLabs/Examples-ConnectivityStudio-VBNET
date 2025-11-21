' WindowsService1: A Windows Service that subscribes to items from the simulation server, and logs their changes into 
' a file.
'
' Install the service by running:
'      C:\Windows\Microsoft.NET\Framework\v2.0.50727\InstallUtil.exe /i WindowsService1.exe
' If you get "Access denied" error when starting the service, change its configuration to run under Local System account.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.IO
Imports System.ServiceProcess
Imports JetBrains.Annotations
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.WindowsService1
    Partial Public Class Service1
        Inherits ServiceBase

        Private Const FilePath As String = "C:\Service1.txt"

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnStart(ByVal args() As String)
            File.Create(FilePath).Close()

            easyDAClient1.SubscribeMultipleItems(New DAItemGroupArguments() {
            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Incrementing (1 s)", 100, Nothing),
            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Ramp (10 s)", 1000, Nothing),
            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BSTR", 1000, Nothing),
            New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Simulation.Register_BOOL", 1000, Nothing)
        })
        End Sub

        Protected Overrides Sub OnStop()
            easyDAClient1.UnsubscribeAllItems()
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub easyDAClient1_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs) Handles easyDAClient1.ItemChanged
            ' ReSharper restore InconsistentNaming
            Dim line As String
            If e.Exception Is Nothing Then
                Trace.Assert(e.Vtq IsNot Nothing)
                line = String.Format("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq.DisplayValue())
            Else
                line = String.Format("{0}: ** {1} **", e.Arguments.ItemDescriptor.ItemId, e.Exception.GetBaseException())
            End If

            Using textWriter = File.AppendText(FilePath)
                textWriter.WriteLine(line)
            End Using
        End Sub
    End Class
End Namespace