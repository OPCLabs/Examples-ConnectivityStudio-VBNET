' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UADocExamples.FileProviders
    Public Class FileProvidersExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf _DirectoryContents2.Enumerate.Main1,
                AddressOf _FileInfo2.Properties.Main1,
                AddressOf _FileInfo2.ReadAllBytes.Main1,
                AddressOf _FileInfo2.ReadAndSeek.Main1,
                AddressOf _FileInfo2.ReadText.Main1,
                AddressOf _WritableDirectoryContents.CreateAndDelete.Main1,
                AddressOf _WritableFileInfo.CopyTo.Main1,
                AddressOf _WritableFileInfo.CreateAndDelete.Main1,
                AddressOf _WritableFileInfo.Write.Main1,
                AddressOf _WritableFileInfo.WriteAllBytes.Main1
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