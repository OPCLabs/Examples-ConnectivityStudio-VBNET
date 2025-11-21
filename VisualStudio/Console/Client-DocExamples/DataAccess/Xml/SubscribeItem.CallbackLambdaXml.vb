' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming
#Region "Example"
' This example shows how subscribe to changes of a single item in an OPC XML-DA server and display the value of the item 
' with each change, using a callback method specified using lambda expression.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess

Namespace Global.DocExamples.DataAccess.Xml
    Partial Friend Class SubscribeItem
        Shared Sub CallbackLambdaXml()
            ' Instantiate the client object
            Dim client = New EasyDAClient()

            Console.WriteLine("Subscribing...")
            ' The callback is a lambda expression the displays the value
            client.SubscribeItem(
                    "http://opcxml.demo-this.com/XmlDaSampleServer/Service.asmx",
                    "Dynamic/Analog Types/Int",
                    1000,
                    Sub(sender, eventArgs)
                        Debug.Assert(eventArgs IsNot Nothing)
                        If eventArgs.Succeeded Then
                            Debug.Assert(eventArgs.Vtq IsNot Nothing)
                            Console.WriteLine(eventArgs.Vtq.ToString())
                        Else
                            Console.WriteLine("*** Failure: {0}", eventArgs.ErrorMessageBrief)
                        End If
                    End Sub,
                    state:=Nothing)

            Console.WriteLine("Processing item changed events for 30 seconds...")
            Threading.Thread.Sleep(30 * 1000)

            Console.WriteLine("Unsubscribing...")
            client.UnsubscribeAllItems()

            Console.WriteLine("Waiting for 2 seconds...")
            Threading.Thread.Sleep(2 * 1000)

            Console.WriteLine("Finished.")
        End Sub
    End Class
End Namespace
#End Region
