' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

#Region "Example"
' Logs OPC Alarms and Events notifications into an XML file.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Xml
Imports System.Xml.Serialization
Imports OpcLabs.BaseLib.Runtime.InteropServices
Imports OpcLabs.EasyOpc.AlarmsAndEvents
Imports OpcLabs.EasyOpc.AlarmsAndEvents.OperationModel

Namespace Global.XmlEventLogger
    Friend Class Program
        Shared WithEvents _client As New EasyAEClient
        Shared ReadOnly XmlSerializer As New XmlSerializer(GetType(EasyAENotificationEventArgs))
        Shared _xmlWriter As XmlWriter

        <MTAThread> ' needed for COM security initialization to succeed
        Shared Sub Main()
            ComManagement.Instance.AssureSecurityInitialization()

            Console.WriteLine("Starting up...")
            _xmlWriter = XmlWriter.Create("OpcEvents.xml", New XmlWriterSettings With {.Indent = True, .CloseOutput = True})
            ' The root element can have any name you need, but the name below also allows reading the log back as .NET array
            _xmlWriter.WriteStartElement("ArrayOfEasyAENotificationEventArgs")

            Console.WriteLine("Logging events for 30 seconds...")
            Dim handle As Integer = _client.SubscribeEvents("", "OPCLabs.KitEventServer.2", 100)
            Threading.Thread.Sleep(30 * 1000)

            Console.WriteLine()
            Console.WriteLine("Shutting down...")
            _client.UnsubscribeEvents(handle)
            _xmlWriter.WriteEndElement() ' not really necessary - XmlWriter would write the end tag for us anyway
            _xmlWriter.Close()

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub Notification(ByVal sender As Object, ByVal eventArgs As Object) Handles _client.Notification
            Debug.Assert(eventArgs IsNot Nothing)
            Console.Write(".")
            XmlSerializer.Serialize(_xmlWriter, eventArgs)
        End Sub
    End Class
End Namespace
#End Region
