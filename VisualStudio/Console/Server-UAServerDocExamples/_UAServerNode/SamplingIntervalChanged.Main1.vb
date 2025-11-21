' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable ConvertClosureToMethodGroup
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to adjust to sampling interval changes in the push data provision model.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.NodeSpace
Imports Timer = System.Timers.Timer

Namespace Global.UAServerDocExamples._UAServerNode
    Partial Friend Class SamplingIntervalChanged
        Shared Sub Main1()
            ' Instantiate the server object.
            ' By default, the server will run on endpoint URL "opc.tcp://localhost:48040/".
            Dim server = New EasyUAServer()

            ' Create a timer for pushing the data for OPC reads. In a real server the activity may also come from other
            ' sources.
            Dim timer = New Timer With {.AutoReset = True}

            ' Create a read-only data variable.
            Dim dataVariable = UADataVariable.CreateIn(server.Objects, "SubscribeToThisVariable") _
                .ValueType(Of Integer)() _
                .Writable(False)

            AddHandler dataVariable.SamplingIntervalChanged, Sub(sender, args)
                                                                 ' Obtain and display the new sampling interval.
                                                                 Dim samplingInterval As Integer = dataVariable.SamplingInterval
                                                                 Console.WriteLine($"Sampling interval changed to: {samplingInterval}")

                                                                 ' Adjust the timer interval accordingly. If the sampling interval is infinite, stop the timer. Otherwise,
                                                                 ' set the timer interval to half of the sampling interval, but at least 1 millisecond, and start the timer.
                                                                 If samplingInterval = Timeout.Infinite Then
                                                                     timer.Enabled = False
                                                                 Else
                                                                     timer.Interval = Math.Max(samplingInterval / 2, 1)
                                                                     timer.Enabled = True
                                                                 End If
                                                                 args.Handled = True
                                                             End Sub

            ' Set the read attribute data of the data variable to a random value whenever the timer interval elapses.
            Dim random = New Random()
            AddHandler timer.Elapsed, Sub(sender, args) dataVariable.UpdateReadAttributeData(random.Next())

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

