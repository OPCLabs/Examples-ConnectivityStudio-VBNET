' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to handle the read event on a folder, providing a way to implement reading of multiple data
' variables using a single handler.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib.NodeSpace
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UAServerDocExamples._UAServerNode
    Partial Friend Class Read
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a folder node.
            Dim myFolder As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder")

            ' Create data variables in the folder. Distinguish them by their state.
            myFolder.Add(New UADataVariable("MyDataVariable1").ValueType(Of Integer)().SetState(1))
            myFolder.Add(New UADataVariable("MyDataVariable2").ValueType(Of Integer)().SetState(2))
            myFolder.Add(New UADataVariable("MyDataVariable3").ValueType(Of Integer)().SetState(3))
            myFolder.Add(New UADataVariable("MyDataVariable4").ValueType(Of Integer)().SetState(4))
            myFolder.Add(New UADataVariable("MyDataVariable5").ValueType(Of Integer)().SetState(5))

            ' Handle the read event for the folder.
            AddHandler myFolder.Read, AddressOf MyFolderOnRead

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

        ''' <summary>
        ''' Event handler for the read event on the folder.  
        ''' </summary>
        ''' <param name="sender">The data variable object that sends the event.</param>
        ''' <param name="e">Data for the variable read event.</param>
        Private Shared Sub MyFolderOnRead(ByVal sender As Object, ByVal e As UADataVariableReadEventArgs)
            ' Obtain the state associated with the data variable that is being read.
            ' Use it as the offset for the random value, so that each data variable generates values in a unique range.
            Dim offset As Integer = CInt(e.DataVariable.State * 100)

            ' Generate a random value, indicate that the read has been handled, and return the generated value.
            e.HandleAndReturn(Random.Next(offset, offset + 100))
        End Sub

        Private Shared ReadOnly Random As Random = New Random()
    End Class
End Namespace
#End Region

