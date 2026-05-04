' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' Shows how to display all fields of the available license(s).
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Collections.Specialized
Imports OpcLabs.EasyOpc.UA

Namespace Global.UAServerDocExamples.Licensing
    Partial Friend Class LicenseInfo
        Shared Sub AllFields()
            ' Instantiate the server object.
            Dim server = New EasyUAServer()

            '  Obtain the license info.
            Dim licenseInfo As StringObjectDictionary = server.LicenseInfo

            ' Display all elements.
            For Each pair As KeyValuePair(Of String, Object) In licenseInfo
                Console.WriteLine($"{pair.Key}: {pair.Value}")
            Next pair
        End Sub
    End Class
End Namespace
#End Region

