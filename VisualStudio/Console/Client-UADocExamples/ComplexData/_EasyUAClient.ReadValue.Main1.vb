' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to read complex data with OPC UA Complex Data plug-in.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.ComplexData
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._EasyUAClient

    Friend Class ReadValue

        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Define which node we will work with.
            Dim nodeDescriptor As UANodeDescriptor = _
                "nsu=http://test.org/UA/Data/ ;i=10239"  ' [ObjectsFolder]/Data.Static.Scalar.StructureValue

            ' Instantiate the client object.
            Dim client = New EasyUAClient

            ' Read a node which returns complex data. This is done in the same way as regular reads - just the data 
            ' returned is different.
            Dim value As Object
            Try
                value = client.ReadValue(endpointDescriptor, nodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Display basic information about what we have read.
            Console.WriteLine(value)


            ' We know that this node returns complex data, so we can type cast to UAGenericObject.
            Dim genericObject = CType(value, UAGenericObject)

            ' The actual data is in the GenericData property of the UAGenericObject.
            '
            ' If we want to see the whole hierarchy of the received complex data, we can format it with the "V" (verbose)
            ' specifier. In the debugger, you can view the same by displaying the private DebugView property.
            Console.WriteLine()
            Console.WriteLine("{0:V}", genericObject.GenericData)

            ' For processing the internals of the data, refer to examples for GenericData and DataType classes.


            ' Example output (truncated):
            '
            '(ScalarValueDataType) structured
            '
            '(ScalarValueDataType) structured   
            '  [BooleanValue] (Boolean) primitive; True {System.Boolean}
            '  [ByteStringValue] (ByteString) primitive; System.Byte[] {System.Byte[]}
            '  [ByteValue] (Byte) primitive; 153 {System.Byte}
            '  [DateTimeValue] (DateTime) primitive; 5/11/2013 4:32:00 PM {System.DateTime}
            '  [DoubleValue] (Double) primitive; -8.93178007363702E+27 {System.Double}
            '  [EnumerationValue] (Int32) primitive; 0 {System.Int32}
            '  [ExpandedNodeIdValue] (ExpandedNodeId) structured
            '    [NamespaceURI] (CharArray) primitive; "http://samples.org/UA/memorybuffer/Instance" {System.String}
            '    [NamespaceURISpecified] (Bit) primitive; True {System.Boolean}
            '    [NodeIdType] (NodeIdType) enumeration; 3 (String)
            '    [ServerIndexSpecified] (Bit) primitive; False {System.Boolean}
            '    [String] (StringNodeId) structured
            '      [Identifier] (CharArray) primitive; "????" {System.String}
            '      [NamespaceIndex] (UInt16) primitive; 0 {System.UInt16}
            '  [FloatValue] (Float) primitive; 78.37176 {System.Single}
            '  [GuidValue] (Guid) primitive; 8129cdaf-24d9-8140-64f2-3a6d7a957fd7 {System.Guid}
            '  [Int16Value] (Int16) primitive; 2793 {System.Int16}
            '  [Int32Value] (Int32) primitive; 1133391074 {System.Int32}
            '  [Int64Value] (Int64) primitive; -1039109760798965779 {System.Int64}
            '  [Integer] (Variant) structured
            '    [ArrayDimensionsSpecified] sequence[1]
            '      [0] (Bit) primitive; False {System.Boolean}
            '    [ArrayLengthSpecified] sequence[1]
            '      [0] (Bit) primitive; False {System.Boolean}
            '    [Int64] sequence[1]
            '      [0] (Int64) primitive; 0 {System.Int64}
            '    [VariantType] sequence[6]
            '      [0] (Bit) primitive; False {System.Boolean}
            '      [1] (Bit) primitive; False {System.Boolean}
            '      [2] (Bit) primitive; False {System.Boolean}
            '      [3] (Bit) primitive; True {System.Boolean}
            '      [4] (Bit) primitive; False {System.Boolean}
            '      [5] (Bit) primitive; False {System.Boolean}
            '  [LocalizedTextValue] (LocalizedText) structured
            '    [Locale] (CharArray) primitive; "ko" {System.String}
            '    [LocaleSpecified] (Bit) primitive; True {System.Boolean}
            '    [Reserved1] sequence[6]
            '      [0] (Bit) primitive; False {System.Boolean}
            '      [1] (Bit) primitive; False {System.Boolean}
            '      [2] (Bit) primitive; False {System.Boolean}
            '      [3] (Bit) primitive; False {System.Boolean}
            '      [4] (Bit) primitive; False {System.Boolean}
            '      [5] (Bit) primitive; False {System.Boolean}
            '    [Text] (CharArray) primitive; "? ?? ??+ ??? ??) ?: ???? ?! ?!" {System.String}
            '    [TextSpecified] (Bit) primitive; True {System.Boolean}
            '  [NodeIdValue] (NodeId) structured                                                                               
        End Sub
    End Class
End Namespace

#End Region
