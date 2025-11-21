' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

Imports DocExamples.AlarmsAndEvents
Imports DocExamples.DataAccess
Imports DocExamples.DataAccess.Xml
Imports OpcLabs.BaseLib.Console

Namespace Global.DocExamples

    Friend Class Program
        <MTAThread> ' needed for COM security initialization to succeed
        Shared Sub Main()
            Dim action As Action
            Do
                Console.WriteLine()
                action = ConsoleDialog.SelectItem("Select OPC specification", "Return", New Dictionary(Of Action, String) From
            {
                {Sub()
                     CommonExamplesMenu.Main1()
                 End Sub _
               , "Common examples"},
                {Sub()
                     AEExamplesMenu.Main1()
                 End Sub _
               , "OPC Alarms&Events"},
                {Sub()
                     DAExamplesMenu.Main1()
                 End Sub _
               , "OPC Data Access"},
                {AddressOf XmlExamplesMenu.Main1, "OPC XML-DA"}
            })
                If Not IsNothing(action) Then
                    action()
                End If
            Loop While Not IsNothing(action)
        End Sub
    End Class
End Namespace