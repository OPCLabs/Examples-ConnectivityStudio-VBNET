' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable UseObjectOrCollectionInitializer
#Region "Example"
' This example shows different ways of constructing generic data.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Collections
Imports OpcLabs.BaseLib.DataTypeModel

Namespace Global.UADocExamples.ComplexData._GenericData

    Friend Class _Construction

        Public Shared Sub Main1()
            ' Create enumeration data with value of 1.
            Dim enumerationData = New EnumerationData(1)
            Console.WriteLine(enumerationData)

            ' Create opaque data from an array of 2 bytes, specifying its size as 15 bits.
            Dim opaqueData1 = New OpaqueData(New Byte() {170, 85}, sizeInBits:=15)
            Console.WriteLine(opaqueData1)

            ' Create opaque data from a bit array.
            Dim opaqueData2 = New OpaqueData(New BitArray(New Boolean() {False, True, False, True, False}))
            Console.WriteLine(opaqueData2)

            ' Create primitive data with System.Double value of 180.0.
            Dim primitiveData1 = New PrimitiveData(180)
            Console.WriteLine(primitiveData1)

            ' Create primitive data with System.String value.
            Dim primitiveData2 = New PrimitiveData("Temperature is too high!")
            Console.WriteLine(primitiveData2)

            ' Create sequence data with two elements, using collection initializer syntax.
            Dim sequenceData1 = New SequenceData() From {opaqueData1, opaqueData2}
            Console.WriteLine(sequenceData1)

            ' Create the same sequence data, using the Add method.
            Dim sequenceData2 = New SequenceData
            sequenceData2.Elements.Add(opaqueData1)
            sequenceData2.Elements.Add(opaqueData2)
            Console.WriteLine(sequenceData2)

            ' Create the same sequence data, using an array (an enumerable) of its elements.
            Dim sequenceData3 = New SequenceData(New GenericDataCollection(New OpaqueData() {opaqueData1, opaqueData2}))
            Console.WriteLine(sequenceData3)

            ' Create structured data with two members, using collection initializer syntax.
            Dim structuredData1 = New StructuredData() From { _
                {"Message", primitiveData2}, _
                {"Status", enumerationData}}
            Console.WriteLine(structuredData1)

            ' Create the same structured data using the Add method.
            Dim structuredData2 = New StructuredData()
            structuredData2.Add("Message", primitiveData2)
            structuredData2.Add("Status", enumerationData)
            Console.WriteLine(structuredData2)
        End Sub
    End Class
End Namespace

#End Region
