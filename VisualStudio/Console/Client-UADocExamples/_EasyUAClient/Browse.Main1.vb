' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain nodes under a given node of the OPC-UA address space. 
' For each node, it displays its browse name and node ID.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.Navigation.Parsing
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class Browse
        Public Shared Sub Main1()
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Dim browsePathParser As UABrowsePathParser = New UABrowsePathParser("http://test.org/UA/Data/")
            Dim nodeDescriptor As UANodeDescriptor = browsePathParser.Parse("[ObjectsFolder]/Data/Static/UserScalar")

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' perform the operation
            Dim nodeElementCollection As UANodeElementCollection
            Try
                nodeElementCollection = client.Browse(
                    endpointDescriptor,
                    nodeDescriptor,
                    UABrowseParameters.AllForwardReferences)
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}")
                Exit Sub
            End Try

            ' Display results
            For Each nodeElement As UANodeElement In nodeElementCollection
                Debug.Assert(nodeElement IsNot Nothing)
                Console.WriteLine($"{nodeElement.BrowseName}: {nodeElement.NodeId}")
            Next nodeElement
        End Sub

        ' Example output
        '
        'BooleanValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10384
        'SByteValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10385
        'ByteValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10386
        'Int16Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10387
        'UInt16Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10388
        'Int32Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10389
        'UInt32Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10390
        'Int64Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10391
        'UInt64Value: nsu=http://test.org/UA/Data/ ;ns=2;i=10392
        'FloatValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10393
        'DoubleValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10394
        'StringValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10395
        'DateTimeValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10396
        'GuidValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10397
        'ByteStringValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10398
        'XmlElementValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10399
        'NodeIdValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10400
        'ExpandedNodeIdValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10401
        'QualifiedNameValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10402
        'LocalizedTextValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10403
        'StatusCodeValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10404
        'VariantValue: nsu=http://test.org/UA/Data/ ;ns=2;i=10405
        'SimulationActive: nsu=http://test.org/UA/Data/ ;ns=2;i=10328
        'GenerateValues: nsu=http://test.org/UA/Data/ ;ns=2;i=10329
        'CycleComplete: nsu=http://test.org/UA/Data/ ;ns=2;i=10331
        'UserScalarValueObjectType: nsu=http://test.org/UA/Data/ ;ns=2;i=9921
    End Class
End Namespace

#End Region
