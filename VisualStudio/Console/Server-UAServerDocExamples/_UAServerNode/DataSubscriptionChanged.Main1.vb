' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to implement own handling of data subscriptions.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports Timer = System.Timers.Timer

Namespace Global.UAServerDocExamples._UAServerNode
    Partial Friend Class DataSubscriptionChanged
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a read-only data variable.
            Dim random = New Random()
            Dim dataVariable = UADataVariable.CreateIn(server.Objects, "SubscribeToThisVariable") _
                .ValueType(Of Integer)() _
                .Writable(False)

            dataVariable.UseDataPolling = False ' Recommended, but not strictly necessary.
            AddHandler dataVariable.DataSubscriptionChanged, Sub(sender, args)
                                                                 Select Case args.Action
                                                                     Case UADataSubscriptionChangedAction.Add
                                                                         ' Obtain the sampling interval from the data subscription.
                                                                         Dim samplingInterval As Integer = args.DataSubscription.SamplingInterval
                                                                         Console.WriteLine($"Data subscription added, sampling interval: {samplingInterval}")

                                                                         ' Create a timer that will provide the data variable with a new data. In a real server the activity
                                                                         ' may also come from other sources.
                                                                         Dim timer = New Timer With {.AutoReset = True, .Interval = samplingInterval}
                                                                         args.DataSubscription.State = timer

                                                                         ' Set the read attribute data of the data variable to a random value whenever the timer interval elapses.
                                                                         AddHandler timer.Elapsed, Sub(s, e) args.DataSubscription.OnNext(random.Next())

                                                                         ' Start the subscription timer.
                                                                         timer.Start()

                                                                     Case UADataSubscriptionChangedAction.Remove
                                                                         Console.WriteLine("Data subscription removed")

                                                                         ' Dispose of the subscription timer (stopping it too).
                                                                         Dim timer As Timer = CType(args.DataSubscription.State, Timer)
                                                                         timer.Dispose()

                                                                     Case UADataSubscriptionChangedAction.Modify
                                                                         Dim samplingInterval As Integer = args.DataSubscription.SamplingInterval
                                                                         Console.WriteLine($"Data subscription modified, sampling interval: {samplingInterval}")

                                                                         ' Change the interval of the subscription timer.
                                                                         Dim timer As Timer = CType(args.DataSubscription.State, Timer)
                                                                         timer.Interval = samplingInterval
                                                                 End Select
                                                             End Sub

            ' The read behavior of the data variable needs to be defined as well, separately from the data subscriptions.
            dataVariable.ReadValueFunction(Function() random.Next())

            ' Start the server.
            Console.WriteLine("The server is starting...")
            server.Start()

            Console.WriteLine("The server is started.")
            Console.WriteLine()

            ' Let the user decide when to stop.
            Console.WriteLine("Press Enter to stop the server...")
            Console.ReadLine()

            ' Stop the server.
            Console.WriteLine("The server is stopping...")
            server.Stop()

            Console.WriteLine("The server is stopped.")
        End Sub
    End Class
End Namespace
#End Region

