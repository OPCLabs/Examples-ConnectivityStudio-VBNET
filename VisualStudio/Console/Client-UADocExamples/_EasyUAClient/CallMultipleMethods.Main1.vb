' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to call multiple methods, and pass arguments to and
' from them.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples._EasyUAClient
    Friend Class CallMultipleMethods
        Public Shared Sub Main1()

            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"
            Dim nodeDescriptor As UANodeDescriptor = "nsu=http://test.org/UA/Data/ ;i=10755"

            Dim inputs1() As Object =
            { _
                False,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            }

            Dim typeCodes1() As TypeCode =
            { _
                TypeCode.Boolean,
                TypeCode.SByte,
                TypeCode.Byte,
                TypeCode.Int16,
                TypeCode.UInt16,
                TypeCode.Int32,
                TypeCode.UInt32,
                TypeCode.Int64,
                TypeCode.UInt64,
                TypeCode.Single,
                TypeCode.Double
            }

            Dim inputs2() As Object =
            { _
                False,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                "eleven"
            }

            Dim typeCodes2() As TypeCode =
            { _
                TypeCode.Boolean,
                TypeCode.SByte,
                TypeCode.Byte,
                TypeCode.Int16,
                TypeCode.UInt16,
                TypeCode.Int32,
                TypeCode.UInt32,
                TypeCode.Int64,
                TypeCode.UInt64,
                TypeCode.Single,
                TypeCode.Double,
                TypeCode.String
            }

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Perform the operation
            Dim results() As ValueArrayResult = client.CallMultipleMethods(New UACallArguments() _
    { _
        New UACallArguments(endpointDescriptor, nodeDescriptor, _
            "nsu=http://test.org/UA/Data/ ;i=10756", inputs1, typeCodes1), _
        New UACallArguments(endpointDescriptor, nodeDescriptor, _
            "nsu=http://test.org/UA/Data/ ;i=10774", inputs2, typeCodes2) _
    } _
            )

            ' Display results
            For i As Integer = 0 To results.Length - 1
                Console.WriteLine()
                Console.WriteLine("results[{0}]:", i)

                Dim result As ValueArrayResult = results(i)
                If result.Succeeded Then
                    Dim outputs As Object() = result.ValueArray
                    Debug.Assert(outputs IsNot Nothing)

                    For j As Integer = 0 To outputs.Length - 1
                        Console.WriteLine("    outputs[{0}]: {1}", j, outputs(j))
                    Next j
                Else
                    Console.WriteLine("*** Failure: {0}", result.ErrorMessageBrief)
                End If
            Next i

            ' Example output:
            'outputs[0]: False
            'outputs[1]: 1
            'outputs[2]: 2
            'outputs[3]: 3
            'outputs[4]: 4
            'outputs[5]: 5
            'outputs[6]: 6
            'outputs[7]: 7
            'outputs[8]: 8
            'outputs[9]: 9
            'outputs[10]: 10       

            'results[1]:
            'outputs[0]: False
            'outputs[1]: 1
            'outputs[2]: 2
            'outputs[3]: 3
            'outputs[4]: 4
            'outputs[5]: 5
            'outputs[6]: 6
            'outputs[7]: 7
            'outputs[8]: 8
            'outputs[9]: 9
            'outputs[10]: 10
            'outputs[11]: eleven
        End Sub
    End Class
End Namespace

#End Region
