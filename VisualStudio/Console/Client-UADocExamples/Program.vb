' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
Imports OpcLabs.BaseLib.Console

Namespace Global.UADocExamples
    Friend Class Program
        Shared Sub Main()
            Console.OutputEncoding = Text.Encoding.UTF8

            Dim action As Action
            Do
                Console.WriteLine()
                action = ConsoleDialog.SelectItem("Select example group", "Return", New Dictionary(Of Action, String) From
            {
                {AddressOf UAExamplesMenu.Main1, "Main"},
                {AddressOf AlarmsAndConditions.AlarmsAndConditionsExamplesMenu.Main1, "Alarms And Conditions"},
                {AddressOf ComplexData.ComplexDataExamplesMenu.Main1, "Complex Data"},
                {AddressOf FileProviders.FileProvidersExamplesMenu.Main1, "File Providers"},
                {AddressOf FileTransfer.FileTransferExamplesMenu.Main1, "File Transfer"},
                {AddressOf Gds.GdsExamplesMenu.Main1, "GDS"},
                {AddressOf Interaction.InteractionExamplesMenu.Main1, "Interaction"},
                {AddressOf Licensing.LicensingExamplesMenu.Main1, "Licensing"},
                {AddressOf Specialized.SpecializedExamplesMenu.Main1, "Specialized"}
            })
                If action IsNot Nothing Then
                    action()
                End If
            Loop While action IsNot Nothing
        End Sub
    End Class
End Namespace