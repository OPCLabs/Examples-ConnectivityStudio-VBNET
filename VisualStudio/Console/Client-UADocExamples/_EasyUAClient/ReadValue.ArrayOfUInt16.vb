' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to read a value from a single node that is an array of UInt16.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class ReadValue
        Public Shared Sub ArrayOfUInt16()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            Console.WriteLine("Obtaining value of a node...")
            Dim value As Int32()
            Try
                ' UInt16 is returned as Int32, because UInt16 is not a CLS-compliant type (and is not supported in VB.NET).
                ' // /Data.Dynamic.Array.UInt16Value
                value = CType(client.ReadValue(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;ns=2;i=10932"), Int32())
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            If Not IsNothing(value) Then
                Console.WriteLine(value(0))
                Console.WriteLine(value(1))
                Console.WriteLine(value(2))
            End If
        End Sub
    End Class
End Namespace

#End Region

