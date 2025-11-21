' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

Imports OpcLabs.BaseLib.Console
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Engine

Namespace Global.UADocExamples.AlarmsAndConditions
    Public Class AlarmsAndConditionsExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
            {
                AddressOf Acknowledge.AckedState,
                AddressOf Acknowledge.Main1,
                AddressOf BaseEvent.Main1,
                AddressOf BrowseEventSources.Overload2,
                AddressOf BrowseNotifiers.Overload2,
                AddressOf FieldResults.Main1,
                AddressOf PullEventNotification.Main1,
                AddressOf ReadAlarmState.Main1,
                AddressOf SelectClauses.Main1,
                AddressOf SubscribeEvent.CallbackLambda,
                AddressOf SubscribeEvent.Overload1,
                AddressOf SubscribeMultipleMonitoredItems.Events,
                AddressOf WhereClause.Main1
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