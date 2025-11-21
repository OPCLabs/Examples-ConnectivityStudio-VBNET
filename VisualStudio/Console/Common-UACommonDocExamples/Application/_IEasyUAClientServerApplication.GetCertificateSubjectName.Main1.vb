' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to get the subject name of application certificates.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA.Application
Imports OpcLabs.EasyOpc.UA.Application.Extensions

Namespace Global.UACommonDocExamples.Application._IEasyUAClientServerApplication
    Friend Class GetCertificateSubjectName
        Public Shared Sub Main1()
            ' Obtain the application interface.
            Dim application As EasyUAApplication = EasyUAApplication.Instance

            ' Get the subject name of application certificates.
            Dim certificateSubjectName = application.GetCertificateSubjectName()

            ' Display results
            Console.WriteLine("Certificate subject name: {0}", certificateSubjectName)
        End Sub
    End Class
End Namespace

#End Region
