' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable AssignNullToNotNullAttribute
' ReSharper disable PossibleNullReferenceException
' ReSharper disable StringLiteralTypo
#Region "Example"
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client, server and subscriber examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugEdgeNodeDemoLibrary
    Public Module DemoMetrics
        ''' <summary>
        ''' Adds metrics that demonstrate various features of EasySparkplug.
        ''' </summary>
        Public Function Create() As SparkplugMetric()
            Return { _
                     _ ' Demonstrate that in the simplest case, metrics can be added directly to the root level.
            New SparkplugMetric("Ramp").ReadValueFunction(Function() (Environment.TickCount Mod 1000) / 1000.0),
            New SparkplugMetric("Simple").ReadWriteValue(0),
            New SparkplugMetric("Simple2").ReadWriteValue(0) _
            ,
             _
             _ ' Demonstrate the fact that metrics can be organized in folders, and that metrics can be nested.
            New SparkplugMetric("Random").ReadValueFunction(Function() Random.NextDouble()),
            New SparkplugMetric("Nested/Random").ReadValueFunction(Function() Random.NextDouble()),
            New SparkplugMetric("Nested/FurtherNested/Random").ReadValueFunction(Function() Random.NextDouble()) _
            ,
             _
             _ ' Demonstrate that the metric may decide to fail the write operation.
             _ ' Note that the consumer can only determine that the write operation failed by subscribing to the metric and
             _ ' observing whether the value has changed after the write operation
            New SparkplugMetric("WriteFailure").WriteFunction(Of Integer)(Function(__) False)
            }
        End Function

        Private ReadOnly Random As Random = New Random()

    End Module
End Namespace
#End Region
