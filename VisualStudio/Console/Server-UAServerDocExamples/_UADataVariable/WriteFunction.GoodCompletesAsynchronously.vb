' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to indicate that the write operation completes asynchronously. This is an example of the push
' data consumption model.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDocExamples._UADataVariable
    Partial Friend Class WriteFunction
        Shared Sub GoodCompletesAsynchronously()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a writable data variable and add a function that will be called when the data variable is written to.
            ' The function returns a status code that indicates the outcome of the Write operation. Since we do not know the
            ' true outcome of the write operation at the time of the function call, we return status code
            ' "GoodCompletesAsynchronously".
            server.Add(New UADataVariable("WriteToThisVariable").WriteFunction(Of Integer)(
                Function(data)
                    ' The actual write operation Is performed asynchronously, on a separate thread.
                    Dim thread = New Thread(Sub() Console.WriteLine($"Attribute data written: {data}"))
                    thread.Start()

                    Return UACodeBits.GoodCompletesAsynchronously
                End Function
                ))


            ' Start the server.
            Console.WriteLine("The server is starting...")
            server.Start()

            Console.WriteLine("The server is started.")
            Console.WriteLine("Any value written to the example data variable will be displayed on the console.")
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

