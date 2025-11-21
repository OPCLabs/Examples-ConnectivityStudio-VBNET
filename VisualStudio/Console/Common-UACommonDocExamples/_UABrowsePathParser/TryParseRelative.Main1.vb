' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' Attempts to parse a relative OPC-UA browse path and displays its elements.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.BaseLib
Imports OpcLabs.EasyOpc.UA.Navigation
Imports OpcLabs.EasyOpc.UA.Navigation.Parsing

Namespace Global.UACommonDocExamples._UABrowsePathParser
    Friend Class TryParseRelative
        Public Shared Sub Main1()
            Dim browsePathElements = New UABrowsePathElementCollection()

            Dim browsePathParser = New UABrowsePathParser()
            Dim stringParsingError As IStringParsingError = browsePathParser.TryParseRelative("/Data.Dynamic.Scalar.CycleComplete", browsePathElements)

            ' Display results
            If Not stringParsingError Is Nothing Then
                Console.WriteLine("*** Error: {0}", stringParsingError)
                Exit Sub
            End If

            For Each browsePathElement As UABrowsePathElement In browsePathElements
                Console.WriteLine(browsePathElement)
            Next browsePathElement

            ' Example output:
            ' /Data
            ' .Dynamic
            ' .Scalar
            ' .CycleComplete
        End Sub
    End Class
End Namespace

#End Region
