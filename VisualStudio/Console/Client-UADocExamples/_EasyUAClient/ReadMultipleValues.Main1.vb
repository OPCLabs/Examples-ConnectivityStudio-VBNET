' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to read the Value attributes of 3 different nodes at once. Using the same method, it is also possible 
' to read multiple attributes of the same node.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Partial Friend Class ReadMultipleValues
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain values. By default, the Value attributes of the nodes will be read.
            Dim valueResultArray() As ValueResult = client.ReadMultipleValues(New UAReadArguments() _
               {
                   New UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10845"),
                   New UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10853"),
                   New UAReadArguments(endpointDescriptor, "nsu=http://test.org/UA/Data/ ;i=10855")
               }
            )

            ' Display results
            For Each valueResult As ValueResult In valueResultArray
                If valueResult.Succeeded Then
                    Console.WriteLine("Value: {0}", valueResult.Value)
                Else
                    Console.WriteLine("*** Failure: {0}", valueResult.ErrorMessageBrief)
                End If
            Next valueResult

            ' Example output:
            '
            'Value: 8
            'Value: -8.06803E+21
            'Value: Strawberry Pig Banana Snake Mango Purple Grape Monkey Purple? Blueberry Lemon^            
        End Sub
    End Class
End Namespace

#End Region
