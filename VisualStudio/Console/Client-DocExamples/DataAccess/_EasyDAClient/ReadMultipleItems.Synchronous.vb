' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read 4 items at once synchronously, and display their values, timestamps and qualities.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class ReadMultipleItems
        Public Shared Sub Synchronous()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            ' Specify that only synchronous method is allowed. By default, both synchronous and asynchronous methods are
            ' allowed, and the component picks a suitable method automatically. Disallowing asynchronous method leaves
            ' only the synchronous method available for selection.
            client.InstanceParameters.Mode.AllowAsynchronousMethod = False

            Dim vtqResults() As DAVtqResult = client.ReadMultipleItems("OPCLabs.KitServer.2",
                New DAItemDescriptor() {"Simulation.Random", "Trends.Ramp (1 min)", "Trends.Sine (1 min)", "Simulation.Register_I4"})

            For i = 0 To vtqResults.Length - 1
                Debug.Assert(vtqResults(i) IsNot Nothing)

                If vtqResults(i).Succeeded Then
                    Console.WriteLine("vtqResult[{0}].Vtq: {1}", i, vtqResults(i).Vtq)
                Else
                    Console.WriteLine("vtqResult[{0}] *** Failure: {1}", i, vtqResults(i).ErrorMessageBrief)
                End If
            Next i
        End Sub

        ' Example output:
        '
        'vtqResult[0].Vtq: 0.00125125888851588 { System.Double} @2020-04-10T15:29:20.642; GoodNonspecific(192)
        'vtqResult[1].Vtq: 0.344052940607071 {System.Double} @2020-04-10T15:29:20.643; GoodNonspecific(192)
        'vtqResult[2].Vtq: 0.830410616568378 {System.Double} @2020-04-10T15:29:20.643; GoodNonspecific(192)
        'vtqResult[3].Vtq: 0 {System.Int32} @1601-01-01T00:00:00.000; GoodNonspecific(192)
    End Class
End Namespace
#End Region
