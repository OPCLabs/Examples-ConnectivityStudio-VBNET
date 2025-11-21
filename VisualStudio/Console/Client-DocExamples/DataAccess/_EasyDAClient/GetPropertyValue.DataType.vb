' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain a data type of an OPC item.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class GetPropertyValue
        Public Shared Sub DataType()
            Dim client = New EasyDAClient()

            ' Get the value of DataType property; it is a 16-bit signed integer
            Dim aDataType As Short
            Try
                aDataType = CShort(Fix(client.GetPropertyValue("", "OPCLabs.KitServer.2", "Simulation.Random", DAPropertyIds.DataType)))
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            ' Convert the data type to VarType
            Dim varType = CType(aDataType, VarType)

            ' Display the obtained data type
            Console.WriteLine("DataType: {0}", aDataType) ' Display data type as numerical value
            Console.WriteLine("VarType: {0}", varType) ' Display data type symbolically

            ' Code below illustrates how decisions can be made based on type
            Select Case varType
                Case VarTypes.R8
                    Console.WriteLine("The data type is VarTypes.R8, as we expected.")

                    ' other cases may come here ...

                Case Else
                    Console.WriteLine("The data type is not as we expected!")
            End Select
        End Sub
    End Class
End Namespace
#End Region
