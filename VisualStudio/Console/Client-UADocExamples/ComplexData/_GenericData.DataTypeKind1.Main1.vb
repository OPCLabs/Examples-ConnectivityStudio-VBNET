' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to process generic data type, displaying some of its properties, recursively.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.DataTypeModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.ComplexData
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._GenericData

    Friend Class DataTypeKind1

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

            ' Read a node. We know that this node returns complex data, so we can type cast to UAGenericObject.
            Dim genericObject As UAGenericObject
            Try
                genericObject = CType(client.ReadValue(endpointDescriptor, nodeDescriptor), UAGenericObject)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Process the generic data type. We will inspect some of its properties, and dump them.
            ProcessGenericData(genericObject.GenericData, maximumDepth:=2)
        End Sub


        ' Process the generic data type. Its structure can sometimes be quite deep, therefore we are limiting the depth
        ' of the recursion using maximumDepth.
        Public Shared Sub ProcessGenericData(ByVal genericData As GenericData, ByVal maximumDepth As Integer)
            If (maximumDepth = 0) Then
                Return
            End If

            Console.WriteLine()
            Console.WriteLine("genericData.DataType: {0}", genericData.DataType)

            Select Case (genericData.DataTypeKind)
                Case DataTypeKind.Enumeration
                    Console.WriteLine("The generic data is an enumeration.")
                    Dim enumerationData = CType(genericData, EnumerationData)
                    Console.WriteLine("Its value is {0}.", enumerationData.Value)
                    ' There is also a ValueName that you can inspect (if known).

                Case DataTypeKind.Opaque
                    Console.WriteLine("The generic data is opaque.")
                    Dim opaqueData = CType(genericData, OpaqueData)
                    Console.WriteLine("Its size is {0} bits.", opaqueData.SizeInBits)
                    Console.WriteLine("The data bytes are {0}.", BitConverter.ToString(opaqueData.ByteArray))
                    ' Use the Value property (a BitArray) if you need to access the value bit by bit.

                Case DataTypeKind.Primitive
                    Console.WriteLine("The generic data is primitive.")
                    Dim primitiveData = CType(genericData, PrimitiveData)
                    Console.WriteLine("Its value is ""{0}"".", primitiveData.Value)

                Case DataTypeKind.Sequence
                    Console.WriteLine("The generic data is a sequence.")
                    Dim sequenceData = CType(genericData, SequenceData)
                    Console.WriteLine("It has {0} elements.", sequenceData.Elements.Count)
                    Console.WriteLine("A dump of the elements follows.")
                    For Each element As GenericData In sequenceData.Elements
                        ProcessGenericData(element, (maximumDepth - 1))
                    Next

                Case DataTypeKind.Structured
                    Console.WriteLine("The generic data is structured.")
                    Dim structuredData = CType(genericData, StructuredData)
                    Console.WriteLine("It has {0} field data members.", structuredData.FieldData.Count)
                    Console.WriteLine("The names of the fields are: {0}.", String.Join(", ", structuredData.FieldData.Keys))

                    Console.WriteLine("A dump of each of the fields follows.")
                    For Each pair As KeyValuePair(Of String, GenericData) In structuredData.FieldData
                        Console.WriteLine()
                        Console.WriteLine("Field name: {0}", pair.Key)
                        ProcessGenericData(pair.Value, (maximumDepth - 1))
                    Next

                Case DataTypeKind.Union
                    Console.WriteLine("The generic data is union.")
                    Dim unionData = CType(genericData, UnionData)
                    Console.WriteLine("The name of current field is: {0}", unionData.FieldName)
                    Console.WriteLine("Current field value is: {0}", unionData.FieldValue)
            End Select

        End Sub
    End Class
End Namespace

#End Region
