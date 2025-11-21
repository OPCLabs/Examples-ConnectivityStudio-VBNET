' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' Shows how to display all fields of the available license(s).
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports Opc.Ua
Imports OpcLabs.BaseLib.Collections.Specialized
Imports OpcLabs.EasyOpc.UA

Namespace Global.UADocExamples.Licensing
    Partial Friend Class LicenseInfo
        Public Shared Sub AllFields()

            ' Instantiate the client object.
            Dim client = New EasyUAClient()

            ' Obtain the license info.
            Dim licenseInfo As StringObjectDictionary = client.LicenseInfo

            ' Display all elements.
            For Each pair As KeyValuePair(Of String, Object) In licenseInfo
                Console.WriteLine($"{pair.Key}: {pair.Value}")
            Next pair
        End Sub
    End Class
End Namespace

#End Region
