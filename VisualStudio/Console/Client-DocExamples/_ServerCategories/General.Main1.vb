' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable CommentTypo
' ReSharper disable StringLiteralTypo
#Region "Example"
' This example shows all information available about categories that particular OPC servers do support.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples._ServerCategories
    Partial Friend Class General
        Shared Sub Main1()
            ' Instantiate the OPC-DA client object.
            Dim daClient = New EasyDAClient()

            Console.WriteLine()
            Console.WriteLine("OPC DATA ACCESS")
            Dim daServerElements As ServerElementCollection
            Try
                daServerElements = daClient.BrowseServers("")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try
            DumpServerElements(daServerElements)

            ' Instantiate the OPC-A&E client object.
            Dim aeClient = New EasyAEClient()

            Console.WriteLine()
            Console.WriteLine("OPC ALARMS AND EVENTS")
            Dim aeServerElements As ServerElementCollection
            Try
                aeServerElements = aeClient.BrowseServers("")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try
            DumpServerElements(aeServerElements)
        End Sub

        Private Shared Sub DumpServerElements(serverElements As ServerElementCollection)
            For Each serverElement In serverElements
                Console.WriteLine($"Categories of ""{serverElement.ProgId}"":")
                Dim serverCategories As ServerCategories = serverElement.ServerCategories
                Console.WriteLine($"    .OpcAlarmsAndEvents10: {serverCategories.OpcAlarmsAndEvents10}")
                Console.WriteLine($"    .OpcDataAccess10: {serverCategories.OpcDataAccess10}")
                Console.WriteLine($"    .OpcDataAccess20: {serverCategories.OpcDataAccess20}")
                Console.WriteLine($"    .OpcDataAccess30: {serverCategories.OpcDataAccess30}")
                Console.WriteLine($"    .ToString(): {serverCategories}")
            Next serverElement
        End Sub

        ' Example output
        '
        'OPC DATA ACCESS
        'Categories of "OPCLabs.KitServer.2":
        '    .OpcAlarmsAndEvents10 False
        '    .OpcDataAccess10: True
        '    .OpcDataAccess20: True
        '    .OpcDataAccess30: True
        '    .ToString(): (OpcDataAccess10, OpcDataAccess20, OpcDataAccess30)
        '
        'OPC ALARMS AND EVENTS
        'Categories of "OPCLabs.KitEventServer.2":
        '    .OpcAlarmsAndEvents10 True
        '    .OpcDataAccess10: False
        '    .OpcDataAccess20: False
        '    .OpcDataAccess30: False
        '    .ToString(): (OpcAlarmsAndEvents10)
    End Class
End Namespace
#End Region
