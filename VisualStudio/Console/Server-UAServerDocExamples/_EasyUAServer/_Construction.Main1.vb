' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable RedundantExplicitParamsArrayCreation
' ReSharper disable UnusedVariable
#Region "Example"
' This example shows different ways of constructing the EasyUAServer object.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class _Construction
        Shared Sub Main1()
            ' The toolkit provides a ready-made shared instance of the server object which you can use without even having
            ' to construct it. Not recommended for use in library code, because it is a shared instance, and its usage may
            ' therefore conflict with other code using the same instance.
            Dim server0 = EasyUAServer.SharedInstance


            ' The simplest way to construct the server object is to use the default constructor. The server will run on its
            ' default endpoint URL "opc.tcp://localhost:48040/".
            Dim server1 = New EasyUAServer()


            ' The simplest way to construct the server object is to use the default constructor. The server will run on its
            ' constructor.
            Dim server2 = New EasyUAServer("opc.tcp://localhost:38444")


            ' The server object can also be constructed with multiple endpoint URL strings passed as an array to the
            ' constructor.
            Dim server3 = New EasyUAServer(
            {
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446"
            })


            ' If the language supports variable number of arguments (such as C# or VB.NET), the multiple endpoint URL
            ' strings can be passed to it as separate arguments, instead of having to create an array of them.
            Dim server4 = New EasyUAServer(
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446")


            ' The server object can be constructed with specific message security modes.
            Dim server5 = New EasyUAServer(UAMessageSecurityModes.Secure)


            ' The message security modes can be combined with the endpoint URL string.
            Dim server6 = New EasyUAServer(UAMessageSecurityModes.Secure, "opc.tcp://localhost:38444")


            ' The message security modes can also be combined with multiple endpoint URL strings in an array.
            Dim server7 = New EasyUAServer(UAMessageSecurityModes.Secure,
            {
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446"
            })


            ' If the language supports variable number of arguments (such as C# or VB.NET), the message security modes can
            ' be combined with multiple endpoint URL strings passed to it as separate arguments, instead of having to create
            ' an array of them.
            Dim server8 = New EasyUAServer(
                UAMessageSecurityModes.Secure,
                "opc.tcp://localhost:38444", "opc.tcp://localhost:38445", "opc.tcp://localhost:38446")


            ' The endpoint can be specified using the Uri object.
            Dim server9 = New EasyUAServer(New Uri("opc.tcp://localhost:38444"))


            ' The server object can also be constructed with multiple Uri objects for server endpoints, passed as an array
            ' to the constructor.
            Dim server10 = New EasyUAServer(
            {
                New Uri("opc.tcp://localhost:38444"),
                New Uri("opc.tcp://localhost:38445"),
                New Uri("opc.tcp://localhost:38446")
            })


            ' If the language supports variable number of arguments (such as C# or VB.NET), the multiple endpoint Uri
            ' objects can be passed to it as separate arguments, instead of having to create an array of them.
            Dim server11 = New EasyUAServer(
                New Uri("opc.tcp://localhost:38444"),
                New Uri("opc.tcp://localhost:38445"),
                New Uri("opc.tcp://localhost:38446")
            )


            ' The message security modes can be combined with the endpoint Uri object.
            Dim server12 = New EasyUAServer(UAMessageSecurityModes.Secure, New Uri("opc.tcp://localhost:38444"))


            ' The message security modes can also be combined with multiple endpoint Uri objects in an array.
            Dim server13 = New EasyUAServer(UAMessageSecurityModes.Secure,
            {
                New Uri("opc.tcp://localhost:38444"),
                New Uri("opc.tcp://localhost:38445"),
                New Uri("opc.tcp://localhost:38446")
            })


            ' If the language supports variable number of arguments (such as C# or VB.NET), the message security modes can
            ' be combined with multiple endpoint Uri objects passed to it as separate arguments, instead of having to create
            ' an array of them.
            Dim server14 = New EasyUAServer(
                UAMessageSecurityModes.Secure,
                New Uri("opc.tcp://localhost:38444"),
                New Uri("opc.tcp://localhost:38445"),
                New Uri("opc.tcp://localhost:38446")
            )


            ' The message security modes and the endpoint URL string can be set after the server object is constructed.
            Dim server15 = New EasyUAServer()
            server15.MessageSecurityModes = UAMessageSecurityModes.Secure
            server15.EndpointUrlString = "opc.tcp://localhost:38444"


            ' If the language supports property initializers (such as C# or VB.NET), the above code can be written more
            ' concisely.
            Dim server16 = New EasyUAServer With {
                .MessageSecurityModes = UAMessageSecurityModes.Secure,
                .EndpointUrlString = "opc.tcp://localhost:38444"
            }


            ' If the language supports collection initializers (such as C# or VB.NET), the server object can be constructed
            ' with the contents of the Objects folder, such as the data variables, in a single statement.
            Dim server17 = New EasyUAServer From
            {
                New UADataVariable("Constant1").ConstantValue(42),
                New UADataVariable("Constant2").ConstantValue("abc")
            }

        End Sub
    End Class
End Namespace
#End Region

