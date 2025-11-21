' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
#Region "Example"
' Shows how to register a license located in an embedded managed resource, verifying its existence upfront.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Reflection
Imports OpcLabs.BaseLib.ComponentModel
Imports OpcLabs.BaseLib.Portable
Imports OpcLabs.EasyOpc.UA

Namespace Global.UADocExamples.Licensing
    Partial Friend Class _LicensingManagement
        Public Shared Sub RegisterManagedResourceWithExistenceCheck()

            ' Register a license that is we embed as a managed resource in this program.

            ' The first two arguments should always be "QuickOPC" and "Multipurpose".
            ' The third argument determines the assembly where the license resides.
            ' The fourth argument is the namespace-qualified name of the managed resource, or a pattern identifying it.
            ' We could use precise "UADocExamples.Licensing.Key-DemoOrTrial-WebForm-1999003494-20180611.bin" instead.
            Try
                LicensingManagement.Instance.RegisterManagedResourceWithExistenceCheck("QuickOPC", "Multipurpose",
                    Assembly.GetExecutingAssembly(), "*.Key-*.*")
            Catch customException As CustomException
                Console.WriteLine($"*** Failure: {customException.Message}")
                Exit Sub
            End Try

            ' Instantiate the client object, obtain the serial number from the license info, and display the serial number.
            Dim client = New EasyUAClient()
            Dim serialNumber As Long = CUInt(client.LicenseInfo("Multipurpose.SerialNumber"))
            Console.WriteLine("SerialNumber: {0}", serialNumber)

            ' The license we ship for this purpose is a trial license with low runtime limit, so it won't be of much use.
            ' But you get the point...

        End Sub
    End Class
End Namespace

#End Region
