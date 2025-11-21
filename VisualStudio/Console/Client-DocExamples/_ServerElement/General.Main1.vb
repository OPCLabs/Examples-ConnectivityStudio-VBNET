' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable CommentTypo
' ReSharper disable StringLiteralTypo
#Region "Example"
' This example shows all information available about OPC servers.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples._ServerElement
    Partial Friend Class General
        Shared Sub Main1()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Dim serverElements As ServerElementCollection
            Try
                serverElements = client.BrowseServers("")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each serverElement In serverElements
                Console.WriteLine($"Information about server ""{serverElement}"":")
                Console.WriteLine($"    .ServerClass: {serverElement.ServerClass}")
                Console.WriteLine($"    .ClsidString: {serverElement.ClsidString}")
                Console.WriteLine($"    .ProgId: {serverElement.ProgId}")
                Console.WriteLine($"    .Description: {serverElement.Description}")
                Console.WriteLine($"    .Vendor: {serverElement.Vendor}")
                Console.WriteLine($"    .ServerCategories: {serverElement.ServerCategories}")
                Console.WriteLine($"    .VersionIndependentProgId: {serverElement.VersionIndependentProgId}")
            Next serverElement

        End Sub

        ' Example output
        '
        'Information about server "opcda:OPCLabs.KitServer.2/%7Bc8a12f17-1e03-401e-b53d-6c654dd576da%7D":
        '    .ServerClass OPCLabs.KitServer.2
        '    .ClsidString: c8a12f17-1e03-401e-b53d-6c654dd576da
        '    .ProgId: OPCLabs.KitServer.2
        '    .Description: OPC Labs Kit Server
        '    .Vendor: OPC Labs, http : 'www.opclabs.com
        '    .ServerCategories: (OpcDataAccess10, OpcDataAccess20, OpcDataAccess30)
        '    .VersionIndependentProgId: OPCLabs.KitServer
    End Class
End Namespace
#End Region
