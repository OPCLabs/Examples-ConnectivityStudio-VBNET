' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
' ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#Region "Example"
' This example shows how to get value of multiple OPC XML-DA properties, and handle errors.
'
' Note that some properties may not have a useful value initially (e.g. until the item Is activated in a group), which also the
' case with Timestamp property as implemented by the demo server. This behavior is server-dependent, and normal. You can run 
' IEasyDAClient.ReadMultipleItemValues.Main.vbs shortly before this example, in order to obtain better property values. Your 
' code may also subscribe to the items in order to assure that they remain active.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class GetMultiplePropertyValues
        Shared Sub Main1Xml()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Dim serverDescriptor As ServerDescriptor = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx"

            ' Get the values of Timestamp and AccessRights properties of two items.
            Dim results() As ValueResult = client.GetMultiplePropertyValues(New DAPropertyArguments() {
                New DAPropertyArguments(serverDescriptor, "Dynamic/Analog Types/Int", DAPropertyDescriptor.Timestamp),
                New DAPropertyArguments(serverDescriptor, "Dynamic/Analog Types/Int", DAPropertyDescriptor.AccessRights),
                New DAPropertyArguments(serverDescriptor, "Static/Analog Types/Double", DAPropertyDescriptor.Timestamp),
                New DAPropertyArguments(serverDescriptor, "Static/Analog Types/Double", DAPropertyDescriptor.AccessRights)
             })

            For i = 0 To results.Length - 1
                Dim valueResult As ValueResult = results(i)
                If valueResult.Exception Is Nothing Then
                    Console.WriteLine($"results({i}).Value: {valueResult.Value}")
                Else
                    Console.WriteLine($"results({i}).Exception.Message: {valueResult.Exception.Message}")
                End If
            Next i

        End Sub

    End Class
End Namespace
#End Region
