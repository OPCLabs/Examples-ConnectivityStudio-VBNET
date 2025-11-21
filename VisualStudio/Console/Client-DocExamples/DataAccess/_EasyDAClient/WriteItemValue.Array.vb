' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable VBWarnings::BC42016
#Region "Example"
' Shows how to write into an OPC item that is of array type, and read the array value back.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class WriteItemValue
        Public Shared Sub Array()
            Dim client As New EasyDAClient()

            Console.WriteLine("Writing array value...")
            Try
                client.WriteItemValue("", "OPCLabs.KitServer.2", "Simulation.Register_ArrayOfI2", New Int16() {1234, 2345, 3456})
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            Console.WriteLine("Reading array value...")
            Dim value As Int16()
            Try
                value = CType(client.ReadItemValue("", "OPCLabs.KitServer.2", "Simulation.Register_ArrayOfI2"), Int16())
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            If Not IsNothing(value) Then
                Console.WriteLine(value(0))
                Console.WriteLine(value(1))
                Console.WriteLine(value(2))
            End If
        End Sub
    End Class
End Namespace
#End Region
