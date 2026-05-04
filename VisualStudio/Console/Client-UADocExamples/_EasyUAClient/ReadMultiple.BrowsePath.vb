' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to read the attributes of 4 OPC-UA nodes specified by browse paths at once, and display the
' results.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Navigation.Parsing
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class ReadMultiple
        Public Shared Sub BrowsePath()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            Dim browsePathParser = New UABrowsePathParser()
            browsePathParser.DefaultNamespaceUriString = "http://test.org/UA/Data/"

            ' Prepare arguments
            ' Note: Add error handling around the following statement if the browse paths are Not guaranteed to be
            ' syntactically valid.
            Dim readArgumentsArray = New UAReadArguments() _
            {
                New UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Dynamic/Scalar/FloatValue")),
                New UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Dynamic/Scalar/SByteValue")),
                New UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Static/Array/UInt16Value")),
                New UAReadArguments(endpointDescriptor,
                    browsePathParser.Parse("[ObjectsFolder]/Data/Static/UserScalar/Int32Value"))
            }


            ' Obtain attribute data. By default, the Value attributes of the nodes will be read.
            Dim resultArray() As UAAttributeDataResult = client.ReadMultiple(readArgumentsArray)

            ' Display results
            For i As Integer = 0 To resultArray.Length - 1
                Dim attributeDataResult As UAAttributeDataResult = resultArray(i)
                If attributeDataResult.Succeeded Then
                    Console.WriteLine("results[{0}].AttributeData: {1}", i, attributeDataResult.AttributeData)
                Else
                    Console.WriteLine("results[{0}]: *** Failure: {1}", i, attributeDataResult.ErrorMessageBrief)
                End If
            Next i

            ' Example output:
            'results[0].AttributeData 4.187603E+21 {System.Single} @2019-11-09T14:05:46.268 @@2019-11-09T14:05:46.268; Good
            'results[1].AttributeData: -98 {System.Int16} @2019-11-09T14:05:46.268 @@2019-11-09T14:05:46.268; Good
            'results[2].AttributeData: [58] {38240, 11129, 64397, 22845, 30525, ...} {System.Int32[]} @2019-11-09T14:00:07.543 @@2019-11-09T14:05:46.268; Good
            'results[3].AttributeData: 1280120396 {System.Int32} @2019-11-09T14:00:07.590 @@2019-11-09T14:05:46.268; Good
        End Sub
    End Class
End Namespace

#End Region
