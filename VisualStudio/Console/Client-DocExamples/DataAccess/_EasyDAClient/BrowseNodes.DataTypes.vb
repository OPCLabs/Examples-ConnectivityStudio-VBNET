' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain data types of leaves in the OPC-DA address 
' space by browsing and filtering, i.e. without the use of OPC properties. 
' This technique allows determining the data types with servers that only 
' support OPC-DA 1.0. It can also be more effective than the use of 
' GetMultiplePropertyValues, if there is large number of leaves, and 
' relatively small number of data types to be checked.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.AddressSpace
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClient
    Partial Friend Class BrowseNodes
        Shared Sub DataTypes()
            Dim client = New EasyDAClient()

            ' Define the list of data types we will be checking for. 
            ' Change as needed for your application.
            ' This technique is only usable if there is a known list of 
            ' data types you are interested in. If you are interested in 
            ' all leaves, even those that are of data types not explicitly 
            ' listed, always include VarTypes.Empty as the first data type. 
            ' The leaves of "unlisted" data types will have VarTypes.Empty 
            ' associated with them.
            Dim dataTypes() = New VarType() {VarTypes.Empty, VarTypes.I2, VarTypes.R4}

            ' For each leaf found, this dictionary wil hold its associated data type.
            Dim dataTypeDictionary = New Dictionary(Of DANodeElement, VarType)()

            ' For each data type, browse for leaves of this data type.
            For Each dataType As VarType In dataTypes
                Dim browseParameters = New DABrowseParameters(DABrowseFilter.Leaves, "", "", dataType)
                Dim nodeElements As DANodeElementCollection
                Try
                    nodeElements = client.BrowseNodes("", "OPCLabs.KitServer.2", "Greenhouse", browseParameters)
                Catch opcException As OpcException
                    Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                    Exit Sub
                End Try

                ' Store the leaf information into the dictionary, and 
                ' associate the current data type with it.
                For Each nodeElement In nodeElements
                    dataTypeDictionary(nodeElement) = dataType
                Next nodeElement
            Next dataType

            For Each pair In dataTypeDictionary
                Dim nodeElement As DANodeElement = pair.Key
                Dim dataType As VarType = pair.Value
                Console.WriteLine("{0}: {1}", nodeElement, dataType)
            Next pair
        End Sub
    End Class
End Namespace
#End Region
