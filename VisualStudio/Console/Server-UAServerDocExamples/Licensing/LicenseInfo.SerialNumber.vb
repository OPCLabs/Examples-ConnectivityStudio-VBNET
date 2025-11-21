' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable LocalizableElement
#Region "Example"
' Shows how to obtain the serial number of the active license, and determine whether it is a stock demo or trial license.
' You can use any OPC UA client, including our Connectivity Explorer and OpcCmd utility, to connect to the server. 
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA

Namespace Global.UAServerDocExamples.Licensing
    Partial Friend Class LicenseInfo
        Shared Sub SerialNumber()
            ' Instantiate the server object.
            Dim server = New EasyUAServer()

            '  Obtain the serial number from the license info.
            Dim serialNumber As Long = CUInt(server.LicenseInfo("Multipurpose.SerialNumber"))

            ' Display the serial number.
            Console.WriteLine("SerialNumber: {0}", serialNumber)

            ' Determine whether we are running as demo or trial.
            If (1111110000 <= serialNumber) And (serialNumber <= 1111119999) Then
                Console.WriteLine("This is a stock demo or trial license.")
            Else
                Console.WriteLine("This is not a stock demo or trial license.")
            End If
        End Sub
    End Class
End Namespace
#End Region

