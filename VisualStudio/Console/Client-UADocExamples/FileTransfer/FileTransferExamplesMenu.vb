' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UADocExamples.FileTransfer
    Public Class FileTransferExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _EasyUAFileTransferClient.BrowseFiles.Main1,
                AddressOf _EasyUAFileTransferClient.Copy.File1,
                AddressOf _EasyUAFileTransferClient.CreateDirectoryAndDelete.Main1,
                AddressOf _EasyUAFileTransferClient.CreateFileAndDelete.Main1,
                AddressOf _EasyUAFileTransferClient.GetFileProperties.Main1,
                AddressOf _EasyUAFileTransferClient.ReadAllBytes.Main1,
                AddressOf _EasyUAFileTransferClient.ReadAndSetFilePosition.Main1,
                AddressOf _EasyUAFileTransferClient.WriteAllBytes.Main1,
                AddressOf _EasyUAFileTransferClient.WriteFile.Main1,
                AddressOf _UAFileStream.ReadAndSeek.Main1,
                AddressOf _UAFileStream.ReadText.Main1
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