' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to subscribe to complex data with OPC UA Complex Data plug-in.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.ComplexData._EasyUAClient

    Friend Class SubscribeDataChange

        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Define which node we will work with.
            Dim nodeDescriptor As UANodeDescriptor = _
                "nsu=http://test.org/UA/Data/ ;i=10867"  ' [ObjectsFolder]/Data.Dynamic.Scalar.StructureValue

            ' Instantiate the client object and hook the event handler.
            Dim client = New EasyUAClient
            AddHandler client.DataChangeNotification, AddressOf client_DataChangeNotification

            ' Subscribe to a node which returns complex data. This is done in the same way as regular subscribes - just 
            ' the data returned is different.
            Console.WriteLine("Subscribing...")
            client.SubscribeDataChange(endpointDescriptor, nodeDescriptor, samplingInterval:=1000)

            Console.WriteLine("Processing data change events for 20 seconds...")
            Threading.Thread.Sleep((20 * 1000))

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep((5 * 1000))

            ' Unhook the event handler.
            RemoveHandler client.DataChangeNotification, AddressOf client_DataChangeNotification


            ' Example output:
            '
            'Subscribing...
            'Processing data change events for 20 seconds...
            '(ScalarValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ScalarValueDataType) structured
            '(ScalarValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ScalarValueDataType) structured
            '(ArrayValueDataType) structured
            '(ArrayValueDataType) structured
            '(ScalarValueDataType) structured
            '(ScalarValueDataType) structured
            '(ScalarValueDataType) structured
            'Unsubscribing...
            'Waiting for 5 seconds...
        End Sub

        Private Shared Sub client_DataChangeNotification(sender As Object, e As EasyUADataChangeNotificationEventArgs)
            ' Display value
            If e.Succeeded Then
                Console.WriteLine("Value: {0}", e.AttributeData.Value)
            Else
                Console.WriteLine("*** Failure: {0}", e.ErrorMessageBrief)
            End If

            ' For processing the internals of the data, refer to examples for GenericData and DataType classes.
        End Sub
    End Class
End Namespace

#End Region
