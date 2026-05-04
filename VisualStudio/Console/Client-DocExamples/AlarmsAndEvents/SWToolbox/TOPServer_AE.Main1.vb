' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how to work with Software Tolbox TOP Server 5 Alarms and Events.
' Use simdemo_WithA&E.opf configuration file and write a value above 1000 to Channel1.Device1.Tag1 or Channel1.Device1.Tag2.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.AddressSpace
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.AlarmsAndEvents.SWToolbox

    Friend Class TOPServer_AE
        Public Shared Sub Main1()
            Dim client = New EasyAEClient()

            ' Browse for some areas and sources

            Dim areaElements As AENodeElementCollection
            Try
                areaElements = client.BrowseAreas("", "SWToolbox.TOPServer_AE.V5", "")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each areaElement As AENodeElement In areaElements
                Debug.Assert(areaElement IsNot Nothing)
                Debug.Assert(areaElement.QualifiedName IsNot Nothing)

                Console.WriteLine("areaElements[""{0}""]:", areaElement.Name)
                Console.WriteLine("    .QualifiedName: {0}", areaElement.QualifiedName)

                Dim sourceElements As AENodeElementCollection = client.BrowseSources("", "SWToolbox.TOPServer_AE.V5", areaElement.QualifiedName)
                For Each sourceElement As AENodeElement In sourceElements
                    Debug.Assert(sourceElement IsNot Nothing)

                    Console.WriteLine("    sourceElements[""{0}""]:", sourceElement.Name)
                    Console.WriteLine("        .QualifiedName: {0}", sourceElement.QualifiedName)
                Next sourceElement
            Next areaElement

            ' Query for event categories

            Dim categoryElements As AECategoryElementCollection
            Try
                categoryElements = client.QueryEventCategories("", "SWToolbox.TOPServer_AE.V5")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            For Each categoryElement As AECategoryElement In categoryElements
                Debug.Assert(categoryElement IsNot Nothing)
                Console.WriteLine("CategoryElements[""{0}""].Description: {1}", categoryElement.CategoryId, categoryElement.Description)
            Next categoryElement

            ' Subscribe to events, wait, and unsubscribe

            Dim eventHandler = New EasyAENotificationEventHandler(AddressOf client_Notification)
            AddHandler client.Notification, eventHandler

            Dim handle As Integer = client.SubscribeEvents("", "SWToolbox.TOPServer_AE.V5", 1000)

            Console.WriteLine("Processing event notifications for 1 minute...")
            Thread.Sleep(60 * 1000)

            client.UnsubscribeEvents(handle)
        End Sub

        ' Notification event handler
        Private Shared Sub client_Notification(ByVal sender As Object, ByVal e As EasyAENotificationEventArgs)
            If e.Exception IsNot Nothing Then
                Console.WriteLine("e.Exception.Message: {0}", e.Exception.Message)
            End If
            If e.Exception IsNot Nothing Then
                Console.WriteLine("e.Exception.Source: {0}", e.Exception.Source)
            End If
            Console.WriteLine("e.Refresh: {0}", e.Refresh)
            Console.WriteLine("e.RefreshComplete: {0}", e.RefreshComplete)
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.QualifiedSourceName: {0}", e.EventData.QualifiedSourceName)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Time: {0}", e.EventData.Time)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Message: {0}", e.EventData.Message)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.EventType: {0}", e.EventData.EventType)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.CategoryId: {0}", e.EventData.CategoryId)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Severity: {0}", e.EventData.Severity)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.ConditionName: {0}", e.EventData.ConditionName)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.SubconditionName: {0}", e.EventData.SubconditionName)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Enabled: {0}", e.EventData.Enabled)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Active: {0}", e.EventData.Active)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Acknowledged: {0}", e.EventData.Acknowledged)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.Quality: {0}", e.EventData.Quality)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.AcknowledgeRequired: {0}", e.EventData.AcknowledgeRequired)
            End If
            If e.EventData IsNot Nothing Then
                Console.WriteLine("e.EventData.ActiveTime: {0}", e.EventData.ActiveTime)
            End If
        End Sub
    End Class

End Namespace
#End Region
