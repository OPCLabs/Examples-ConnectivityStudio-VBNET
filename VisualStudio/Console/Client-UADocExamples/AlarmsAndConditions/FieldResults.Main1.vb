' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to display all fields of incoming events, or extract specific fields.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.AlarmsAndConditions
Imports OpcLabs.EasyOpc.UA.Filtering
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Friend Class FieldResults
        Public Shared Sub Main1()
            ' Instantiate the client object and hook events
            Dim client = New EasyUAClient()
            AddHandler client.EventNotification, AddressOf client_EventNotification

            Console.WriteLine("Subscribing...")
            client.SubscribeEvent( _
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                UAObjectIds.Server, _
                1000)

            Console.WriteLine("Processing event notifications for 30 seconds...")
            Threading.Thread.Sleep(30 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 5 seconds...")
            Threading.Thread.Sleep(5 * 1000)
        End Sub

        Private Shared Sub client_EventNotification(ByVal sender As Object, ByVal e As EasyUAEventNotificationEventArgs)
            Console.WriteLine()

            ' Display the event
            If e.EventData Is Nothing Then
                Console.WriteLine(e)
                Exit Sub
            End If
            Console.WriteLine("All fields:")
            For Each pair In e.EventData.FieldResults
                Dim attributeField = pair.Key
                Dim valueResult = pair.Value
                Console.WriteLine("  {0} -> {1}", attributeField, valueResult)
            Next pair
            ' Extracting a specific field using a standard operand symbol
            Console.WriteLine("Source name: {0}", _
                e.EventData.FieldResults(UABaseEventObject.Operands.SourceName))
            ' Extracting a specific field using an event type ID and a simple relative path
            Console.WriteLine("Message: {0}", _
                e.EventData.FieldResults(UAFilterElements.SimpleAttribute(UAObjectTypeIds.BaseEventType, "/Message")))
        End Sub
    End Class
End Namespace

#End Region
