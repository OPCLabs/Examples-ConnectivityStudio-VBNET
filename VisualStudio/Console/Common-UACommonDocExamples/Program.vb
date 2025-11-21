' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
Imports OpcLabs.BaseLib.Console

Namespace Global.UACommonDocExamples

    Friend Class Program
        Shared Sub Main()
            Console.OutputEncoding = Text.Encoding.UTF8

            Dim action As Action
            Do
                Console.WriteLine()
                action = ConsoleDialog.SelectItem("Select example group", "Return", New Dictionary(Of Action, String) From
                {
                    {AddressOf UACommonExamplesMenu.Main1, "Main"},
                    {AddressOf Application.ApplicationExamplesMenu.Main1, "Application"}
                })
                If action IsNot Nothing Then
                    action()
                End If
            Loop While action IsNot Nothing
        End Sub
    End Class
End Namespace