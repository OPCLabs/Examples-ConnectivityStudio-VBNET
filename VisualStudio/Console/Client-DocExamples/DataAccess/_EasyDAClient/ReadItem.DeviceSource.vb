' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read a single item from the device, and display its value, timestamp and quality.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadItem
        Public Shared Sub DeviceSource()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Console.WriteLine("Reading item...")
            Dim vtq As DAVtq
            Try
                ' DADataSource enumeration:
                ' Selects the data source for OPC reads (from device, from OPC cache, or dynamically determined).
                ' The data source (memory, OPC cache or OPC device) selection is based on the desired value age and
                ' current status of data received from the server.

                vtq = client.ReadItem("OPCLabs.KitServer.2", "Simulation.Random", DADataSource.Device)
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            Console.WriteLine("Vtq: {0}", vtq)
        End Sub
    End Class
End Namespace
#End Region
