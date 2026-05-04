' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
#Region "Example"
' This example shows how to obtain a dictionary of OPC property values for an OPC item, and extract property values.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.Extensions
Imports OpcLabs.EasyOpc.OperationModel

Namespace Global.DocExamples.DataAccess._EasyDAClientExtension
    Friend Class GetPropertyValueDictionary
        Public Shared Sub Main1()
            Dim client = New EasyDAClient()

            ' Get dictionary of property values, for all well-known properties
            Dim propertyValueDictionary As DAPropertyValueDictionary
            Try
                propertyValueDictionary = client.GetPropertyValueDictionary("", "OPCLabs.KitServer.2", "Simulation.Random")
            Catch opcException As OpcException
                Console.WriteLine("*** Failure: {0}", opcException.GetBaseException().Message)
                Exit Sub
            End Try

            ' Display some of the obtained property values
            ' The production code should also check for the .Exception first, before getting .Value
            Console.WriteLine("propertyValueDictionary[DAPropertyId.AccessRights].Value: {0}", propertyValueDictionary(DAPropertyIds.AccessRights).Value)
            Console.WriteLine("propertyValueDictionary[DAPropertyId.DataType].Value: {0}", propertyValueDictionary(DAPropertyIds.DataType).Value)
            Console.WriteLine("propertyValueDictionary[DAPropertyId.Timestamp].Value: {0}", propertyValueDictionary(DAPropertyIds.Timestamp).Value)
        End Sub
    End Class
End Namespace
#End Region
