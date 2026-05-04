' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable VBWarnings::BC42016
#Region "Example"
' Shows how to read an OPC item that is of array type.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadItemValue
        Public Shared Sub Array()
            Dim client As New EasyDAClient()

            Console.WriteLine("Reading array value...")
            Dim value As Int32()
            Try
                ' UInt16 is returned as Int32, because UInt16 is not a CLS-compliant type (and is not supported in VB.NET).
                value = CType(client.ReadItemValue("", "OPCLabs.KitServer.2", "Simulation.ReadValue_ArrayOfUI2"), Int32())
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
