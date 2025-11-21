' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
'using System.Linq;

Imports OpcLabs.EasyOpc.DataAccess.OperationModel ' ReSharper disable CheckNamespace
Namespace SubscribeToMany
    ' ReSharper restore CheckNamespace

    Partial Public Class Form1
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub

        Private _changeCount As Integer
        Private _startTickCount As Integer

        Private Function GetElapsedTime() As Double
            Return (Environment.TickCount - _startTickCount) / 1000.0
        End Function

        ' ReSharper disable InconsistentNaming
        Private Sub startButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles startButton.Click
            ' ReSharper restore InconsistentNaming

            Dim numberOfItems As Integer = CInt(Fix(numberOfItemsNumericUpDown.Value))

            startButton.Enabled = False
            numberOfItemsNumericUpDown.Enabled = False

            Dim argumentArray = New DAItemGroupArguments(numberOfItems - 1) {}
            For i As Integer = 0 To numberOfItems - 1
                Dim listViewItem = New ListViewItem()
                valuesListView.Items.Add(listViewItem)
                Dim copy As Integer = (i \ 100) + 1
                Dim phase As Integer = i Mod 100
                Dim itemId As String = String.Format("Simulation.Incrementing.Copy_{0}.Phase_{1}", copy, phase)
                argumentArray(i) = New DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 50, listViewItem)
            Next i

            _changeCount = 0
            _startTickCount = Environment.TickCount
            timer1.Start()

            easyDAClient1.SubscribeMultipleItems(argumentArray)

        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub easyDAClient1_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs) Handles easyDAClient1.ItemChanged
            ' ReSharper restore InconsistentNaming

            _changeCount += 1

            Dim listViewItem = CType(e.Arguments.State, ListViewItem)
            Dim itemText As String
            If e.Exception IsNot Nothing Then
                itemText = "*Error*"
            Else
                itemText = e.Vtq.DisplayValue()
            End If
            listViewItem.Text = itemText
        End Sub

        ' ReSharper disable InconsistentNaming
        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
            ' ReSharper restore InconsistentNaming

            Dim elapsedTime As Double = GetElapsedTime()
            ' ReSharper disable CompareOfFloatsByEqualityOperator
            If elapsedTime = 0.0 Then
                ' ReSharper restore CompareOfFloatsByEqualityOperator
                Return
            End If

            Dim changeCount As Integer = _changeCount
            Dim changesPerSecond As Double = changeCount / elapsedTime

            changeCountTextBox.Text = changeCount.ToString()
            elapsedTimeTextBox.Text = String.Format("{0:0.0}", elapsedTime)
            changesPerSecondTextBox.Text = String.Format("{0:0.0}", changesPerSecond)
        End Sub
    End Class
End Namespace
