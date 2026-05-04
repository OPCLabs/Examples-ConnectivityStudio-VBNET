' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' Shows how to display all fields of the available license(s).
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Collections.Specialized
Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.Licensing
    Partial Class LicenseInfo
        Public Shared Sub AllFields()
            ' Instantiate the edge node object.
            ' NOTE: If you are using the consumer object (EasySparkplugConsumer), instantiate it instead.
            Dim edgeNode = New EasySparkplugEdgeNode()

            ' Obtain the license info.
            Dim licenseInfo As StringObjectDictionary = edgeNode.LicenseInfo

            ' Display all elements.
            For Each pair As KeyValuePair(Of String, Object) In licenseInfo
                Console.WriteLine($"{pair.Key}: {pair.Value}")
            Next
        End Sub
    End Class
End Namespace
#End Region
