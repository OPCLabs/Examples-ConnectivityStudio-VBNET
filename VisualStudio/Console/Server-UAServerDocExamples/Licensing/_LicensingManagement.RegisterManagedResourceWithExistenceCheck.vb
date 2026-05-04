' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable InconsistentNaming
' ReSharper disable LocalizableElement
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' Shows how to register a license located in an embedded managed resource, verifying its existence upfront.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Reflection
Imports OpcLabs.BaseLib.Portable
Imports OpcLabs.EasyOpc.UA

Namespace Global.UAServerDocExamples.Licensing
    Partial Friend Class _LicensingManagement
        Shared Sub RegisterManagedResourceWithExistenceCheck()
            ' Register a license that is we embed as a managed resource in this program.

            ' The first two arguments should always be "OPCWizard" and "Multipurpose".
            ' The third argument determines the assembly where the license resides.
            ' The fourth argument is the namespace-qualified name of the managed resource, or a pattern identifying it.
            ' We could use precise "Key-DemoOrTrial-WebForm-1995003302-20240717.txt" instead.
            Try
                OpcLabs.BaseLib.ComponentModel.LicensingManagement.Instance.RegisterManagedResourceWithExistenceCheck("OPCWizard", "Multipurpose",
                    Assembly.GetExecutingAssembly(), "*.Key-*.*")
            Catch customException As CustomException
                ' This exception will be thrown if the specified managed resource does not exist or is not accessible.
                ' Note, however, that the validity of the license is not checked at this point.
                ' You can experiment with this code path by modifying the resource name in the call above.
                Console.WriteLine($"*** Failure: {customException.Message}")
                Return
            End Try

            ' Instantiate the server object, obtain the serial number from the license info, and display the serial number.
            Dim server = New EasyUAServer()
            Dim serialNumber As Long = CUInt(server.LicenseInfo("Multipurpose.SerialNumber"))
            Console.WriteLine("SerialNumber: {0}", serialNumber)

            ' The license we ship for this purpose is a trial license with low runtime limit, so it won't be of much use.
            ' But you get the point...
        End Sub
    End Class
End Namespace
#End Region

