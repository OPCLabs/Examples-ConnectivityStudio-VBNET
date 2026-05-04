' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to process a data type, displaying some of its properties, recursively.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.DataTypeModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.ComplexData
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._DataType

    Friend Class Kind

        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Define which node we will work with.
            Dim nodeDescriptor As UANodeDescriptor =
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
            ' The data type is in the GenericData.DataType property of the UAGenericObject.

            Dim dataType As DataType = genericObject.GenericData.DataType

            ' Process the data type. We will inspect some of its properties, and dump them.
            ProcessDataType(dataType, maximumDepth:=2)
        End Sub


        ' Process the data type. It can be recursive in itself, so if you do not know the data type you are dealing with, 
        ' it is recommended to make safeguards against infinite looping or recursion - here, the maximumDepth.
        Public Shared Sub ProcessDataType(dataType As DataType, ByVal maximumDepth As Integer)
            If (maximumDepth = 0) Then
                Return
            End If

            Console.WriteLine()
            Console.WriteLine("dataType.Name: {0}", dataType.Name)

            Select Case (dataType.Kind)
                Case DataTypeKind.Enumeration
                    Console.WriteLine("The data type is an enumeration.")
                    Dim enumerationDataType = CType(dataType, EnumerationDataType)
                    Console.WriteLine("It has {0} enumeration members.", enumerationDataType.EnumerationMembers.Count)
                    Console.WriteLine("The names of the enumeration members are: {0}.",
                                      String.Join(", ", enumerationDataType.EnumerationMembers.Select(Function(member) member.Name)))
                    ' Here you can process the members, or inspect SizeInBits etc.

                Case DataTypeKind.Opaque
                    Console.WriteLine("The data type is opaque.")
                    Dim opaqueDataType = CType(dataType, OpaqueDataType)
                    Console.WriteLine("Its size is {0} bits.", opaqueDataType.SizeInBits)
                    ' There isn't much more you can learn about an opaque data type (well, it may have Description and 
                    ' other common members). It is, after all, opaque...

                Case DataTypeKind.Primitive
                    Console.WriteLine("The data type is primitive.")
                    Dim primitiveDataType = CType(dataType, PrimitiveDataType)
                    Console.WriteLine("Its .NET value type is ""{0}"".", primitiveDataType.ValueType)
                    ' There isn't much more you can learn about the primitive data type.

                Case DataTypeKind.Sequence
                    Console.WriteLine("The data type is a sequence.")
                    Dim sequenceDataType = CType(dataType, SequenceDataType)
                    Console.WriteLine("Its length is {0} (-1 means that the length can vary).", sequenceDataType.Length)
                    Console.WriteLine("A dump of the element data type follows.")
                    ProcessDataType(sequenceDataType.ElementDataType, (maximumDepth - 1))

                Case DataTypeKind.Structured
                    Console.WriteLine("The data type is structured.")
                    Dim structuredDataType = CType(dataType, StructuredDataType)
                    Console.WriteLine("It has {0} data fields.", structuredDataType.DataFields.Count)
                    Console.WriteLine("The names of the data fields are: {0}.",
                                      String.Join(", ", structuredDataType.DataFields.Select(Function(field) field.Name)))
                    Console.WriteLine("A dump of each of the data fields follows.")

                    For Each dataField As DataField In structuredDataType.DataFields
                        Console.WriteLine()
                        Console.WriteLine("dataField.Name: {0}", dataField.Name)
                        ' Note that every data field also has properties like IsLength, IsOptional, IsSwitch which might 
                        ' be of interest but we are not dumping them here.
                        ProcessDataType(dataField.DataType, (maximumDepth - 1))
                    Next

                Case DataTypeKind.Union
                    Console.WriteLine("The data type is union.")
                    Dim unionDataType = CType(dataType, UnionDataType)
                    Console.WriteLine("It has {0} data fields.", unionDataType.DataFields.Count)
                    Console.WriteLine("The names of the data fields are: {0}.",
                                      String.Join(", ", unionDataType.DataFields.Select(Function(field) field.Name)))

            End Select

        End Sub
    End Class
End Namespace

#End Region
