' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to read a range of values from an array.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples._UAIndexRangeList
    Friend Class Usage
        Public Shared Sub ReadValue()

            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain the value, indicating that just the elements 2 to 4 should be returned
            Dim value As Object
            Try
                value = client.ReadValue(
                    New UAReadArguments(
                        endpointDescriptor,
                        "nsu=http://test.org/UA/Data/ ;i=10305",
                        UAIndexRangeList.OneDimension(2, 4)))
                ' or "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            ' Cast to typed array
            Dim arrayValue = DirectCast(value, Int32())

            ' Display results
            For i = 0 To 2
                Console.WriteLine("arrayValue[{0}]: {1}", i, arrayValue(i))
            Next
        End Sub
    End Class
End Namespace

#End Region
