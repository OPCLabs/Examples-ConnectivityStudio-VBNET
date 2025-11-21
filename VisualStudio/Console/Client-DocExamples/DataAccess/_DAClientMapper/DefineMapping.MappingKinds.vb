' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable UnusedMember.Global
#Region "Example"
' This example for OPC DA type-less mapping shows how to define mappings of various kinds, and perform subscribe and 
' unsubscribe operations.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComponentModel.Linking
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.Generic
Imports OpcLabs.EasyOpc.DataAccess.LiveMapping

Namespace Global.DocExamples.DataAccess._DAClientMapper
    Partial Friend Class DefineMapping
        Class MyClassMappingKinds
            Public WriteOnly Property CurrentValue As Double
                Set(value As Double)
                    ' Display the incoming value
                    Console.WriteLine("Value: {0}", value)
                End Set
            End Property

            Public WriteOnly Property CurrentVtq As DAVtq(Of Double)
                Set(value As DAVtq(Of Double))
                    ' Display the incoming Vtq
                    Console.WriteLine("Vtq: {0}", value)
                End Set
            End Property

            Public WriteOnly Property CurrentException As Exception
                Set(value As Exception)
                    ' Display the incoming exception
                    Console.WriteLine("Exception: {0}", value)
                End Set
            End Property

            Public WriteOnly Property CurrentResult As DAVtqResult(Of Double)
                Set(value As DAVtqResult(Of Double))
                    ' Display the incoming result
                    Console.WriteLine("Result: {0}", value)
                End Set
            End Property
        End Class

        Public Shared Sub MappingKinds()
            Dim mapper = New DAClientMapper()
            Dim target = New MyClassMappingKinds()

            ' Define several type-less mappings for the same source, with different mapping kinds.

            Dim targetType = target.GetType()
            Dim source = New DAClientItemSource("OPCLabs.KitServer.2", "Demo.Ramp", 1000, DADataSource.Cache)

            mapper.DefineMapping(
                 source,
                 New DAClientItemMapping(GetType(Double)),
                 New ObjectMemberLinkingTarget(targetType, target, "CurrentValue"))

            mapper.DefineMapping(
                 source,
                 New DAClientItemMapping(GetType(Double), DAItemMappingKind.Vtq),
                 New ObjectMemberLinkingTarget(targetType, target, "CurrentVtq"))

            mapper.DefineMapping(
                 source,
                 New DAClientItemMapping(GetType(Double), DAItemMappingKind.Exception),
                 New ObjectMemberLinkingTarget(targetType, target, "CurrentException"))

            mapper.DefineMapping(
                 source,
                 New DAClientItemMapping(GetType(Double), DAItemMappingKind.Result),
                 New ObjectMemberLinkingTarget(targetType, target, "CurrentResult"))

            ' Perform a subscribe operation.
            mapper.Subscribe(True)

            Threading.Thread.Sleep(30 * 1000)

            ' Perform an unsubscribe operation.
            mapper.Subscribe(False)
        End Sub
    End Class
End Namespace
#End Region
