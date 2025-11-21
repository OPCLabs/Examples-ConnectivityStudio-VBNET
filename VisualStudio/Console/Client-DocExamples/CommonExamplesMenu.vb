' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.DocExamples

    Public Class CommonExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                {
                    AddressOf _ServerCategories.General.Main1,
                    AddressOf _ServerElement.General.Main1
                }

            Dim actionList = New List(Of Action)(actionArray)
            Dim cont As Boolean
            Do
                Console.WriteLine()
                cont = ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList)
                If cont Then
                    Console.WriteLine("Press Enter to continue...")
                    Console.ReadLine()
                End If
            Loop While cont
        End Sub
    End Class

End Namespace
