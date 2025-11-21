
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

' ReSharper disable InconsistentNaming
Option Explicit On
Option Strict On

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Public Class Form1

    ' Called whenever one or more subscribed items change
    Private Shared Sub EasyDAClient1_ItemChanged(sender As Object, e As EasyDAItemChangedEventArgs) Handles EasyDAClient1.ItemChanged
        ' We have passed the reference to the associated ListVeiwItem in a State argument when subscribing
        Dim listViewItem As ListViewItem = CType(e.Arguments.State, ListViewItem)
        UpdateListViewItem(listViewItem, e.Exception, e.Vtq)
    End Sub

    ' Updates ListViewItem (from reading or change notification) with exception and value/timestamp/quality
    Private Shared Sub UpdateListViewItem(ByVal listViewItem As ListViewItem, ByVal exception As Exception, ByVal vtq As DAVtq)
        Dim vtqText As String = ""
        Dim exceptionText As String = ""

        If (Exception Is Nothing) Then
            vtqText = vtq.ToString()
        Else
            exceptionText = Exception.Message
        End If

        listViewItem.SubItems(1).Text = vtqText
        listViewItem.SubItems(2).Text = exceptionText
    End Sub

    ' IsSubscribed setter takes care of enabling/disabling the user interface elements
    Protected Property IsSubscribed As Boolean
        Get
            Return _subscribed
        End Get
        Set(value As Boolean)
            Dim oldValue As Boolean = _subscribed
            _subscribed = value
            If (_subscribed <> oldValue) Then
                UpdateButtons()
            End If
        End Set
    End Property

    Private Sub ReadButton_Click(sender As Object, e As EventArgs) Handles ReadButton.Click
        ' Prepare an array describing what we want to read and how
        Dim count As Integer = ItemsListView.Items.Count
        Dim argumentsArray(count - 1) As DAReadItemArguments
        For i As Integer = 0 To count - 1
            Dim listViewItem As ListViewItem = ItemsListView.Items(i)
            Dim itemId As String = listViewItem.Text
            argumentsArray(i) = New DAReadItemArguments("OPCLabs.KitServer.2", itemId, 50)
        Next

        ' Execute the Read operation
        Dim resultsArray() As DAVtqResult = EasyDAClient1.ReadMultipleItems(argumentsArray)

        ' Go through the result array, and update the list view items
        For i As Integer = 0 To count - 1
            UpdateListViewItem(ItemsListView.Items(i), resultsArray(i).Exception, resultsArray(i).Vtq)
        Next
    End Sub

    Private Sub SubscribeButton_Click(sender As Object, e As EventArgs) Handles SubscribeButton.Click
        IsSubscribed = True

        ' Prepare an array describing what we want to subscribe to and how
        Dim count As Integer = ItemsListView.Items.Count
        Dim argumentsArray(count - 1) As DAItemGroupArguments
        For i As Integer = 0 To count - 1
            Dim listViewItem As ListViewItem = ItemsListView.Items(i)
            Dim itemId As String = listViewItem.Text
            ' We use the last argument (State) to provide a reference to the associated ListViewItem
            argumentsArray(i) = New DAItemGroupArguments("OPCLabs.KitServer.2", itemId, 100, listViewItem)
        Next

        ' Execute the Subscribe operation
        EasyDAClient1.SubscribeMultipleItems(argumentsArray)
    End Sub

    Private Sub UnsubscribeButton_Click(sender As Object, e As EventArgs) Handles UnsubscribeButton.Click
        IsSubscribed = False

        ' Execute the Unsubscribe operation
        EasyDAClient1.UnsubscribeAllItems()
    End Sub

    Private Sub UpdateButtons()
        ReadButton.Enabled = Not IsSubscribed
        SubscribeButton.Enabled = Not IsSubscribed
        UnsubscribeButton.Enabled = IsSubscribed
    End Sub

    Private _subscribed As Boolean
End Class
' ReSharper restore InconsistentNaming
