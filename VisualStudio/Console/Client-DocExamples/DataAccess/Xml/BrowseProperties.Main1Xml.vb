' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to enumerate all properties of an OPC XML-DA item. For each property, it displays its Id and description.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class BrowseProperties
        Shared Sub Main1Xml()
            Dim client = New EasyDAClient()

            Dim propertyElements As DAPropertyElementCollection
            Try
                propertyElements = client.BrowseProperties("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Dynamic/Analog Types/Int")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each propertyElement In propertyElements
                Console.WriteLine($"PropertyElements(""{propertyElement.PropertyId.NumericalValue}"").Description: {propertyElement.Description}")
            Next propertyElement

        End Sub

        ' Example output
        '
        'PropertyElements("1").Description: Item Canonical DataType
        'PropertyElements("2").Description: Item Value
        'PropertyElements("3").Description: Item Quality
        'PropertyElements("4").Description: Item Timestamp
        'PropertyElements("5").Description: Item Access Rights
        'PropertyElements("6").Description: Server Scan Rate
        'PropertyElements("7").Description: Item EU Type
        'PropertyElements("8").Description: Item EU Info
        'PropertyElements("102").Description: High EU
        'PropertyElements("103").Description: Low EU
    End Class
End Namespace
#End Region
