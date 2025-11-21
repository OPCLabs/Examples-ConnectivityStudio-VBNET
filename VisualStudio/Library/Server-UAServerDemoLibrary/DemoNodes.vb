' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
' ReSharper disable StringLiteralTypo
#Region "Example"
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Generic
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDemoLibrary
    Public Module DemoNodes
        ''' <summary>
        ''' Adds nodes that demonstrate various features of the OPC Wizard.
        ''' </summary>
        ''' <param name="parentFolder">The folder to which to add the nodes.</param>
        Sub AddToParent(parentFolder As UAFolder)
            ' Demonstrate that in the simplest case, the data variable can be added directly to the Objects folder.
            parentFolder.Add(New UADataVariable("Simple").ReadWriteValue(0))

            ' Demonstrate the fact that nodes can be organized in folders, and that data variables can be nested.
            Dim demoFolder As UAFolder = UAFolder.CreateIn(parentFolder, "Demo")

            ' Demonstrate that the data variables can be nested.
            demoFolder.Add(
                New UADataVariable("Random") From
                {
                    New UADataVariable("Nested").ReadValueFunction(Function() Random.NextDouble())
                }.ReadValueFunction(Function() Random.NextDouble()))

            ' Demonstrate that the data values returned can also contain status codes that are not fully "Good".
            demoFolder.Add(New UADataVariable("GoodLocalOverride").ReadWrite(New UAAttributeData(Of Integer)(
                value:=0,
                UACodeBits.GoodLocalOverride,
                DateTime.UtcNow)))
            demoFolder.Add(New UADataVariable("BadNoCommunication").ReadWrite(New UAAttributeData(Of Integer)(
                value:=0,
                UACodeBits.BadNoCommunication,
                DateTime.UtcNow)))
            demoFolder.Add(New UADataVariable("Unreliable").ReadFunction(Function() New UAAttributeData(Of Integer)(
                value:=42,
                If(Random.Next(2) <> 0, UACodeBits.Good, UACodeBits.BadNoCommunication),
                DateTime.UtcNow)))

            ' Demonstrate that the data variable may decide to fail the write operation.
            demoFolder.Add(New UADataVariable("WriteFailure").WriteFunction(Of Integer)(Function() UACodeBits.BadNoCommunication))

            ' Depending on the needs, you can specify custom minimum sampling interval for the data variable.
            demoFolder.Add(New UADataVariable("SlowSampling") _
                .ReadValueFunction(Function() Random.Next()) _
            .SetMinimumSamplingInterval(5 * 1000))

            ' Create a 3-dimensional array data variable.
            demoFolder.Add(New UADataVariable("Array3D").ReadWriteValue(New Integer(1, 3, 2) {}))


            ' A data variable of data type BaseDataType that, in fact, only accepts float values to be written into it.
            ' This is a demonstration of how the data variable should *not* behave, because the client has no way of
            ' determining the data type that the server expects.
            Dim variantRestrictedDataVariable = New UADataVariable("VariantRestricted")
            demoFolder.Add(variantRestrictedDataVariable _
                .WriteValueFunction(Of Object)(Function(value As Object) As UAStatusCode
                                                   If (TypeOf value Is Single) Then
                                                       variantRestrictedDataVariable.UpdateWriteAttributeData(value)
                                                       Return Nothing
                                                   End If
                                                   Return UACodeBits.BadTypeMismatch
                                               End Function
                ) _
                .ReadWriteValue(CType(0.0F, Object)))
        End Sub

        Private ReadOnly Random As Random = New Random()
    End Module
End Namespace
#End Region

