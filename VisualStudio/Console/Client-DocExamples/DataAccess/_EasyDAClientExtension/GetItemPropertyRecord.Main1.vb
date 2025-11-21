' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain a structure containing property values for an OPC item, and display some property values.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.Extensions
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClientExtension
    Friend Class GetItemPropertyRecord
        Public Shared Sub Main1()
            Dim client = New EasyDAClient()

            ' Get a structure containing values of all well-known properties
            Dim itemPropertyRecord As DAItemPropertyRecord
            Try
                itemPropertyRecord = client.GetItemPropertyRecord("", "OPCLabs.KitServer.2", "Simulation.Random")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            ' Display some of the obtained property values
            Console.WriteLine("itemPropertyRecord.AccessRights: {0}", itemPropertyRecord.AccessRights)
            Console.WriteLine("itemPropertyRecord.DataType: {0}", itemPropertyRecord.DataType)
            Console.WriteLine("itemPropertyRecord.Timestamp: {0}", itemPropertyRecord.Timestamp)
        End Sub
    End Class
End Namespace
#End Region
