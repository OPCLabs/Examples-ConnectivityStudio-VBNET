' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' Shows how to assure presence of the own application certificate pack, and display default application certificate
' thumbprint.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.Security.Cryptography.PkiCertificates
Imports OpcLabs.EasyOpc.UA.Application
Imports OpcLabs.EasyOpc.UA.Application.Extensions
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UACommonDocExamples.Application._IEasyUAClientServerApplication
    Friend Class AssureOwnCertificatePack
        Public Shared Sub Main1()
            ' Obtain the application interface.
            Dim application As EasyUAApplication = EasyUAApplication.Instance

            Try
                Console.WriteLine("Assuring presence of the own application certificate pack...")
                Dim created As Boolean = application.AssureOwnCertificatePack()

                Console.WriteLine(If(created,
                                  "A new certificate pack has been created.",
                                  "An existing certificate pack has been found."))

                Console.WriteLine()
                Console.WriteLine("Finding the current default application certificate...")
                Dim pkiCertificate As IPkiCertificate = application.FindOwnCertificate()

                Console.WriteLine()
                Console.WriteLine($"The thumbprint of the current default application certificate is: {pkiCertificate?.Thumbprint}")
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
                Exit Sub
            End Try
        End Sub
    End Class
End Namespace

#End Region
