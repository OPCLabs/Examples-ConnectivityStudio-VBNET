' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how data from parent node can be used in the read event handler.
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
    Partial Friend Class ParentNode
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create multiple folder nodes, each with a data variable in it. Distinguish the folders by their state, however
            ' the data variables are constructed the same, and use the same read event handler.

            Dim myFolder1 As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder1").SetState(1)
            AddHandler UADataVariable.CreateIn(myFolder1, "MyDataVariable").Read, AddressOf MyDataVariableOnRead

            Dim myFolder2 As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder2").SetState(2)
            AddHandler UADataVariable.CreateIn(myFolder2, "MyDataVariable").Read, AddressOf MyDataVariableOnRead

            Dim myFolder3 As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder3").SetState(3)
            AddHandler UADataVariable.CreateIn(myFolder3, "MyDataVariable").Read, AddressOf MyDataVariableOnRead

            Dim myFolder4 As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder4").SetState(4)
            AddHandler UADataVariable.CreateIn(myFolder4, "MyDataVariable").Read, AddressOf MyDataVariableOnRead

            Dim myFolder5 As UAFolder = UAFolder.CreateIn(server.Objects, "MyFolder5").SetState(5)
            AddHandler UADataVariable.CreateIn(myFolder5, "MyDataVariable").Read, AddressOf MyDataVariableOnRead

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
        ''' Event handler for the read event on the data variable. 
        ''' </summary>
        ''' <param name="sender">The data variable object that sends the event.</param>
        ''' <param name="e">Data for the variable read event.</param>
        Private Shared Sub MyDataVariableOnRead(ByVal sender As Object, ByVal e As UADataVariableReadEventArgs)
            ' Obtain the parent folder of the data variable that is being read.
            Dim parentNode As UAServerNode = e.DataVariable.ParentNode

            ' Obtain the state associated with the folder, where the data variable is located.
            ' Use it as the offset for the random value, so that each data variable generates values in a unique range.
            Dim offset As Integer = CInt(parentNode.State * 100)

            ' Generate a random value, indicate that the read has been handled, and return the generated value.
            e.HandleAndReturn(Random.Next(offset, offset + 100))
        End Sub

        Private Shared ReadOnly Random As Random = New Random()
    End Class
End Namespace
#End Region

