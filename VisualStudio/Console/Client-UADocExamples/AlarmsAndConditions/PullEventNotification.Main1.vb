' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to subscribe to event notifications, pull events, and display each incoming event.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Friend Class PullEventNotification
        Public Shared Sub Main1()
            ' Instantiate the client object
            Dim client = New EasyUAClient()
            ' In order to use event pull, you must set a non-zero queue capacity upfront.
            client.PullEventNotificationQueueCapacity = 1000

            Console.WriteLine("Subscribing...")
            client.SubscribeEvent(
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer",
                UAObjectIds.Server,
                1000)

            Console.WriteLine("Processing event notifications for 30 seconds...")
            Dim endTick As Integer = Environment.TickCount + 30 * 1000
            Do
                Dim eventArgs As EasyUAEventNotificationEventArgs = client.PullEventNotification(2 * 1000)
                If Not eventArgs Is Nothing Then
                    ' Handle the notification event
                    Console.WriteLine(eventArgs)
                End If
            Loop While Environment.TickCount < endTick
        End Sub
    End Class
End Namespace

#End Region
