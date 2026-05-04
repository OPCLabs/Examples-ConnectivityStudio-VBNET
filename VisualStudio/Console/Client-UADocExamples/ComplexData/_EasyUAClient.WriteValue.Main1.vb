' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to write complex data with OPC UA Complex Data plug-in.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib.DataTypeModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.ComplexData
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._EasyUAClient

    Friend Class WriteValue

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

            ' Read a node which returns complex data. 
            ' We know that this node returns complex data, so we can type cast to UAGenericObject.
            Console.WriteLine("Reading...")
            Dim genericObject As UAGenericObject
            Try
                genericObject = CType(client.ReadValue(endpointDescriptor, nodeDescriptor), UAGenericObject)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try


            ' Modify the data read.
            ' This node returns one of the two data types, randomly (this is not common, usually the type is fixed). The
            ' data types are sub-types of one common type which the data type of the node. We therefore use the data type 
            ' ID in the returned UAGenericObject to detect which data type has been returned.
            ' For processing the internals of the data, refer to examples for GenericData and DataType classes.
            ' We know how the data is structured, and have hard-coded a logic that modifies certain values inside. It is
            ' also possible to discover the structure of the data type in the program, and write generic clients that can 
            ' cope with any kind of complex data.
            '
            ' Note that the code below is not fully robust - it will throw an exception if the data is not as expected.
            Console.WriteLine("Modifying...")
            Console.WriteLine(genericObject.DataTypeId)
            If genericObject.DataTypeId.NodeDescriptor.Match("nsu=http://test.org/UA/Data/ ;i=9440") Then    ' ScalarValueDataType
                ' Negate the byte in the "ByteValue" field.
                Dim structuredData = CType(genericObject.GenericData, StructuredData)
                Dim byteValue = CType(structuredData.FieldData("ByteValue"), PrimitiveData)
                byteValue.Value = CType(Not CType(byteValue.Value, Byte), Byte)
                Console.WriteLine(byteValue.Value)
            ElseIf genericObject.DataTypeId.NodeDescriptor.Match("nsu=http://test.org/UA/Data/ ;i=9669") Then    ' ArrayValueDataType
                ' Negate bytes at indexes 0 and 1 of the array in the "ByteValue" field.
                Dim structuredData = CType(genericObject.GenericData, StructuredData)
                Dim byteValue = CType(structuredData.FieldData("ByteValue"), SequenceData)
                Dim element0 = CType(byteValue.Elements(0), PrimitiveData)
                Dim element1 = CType(byteValue.Elements(1), PrimitiveData)
                element0.Value = CType(Not CType(element0.Value, Byte), Byte)
                element1.Value = CType(Not CType(element1.Value, Byte), Byte)
                Console.WriteLine(element0.Value)
                Console.WriteLine(element1.Value)
            End If


            ' Write the modified complex data back to the node.
            ' The data type ID in the UAGenericObject is borrowed without change from what we have read, so that the server
            ' knows which data type we are writing. The data type ID not necessary if writing precisely the same data type
            ' as the node has (not a subtype).
            Console.WriteLine("Writing...")
            Try
                client.WriteValue(endpointDescriptor, nodeDescriptor, genericObject)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
        End Sub
    End Class
End Namespace

#End Region
