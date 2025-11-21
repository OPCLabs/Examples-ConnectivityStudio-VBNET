' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UAServerDocExamples
    Public Class UACommonExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                    {
                        AddressOf _EasyUAServer._Construction.Main1,
                        AddressOf _EasyUAServer._Parameterization.OpcCompliance,
                        AddressOf _EasyUAServer.ConversionError.Main1,
                        AddressOf _EasyUAServer.Dispose.Main1,
                        AddressOf _EasyUAServer.EffectiveEndpointDescriptor.Main1,
                        AddressOf _EasyUAServer.EndpointStateChanged.Main1,
                        AddressOf _EasyUAServer.EndpointUrl.Main1,
                        AddressOf _EasyUAServer.EndpointUrlString.Main1,
                        AddressOf _EasyUAServer.FindServerNode.Main1,
                        AddressOf _EasyUAServer.HostNames.Main1,
                        AddressOf _EasyUAServer.LogEntry.Main1,
                        AddressOf _EasyUAServer.MessageSecurityModes.Secure,
                        AddressOf _EasyUAServer.MessageSecurityModes.SecurityNone,
                        AddressOf _EasyUAServer.Objects.Main1,
                        AddressOf _EasyUAServer.ObjectsNamespaceUri.Main1,
                        AddressOf _EasyUAServer.ObjectsNamespaceUriString.Main1,
                        AddressOf _EasyUAServer.Start_Stop.Main1,
                                                                 _
                        AddressOf _EasyUAServerConnectionMonitoring.ClientSessions.Main1,
                                                                                         _
                        AddressOf _UADataVariable._Building.Nested,
                        AddressOf _UADataVariable.ConstantValue.Main1,
                        AddressOf _UADataVariable.ReadAttributeData.Main1,
                        AddressOf _UADataVariable.ReadFunction.Main1,
                        AddressOf _UADataVariable.ReadFunction.Unreliable,
                        AddressOf _UADataVariable.ReadValueFunction.Array,
                        AddressOf _UADataVariable.ReadValueFunction.ByteString,
                        AddressOf _UADataVariable.ReadValueFunction.Main1,
                        AddressOf _UADataVariable.ReadValueFunction.UInt16,
                        AddressOf _UADataVariable.ReadWrite.Bad,
                        AddressOf _UADataVariable.ReadWrite.Main1,
                        AddressOf _UADataVariable.ReadWriteValue.Array,
                        AddressOf _UADataVariable.ReadWriteValue.Array2D,
                        AddressOf _UADataVariable.ReadWriteValue.Array3D,
                        AddressOf _UADataVariable.ReadWriteValue.BoundedArray,
                        AddressOf _UADataVariable.ReadWriteValue.ByteString,
                        AddressOf _UADataVariable.ReadWriteValue.FullyWritable,
                        AddressOf _UADataVariable.ReadWriteValue.Main1,
                        AddressOf _UADataVariable.SetMinimumSamplingInterval.Main1,
                        AddressOf _UADataVariable.UpdateReadAttributeData.Main1,
                        AddressOf _UADataVariable.WriteAttributeData.Main1,
                        AddressOf _UADataVariable.WriteFunction.GoodCompletesAsynchronously,
                        AddressOf _UADataVariable.WriteFunction.Main1,
                        AddressOf _UADataVariable.WriteValueAction.ByteString,
                        AddressOf _UADataVariable.WriteValueAction.Main1,
                        AddressOf _UADataVariable.WriteValueAction.UInt16,
                        AddressOf _UADataVariable.WriteValueAction.WriteOnly1,
                        AddressOf _UADataVariable.WriteValueFunction.Main1,
                                                                           _
                        AddressOf _UAFolder._Building.Nested,
                                                             _
                        AddressOf _UAServerNode.DataSubscriptionChanged.Main1,
                        AddressOf _UAServerNode.EffectiveNodeDescriptor.Main1,
                        AddressOf _UAServerNode.Indexer.Main1,
                        AddressOf _UAServerNode.OnRead.Main1,
                        AddressOf _UAServerNode.OnWrite.Main1,
                        AddressOf _UAServerNode.ParentNode.Main1,
                        AddressOf _UAServerNode.Read.Main1,
                        AddressOf _UAServerNode.SamplingIntervalChanged.Main1,
                        AddressOf _UAServerNode.Starting_Stopped.Main1,
                        AddressOf _UAServerNode.Write.Main1
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
' ReSharper restore CheckNamespace
