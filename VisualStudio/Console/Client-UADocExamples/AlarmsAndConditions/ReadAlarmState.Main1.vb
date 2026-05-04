' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
' ReSharper disable InconsistentNaming
' ReSharper disable PossibleNullReferenceException
#Region "Example"
' This example shows how to read a Low state of a limit alarm. Note that you should not normally read a state of an alarm
' from inside its event notification, because the state might have already changed. Instead, include the information you
' need in the Select clauses when subscribing for the event.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.AddressSpace
Imports OpcLabs.EasyOpc.UA.Navigation

Namespace Global.UADocExamples.AlarmsAndConditions
    Friend Class ReadAlarmState
        Public Shared Sub Main1()
            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:62544/Quickstarts/AlarmConditionServer"

            Dim alarmNodeDescriptor As UANodeDescriptor = New UANodeId(
                namespaceUriString:="http://opcfoundation.org/Quickstarts/AlarmCondition",
                identifier:="1:Colours/EastTank?Yellow")

            ' Knowing the alarm node, and the fact that is an instance of NonExclusiveLevelAlarmType (or its subtype),
            ' determine what is its LowState/Id node.
            Dim lowStateIdNodeDescriptor As UANodeDescriptor = New UABrowsePath(alarmNodeDescriptor,
                New UABrowsePathElement() {
                    UABrowsePathElement.CreateSimple("ns=0;s=LowState"),
                    UABrowsePathElement.CreateSimple("ns=0;s=Id")
                })

            ' Instantiate the client object.
            Dim client = New EasyUAClient()

            Console.WriteLine("Reading alarm state...")
            Dim lowStateId = CBool(client.ReadValue(endpointDescriptor, lowStateIdNodeDescriptor))

            Console.WriteLine($"Id of LowState: {lowStateId}")
        End Sub
    End Class
End Namespace

#End Region
