' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to let the user browse for an OPC-UA server endpoint.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Forms.Browsing

Namespace UAFormsDocExamples._UAEndpointDialog
    Friend Class ShowDialog
        Shared Sub Main1(owner As IWin32Window)
            Dim endpointDialog = New UAEndpointDialog() With {
                .DiscoveryHost = "opcua.demo-this.com"
                }

            Dim dialogResult As DialogResult = endpointDialog.ShowDialog(owner)
            If dialogResult <> DialogResult.OK Then
                Return
            End If

            ' Display results
            MessageBox.Show(owner, endpointDialog.DiscoveryElement.ToString())
        End Sub
    End Class
End Namespace
#End Region