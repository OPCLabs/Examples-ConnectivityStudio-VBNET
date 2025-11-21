' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to write into multiple OPC XML-DA items using a single method call, and read multiple item values back.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.OperationModel
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class WriteMultipleItemValues
        Public Shared Sub Main1Xml()
            ' Instantiate the client object.
            Dim client = New EasyDAClient()

            Console.WriteLine("Writing multiple item values...")
            Dim resultArray As OperationResult() = client.WriteMultipleItemValues(New DAItemValueArguments() {
                New DAItemValueArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Static/Analog Types/Int", 12345),
                New DAItemValueArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Static/Simple Types/Boolean", True),
                New DAItemValueArguments("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx", "Static/Analog Types/Double", 234.56)
            })

            For i = 0 To resultArray.Length - 1
                Debug.Assert(resultArray(i) IsNot Nothing)

                If resultArray(i).Succeeded Then
                    Console.WriteLine("Results[{0}]: success", i)
                Else
                    Console.WriteLine("Results[{0}] *** Failure: {1}", i, resultArray(i).ErrorMessageBrief)
                End If
            Next i

            Console.WriteLine()
            Console.WriteLine("Reading multiple item values...")
            Dim valueResultArray() As ValueResult = client.ReadMultipleItemValues("http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                New DAItemDescriptor() {
                    "Static/Analog Types/Int",
                        "Static/Simple Types/Boolean",
                        "Static/Analog Types/Double"})

            For i = 0 To valueResultArray.Length - 1
                Debug.Assert(valueResultArray(i) IsNot Nothing)
                Console.WriteLine("valueResultArray[{0}]: {1}", i, valueResultArray(i))
            Next i

            ' Example output:
            '
            'Writing multiple item values...
            'Results[0]: success
            'Results[1]: success
            'Results[2]: success
            '
            'Reading multiple item values...
            'valueResultArray[0]: Success; 12345 {System.Int32}
            'valueResultArray[1]: Success; True {System.Boolean}
            'valueResultArray[2]: Success; 234.56 {System.Single}
        End Sub
    End Class
End Namespace
#End Region
