' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how the current node and selected nodes can be persisted between dialog invocations.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Forms.Browsing

Namespace UAFormsDocExamples._UABrowseDialog
    Partial Friend Class ShowDialog
        Shared Sub SelectionDescriptors(owner As IWin32Window)
            ' The variables that persist the current and selected nodes.

            Dim currentNodeDescriptor = New UABrowseNodeDescriptor()
            Dim selectionDescriptors = New UABrowseNodeDescriptorCollection()

            ' The initial current node (optional).

            currentNodeDescriptor.EndpointDescriptor = "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"

            ' Repeatedly show the dialog until the user cancels it.

            Do
                Dim browseDialog = New UABrowseDialog()
                browseDialog.Mode.MultiSelect = True

                ' Set the dialog inputs from the persistence variables.

                browseDialog.InputsOutputs.CurrentNodeDescriptor = currentNodeDescriptor
                browseDialog.InputsOutputs.SelectionDescriptors.Clear()

                For Each browseNodeDescriptor As UABrowseNodeDescriptor In selectionDescriptors
                    browseDialog.InputsOutputs.SelectionDescriptors.Add(browseNodeDescriptor)
                Next browseNodeDescriptor

                Dim dialogResult As DialogResult = browseDialog.ShowDialog(owner)
                If dialogResult <> DialogResult.OK Then
                    Exit Do
                End If

                ' Update the persistence variables with the dialog output.

                currentNodeDescriptor = browseDialog.InputsOutputs.CurrentNodeDescriptor
                selectionDescriptors.Clear()
                For Each browseNodeDescriptor As UABrowseNodeDescriptor In browseDialog.InputsOutputs.SelectionDescriptors
                    selectionDescriptors.Add(browseNodeDescriptor)
                Next browseNodeDescriptor
            Loop While True
        End Sub
    End Class
End Namespace
#End Region