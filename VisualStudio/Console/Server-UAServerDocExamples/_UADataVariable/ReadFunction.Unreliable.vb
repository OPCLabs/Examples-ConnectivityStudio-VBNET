' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to create a data variable and implement reading its attribute data using a function, providing
' "Good" or "Bad" status code as needed.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports Newtonsoft.Json.Linq
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Generic
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._UADataVariable
    Partial Friend Class ReadFunction
        Shared Sub Unreliable()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a data variable, defining its read attribute data (including status code and source timestamp) by a
            ' function. Alternate "Good" and "Bad" status codes randomly.
            ' The type of the data variable (Int32, in this case) is inferred from the type returned by the function.
            Dim random = New Random()
            server.Add(New UADataVariable("ReadThisVariable").ReadFunction(Function() New UAAttributeData(Of Integer)(
                42,
                If(random.Next(2) <> 0, UACodeBits.Good, UACodeBits.BadNoCommunication),
                DateTime.UtcNow)))

            ' Start the server.
            Console.WriteLine("The server is starting...")
            server.Start()

            Console.WriteLine("The server is started.")
            Console.WriteLine()

            ' Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...")
            Console.ReadLine()

            ' Stop the server.
            Console.WriteLine("The server is stopping...")
            server.Stop()

            Console.WriteLine("The server is stopped.")
        End Sub
    End Class
End Namespace
#End Region

