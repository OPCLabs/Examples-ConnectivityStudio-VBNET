' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to publish a payload with multiple command metrics for a given edge node.
'
' In order to publish or observe messages for this example, start the SparkplugEdgeNodeConsoleDemo program first.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug
Imports OpcLabs.EasySparkplug.OperationModel

Namespace Global.SparkplugDocExamples.Consumer._EasySparkplugConsumer
    Class PublishEdgeNodePayload
        Public Shared Sub Overload1()
            ' Note that the default port for the "mqtt" scheme is 1883.
            Dim hostDescriptor = New SparkplugHostDescriptor("mqtt://localhost")

            ' Instantiate the consumer object.
            Dim consumer = New EasySparkplugConsumer()

            ' Create the payload with multiple command metrics.
            Dim payload = New SparkplugPayload From
            {
                {"Simple", New SparkplugMetricData(42)},
                {"Simple2", New SparkplugMetricData(43)}
            }

            Console.WriteLine("Publishing...")
            Try
                consumer.PublishEdgeNodePayload(hostDescriptor, "easyGroup", "easySparkplugDemo", payload)
            Catch sparkplugException As SparkplugException
                Console.WriteLine($"*** Failure: {sparkplugException.GetBaseException().Message}")
                Return
            End Try

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
