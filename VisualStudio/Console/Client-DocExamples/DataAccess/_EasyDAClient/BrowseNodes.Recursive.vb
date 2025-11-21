' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to recursively browse the nodes in the OPC address space.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class BrowseNodes

        Shared Sub Recursive()
            Dim stopwatch = New Stopwatch()
            stopwatch.Start()

            Dim client = New EasyDAClient()
            _branchCount = 0
            _leafCount = 0

            Try
                BrowseFromNode(client, "OPCLabs.KitServer.2", "")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            stopwatch.Stop()
            Console.WriteLine("Browsing has taken (milliseconds): {0}", stopwatch.ElapsedMilliseconds)
            Console.WriteLine("Branch count: {0}", _branchCount)
            Console.WriteLine("Leaf count: {0}", _leafCount)
        End Sub

        Private Shared Sub BrowseFromNode( _
            client As EasyDAClient,
            serverDescriptor As ServerDescriptor,
            parentNodeDescriptor As DANodeDescriptor)

            Debug.Assert(client IsNot Nothing)
            Debug.Assert(serverDescriptor IsNot Nothing)
            Debug.Assert(parentNodeDescriptor IsNot Nothing)

            ' Obtain all node elements under parentNodeDescriptor
            Dim browseParameters = New DABrowseParameters() ' no filtering whatsoever
            Dim nodeElementCollection As DANodeElementCollection =
                client.BrowseNodes(serverDescriptor, parentNodeDescriptor, browseParameters)
            ' Remark: that BrowseNodes(...) may also throw OpcException; a production code should contain handling for 
            ' it, here omitted for brevity.

            For Each nodeElement As DANodeElement In nodeElementCollection
                Debug.Assert(nodeElement IsNot Nothing)

                Console.WriteLine(nodeElement)

                ' If the node is a branch, browse recursively into it.
                If nodeElement.IsBranch Then
                    _branchCount += 1
                    BrowseFromNode(client, serverDescriptor, nodeElement)
                Else
                    _leafCount += 1
                End If
            Next nodeElement
        End Sub

        Private Shared _branchCount As Integer
        Private Shared _leafCount As Integer
    End Class
End Namespace
#End Region
