' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain a data type of all OPC XML-DA items under a branch.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.DataAccess.Xml
    Friend Class GetMultiplePropertyValues
        Public Shared Sub DataTypeXml()
            Dim client = New EasyDAClient()
            Dim serverDescriptor As ServerDescriptor = "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx"

            ' Browse for all leaves under the "Simulation" branch
            Dim nodeElementCollection = client.BrowseLeaves(serverDescriptor, "Static/Analog Types")

            ' Create list of node descriptors, one for each leaf obtained
            ' filter out hint leafs that do not represent real OPC items (rare)
            Dim nodeDescriptorArray() As DANodeDescriptor = nodeElementCollection _
                .Where(Function(element) Not element.IsHint) _
                .Select(Function(element) New DANodeDescriptor(element)) _
                .ToArray()

            ' Get the value of DataType property; it is a 16-bit signed integer
            Dim valueResultArray() As ValueResult = client.GetMultiplePropertyValues(serverDescriptor,
                    nodeDescriptorArray, DAPropertyIds.DataType)

            For i = 0 To valueResultArray.Length - 1
                Dim nodeDescriptor = nodeDescriptorArray(i)

                ' Check if there has been an error getting the property value
                Dim valueResult As ValueResult = valueResultArray(i)
                If valueResult.Exception IsNot Nothing Then
                    Console.WriteLine("{0} *** Failure: {1}", nodeDescriptor.NodeId, valueResult.Exception.Message)
                    Continue For
                End If

                ' Convert the data type to VarType
                Dim varType = CType(CShort(valueResult.Value), VarType)

                ' Display the obtained data type
                Console.WriteLine("{0}: {1}", nodeDescriptor.ItemId, varType)
            Next i
        End Sub
    End Class
End Namespace
#End Region
