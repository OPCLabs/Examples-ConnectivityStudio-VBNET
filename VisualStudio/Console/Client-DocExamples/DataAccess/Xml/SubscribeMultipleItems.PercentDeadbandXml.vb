' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how subscribe to changes of multiple items with percent deadband.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class SubscribeMultipleItems
        Public Shared Sub PercentDeadbandXml()
            ' Instantiate the client object.
            Using client = New EasyDAClient()
                AddHandler client.ItemChanged, AddressOf client_PercentDeadband_ItemChanged

                Console.WriteLine("Subscribing with different percent deadbands...")
                client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", New DAItemDescriptor("Dynamic/Analog Types/Int",
                                VarTypes.Empty), New DAGroupParameters(requestedUpdateRate:=100, percentDeadband:=10.0F), Nothing),
                    New DAItemGroupArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", New DAItemDescriptor("Dynamic/Analog Types/Double",
                                VarTypes.Empty), New DAGroupParameters(requestedUpdateRate:=100, percentDeadband:=5.0F), Nothing)
                    })

                Console.WriteLine("Processing item changed events for 1 minute...")
                Thread.Sleep(60 * 1000)
            End Using
        End Sub

        ' Item changed event handler
        Private Shared Sub client_PercentDeadband_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs)
            If e.Succeeded Then
                Console.WriteLine("{0}: {1}", e.Arguments.ItemDescriptor.ItemId, e.Vtq)
            Else
                Console.WriteLine("{0} *** Failure: {1}", e.Arguments.ItemDescriptor.ItemId, e.ErrorMessageBrief)
            End If
        End Sub
    End Class
End Namespace
#End Region
