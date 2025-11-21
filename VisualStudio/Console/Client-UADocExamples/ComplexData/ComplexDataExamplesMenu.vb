' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UADocExamples.ComplexData

    Friend Class ComplexDataExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                    {
                        AddressOf _DataType.Kind.Main1,
                        AddressOf _EasyUAClient.ReadValue.Main1,
                        AddressOf _EasyUAClient.SubscribeDataChange.Main1,
                        AddressOf _EasyUAClient.WriteValue.Main1,
                        AddressOf _GenericData._Construction.Main1,
                        AddressOf _GenericData.DataTypeKind1.Main1,
                        AddressOf _IEasyUAClientComplexData.ResolveDataType.Main1,
                        AddressOf _IUADataTypeDictionaryProvider.ResolveDataTypeDescriptorFromDataTypeEncodingId.Main1,
                        AddressOf _PluginSetup.Enabled.Main1,
                        AddressOf _UAComplexDataClientPluginParameters.IsolatedDataTypeModelProvider.Main1
                    }

            Dim actionList = New List(Of Action)(actionArray)


            Dim originalSharedParameters = CType(EasyUAClient.SharedParameters.Clone(), EasyUAClientSharedParameters)
            Do
                Console.WriteLine()
                If Not ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList) Then
                    Exit Do
                End If

                Console.WriteLine("Press Enter to continue...")
                Console.ReadLine()

                If EasyUAClient.SharedParameters <> originalSharedParameters Then
                    Using ConsoleUtilities.WithForegroundColor(ConsoleColor.Yellow)
                        Console.WriteLine(
                            "This example has changed some global parameters that can influence how other examples work. " +
                            "For this reason, the application will now exit. Start it again to continue.")
                        Console.WriteLine("Press Enter to continue...")
                        Console.ReadLine()
                        Environment.Exit(0)
                    End Using
                End If
            Loop While True
        End Sub
    End Class
End Namespace
