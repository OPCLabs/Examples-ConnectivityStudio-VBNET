' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to wait on an item until a value with "good" quality becomes available.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.Extensions
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClientExtension
    Friend Class WaitForItemValue
        Public Shared Sub Main1()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Console.WriteLine("Waiting until an item value with ""good"" quality becomes available...")
            Dim value As Object
            Try
                value = client.WaitForItemValue("", "OPCLabs.KitServer.2", "Demo.Unreliable",
                    groupParameters:=100,   ' this Is the requested update rate
                    millisecondsTimeout:=60 * 1000)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            ' Display the obtained item value.
            Console.WriteLine($"value: {value}")
        End Sub
    End Class
End Namespace
#End Region
