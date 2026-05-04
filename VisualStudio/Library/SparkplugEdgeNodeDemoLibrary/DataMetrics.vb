' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
' ReSharper disable StringLiteralTypo
#Region "Example"
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugEdgeNodeDemoLibrary
    Public Module DataMetrics
        Public Function Create() As SparkplugMetric()
            ' Create read-only metrics of various data types, without adding them to the result first. We store
            ' references to them individually, because we later implement write-only metrics that write to these
            ' read-only metrics.
            Dim booleanReadOnlyMetric = New SparkplugMetric("ReadOnly/BooleanValue").Writable(False).ValueType(Of Boolean)()
            Dim bytesReadOnlyMetric = New SparkplugMetric("ReadOnly/BytesValue").Writable(False).ValueType(Of Byte())()
            Dim dateTimeReadOnlyMetric = New SparkplugMetric("ReadOnly/DateTimeValue").Writable(False).ValueType(Of DateTime)()
            Dim doubleReadOnlyMetric = New SparkplugMetric("ReadOnly/DoubleValue").Writable(False).ValueType(Of Double)()
            Dim floatReadOnlyMetric = New SparkplugMetric("ReadOnly/FloatValue").Writable(False).ValueType(Of Single)()
            Dim int16ReadOnlyMetric = New SparkplugMetric("ReadOnly/Int16Value").Writable(False).ValueType(Of Short)()
            Dim int32ReadOnlyMetric = New SparkplugMetric("ReadOnly/Int32Value").Writable(False).ValueType(Of Integer)()
            Dim int64ReadOnlyMetric = New SparkplugMetric("ReadOnly/Int64Value").Writable(False).ValueType(Of Long)()
            Dim int8ReadOnlyMetric = New SparkplugMetric("ReadOnly/Int8Value").Writable(False).ValueType(Of SByte)()
            Dim stringReadOnlyMetric = New SparkplugMetric("ReadOnly/StringValue").Writable(False).ValueType(Of String)()
            ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
            ' set explicitly for Text metrics, otherwise the default String data type is used.
            Dim textReadOnlyMetric = New SparkplugMetric("ReadOnly/TextValue").Writable(False).ValueType(SparkplugDataType.Text)
            Dim uInt16ReadOnlyMetric = New SparkplugMetric("ReadOnly/UInt16Value").Writable(False).ValueType(Of UShort)()
            Dim uInt32ReadOnlyMetric = New SparkplugMetric("ReadOnly/UInt32Value").Writable(False).ValueType(Of UInteger)()
            Dim uInt64ReadOnlyMetric = New SparkplugMetric("ReadOnly/UInt64Value").Writable(False).ValueType(Of ULong)()
            Dim uInt8ReadOnlyMetric = New SparkplugMetric("ReadOnly/UInt8Value").Writable(False).ValueType(Of Byte)()
            Dim uuidReadOnlyMetric = New SparkplugMetric("ReadOnly/UuidValue").Writable(False).ValueType(Of Guid)()

            Return { _
                     _ ' Create Constant sub-folder. It contains read-only metrics with constant values.
                New SparkplugMetric("Constant/BooleanValue").ConstantValue(True),
                New SparkplugMetric("Constant/BytesValue").ConstantValue(New Byte() {&H57, &H21, &H40, &HFC}),
                New SparkplugMetric("Constant/DateTimeValue").ConstantValue _
                ( _
                  _ ' We are passing In UTC times, because we want always the same result, And so we must specify
                  _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                  _ ' producer, And the result will depend on the time zone.
                    DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc)),
                New SparkplugMetric("Constant/DoubleValue").ConstantValue(0.0000000000775630105797),
                New SparkplugMetric("Constant/FloatValue").ConstantValue(2.77002E+29F),
                New SparkplugMetric("Constant/Int16Value").ConstantValue(CShort(30956)),
                New SparkplugMetric("Constant/Int32Value").ConstantValue(276673160),
                New SparkplugMetric("Constant/Int64Value").ConstantValue(1412096336825367659),
                New SparkplugMetric("Constant/Int8Value").ConstantValue(CSByte(-113)),
                New SparkplugMetric("Constant/StringValue").ConstantValue("lorem ipsum") _
                ,
                 _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                 _ ' for Text metrics, otherwise the default String data type is used.
                New SparkplugMetric("Constant/TextValue").ConstantValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                New SparkplugMetric("Constant/UInt16Value").ConstantValue(CUShort(64421)),
                New SparkplugMetric("Constant/UInt32Value").ConstantValue(3853116537UI),
                New SparkplugMetric("Constant/UInt64Value").ConstantValue(9431348106520835314UL),
                New SparkplugMetric("Constant/UInt8Value").ConstantValue(CByte(144)),
                New SparkplugMetric("Constant/UuidValue").ConstantValue(
                    New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")) _
                    ,
                     _
                     _ ' Create Dynamic sub-folder. It contains metrics with dynamically changing values.
                New SparkplugMetric("Dynamic/BooleanValue").ReadValueFunction(Function() NextRandomBoolean()),
                New SparkplugMetric("Dynamic/BytesValue").ReadValueFunction(Function() NextRandomBytes()),
                New SparkplugMetric("Dynamic/DateTimeValue").ReadValueFunction(Function() NextRandomDateTime()),
                New SparkplugMetric("Dynamic/DoubleValue").ReadValueFunction(Function() NextRandomDouble()),
                New SparkplugMetric("Dynamic/FloatValue").ReadValueFunction(Function() NextRandomFloat()),
                New SparkplugMetric("Dynamic/Int16Value").ReadValueFunction(Function() NextRandomInt16()),
                New SparkplugMetric("Dynamic/Int32Value").ReadValueFunction(Function() NextRandomInt32()),
                New SparkplugMetric("Dynamic/Int64Value").ReadValueFunction(Function() NextRandomInt64()),
                New SparkplugMetric("Dynamic/Int8Value").ReadValueFunction(Function() NextRandomInt8()),
                New SparkplugMetric("Dynamic/StringValue").ReadValueFunction(Function() NextRandomString()) _
                ,
                 _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                 _ ' for Text metrics, otherwise the default String data type is used.
                New SparkplugMetric("Dynamic/TextValue").ReadValueFunction(SparkplugDataType.Text, Function() NextRandomText()),
                New SparkplugMetric("Dynamic/UInt16Value").ReadValueFunction(Function() NextRandomUInt16()),
                New SparkplugMetric("Dynamic/UInt32Value").ReadValueFunction(Function() NextRandomUInt32()),
                New SparkplugMetric("Dynamic/UInt64Value").ReadValueFunction(Function() NextRandomUInt64()),
                New SparkplugMetric("Dynamic/UInt8Value").ReadValueFunction(Function() NextRandomUInt8()),
                New SparkplugMetric("Dynamic/UuidValue").ReadValueFunction(Function() NextRandomUuid()),
                                                                                                        _
                New SparkplugMetric("Dynamic/BooleanArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomBoolean())),
                New SparkplugMetric("Dynamic/DateTimeArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomDateTime())),
                New SparkplugMetric("Dynamic/DoubleArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomDouble())),
                New SparkplugMetric("Dynamic/FloatArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomFloat())),
                New SparkplugMetric("Dynamic/Int16ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomInt16())),
                New SparkplugMetric("Dynamic/Int32ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomInt32())),
                New SparkplugMetric("Dynamic/Int64ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomInt64())),
                New SparkplugMetric("Dynamic/Int8ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomInt8())),
                New SparkplugMetric("Dynamic/StringArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomString())),
                New SparkplugMetric("Dynamic/UInt16ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomUInt16())),
                New SparkplugMetric("Dynamic/UInt32ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomUInt32())),
                New SparkplugMetric("Dynamic/UInt64ArrayValue").ReadValueFunction(
                    Function() NextRandomArray(Function() NextRandomUInt64())) _
                    ,
                     _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                     _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                New SparkplugMetric("Dynamic/UInt8ArrayValue").ReadValueFunction(SparkplugDataType.UInt8Array,
                    Function() NextRandomArray(Function() NextRandomUInt8())) _
                    ,
                     _
                     _ ' The FullyWritable sub-folder contains metrics that have not only writable value, but also writable
                     _ ' timestamp.
                New SparkplugMetric("FullyWritable/BooleanValue").ReadWriteValue(True),
                New SparkplugMetric("FullyWritable/BytesValue").ReadWriteValue(New Byte() {&H57, &H21, &H40, &HFC}),
                New SparkplugMetric("FullyWritable/DateTimeValue").ReadWriteValue _
                ( _
                  _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                  _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                  _ ' producer, and the result will depend on the time zone.
                    DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                    DateTimeKind.Utc)),
                New SparkplugMetric("FullyWritable/DoubleValue").ReadWriteValue(0.0000000000775630105797),
                New SparkplugMetric("FullyWritable/FloatValue").ReadWriteValue(2.77002E+29F),
                New SparkplugMetric("FullyWritable/Int16Value").ReadWriteValue(CShort(-30956)),
                New SparkplugMetric("FullyWritable/Int32Value").ReadWriteValue(276673160),
                New SparkplugMetric("FullyWritable/Int64Value").ReadWriteValue(1412096336825367659),
                New SparkplugMetric("FullyWritable/Int8Value").ReadWriteValue(CSByte(-113)),
                New SparkplugMetric("FullyWritable/StringValue").ReadWriteValue("lorem ipsum") _
                ,
                 _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                 _ ' for Text metrics, otherwise the default String data type is used.
                New SparkplugMetric("FullyWritable/TextValue").ReadWriteValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                New SparkplugMetric("FullyWritable/UInt16Value").ReadWriteValue(CUShort(64421)),
                New SparkplugMetric("FullyWritable/UInt32Value").ReadWriteValue(3853116537UI),
                New SparkplugMetric("FullyWritable/UInt64Value").ReadWriteValue(9431348106520835314UL),
                New SparkplugMetric("FullyWritable/UInt8Value").ReadWriteValue(CByte(144)),
                New SparkplugMetric("FullyWritable/UuidValue").ReadWriteValue(
                    New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")) _
                    ,
                     _
                     _ ' The ReadOnly sub-folder contains metrics that are read-only, and their values can be changed through
                     _ ' corresponding data metrics in the WriteOnly sub-folder.
                booleanReadOnlyMetric,
                bytesReadOnlyMetric,
                dateTimeReadOnlyMetric,
                doubleReadOnlyMetric,
                floatReadOnlyMetric,
                int16ReadOnlyMetric,
                int32ReadOnlyMetric,
                int64ReadOnlyMetric,
                int8ReadOnlyMetric,
                stringReadOnlyMetric,
                textReadOnlyMetric,
                uInt16ReadOnlyMetric,
                uInt32ReadOnlyMetric,
                uInt64ReadOnlyMetric,
                uInt8ReadOnlyMetric,
                uuidReadOnlyMetric _
                ,
                 _
                 _ ' The Static sub-folder contains metrics with static values which can be changed through writing to
                 _ ' them (so-called "registers").
                New SparkplugMetric("Static/BooleanValue").ReadWriteValue(True),
                New SparkplugMetric("Static/BytesValue").ReadWriteValue(New Byte() {&H57, &H21, &H40, &HFC}),
                New SparkplugMetric("Static/DateTimeValue").ReadWriteValue _
                (
                 _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                 _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                 _ ' server, and the result will depend on the time zone.
                        DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc)),
                New SparkplugMetric("Static/DoubleValue").ReadWriteValue(0.0000000000775630105797),
                New SparkplugMetric("Static/FloatValue").ReadWriteValue(2.77002E+29F),
                New SparkplugMetric("Static/Int16Value").ReadWriteValue(CShort(-30956)),
                New SparkplugMetric("Static/Int32Value").ReadWriteValue(276673160),
                New SparkplugMetric("Static/Int64Value").ReadWriteValue(1412096336825367659),
                New SparkplugMetric("Static/Int8Value").ReadWriteValue(CSByte(-113)),
                New SparkplugMetric("Static/StringValue").ReadWriteValue("lorem ipsum") _
                ,
                 _ ' Sparkplug Text and String data types are both represented as System.String in .NET. The data type must be
                 _ ' for Text metrics, otherwise the default String data type is used.
                New SparkplugMetric("Static/TextValue").ReadWriteValue(SparkplugDataType.Text, "lorem ipsum dolor sit"),
                New SparkplugMetric("Static/UInt16Value").ReadWriteValue(CUShort(64421)),
                New SparkplugMetric("Static/UInt32Value").ReadWriteValue(3853116537UI),
                New SparkplugMetric("Static/UInt64Value").ReadWriteValue(9431348106520835314UL),
                New SparkplugMetric("Static/UInt8Value").ReadWriteValue(CByte(144)),
                New SparkplugMetric("Static/UuidValue").ReadWriteValue(
                    New Guid("{1AEF59AE-5029-42A7-9AE2-B2DC00072999}")), _
                                                                         _
                New SparkplugMetric("Static/BooleanArrayValue").ReadWriteValue(
                {
                    True,
                    False,
                    True
                }),
                New SparkplugMetric("Static/DateTimeArrayValue").ReadWriteValue(
                { _
                  _ ' We are passing in UTC times, because we want always the same result, and so we must specify
                  _ ' the DateTimeKind. You can pass in local times, but then they will be converted to UTC by the
                  _ ' server, and the result will depend on the time zone.
                    DateTime.SpecifyKind(New DateTime(2024, 7, 12, 14, 4, 55).AddSeconds(0.444),
                        DateTimeKind.Utc),
                    DateTime.SpecifyKind(New DateTime(2024, 4, 8), DateTimeKind.Utc),
                    DateTime.SpecifyKind(New DateTime(2023, 8, 14, 18, 13, 0), DateTimeKind.Utc)
                }),
                New SparkplugMetric("Static/DoubleArrayValue").ReadWriteValue(
                {
                    0.0000000000775630105797,
                    -0.467227097818268,
                    -3.51653052582609E+300
                }),
                New SparkplugMetric("Static/FloatArrayValue").ReadWriteValue(
                {
                    2.77002E+29F,
                    -1.103936E+36F,
                    -9.002293E-28F
                }),
                New SparkplugMetric("Static/Int16ArrayValue").ReadWriteValue(New Short() _
                {
                    -30956,
                    31277,
                    21977
                }),
                New SparkplugMetric("Static/Int32ArrayValue").ReadWriteValue(
                {
                    276673160,
                    630080334,
                    -391755284
                }),
                New SparkplugMetric("Static/Int64ArrayValue").ReadWriteValue(
                {
                    1412096336825367659,
                    -808781653700434592,
                    4707848393174903135
                }),
                New SparkplugMetric("Static/Int8ArrayValue").ReadWriteValue(New SByte() _
                {
                    -113,
                    -92,
                    2
                }),
                New SparkplugMetric("Static/StringArrayValue").ReadWriteValue(
                {
                    "lorem ipsum",
                    "dolor sit amet",
                    "consectetur adipiscing elit"
                }),
                New SparkplugMetric("Static/UInt16ArrayValue").ReadWriteValue(New UShort() _
                {
                    64421,
                    22663,
                    36755
                }),
                New SparkplugMetric("Static/UInt32ArrayValue").ReadWriteValue(New UInteger() _
                {
                    3853116537,
                    968679231,
                    995611904
                }),
                New SparkplugMetric("Static/UInt64ArrayValue").ReadWriteValue(New ULong() _
                {
                    9431348106520835314UL,
                    15635738044048254300UL,
                    946287779964705249
                }), _
                    _ ' This is a tricky case. We want array of UInt8-s, but that is automatically recognized as scalar
                    _ ' Sparkplug Bytes. For a true array of Byte-s, the data type must be specified explicitly.
                New SparkplugMetric("Static/UInt8ArrayValue").ReadWriteValue(
                    dataType:=SparkplugDataType.UInt8Array,
                    value:=New Byte() _
                    {
                        144,
                        19,
                        233
                    }), _
                        _
                        _ ' Create and add write-only metrics of various data types. Implement write actions that write the value
                        _ ' to the corresponding read-only metric of the same data type.
                New SparkplugMetric("WriteOnly/BooleanValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As Boolean) booleanReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/BytesValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As Byte()) bytesReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/DateTimeValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As DateTime) dateTimeReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/DoubleValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As Double) doubleReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/FloatValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As Single) floatReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/Int16Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As Short) int16ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/Int32Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As Integer()) int32ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/Int64Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As Long) int64ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/Int8Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As SByte) int8ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/StringValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As String) stringReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/UInt16Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As UShort) uInt16ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/UInt32Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As UInteger) uInt32ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/UInt64Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As ULong) uInt64ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/UInt8Value").Readable(False).WriteValueAction(
                    Sub(ByVal value As Byte) uInt8ReadOnlyMetric.UpdateReadData(value)),
                New SparkplugMetric("WriteOnly/UuidValue").Readable(False).WriteValueAction(
                    Sub(ByVal value As Guid) uuidReadOnlyMetric.UpdateReadData(value))
            }
        End Function

        ' Random value generators.

        Private ReadOnly Random As Random = New Random()

        Private ReadOnly RandomStrings As String() = {"lorem", "ipsum", "dolor", "sit", "amet"}

        Private Function NextRandomArray(Of T)(ByVal [nextRandomElement] As Func(Of T)) As T()
            Return {nextRandomElement(), nextRandomElement(), nextRandomElement()}
        End Function

        Private Function NextRandomBoolean() As Boolean
            Return Random.Next(2) <> 0
        End Function

        Private Function NextRandomBytes() As Byte()
            Return {NextRandomUInt8(), NextRandomUInt8(), NextRandomUInt8(), NextRandomUInt8()}
        End Function

        Private Function NextRandomDateTime() As DateTime
            Return DateTime.MinValue.AddMilliseconds((DateTime.MaxValue - New DateTime(1700, 1, 1)).TotalMilliseconds *
                                              Random.NextDouble())
        End Function

        Private Function NextRandomFloat() As Single
            Return CSng(Math.Pow(10, Math.Log10(Single.MaxValue) * Random.NextDouble())) * (2 * Random.Next(2) - 1)
        End Function

        Private Function NextRandomDouble() As Double
            Return Math.Pow(10, Math.Log10(Double.MaxValue) * Random.NextDouble()) * (2 * Random.Next(2) - 1)
        End Function

        Private Function NextRandomInt16() As Short
            Return CShort(Random.Next(Short.MinValue, Short.MaxValue + 1))
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

        Private Function NextRandomInt8() As SByte
            Return CSByte(Random.Next(SByte.MinValue, SByte.MaxValue + 1))
        End Function

        Private Function NextRandomString() As String
            Return RandomStrings(Random.Next(RandomStrings.Length))
        End Function

        Private Function NextRandomText() As String
            Return NextRandomString() + " " + NextRandomString()
        End Function

        Private Function NextRandomUInt16() As UShort
            Return CUShort(Random.Next(UShort.MinValue, UShort.MaxValue + 1))
        End Function

        Private Function NextRandomUInt32() As UInteger
            Dim buffer As Byte() = New Byte(3) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToUInt32(buffer, 0)
        End Function

        Private Function NextRandomUInt64() As ULong
            Dim buffer = New Byte(7) {}
            Random.NextBytes(buffer)
            Return BitConverter.ToUInt64(buffer, 0)
        End Function

        Private Function NextRandomUInt8() As Byte
            Return Convert.ToByte(Random.Next(Byte.MinValue, Byte.MaxValue + 1))
        End Function

        Private Function NextRandomUuid() As Guid
            Return Guid.NewGuid()
        End Function
    End Module
End Namespace
#End Region
