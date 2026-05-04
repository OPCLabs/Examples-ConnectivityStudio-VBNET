' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain all nodes under the "Simulation" branch of the address space. For each node, it displays
' whether the node is a branch or a leaf.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class BrowseNodes
        Shared Sub Main1()
            Dim client = New EasyDAClient()

            Dim nodeElements As DANodeElementCollection
            Try
                nodeElements = client.BrowseNodes("", "OPCLabs.KitServer.2", "Greenhouse", DABrowseParameters.Default)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each nodeElement In nodeElements
                Console.WriteLine($"NodeElements(""{nodeElement.Name}""):")
                Console.WriteLine($"    .IsBranch: {nodeElement.IsBranch}")
                Console.WriteLine($"    .IsLeaf: {nodeElement.IsLeaf}")
            Next nodeElement

        End Sub
    End Class
End Namespace
#End Region
