Imports JetBrains.Annotations

Namespace Global.WindowsService1
    Partial Public Class Service1
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.easyDAClient1 = New OpcLabs.EasyOpc.DataAccess.EasyDAClient(Me.components)
            ' 
            ' easyDAClient1
            ' 
            '			Me.easyDAClient1.ItemChanged += New System.EventHandler(Of OpcLabs.EasyOpc.DataAccess.EasyDAItemChangedEventArgs)(Me.easyDAClient1_ItemChanged)
            ' 
            ' Service1
            ' 
            Me.ServiceName = "Service1"

        End Sub

#End Region

        Private WithEvents easyDAClient1 As OpcLabs.EasyOpc.DataAccess.EasyDAClient
    End Class
End Namespace