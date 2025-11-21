' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

Imports OpcLabs.BaseLib.Console
Imports SparkplugDocExamples.Consumer
Imports SparkplugDocExamples.EdgeNode
Imports SparkplugDocExamples.Licensing

Namespace Global.SparkplugDocExamples
    Public Class SparkplugExamplesMenu
        Shared Sub Main1()
            Dim action As Action
            Do
                Console.WriteLine()
                action = ConsoleDialog.SelectItem("Select example group", "Return", New Dictionary(Of Action, String) From
            {
                {AddressOf ConsumerExamplesMenu.Main1, "Consumer"},
                {AddressOf EdgeNodeExamplesMenu.Main1, "EdgeNode"},
                {AddressOf LicensingExamplesMenu.Main1, "Licensing"}
            })
                If action IsNot Nothing Then
                    action()
                End If
            Loop While action IsNot Nothing

        End Sub
    End Class
End Namespace

