' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UACommonDocExamples.Application
    Public Class ApplicationExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _IEasyUAClientServerApplication.AssureOwnCertificatePack.Main1,
                AddressOf _IEasyUAClientServerApplication.FindGdsRegistrations.Main1,
                AddressOf _IEasyUAClientServerApplication.GetApplicationElement.Main1,
                AddressOf _IEasyUAClientServerApplication.GetCertificateSubjectName.Main1,
                AddressOf _IEasyUAClientServerApplication.ObtainNewCertificatePack.Main1,
                AddressOf _IEasyUAClientServerApplication.ObtainNewCertificatePack.Progress,
                AddressOf _IEasyUAClientServerApplication.RefreshTrustLists.Main1,
                AddressOf _IEasyUAClientServerApplication.RegisterToGds.Main1,
                AddressOf _IEasyUAClientServerApplication.UnregisterFromGds.Main1,
                AddressOf _IEasyUAClientServerApplication.UpdateGdsRegistration.Main1
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
