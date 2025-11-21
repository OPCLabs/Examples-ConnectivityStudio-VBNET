' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' This example shows how to implement an HMI screen by storing OPC "Classic" Item IDs in the Tag property of screen
' controls, And animate the controls by subscribing to all items at once. Also shows a possibility how to write to an OPC
' item from the screen.
'
' Note that the Live Binding programming model can provide similar - And more - features, without a need for coding.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess

Imports OpcLabs.EasyOpc.DataAccess.OperationModel
Imports OpcLabs.EasyOpc.OperationModel ' ReSharper disable CheckNamespace
Namespace HmiScreen

    Partial Public Class Form1
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            easyDAClient1.UnsubscribeAllItems()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' We have configured the read-only controls on the form in the designer by specifying the ItemIDs of the items
            ' they should subscribe to And display in their Tag properties.

            Dim argumentsList = New List(Of DAItemGroupArguments)()
            For Each control As Control In Controls
                Dim itemId = TryCast(control.Tag, String)
                If itemId IsNot Nothing Then
                    ' The State argument of the subscription will be the reference to the control itself.
                    argumentsList.Add(New DAItemGroupArguments("", "OPCLabs.KitServer.2", itemId, 50, control))
                End If
            Next control

            ' Subscribe to the assembled list.
            easyDAClient1.SubscribeMultipleItems(argumentsList.ToArray())
        End Sub

        Private Sub easyDAClient1_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs) Handles easyDAClient1.ItemChanged
            ' The State argument in the incoming notification now holds the reference to the control that should be
            ' updated.

            Dim textBox = TryCast(e.Arguments.State, TextBox)
            If (textBox IsNot Nothing) AndAlso textBox.ReadOnly Then
                If e.Exception Is Nothing Then
                    textBox.Text = e.Vtq.DisplayValue()
                Else
                    textBox.Text = "** Error **"
                End If
            End If
        End Sub

        Private Sub writeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles writeButton.Click
            ' We have configured the writable control on the form in the designer by specifying the ItemID of the item it
            ' should write to in its Tag property.

            Dim textBox As TextBox = writeValueTextBox
            Try
                easyDAClient1.WriteItemValue("", "OPCLabs.KitServer.2", CStr(textBox.Tag), textBox.Text)
            Catch ex As OpcException
                Console.Beep()
            End Try
        End Sub
    End Class
End Namespace
