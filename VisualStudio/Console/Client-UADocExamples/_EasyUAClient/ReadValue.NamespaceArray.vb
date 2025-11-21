' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to read value of server's NamespaceArray, and display the namespace URIs in it.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class ReadValue
        Public Shared Sub NamespaceArray()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Perform the operation: Obtain value of a node
            Dim client = New EasyUAClient()

            ' Obtain value of a node
            Dim value As Object
            Try
                value = client.ReadValue(endpointDescriptor, UAVariableIds.Server_NamespaceArray) ' i = 2255
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try

            If TypeOf value IsNot String() Then
                Console.WriteLine("*** Not a string array")
                Exit Sub
            End If

            Dim arrayValue() As String = CType(value, String())

            ' Display results
            For i = 0 To arrayValue.Length - 1
                Console.WriteLine($"{i}: {arrayValue(i)}")
            Next i
        End Sub

        ' Example output
        '
        '0: http://opcfoundation.org/UA/
        '1: urn:DEMO-5:UA Sample Server
        '2: http://test.org/UA/Data/
        '3: http://test.org/UA/Data'Instance
        '4: http://opcfoundation.org/UA/Boiler/
        '5: http://opcfoundation.org/UA/Boiler'Instance
        '6: http://opcfoundation.org/UA/Diagnostics
        '7: http://samples.org/UA/memorybuffer
        '8: http://samples.org/UA/memorybuffer/Instance
    End Class
End Namespace

#End Region
