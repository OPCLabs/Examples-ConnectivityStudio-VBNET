' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to disable and enable the OPC UA Complex Data plug-in.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._PluginSetup

    Friend Class Enabled

        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Define which node we will work with.
            Dim nodeDescriptor As UANodeDescriptor = _
                "nsu=http://test.org/UA/Data/ ;i=10239"  ' [ObjectsFolder]/Data.Static.Scalar.StructureValue


            ' We will explicitly disable the Complex Data plug-in, and read a node which returns complex data. We will 
            ' receive an object of type UAExtensionObject, which contains the encoded data in its binary form. In this 
            ' form, the data cannot be easily further processed by your application.
            '
            ' Disabling the Complex Data plug-in may be useful e.g. for licensing reasons (when the product edition you 
            ' have does not support the Complex Data plug-in, and you want to avoid the associated error), or for
            ' performance reasons (if you do not need the internal content of the value, for example if your code just
            ' needs to take the value read, and write it elsewhere).

            Dim client1 = New EasyUAClient
            client1.InstanceParameters.PluginSetups.FindName("UAComplexDataClient").Enabled = False

            Dim value1 As Object
            Try
                value1 = client1.ReadValue(endpointDescriptor, nodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
            Console.WriteLine(value1)


            ' Now we will read the same value, but with the Complex Data plug-in enabled. This time we will receive an
            ' object of type UAGenericObject, which contains the data in the decoded form, accessible for further 
            ' processing by your application.
            '
            ' Note that it is not necessary to explicitly enable the Complex Data plug-in like this, because it is enabled
            ' by default.

            Dim client2 = New EasyUAClient
            client2.InstanceParameters.PluginSetups.FindName("UAComplexDataClient").Enabled = True

            Dim value2 As Object
            Try
                value2 = client2.ReadValue(endpointDescriptor, nodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
            Console.WriteLine(value2)


            ' Example output:
            '
            ' Binary Byte[1373]; {nsu=http://test.org/UA/Data/ ;i=11437}
            ' (ScalarValueDataType) structured

            ' On the first line, the type and length of the encoded data is shown, and the node ID is the encoding ID.
            ' On the 2nd line, the kind (structured) and the name of the complex data type (ScalarValueDataType) is shown.
        End Sub
    End Class
End Namespace

#End Region

