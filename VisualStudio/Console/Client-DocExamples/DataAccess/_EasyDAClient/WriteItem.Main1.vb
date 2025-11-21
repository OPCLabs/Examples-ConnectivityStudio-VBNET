' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to write a value, timestamp and quality into a single item.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteItem
        Public Shared Sub Main1()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Try
                client.WriteItem("", "OPCLabs.KitServer.2", "Simulation.Register_I4",
                    12345, DateTime.SpecifyKind(New DateTime(1980, 1, 1), DateTimeKind.Utc), 192)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try
            Console.WriteLine("Success")
        End Sub
    End Class
End Namespace
#End Region
