' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to obtain all method nodes under a given node of the OPC-UA address space.
' For each node, it displays its browse name and node ID.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class BrowseMethods
        Public Shared Sub Overload2()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain methods under the specified node.
            Dim nodeElementCollection As UANodeElementCollection
            Try
                nodeElementCollection = client.BrowseMethods(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10755")
            Catch uaException As UAException
                Console.WriteLine($"*** Failure: {uaException.GetBaseException().Message}")
                Exit Sub
            End Try

            ' Display results
            For Each nodeElement As UANodeElement In nodeElementCollection
                Console.WriteLine($"{nodeElement.BrowseName}: {nodeElement.NodeId}")
            Next nodeElement
        End Sub

        ' Example output:
        'ScalarMethod1 nsu = http  'test.org/UA/Data/ ;ns=2;i=10756
        'ScalarMethod2: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10759
        'ScalarMethod3: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10762
        'ArrayMethod1: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10765
        'ArrayMethod2: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10768
        'ArrayMethod3: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10771
        'UserScalarMethod1: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10774
        'UserScalarMethod2: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10777
        'UserArrayMethod1: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10780
        'UserArrayMethod2: nsu = http : 'test.org/UA/Data/ ;ns=2;i=10783
    End Class
End Namespace

#End Region
