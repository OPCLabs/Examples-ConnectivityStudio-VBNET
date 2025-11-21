' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example for OPC DA type-less mapping shows how to define a mapping and perform subscribe and unsubscribe operations.
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
        Class MyClassSubscribe
            Public WriteOnly Property Value As Double
                Set(value As Double)
                    ' Display the incoming value
                    Console.WriteLine(value)
                End Set
            End Property
        End Class

        Public Shared Sub Subscribe()
            Dim mapper = New DAClientMapper()
            Dim target = New MyClassSubscribe()

            ' Define a type-less mapping.

            mapper.DefineMapping(
                 New DAClientItemSource("OPCLabs.KitServer.2", "Demo.Ramp", 1000, DADataSource.Cache),
                 New DAClientItemMapping(GetType(Double)),
                 New ObjectMemberLinkingTarget(target.GetType(), target, "Value"))

            ' Perform a subscribe operation.
            mapper.Subscribe(True)

            Threading.Thread.Sleep(30 * 1000)

            ' Perform an unsubscribe operation.
            mapper.Subscribe(False)
        End Sub
    End Class
End Namespace
#End Region
