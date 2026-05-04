' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read 4 items from the device, and display their values, timestamps and qualities.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class ReadMultipleItems
        Public Shared Sub DeviceSourceXml()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            ' DADataSource enumeration
            ' Selects the data source for OPC reads (from device, from OPC cache, or dynamically determined).
            ' The data source (memory, OPC cache or OPC device) selection will be based on the desired value age and
            ' current status of data received from the server.

            Dim vtqResults() As DAVtqResult = client.ReadMultipleItems(New DAReadItemArguments() {
                New DAReadItemArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Dynamic/Analog Types/Double", DADataSource.Device),
                New DAReadItemArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Dynamic/Analog Types/Double[]", DADataSource.Device),
                New DAReadItemArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Dynamic/Analog Types/Int", DADataSource.Device),
                New DAReadItemArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Static/Analog Types/Int", DADataSource.Device)
            })

            For i = 0 To vtqResults.Length - 1
                Debug.Assert(vtqResults(i) IsNot Nothing)

                If vtqResults(i).Succeeded Then
                    Console.WriteLine("vtqResults[{0}].Vtq: {1}", i, vtqResults(i).Vtq)
                Else
                    Console.WriteLine("vtqResults[{0}] *** Failure: {1}", i, vtqResults(i).ErrorMessageBrief)
                End If
            Next i
        End Sub

        ' Example output
        '
        'vtqResults[0].Vtq: 100 {Double} @2024-01-01T14:31:03.232; GoodNonspecific (192)
        'vtqResults[1].Vtq: [3] {1000, 1000, 1000} {Double[]} @2024-01-01T14:31:03.232; GoodNonspecific (192)
        'vtqResults[2].Vtq: 700 {Int32} @2024-01-01T14:31:03.232; GoodNonspecific (192)
        'vtqResults[3].Vtq: 0 {Int32} @2024-01-01T14:31:03.232; GoodNonspecific (192)
    End Class
End Namespace
#End Region
