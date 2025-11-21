' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how the OPC UA status codes are formatted to a string containing their symbolic name.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA

Namespace Global.UACommonDocExamples._UAStatusCode
    Partial Friend Class ToString
        Public Shared Sub Main1()
            Dim internalValueArray() As Long = {0, &H80010000&, 2147614720, &H80340000&}

            For Each internalValue As Long In internalValueArray
                Console.WriteLine($"{internalValue}: {New UAStatusCode(internalValue)}")
            Next internalValue


            ' Example output:
            '0: Good
            '2147549184 BadUnexpectedError
            '2147614720: BadInternalError
            '2150891520: BadNodeIdUnknown
        End Sub
    End Class
End Namespace

#End Region
