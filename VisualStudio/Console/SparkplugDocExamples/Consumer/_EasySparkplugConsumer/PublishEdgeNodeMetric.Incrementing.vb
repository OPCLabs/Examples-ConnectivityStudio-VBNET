' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to send an ever-incrementing value to a Sparkplug metric.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugConsumer
    Partial Class PublishEdgeNodeMetric
        Public Shared Sub Incrementing()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            ' 
            Console.WriteLine("Publishing... (press any key to stop)")
            Dim i = 0

            Do
                Console.WriteLine($"@{DateTime.Now}: Publishing {i}")
                Try
                    consumer.PublishEdgeNodeMetric(hostDescriptor,
                        "easyGroup", "easySparkplugDemo", "Simple",
                        New SparkplugMetricData(i)) ' the command metric value
                Catch sparkplugException As SparkplugException
                    Console.WriteLine($"*** Failure: {sparkplugException.GetBaseException().Message}")
                    Return
                End Try
                i = CInt((CLng(i) + 1) And &H7FFFFFFF)
                Thread.Sleep(2 * 1000)
            Loop Until Console.KeyAvailable

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
