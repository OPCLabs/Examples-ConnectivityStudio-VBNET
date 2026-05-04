' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows information available about OPC event category.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents._AECategoryElement

    Friend Class Properties
        Public Shared Sub Main1()
            Dim client = New EasyAEClient()

            Dim categoryElements As AECategoryElementCollection
            Try
                categoryElements = client.QueryEventCategories("", "OPCLabs.KitEventServer.2")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each categoryElement As AECategoryElement In categoryElements
                Debug.Assert(categoryElement IsNot Nothing)

                Console.WriteLine("Information about category {0}:", categoryElement)
                Console.WriteLine("    .CategoryId: {0}", categoryElement.CategoryId)
                Console.WriteLine("    .Description: {0}", categoryElement.Description)
                Console.WriteLine("    .ConditionElements:")
                If categoryElement.ConditionElements.Keys IsNot Nothing Then
                    For Each conditionKey As String In categoryElement.ConditionElements.Keys
                        Console.WriteLine("        {0}", conditionKey)
                    Next conditionKey
                End If
                Console.WriteLine("    .AttributeElements:")
                If categoryElement.AttributeElements.Keys IsNot Nothing Then
                    For Each attributeKey As Long In categoryElement.AttributeElements.Keys
                        Console.WriteLine("        {0}", attributeKey)
                    Next attributeKey
                End If
            Next categoryElement
        End Sub
    End Class

End Namespace
#End Region
