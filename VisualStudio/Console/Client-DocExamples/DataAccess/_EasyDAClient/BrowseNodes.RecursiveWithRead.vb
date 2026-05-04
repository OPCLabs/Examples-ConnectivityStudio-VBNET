' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Recursively browses and displays the nodes in the OPC address space, and attempts to read and display values of all OPC 
' items it finds.
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
        Private Const ServerClass As String = "OPCLabs.KitServer.2"

        Private Shared ReadOnly Client As New EasyDAClient()

        Private Shared Sub BrowseAndReadFromNode(parentItemId As String)
            ' Obtain all node elements under parentItemId
            Dim browseParameters = New DABrowseParameters() ' no filtering whatsoever
            Dim nodeElementCollection As DANodeElementCollection = Client.BrowseNodes("", ServerClass, parentItemId, browseParameters)
            ' Remark: that BrowseNodes(...) may also throw OpcException; a production code should contain handling for it, here 
            ' omitted for brevity.

            For Each nodeElement As DANodeElement In nodeElementCollection
                Debug.Assert(nodeElement IsNot Nothing)

                ' If the node is a leaf, it might be possible to read from it
                If nodeElement.IsLeaf Then
                    ' Determine what the display - either the value read, or exception message in case of failure.
                    Dim display As String
                    Try
                        Dim value As Object = Client.ReadItemValue("", ServerClass, nodeElement.ItemId)
                        display = String.Format("{0}", value)
                    Catch exception As OpcException
                        display = String.Format("** {0} **", exception.GetBaseException().Message)
                    End Try

                    Console.WriteLine("{0} -> {1}", nodeElement.ItemId, display)
                    ' If the node is not a leaf, just display its itemId
                Else
                    Console.WriteLine("{0}", nodeElement.ItemId)
                End If

                ' If the node is a branch, browse recursively into it.
                If nodeElement.IsBranch AndAlso (nodeElement.ItemId <> "SimulateEvents") Then ' this branch is too big for the purpose of this example
                    BrowseAndReadFromNode(nodeElement.ItemId)
                End If
            Next nodeElement
        End Sub

        Shared Sub RecursiveWithRead()
            Console.WriteLine("Browsing and reading values...")
            ' Set timeout to only wait 1 second - default would be 1 minute to wait for good quality that may never come.
            Client.InstanceParameters.Timeouts.ReadItem = 1000

            ' Do the actual browsing and reading, starting from root of OPC address space (denoted by empty string for itemId)
            Try
                BrowseAndReadFromNode("")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try
        End Sub
    End Class
End Namespace
#End Region
