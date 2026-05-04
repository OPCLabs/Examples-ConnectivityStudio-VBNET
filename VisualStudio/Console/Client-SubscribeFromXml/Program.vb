
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports System.Xml
Imports System.Xml.Serialization
Imports OpcLabs.BaseLib.Runtime.InteropServices
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.SimpleLogToSql

    Friend Class Program
        Shared WithEvents _client As New EasyDAClient

        <MTAThread> ' needed for COM security initialization to succeed
        Shared Sub Main()
            ComManagement.Instance.AssureSecurityInitialization()

            Console.WriteLine("Loading items from XML file...")
            Dim xmlSerializer = New XmlSerializer(GetType(DAItemGroupArguments()))
            Dim xmlReader = Xml.XmlReader.Create("OpcItems.xml", New XmlReaderSettings With {.IgnoreWhitespace = True})
            Dim argArray = CType(xmlSerializer.Deserialize(xmlReader), DAItemGroupArguments())

            If argArray IsNot Nothing Then
                Console.WriteLine("Subscribing for 30 seconds...")
                _client.SubscribeMultipleItems(argArray)
                Thread.Sleep(30 * 1000)

                Console.WriteLine("Unsubscribing...")
                _client.UnsubscribeAllItems()
            End If

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub ItemChanged(ByVal sender As Object, ByVal eventArgs As EasyDAItemChangedEventArgs) Handles _client.ItemChanged
            Console.WriteLine("{0}: {1}", eventArgs.Arguments.ItemDescriptor.ItemId, eventArgs.Vtq)
        End Sub
    End Class
End Namespace