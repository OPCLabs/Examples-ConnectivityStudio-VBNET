' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.UADocExamples.Specialized
    Public Class SpecializedExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf UnifiedAutomation_UaSdkNetBundle.RegisterAndUnregisterNodes
            }

            Dim actionList = New List(Of Action)(actionArray)

            Do
                Console.WriteLine()
                If Not ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList) Then
                    Exit Do
                End If

                Console.WriteLine("Press Enter to continue...")
                Console.ReadLine()
            Loop While True
        End Sub
    End Class
End Namespace
' ReSharper restore CheckNamespace
