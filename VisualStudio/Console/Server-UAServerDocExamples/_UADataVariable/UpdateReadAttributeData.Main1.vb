' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to update the read value in the push data provision model. In this model, your code pushes the
' data into the server, and the server then makes the data available to OPC clients.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports Timer = System.Timers.Timer

Namespace Global.UAServerDocExamples._UADataVariable
    Partial Friend Class UpdateReadAttributeData
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a read-only data variable.
            Dim dataVariable = UADataVariable.CreateIn(server.Objects, "ReadThisVariable") _
                .ValueType(Of Integer)() _
                .Writable(False)

            ' Create a timer for pushing the data for OPC reads. In a real server the activity may also come from other
            ' sources.
            Dim timer = New Timer With
            {
                .Interval = 1000,
                .AutoReset = True
            }

            ' Set the read attribute data of the data variable to a random value whenever the timer interval elapses.
            Dim random = New Random()
            AddHandler timer.Elapsed, Sub(sender, args) dataVariable.UpdateReadAttributeData(random.Next())
            timer.Start()

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

