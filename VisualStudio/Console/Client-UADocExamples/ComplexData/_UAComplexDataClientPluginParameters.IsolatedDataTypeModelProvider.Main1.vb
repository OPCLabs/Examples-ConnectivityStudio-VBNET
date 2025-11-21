' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to configure the OPC UA Complex Data plug-in to use a shared data type model provider.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Plugins.ComplexData

Namespace Global.UADocExamples.ComplexData._UAComplexDataClientPluginParameters

    Friend Class IsolatedDataTypeModelProvider

        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Define which node we will work with.
            Dim nodeDescriptor As UANodeDescriptor =
                "nsu=http://test.org/UA/Data/ ;i=10239"  ' [ObjectsFolder]/Data.Static.Scalar.StructureValue


            ' We will create two instances of EasyUAClient class, and configure each of them to use the shared data type
            ' model provider.

            ' Configure the first client object.
            Dim client1 = New EasyUAClient
            Dim complexDataClientPluginParameters1 As UAComplexDataClientPluginParameters =
                client1.InstanceParameters.PluginConfigurations.Find(Of UAComplexDataClientPluginParameters)()
            Debug.Assert(complexDataClientPluginParameters1 IsNot Nothing)
            complexDataClientPluginParameters1.IsolatedDataTypeModelProvider = False

            ' Configure the second client object.
            Dim client2 = New EasyUAClient
            Dim complexDataClientPluginParameters2 As UAComplexDataClientPluginParameters =
                client2.InstanceParameters.PluginConfigurations.Find(Of UAComplexDataClientPluginParameters)()
            Debug.Assert(complexDataClientPluginParameters2 IsNot Nothing)
            complexDataClientPluginParameters2.IsolatedDataTypeModelProvider = False


            ' We will now read the same complex data node using the two client objects.
            '
            ' There is no noticeable difference in the results from the default state in which the client objects are
            ' set to use per-instance data type model provider. But, with the shared data type model provider, the metadata
            ' obtained during the read on the first client object and cached inside the data type model provider are reused
            ' during the read on the second client object, making this and the subsequent operations more efficient.

            ' Read the complex data node using the first client.
            Dim value1 As Object
            Try
                value1 = client1.ReadValue(endpointDescriptor, nodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
            Console.WriteLine(value1)

            ' Read the complex data node using the second client.
            Dim value2 As Object
            Try
                value2 = client2.ReadValue(endpointDescriptor, nodeDescriptor)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
            Console.WriteLine(value2)
        End Sub
    End Class
End Namespace

#End Region
