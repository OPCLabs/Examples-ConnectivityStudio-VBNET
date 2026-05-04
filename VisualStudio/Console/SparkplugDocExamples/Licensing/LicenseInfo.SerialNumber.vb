' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to obtain the serial number of the active license, and determine whether it is a stock demo or trial license.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' Sparkplug examples in C# on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-CSharp .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasySparkplug

Namespace Global.SparkplugDocExamples.Licensing
    Partial Class LicenseInfo
        Public Shared Sub SerialNumber()
            ' Instantiate the edge node object.
            ' NOTE: If you are using the consumer object (EasySparkplugConsumer), instantiate it instead.
            Dim edgeNode = New EasySparkplugEdgeNode()

            ' Obtain the serial number from the license info.
            Dim serialNumber As Long = CUInt(edgeNode.LicenseInfo("Multipurpose.SerialNumber"))

            ' Display the serial number.
            Console.WriteLine("SerialNumber: {0}", serialNumber)

            ' Determine whether we are running as demo or trial.
            If (1111110000 <= serialNumber) AndAlso (serialNumber <= 1111119999) Then
                Console.WriteLine("This is a stock demo or trial license.")
            Else
                Console.WriteLine("This is not a stock demo or trial license.")
            End If
        End Sub
    End Class
End Namespace
#End Region
