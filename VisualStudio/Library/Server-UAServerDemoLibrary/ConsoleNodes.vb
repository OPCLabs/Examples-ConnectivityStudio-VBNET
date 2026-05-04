' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable ArrangeModifiersOrder
' ReSharper disable PossibleNullReferenceException
#Region "Example"
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System
Imports OpcLabs.EasyOpc.UA.NodeSpace

Namespace Global.UAServerDemoLibrary
    Public Module ConsoleNodes
        ''' <summary>
        ''' Adds nodes that allow interaction with the console.
        ''' </summary>
        ''' <param name="parentFolder">The folder to which to add the nodes.</param>
        ''' <remarks>
        ''' <para>
        ''' The Console nodes allow OPC UA clients to display data on the OPC UA server's console (if it has such device).
        ''' They are included mainly for the fun of it, to demonstrate the fact that any actions can be tied to the OPC write
        ''' operations. Real OPC servers will Not do this.</para>
        ''' </remarks>
        Sub AddToParent(parentFolder As UAFolder)
            ' Create the Console folder.
            Dim consoleFolder = New UAFolder("Console") From
            {
                New UADataVariable("Write") _ ' The Write data variable writes the value to the console.
                    .Readable(False) _
                    .WriteValueAction(Sub(s As String) Console.Write(s)),
                New UADataVariable("WriteLine") _ ' The WriteLine data variable writes the value to the console and appends a new line.
                    .Readable(False) _
                    .WriteValueAction(Sub(s As String) Console.WriteLine(s))
            }
            parentFolder.Add(consoleFolder)
        End Sub
    End Module
End Namespace
#End Region

