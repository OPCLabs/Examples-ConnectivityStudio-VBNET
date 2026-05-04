' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example shows how to browse objects under the "Objects" node and display event sources.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.AddressSpace.Standard
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UADocExamples.AlarmsAndConditions
    Friend Class BrowseEventSources
        Public Shared Sub Overload2()

            ' Start browsing from the "Objects" node
            Try
                BrowseFrom(UAObjectIds.ObjectsFolder)
            Catch uaException As UAException
                Console.WriteLine("*** Failure: {0}", uaException.GetBaseException.Message)
            End Try
        End Sub

        Private Shared Sub BrowseFrom(nodeDescriptor As UANodeDescriptor)

            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine("Parent node: {0}", nodeDescriptor)

            ' Instantiate the client object
            Dim client = New EasyUAClient()

            ' Obtain notifiers
            Dim eventSourceNodeElementCollection As UANodeElementCollection = client.BrowseEventSources( _
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                nodeDescriptor)

            ' Display notifiers
            Console.WriteLine()
            Console.WriteLine("Event sources:")
            For Each eventSourceNodeElement As UANodeElement In eventSourceNodeElementCollection
                Console.WriteLine(eventSourceNodeElement)
            Next eventSourceNodeElement

            ' Obtain objects
            Dim objectNodeElementCollection = client.BrowseObjects( _
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer", _
                nodeDescriptor)

            ' Recurse
            For Each objectNodeElement As UANodeElement In objectNodeElementCollection
                BrowseFrom(objectNodeElement)
            Next objectNodeElement
        End Sub
    End Class
End Namespace

#End Region
