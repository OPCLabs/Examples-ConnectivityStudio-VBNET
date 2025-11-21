' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain all branches at the root of the address space. For each branch, it displays whether 
' it may have child nodes.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class BrowseBranches
        Shared Sub Main1()
            Dim client = New EasyDAClient()

            Dim branchElements As DANodeElementCollection
            Try
                branchElements = client.BrowseBranches("", "OPCLabs.KitServer.2", "")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each branchElement In branchElements
                Console.WriteLine($"BranchElements(""{branchElement.Name}"").HasChildren: {branchElement.HasChildren}")
            Next branchElement

        End Sub

        ' Example output
        '
        'BranchElements("$ServerControl").HasChildren: True
        'BranchElements("Boilers").HasChildren: True
        'BranchElements("Simulation").HasChildren: True
        'BranchElements("SimulateEvents").HasChildren: True
        'BranchElements("Trends").HasChildren: True
        'BranchElements("Demo").HasChildren: True
        'BranchElements("Greenhouse").HasChildren: True

    End Class
End Namespace
#End Region
