' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
' ReSharper disable UnusedParameter.Local
#Region "Example"
' This example shows how to handle conversion errors on the server level.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UAServerDocExamples._EasyUAServer
    Partial Friend Class ConversionError
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Define a data variable of type Byte.
            Dim dataVariable As UADataVariable = UADataVariable.CreateIn(server.Objects, "MyDataVariable").ValueType(Of Byte)()

            ' Add a Read handler that returns random values between 0 and 511. Those greater than 255 will cause conversion
            ' errors.
            Dim random = New Random()
            AddHandler dataVariable.Read, Sub(sender, args) args.HandleAndReturn(random.Next(0, 512))

            ' Hook events to the server.
            ' Note that the conversion error event can also be handled on the data variable or folder level, if that's what
            ' you requirements call for.
            AddHandler server.ConversionError, AddressOf ServerOnConversionError

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

        ' Event handler for the ConversionError event. It simply prints out the event.
        Private Shared Sub ServerOnConversionError(ByVal sender As Object, ByVal e As UADataVariableConversionErrorEventArgs)
            Console.WriteLine()
            Console.WriteLine($"*** {e}")

            ' Following are some useful properties in the event notification:
            '   e.DataVariable
            '   e.Action
            '   e.ServiceResult

            Select Case e.Action
                Case UADataVariableConversionAction.Read
                    Console.WriteLine("The conversion error occurred during a Read operation.")
                Case UADataVariableConversionAction.Write
                    Console.WriteLine("The conversion error occurred during a Write operation.")
                Case UADataVariableConversionAction.Update
                    Console.WriteLine("The conversion error occurred during an Update operation.")
            End Select

            Console.WriteLine($"It occured on the data variable: {e.DataVariable}.")
            Console.WriteLine($"The service result was: {e.ServiceResult.Message}")
        End Sub
    End Class
End Namespace
#End Region

