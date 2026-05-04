' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain all branches at the root of the address space. For each branch, it displays whether 
' it may have child nodes.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class BrowseBranches
        Shared Sub Main1Xml()
            Dim client = New EasyDAClient()

            Dim branchElements As DANodeElementCollection
            Try
                branchElements = client.BrowseBranches("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx")
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
        'BranchElements("Static").HasChildren: True
        'BranchElements("Dynamic").HasChildren: True

    End Class
End Namespace
#End Region
