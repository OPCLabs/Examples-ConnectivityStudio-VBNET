' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.
' ReSharper disable CheckNamespace
Imports OpcLabs.BaseLib.Console

Namespace Global.DocExamples.DataAccess.Xml

    Public Class XmlExamplesMenu
        Shared Sub Main1()
            Dim actionArray = New Action() _
                {
                    AddressOf BrowseBranches.Main1Xml,
                    AddressOf BrowseLeaves.Main1Xml,
                    AddressOf BrowseNodes.RecursiveXml,
                    AddressOf BrowseProperties.Main1Xml,
                    AddressOf ChangeItemSubscription.Main1Xml,
                    AddressOf GetMultiplePropertyValues.DataTypeXml,
                    AddressOf GetMultiplePropertyValues.Main1Xml,
                    AddressOf GetPropertyValue.Main1Xml,
                    AddressOf PullItemChanged.Main1Xml,
                    AddressOf ReadItem.DeviceSourceXml,
                    AddressOf ReadItem.Main1Xml,
                    AddressOf ReadItemValue.DeviceSourceXml,
                    AddressOf ReadItemValue.Main1Xml,
                    AddressOf ReadMultipleItems.DeviceSourceXml,
                    AddressOf ReadMultipleItems.Main1Xml,
                    AddressOf ReadMultipleItemValues.Main1Xml,
                    AddressOf SubscribeItem.CallbackLambdaXml,
                    AddressOf SubscribeItem.PercentDeadbandXml,
                    AddressOf SubscribeMultipleItems.PercentDeadbandXml,
                    AddressOf WriteItemValue.Main1Xml,
                    AddressOf WriteMultipleItemValues.Main1Xml
                }

            Dim actionList = New List(Of Action)(actionArray)
            Dim cont As Boolean
            Do
                Console.WriteLine()
                cont = ConsoleDialog.SelectAndPerformAction("Select action to perform", "Return", actionList)
                If cont Then
                    Console.WriteLine("Press Enter to continue...")
                    Console.ReadLine()
                End If
            Loop While cont
        End Sub
    End Class

End Namespace
