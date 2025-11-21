' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
'  This example for OPC DA type-less mapping shows how to define a mapping and perform a read operation.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComponentModel.Linking
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.LiveMapping

Namespace Global.DocExamples.DataAccess._DAClientMapper
    Partial Friend Class DefineMapping
        Class MyClass2
            Public Property Value As Object
        End Class

#Region "Example-DefineAndRead"
        Public Shared Sub Main1()
            Dim mapper = New DAClientMapper()
            Dim target = New MyClass2()

            ' Define a type-less mapping.

            mapper.DefineMapping(
                     New DAClientItemSource(
                         "OPCLabs.KitServer.2",
                         "Simulation.Register_I4",
                         New DAReadParameters(DADataSource.Cache)),
                     New DAClientItemMapping(GetType(Int32)),
                     New ObjectMemberLinkingTarget(target.GetType(), target, "Value"))

            ' Perform a read operation.
            mapper.Read()

            ' Display the result.
            Console.WriteLine(target.Value)
        End Sub
#End Region
    End Class
End Namespace
#End Region
