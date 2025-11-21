' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows information available about OPC event condition.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents._AEConditionElement

    Friend Class Properties
        Private Shared Sub DumpSubconditionNames(ByVal subconditionNames As IEnumerable(Of String))
            For Each name As String In subconditionNames
                Console.WriteLine("            {0}", name)
            Next name
        End Sub

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

                Console.WriteLine("Category {0}:", categoryElement)
                For Each conditionElement As AEConditionElement In categoryElement.ConditionElements
                    Debug.Assert(conditionElement IsNot Nothing)

                    Console.WriteLine("    Information about condition ""{0}"":", conditionElement)
                    Console.WriteLine("        .Name: {0}", conditionElement.Name)
                    Console.WriteLine("        .SubconditionNames:")
                    DumpSubconditionNames(conditionElement.SubconditionNames)
                Next conditionElement
            Next categoryElement
        End Sub
    End Class

End Namespace
#End Region
