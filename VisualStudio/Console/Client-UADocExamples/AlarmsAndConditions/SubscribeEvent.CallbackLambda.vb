' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to subscribe to event notifications and display each incoming event
' using a callback method that is provided as lambda expression.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard

Namespace Global.UADocExamples.AlarmsAndConditions
    Partial Friend Class SubscribeEvent
        Public Shared Sub CallbackLambda()
            ' Instantiate the client object
            Dim client = New EasyUAClient()

            Console.WriteLine("Subscribing...")
            ' The callback is a lambda expression the displays the event
            client.SubscribeEvent( _
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                UAObjectIds.Server, _
                1000, _
                Sub(sender, eventArgs) Console.WriteLine(eventArgs))
            ' Remark: Production code would check e.Exception before accessing e.EventData.

            Console.WriteLine("Processing event notifications for 10 seconds...")
            Threading.Thread.Sleep(10 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllMonitoredItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)
        End Sub
    End Class
End Namespace

#End Region
