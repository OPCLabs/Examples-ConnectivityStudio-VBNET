' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
' ReSharper disable StringLiteralTypo
#Region "Example"
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Reactive
Imports Newtonsoft.Json.Linq
Imports OpcLabs.EasyOpc.AlarmsAndEvents.Engine
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDemoLibrary
    Public Module DataNodes
        ''' <summary>
        ''' Adds static and dynamic nodes that demonstrate various data types and access levels.
        ''' </summary>
        ''' <param name="parentFolder">The folder to which to add the nodes.</param>
        Sub AddToParent(parentFolder As UAFolder)
            ' Create Data folder.
            Dim dataFolder = UAFolder.CreateIn(parentFolder, "Data")

            ' Create read-only data variables of various data types, without adding them to the server first. We store
            ' references to them individually, because we later implement write-only variables that write to these
            ' read-only variables.
            Dim booleanReadOnlyDataVariable As UADataVariable =
                New UADataVariable("BooleanValue").Writable(False).ValueType(Of Boolean)()
            Dim byteStringReadOnlyDataVariable As UADataVariable =
                New UADataVariable("ByteStringValue").Writable(False).ValueType(Of Byte())()
            Dim byteReadOnlyDataVariable As UADataVariable = New UADataVariable("ByteValue").Writable(False).ValueType(Of Byte)()
            Dim dateTimeReadOnlyDataVariable As UADataVariable =
                New UADataVariable("DateTimeValue").Writable(False).ValueType(Of DateTime)()
            Dim doubleReadOnlyDataVariable As UADataVariable =
                New UADataVariable("DoubleValue").Writable(False).ValueType(Of Double)()
            Dim floatReadOnlyDataVariable As UADataVariable =
                New UADataVariable("FloatValue").Writable(False).ValueType(Of Single)()
            Dim guidReadOnlyDataVariable As UADataVariable = New UADataVariable("GuidValue").Writable(False).ValueType(Of Guid)()
            Dim int16ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("Int16Value").Writable(False).ValueType(Of Short)()
            Dim int32ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("Int32Value").Writable(False).ValueType(Of Integer)()
            Dim int64ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("Int64Value").Writable(False).ValueType(Of Long)()
            Dim sByteReadOnlyDataVariable As UADataVariable =
                New UADataVariable("SByteValue").Writable(False).ValueType(Of SByte)()
            Dim stringReadOnlyDataVariable As UADataVariable =
                New UADataVariable("StringValue").Writable(False).ValueType(Of String)()
            Dim uInt16ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("UInt16Value").Writable(False).ValueType(Of UShort)()
            Dim uInt32ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("UInt32Value").Writable(False).ValueType(Of UInteger)()
            Dim uInt64ReadOnlyDataVariable As UADataVariable =
                New UADataVariable("UInt64Value").Writable(False).ValueType(Of ULong)()
            Dim variantReadOnlyDataVariable As UADataVariable =
                New UADataVariable("VariantValue").Writable(False)

            ' Create Constant sub-folder under the Data folder. It contains read-only data variables with constant values.
            dataFolder.Add(
                New UAFolder("Constant") From
                {
                    New UAFolder("Scalar") From
                    {
                        New UADataVariable("BooleanValue").ConstantValue(True),
                        New UADataVariable("ByteStringValue").ConstantValue(New Byte() {&H57, &H21, &H40, &HFC}),
                        New UADataVariable("ByteValue").ConstantValue(CType(144, Byte)) _
                        , _ ' We are passing In UTC times, because we want always the same result, And so we must specify
                          _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                          _ ' server, And the result will depend on the time zone.
                        New UADataVariable("DateTimeValue").ConstantValue(
                            DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                            DateTimeKind.Utc)), _
                                                _
                        New UADataVariable("DoubleValue").ConstantValue(0.0000000000775630105797),
                        New UADataVariable("FloatValue").ConstantValue(2.77002E+29F),
                        New UADataVariable("GuidValue").ConstantValue(
                            New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),
                        New UADataVariable("Int16Value").ConstantValue(CType(-30956, Short)),
                        New UADataVariable("Int32Value").ConstantValue(276673160),
                        New UADataVariable("Int64Value").ConstantValue(1412096336825367659),
                        New UADataVariable("SByteValue").ConstantValue(CType(-113, SByte)),
                        New UADataVariable("StringValue").ConstantValue("lorem ipsum"),
                        New UADataVariable("UInt16Value").ConstantValue(CType(64421, UShort)),
                        New UADataVariable("UInt32Value").ConstantValue(3853116537UI),
                        New UADataVariable("UInt64Value").ConstantValue(9431348106520835314UL),
                        New UADataVariable("VariantValue").ConstantValue(529739609)
                    }
                })

            ' Create Dynamic sub-folder under the Data folder. It contains data variables with dynamically changing values.

            ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
            ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
            ' specified explicitly.

            dataFolder.Add(
                New UAFolder("Dynamic") From
                {
                    New UAFolder("Array") From
                    {
                        New UADataVariable("BooleanValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomBoolean)),
                        New UADataVariable("ByteStringValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomByteString)) _
                        , _ ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                          _ ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                          _ ' specified explicitly.
                        New UADataVariable("ByteValue").ReadValueFunction(
                            dataTypeId:=UADataTypeIds.Byte,
                            arrayRank:=1,
                            Function() NextRandomArray(AddressOf NextRandomByte)),
                        New UADataVariable("DateTimeValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomDateTime)),
                        New UADataVariable("DoubleValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomDouble)),
                        New UADataVariable("FloatValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomFloat)),
                        New UADataVariable("GuidValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomGuid)),
                        New UADataVariable("Int16Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomInt16)),
                        New UADataVariable("Int32Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomInt32)),
                        New UADataVariable("Int64Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomInt64)),
                        New UADataVariable("SByteValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomSByte)),
                        New UADataVariable("StringValue").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomString)),
                        New UADataVariable("UInt16Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomUInt16)),
                        New UADataVariable("UInt32Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomUInt32)),
                        New UADataVariable("UInt64Value").ReadValueFunction(Function() NextRandomArray(AddressOf NextRandomUInt64)),
                        New UADataVariable("VariantValue").ReadValueFunction(Function() NextRandomArray(NextRandomVariant))
                    },
                    New UAFolder("Scalar") From
                    {
                        New UADataVariable("BooleanValue").ReadValueFunction(AddressOf NextRandomBoolean),
                        New UADataVariable("ByteStringValue").ReadValueFunction(AddressOf NextRandomByteString),
                        New UADataVariable("ByteValue").ReadValueFunction(AddressOf NextRandomByte),
                        New UADataVariable("DateTimeValue").ReadValueFunction(AddressOf NextRandomDateTime),
                        New UADataVariable("DoubleValue").ReadValueFunction(AddressOf NextRandomDouble),
                        New UADataVariable("FloatValue").ReadValueFunction(AddressOf NextRandomFloat),
                        New UADataVariable("GuidValue").ReadValueFunction(AddressOf NextRandomGuid),
                        New UADataVariable("Int16Value").ReadValueFunction(AddressOf NextRandomInt16),
                        New UADataVariable("Int32Value").ReadValueFunction(AddressOf NextRandomInt32),
                        New UADataVariable("Int64Value").ReadValueFunction(AddressOf NextRandomInt64),
                        New UADataVariable("SByteValue").ReadValueFunction(AddressOf NextRandomSByte),
                        New UADataVariable("StringValue").ReadValueFunction(AddressOf NextRandomString),
                        New UADataVariable("UInt16Value").ReadValueFunction(AddressOf NextRandomUInt16),
                        New UADataVariable("UInt32Value").ReadValueFunction(AddressOf NextRandomUInt32),
                        New UADataVariable("UInt64Value").ReadValueFunction(AddressOf NextRandomUInt64),
                        New UADataVariable("VariantValue").ReadValueFunction(AddressOf NextRandomVariant)
                    }
                })

            ' The FullyWritable sub-folder contains data variables that have not only writable value, but also writable
            ' source timestamp and status code.
            dataFolder.Add(
                New UAFolder("FullyWritable") From
                {
                    New UAFolder("Scalar") From
                    {
                        New UADataVariable("BooleanValue").ReadWriteValue(True) _
                            .Writable(True, True, True),
                        New UADataVariable("ByteStringValue").ReadWriteValue(New Byte() {&H57, &H21, &H40, &HFC}) _
                            .Writable(True, True, True),
                        New UADataVariable("ByteValue").ReadWriteValue(CType(144, Byte)) _
                            .Writable(True, True, True) _
                             , _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                               _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                               _ ' server, and the result will depend on the time zone.
                        New UADataVariable("DateTimeValue").ReadWriteValue(
                                DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                    DateTimeKind.Utc)) _
                            .Writable(True, True, True),
                        New UADataVariable("DoubleValue").ReadWriteValue(0.0000000000775630105797) _
                            .Writable(True, True, True),
                        New UADataVariable("FloatValue").ReadWriteValue(2.77002E+29F) _
                            .Writable(True, True, True),
                        New UADataVariable("GuidValue") _
                            .ReadWriteValue(New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")) _
                            .Writable(True, True, True),
                        New UADataVariable("Int16Value").ReadWriteValue(CType(-30956, Short)) _
                            .Writable(True, True, True),
                        New UADataVariable("Int32Value").ReadWriteValue(276673160) _
                            .Writable(True, True, True),
                        New UADataVariable("Int64Value").ReadWriteValue(1412096336825367659) _
                            .Writable(True, True, True),
                        New UADataVariable("SByteValue").ReadWriteValue(CType(-113, SByte)) _
                            .Writable(True, True, True),
                        New UADataVariable("StringValue").ReadWriteValue("lorem ipsum") _
                            .Writable(True, True, True),
                        New UADataVariable("UInt16Value").ReadWriteValue(CType(64421, UShort)) _
                            .Writable(True, True, True),
                        New UADataVariable("UInt32Value").ReadWriteValue(3853116537UI) _
                            .Writable(True, True, True),
                        New UADataVariable("UInt64Value").ReadWriteValue(9431348106520835314UL) _
                            .Writable(True, True, True),
                        New UADataVariable("VariantValue").ReadWriteValue(529739609) _
                            .Writable(True, True, True)
                    }
                })

            ' The ReadOnly sub-folder contains data variables that are read-only, and their values can be changed through
            ' corresponding data variables in the WriteOnly sub-folder.
            dataFolder.Add(
                New UAFolder("ReadOnly") From
                {
                    New UAFolder("Scalar") From
                    {
                        booleanReadOnlyDataVariable,
                        byteStringReadOnlyDataVariable,
                        byteReadOnlyDataVariable,
                        dateTimeReadOnlyDataVariable,
                        doubleReadOnlyDataVariable,
                        floatReadOnlyDataVariable,
                        guidReadOnlyDataVariable,
                        int16ReadOnlyDataVariable,
                        int32ReadOnlyDataVariable,
                        int64ReadOnlyDataVariable,
                        sByteReadOnlyDataVariable,
                        stringReadOnlyDataVariable,
                        uInt16ReadOnlyDataVariable,
                        uInt32ReadOnlyDataVariable,
                        uInt64ReadOnlyDataVariable,
                        variantReadOnlyDataVariable
                    }
                })

            ' The Static sub-folder contains data variables with static values which can be changed through writing to
            ' them (so-called "registers").

            ' "Array":
            ' For demonstration, we consistently create one-dimensional arrays with initially 3 elements, where the
            ' first element has the same value as the scalar variable with the same name.

            ' "Array.ByteValue":
            ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
            ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
            ' specified explicitly.

            ' "Array.DateTimeValue":
            ' We are passing in UTC times, because we want always the same result, and so we must specify
            ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
            ' server, and the result will depend on the time zone.

            ' "Array2D":
            ' We create 2-dimensional arrays with 4x3 size, and default element values.

            ' "BoundedArray":
            ' Array nodes with specified and enforced maximum array dimensions.

            ' "BoundedArray.ByteValue":
            ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
            ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
            ' specified explicitly.

            ' "Scalar.DateTimeValue":
            ' We are passing in UTC times, because we want always the same result, and so we must specify
            ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
            ' server, and the result will depend on the time zone.

            dataFolder.Add(
                New UAFolder("Static") From
                { _
                  _ ' For demonstration, we consistently create one-dimensional arrays with initially 3 elements, where the
                  _ ' first element has the same value as the scalar variable with the same name.
                    New UAFolder("Array") From
                    {
                        New UADataVariable("BooleanValue").ReadWriteValue(
                        {
                            True,
                            False,
                            True
                        }),
                        New UADataVariable("ByteStringValue").ReadWriteValue(
                        {
                            New Byte() {&H57, &H21, &H40, &HFC},
                            New Byte() {248, 131, 217, 210},
                            New Byte() {252, 152, 119, 65}
                        }), _
                            _ ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                            _ ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                            _ ' specified explicitly.
                        New UADataVariable("ByteValue").ReadWriteValue(
                            dataTypeId:=UADataTypeIds.Byte,
                            arrayRank:=1,
                            value:=New Byte() _
                            {
                                144,
                                19,
                                233
                            }),
                        New UADataVariable("DateTimeValue").ReadWriteValue(
                        { _
                          _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                          _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                          _ ' server, and the result will depend on the time zone.
                            DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                DateTimeKind.Utc),
                            DateTime.SpecifyKind(New DateTime(2024, 4, 8), DateTimeKind.Utc),
                            DateTime.SpecifyKind(New DateTime(2023, 8, 14, 18, 13, 0), DateTimeKind.Utc)
                        }),
                        New UADataVariable("DoubleValue").ReadWriteValue(
                        {
                            0.0000000000775630105797,
                            -0.467227097818268,
                            -3.51653052582609E+300
                        }),
                        New UADataVariable("FloatValue").ReadWriteValue(
                        {
                            2.77002E+29F,
                            -1.103936E+36F,
                            -9.002293E-28F
                        }),
                        New UADataVariable("GuidValue").ReadWriteValue(
                        {
                            New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}"),
                            New Guid("{E8690EA3-25D0-4F19-9DFC-AA25D2772B2F}"),
                            New Guid("{9E081C84-7953-4A88-B709-447FC187EDD9}")
                        }),
                        New UADataVariable("Int16Value").ReadWriteValue(New Short() _
                        {
                            -30956,
                            31277,
                            21977
                        }),
                        New UADataVariable("Int32Value").ReadWriteValue(
                        {
                            276673160,
                            630080334,
                            -391755284
                        }),
                        New UADataVariable("Int64Value").ReadWriteValue(
                        {
                            1412096336825367659,
                            -808781653700434592,
                            4707848393174903135
                        }),
                        New UADataVariable("SByteValue").ReadWriteValue(New SByte() _
                        {
                            -113,
                            -92,
                            2
                        }),
                        New UADataVariable("StringValue").ReadWriteValue(
                        {
                            "lorem ipsum",
                            "dolor sit amet",
                            "consectetur adipiscing elit"
                        }),
                        New UADataVariable("UInt16Value").ReadWriteValue(New UShort() _
                        {
                            64421US,
                            22663US,
                            36755US
                        }),
                        New UADataVariable("UInt32Value").ReadWriteValue(New UInteger() _
                        {
                            3853116537UI,
                            968679231UI,
                            995611904UI
                        }),
                        New UADataVariable("UInt64Value").ReadWriteValue(New ULong() _
                        {
                            9431348106520835314UL,
                            15635738044048254300UL,
                            946287779964705249UL
                        }),
                        New UADataVariable("VariantValue").ReadWriteValue(New Object() _
                        {
                            529739609,
                            "lorem ipsum",
                            New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")
                        })
                    }, _
                       _
                       _ ' We create 2-dimensional arrays with 4x3 size, and default element values.
                    New UAFolder("Array2D") From
                    {
                        New UADataVariable("BooleanValue").ReadWriteValue(New Boolean(3, 2) {}),
                        New UADataVariable("ByteStringValue").ReadWriteValue(New Byte(3, 2)() {}),
                        New UADataVariable("ByteValue").ReadWriteValue(New Byte(3, 2) {}),
                        New UADataVariable("DateTimeValue").ReadWriteValue(New DateTime(3, 2) {}),
                        New UADataVariable("DoubleValue").ReadWriteValue(New Double(3, 2) {}),
                        New UADataVariable("FloatValue").ReadWriteValue(New Single(3, 2) {}),
                        New UADataVariable("GuidValue").ReadWriteValue(New Guid(3, 2) {}),
                        New UADataVariable("Int16Value").ReadWriteValue(New Short(3, 2) {}),
                        New UADataVariable("Int32Value").ReadWriteValue(New Integer(3, 2) {}),
                        New UADataVariable("Int64Value").ReadWriteValue(New Long(3, 2) {}),
                        New UADataVariable("SByteValue").ReadWriteValue(New SByte(3, 2) {}),
                        New UADataVariable("StringValue").ReadWriteValue(New String(3, 2) {}),
                        New UADataVariable("UInt16Value").ReadWriteValue(New UShort(3, 2) {}),
                        New UADataVariable("UInt32Value").ReadWriteValue(New UInteger(3, 2) {}),
                        New UADataVariable("UInt64Value").ReadWriteValue(New ULong(3, 2) {}),
                        New UADataVariable("VariantValue").ReadWriteValue(New Object(3, 2) {})
                    }, _
                       _
                       _ ' Array nodes with specified and enforced maximum array dimensions.
                    New UAFolder("BoundedArray") From
                    {
                        New UADataVariable("BooleanValue").ReadWriteValue(New Boolean(3) {}, 5),
                        New UADataVariable("ByteStringValue").ReadWriteValue(New Byte(3)() {}, 5) _
                        , _
                          _ ' This is a tricky case. We want array of Byte-s, but that is automatically recognized as scalar
                          _ ' OPC UA ByteString. For a true array of Byte-s, the data type Id and array dimension list must be
                          _ ' specified explicitly.
                        New UADataVariable("ByteValue").ReadWriteValue(
                            dataTypeId:=UADataTypeIds.Byte,
                            arrayDimensionList:={5},
                            New Byte(3) {}),
                        New UADataVariable("DateTimeValue").ReadWriteValue(New DateTime(3) {}, 5),
                        New UADataVariable("DoubleValue").ReadWriteValue(New Double(3) {}, 5),
                        New UADataVariable("FloatValue").ReadWriteValue(New Single(3) {}, 5),
                        New UADataVariable("GuidValue").ReadWriteValue(New Guid(3) {}, 5),
                        New UADataVariable("Int16Value").ReadWriteValue(New Short(3) {}, 5),
                        New UADataVariable("Int32Value").ReadWriteValue(New Integer(3) {}, 5),
                        New UADataVariable("Int64Value").ReadWriteValue(New Long(3) {}, 5),
                        New UADataVariable("SByteValue").ReadWriteValue(New SByte(3) {}, 5),
                        New UADataVariable("StringValue").ReadWriteValue(New String(3) {}, 5),
                        New UADataVariable("UInt16Value").ReadWriteValue(New UShort(3) {}, 5),
                        New UADataVariable("UInt32Value").ReadWriteValue(New UInteger(3) {}, 5),
                        New UADataVariable("UInt64Value").ReadWriteValue(New ULong(3) {}, 5),
                        New UADataVariable("VariantValue").ReadWriteValue(New Object(3) {}, 5)
                    }, _
                       _
                    New UAFolder("Scalar") From
                    {
                        New UADataVariable("BooleanValue").ReadWriteValue(True),
                        New UADataVariable("ByteStringValue").ReadWriteValue(New Byte() {&H57, &H21, &H40, &HFC}),
                        New UADataVariable("ByteValue").ReadWriteValue(CByte(144)) _
                        , _
                          _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                          _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                          _ ' server, and the result will depend on the time zone.
                        New UADataVariable("DateTimeValue").ReadWriteValue(
                            DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                                DateTimeKind.Utc)),
                        New UADataVariable("DoubleValue").ReadWriteValue(0.0000000000775630105797),
                        New UADataVariable("FloatValue").ReadWriteValue(2.77002E+29F),
                        New UADataVariable("GuidValue").ReadWriteValue(
                            New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")),
                        New UADataVariable("Int16Value").ReadWriteValue(CType(-30956, Short)),
                        New UADataVariable("Int32Value").ReadWriteValue(276673160),
                        New UADataVariable("Int64Value").ReadWriteValue(1412096336825367659),
                        New UADataVariable("SByteValue").ReadWriteValue(CType(-113, SByte)),
                        New UADataVariable("StringValue").ReadWriteValue("lorem ipsum"),
                        New UADataVariable("UInt16Value").ReadWriteValue(CType(64421, UShort)),
                        New UADataVariable("UInt32Value").ReadWriteValue(3853116537UI),
                        New UADataVariable("UInt64Value").ReadWriteValue(9431348106520835314UL),
                        New UADataVariable("VariantValue").ReadWriteValue(529739609)
                    }
                })

            ' Create and add write-only data variables of various data types. Implement write actions that write the value
            ' to the corresponding read-only data variable of the same data type.
            dataFolder.Add(
                New UAFolder("WriteOnly") From
                {
                    New UAFolder("Scalar") From
                    {
                        New UADataVariable("BooleanValue").Readable(False).WriteValueAction(Sub(value As Boolean) _
                            booleanReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("ByteStringValue").Readable(False).WriteValueAction(Sub(value As Byte()) _
                            byteStringReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("ByteValue").Readable(False).WriteValueAction(Sub(value As Byte) _
                            byteReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("DateTimeValue").Readable(False).WriteValueAction(Sub(value As DateTime) _
                            dateTimeReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("DoubleValue").Readable(False).WriteValueAction(Sub(value As Double) _
                            doubleReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("FloatValue").Readable(False).WriteValueAction(Sub(value As Single) _
                            floatReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("GuidValue").Readable(False).WriteValueAction(Sub(value As Guid) _
                            guidReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("Int16Value").Readable(False).WriteValueAction(Sub(value As Short) _
                            int16ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("Int32Value").Readable(False).WriteValueAction(Sub(value As Integer()) _
                            int32ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("Int64Value").Readable(False).WriteValueAction(Sub(value As Long) _
                            int64ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("SByteValue").Readable(False).WriteValueAction(Sub(value As SByte) _
                            sByteReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("StringValue").Readable(False).WriteValueAction(Sub(value As String) _
                            stringReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("UInt16Value").Readable(False).WriteValueAction(Sub(value As UShort) _
                            uInt16ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("UInt32Value").Readable(False).WriteValueAction(Sub(value As UInteger) _
                            uInt32ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("UInt64Value").Readable(False).WriteValueAction(Sub(value As ULong) _
                            uInt64ReadOnlyDataVariable.UpdateReadAttributeData(value)),
                        New UADataVariable("VariantValue").Readable(False).WriteValueAction(Sub(value As Object) _
                            variantReadOnlyDataVariable.UpdateReadAttributeData(value))
                    }
                })

        End Sub

        ' Random value generators.
        Private ReadOnly Random As Random = New Random()

        Private ReadOnly RandomStrings As String() = {"lorem", "ipsum", "dolor", "sit", "amet"}

        Private Function NextRandomArray(Of T)(nextRandomElement As Func(Of T)) As T()
            Return {nextRandomElement(), nextRandomElement(), nextRandomElement()}
        End Function

        Private Function NextRandomBoolean() As Boolean
            Return Random.Next(2) <> 0
        End Function

        Private Function NextRandomByte() As Byte
            Return CType(Random.Next(Byte.MinValue, Byte.MaxValue + 1), Byte)
        End Function

        Private Function NextRandomByteString() As Byte()
            Return {NextRandomByte(), NextRandomByte(), NextRandomByte(), NextRandomByte()}
        End Function

        Private Function NextRandomDateTime() As DateTime
            Return DateTime.MinValue.AddMilliseconds((DateTime.MaxValue - DateTime.MinValue).TotalMilliseconds *
                                              Random.NextDouble())
        End Function

        Private Function NextRandomFloat() As Single
            Return CType(Math.Pow(10, Math.Log10(Single.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1), Single)
        End Function

        Private Function NextRandomDouble() As Double
            Return Math.Pow(10, Math.Log10(Double.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1)
        End Function

        Private Function NextRandomGuid() As Guid
            Return Guid.NewGuid()
        End Function

        Private Function NextRandomInt16() As Short
            Return CType(Random.Next(Short.MinValue, Short.MaxValue + 1), Short)
        End Function

        Private Function NextRandomInt32() As Integer
            Dim buffer As Byte() = New Byte(3) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToInt32(buffer, 0)
        End Function

        Private Function NextRandomInt64() As Long
            Dim buffer As Byte() = New Byte(7) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToInt64(buffer, 0)
        End Function

        Private Function NextRandomSByte() As SByte
            Return CType(Random.Next(SByte.MinValue, SByte.MaxValue + 1), SByte)
        End Function

        Private Function NextRandomString() As String
            Return RandomStrings(Random.Next(RandomStrings.Length))
        End Function

        Private Function NextRandomUInt16() As UShort
            Return CType(Random.Next(UShort.MinValue, UShort.MaxValue + 1), UShort)
        End Function

        Private Function NextRandomUInt32() As UInteger
            Dim buffer As Byte() = New Byte(3) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToUInt32(buffer, 0)
        End Function

        Private Function NextRandomUInt64() As ULong
            Dim buffer As Byte() = New Byte(7) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToUInt64(buffer, 0)
        End Function

        Private Function NextRandomVariant() As Object
            Select Case Random.Next(15)
                Case 0
                    Return NextRandomBoolean()
                Case 1
                    Return NextRandomByteString()
                Case 2
                    Return NextRandomByte()
                Case 3
                    Return NextRandomDateTime()
                Case 4
                    Return NextRandomDouble()
                Case 5
                    Return NextRandomFloat()
                Case 6
                    Return NextRandomGuid()
                Case 7
                    Return NextRandomInt16()
                Case 8
                    Return NextRandomInt32()
                Case 9
                    Return NextRandomInt64()
                Case 10
                    Return NextRandomSByte()
                Case 11
                    Return NextRandomString()
                Case 12
                    Return NextRandomUInt16()
                Case 13
                    Return NextRandomUInt32()
                Case 14
                    Return NextRandomUInt64()
                Case Else
                    Return Nothing
            End Select
        End Function
    End Module
End Namespace
#End Region

