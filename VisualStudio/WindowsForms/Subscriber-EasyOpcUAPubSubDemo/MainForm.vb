' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Reflection
Imports OpcLabs.EasyOpc.UA.PubSub
Imports OpcLabs.EasyOpc.UA.PubSub.Engine
Imports OpcLabs.EasyOpc.UA.PubSub.Extensions
Imports OpcLabs.EasyOpc.UA.PubSub.OperationModel

<Assembly: CLSCompliant(True)>

' ReSharper disable InconsistentNaming
' ReSharper disable StringLiteralTypo

Partial Public Class MainForm
    Inherits Form
    Public Sub New()
        InitializeComponent()
    End Sub


    Private _subscribed As Boolean


    ''' <summary>
    ''' The user has pressed the "About" button. Show a message box with information about the executing assembly.
    ''' </summary>
    Private Sub aboutButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutButton.Click
        MessageBox.Show(Me, Assembly.GetExecutingAssembly().FullName, "AssemblyName", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Event handler for the <see cref="EasyUASubscriberCore.DataSetMessage"/> event. It is invoked for every received
    ''' dataset. We display the dataset data received (or the error) on the form.
    ''' </summary>
    Private Sub easyUASubscriber1_DataSetMessage(ByVal sender As Object, ByVal e As EasyUADataSetMessageEventArgs) Handles easyUASubscriber1.DataSetMessage
        Debug.Assert(e IsNot Nothing)

        errorTextBox.BackColor = If(e.Succeeded, SystemColors.Control, Color.Orange)
        errorTextBox.Text = If(e.Succeeded, "", e.Exception?.GetBaseException().Message)

        uaDataSetDataControl1.Value = e.DataSetData
    End Sub

    ''' <summary>
    ''' Unsubscribe from the dataset whenever the form is closing.
    ''' </summary>
    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Subscribed = False
    End Sub

    ''' <summary>
    ''' When the form loads, select the ready-made settings for UADP over MQTT.
    ''' </summary>
    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        readyMadeSettingsComboBox.SelectedIndex = 4
    End Sub

    ''' <summary>
    ''' Act upon a change in the "subscribed" status, i.e. perform the actual subscribe Or unsubscribe. Also, enable or
    ''' disable controls on the screen based on the new status.
    ''' </summary>
    Private Sub OnSubscribedChanged()
        If Subscribed Then
            If SubscribeDataSetArguments IsNot Nothing Then
                easyUASubscriber1.SubscribeDataSet(SubscribeDataSetArguments)
            End If
        Else
            easyUASubscriber1.UnsubscribeAllDataSets()
        End If

        readyMadeSettingsComboBox.Enabled = Not Subscribed
        subscribeButton.Enabled = Not Subscribed
        unsubscribeButton.Enabled = Subscribed
    End Sub

    ''' <summary>
    ''' The user has selected a different set of read-made settings. Change the arguments for dataset subscription in
    ''' accordance with the selected settings.
    ''' </summary>
    Private Sub readyMadeSettingsComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles readyMadeSettingsComboBox.SelectedIndexChanged

        Dim subscribeDataSetArguments2 As UASubscribeDataSetArguments = Nothing
        Dim info As String = Nothing

        Select Case readyMadeSettingsComboBox.SelectedIndex
            Case 0
                subscribeDataSetArguments2 = UASubscribeDataSetArguments.Default
            Case 1
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/json"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.ResourceAddress = "mqtt://opcua-pubsub.demo-this.com"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttJson
                'subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = New UADataSetWriterDescriptor(4) ' not contained in the message
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.PublisherId.ExternalValue = "32"
            Case 2
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.ResourceAddress = "opc.eth://FF-FF-FF-FF-FF-FF"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.EthUadp
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = New UADataSetWriterDescriptor(4)
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.PublisherId.ExternalValue = CDec(31)
                Try
                    subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.UseEthernetCaptureFile("UADemoPublisher-Ethernet.pcap")
                Catch exception As Exception
                    errorTextBox.BackColor = Color.Orange
                    errorTextBox.Text = exception.GetBaseException().Message
                End Try
            Case 3
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.ResourceAddress = "opc.eth://FF-FF-FF-FF-FF-FF"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.EthUadp
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = New UADataSetWriterDescriptor(4)
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.PublisherId.ExternalValue = CDec(31)
                info = "In order to produce network messages for this demo, run the UADemoPublisher tool with the -eth switch. In some cases, you may have to specify the interface name to be used. Additional software may be needed."
            Case 4
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.CommunicationParameters.BrokerDataSetReaderTransportParameters.QueueName = "opcuademo/uadp/none"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.ResourceAddress = "mqtt://opcua-pubsub.demo-this.com"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.MqttUadp
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = New UADataSetWriterDescriptor(4)
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.PublisherId.ExternalValue = "32"
            Case 5
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.ResourceAddress = "opc.udp://239.0.0.1"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.TransportProfileUriString = UAPubSubTransportProfileUriStrings.UdpUadp
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor = New UADataSetWriterDescriptor(4)
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.PublisherId.ExternalValue = CDec(31)
                info = "In order to produce network messages for this demo, run the UADemoPublisher tool. In some cases, you may have to specify the interface name to be used (on the publisher or subscriber side, or both)."
            Case 6
                subscribeDataSetArguments2 = New UASubscribeDataSetArguments()
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ConnectionDescriptor.Name = "FixedLayoutConnection"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.DataSetWriterDescriptor.Name = "SimpleWriter"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.Filter.WriterGroupDescriptor.Name = "FixedLayoutGroup"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ResolverDescriptor.PublisherFileResourceDescriptor = "UADemoPublisher-Default.uabinary"
                subscribeDataSetArguments2.DataSetSubscriptionDescriptor.ResolverDescriptor.ResolverKind = UAPubSubResolverKind.PublisherFile
                info = "In order to produce network messages for this demo, run the UADemoPublisher tool. In some cases, you may have to specify the interface name to be used (on the publisher or subscriber side, or both)."
        End Select

        SubscribeDataSetArguments = subscribeDataSetArguments2
        infoLabel.Visible = Not String.IsNullOrEmpty(info)
        infoLabel.Text = info
    End Sub

    ''' <summary>
    ''' The user has pressed the "Subscribe" button.
    ''' </summary>
    Private Sub subscribeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles subscribeButton.Click
        Subscribed = True
    End Sub

    ''' <summary>
    ''' Determines whether we are currently subscribed to a dataset. Setting this property creates or removes the
    ''' dataset subscription.
    ''' </summary>
    Private Property Subscribed() As Boolean
        Get
            Return _subscribed
        End Get
        Set(ByVal value As Boolean)
            If value = _subscribed Then
                Return
            End If

            _subscribed = value
            OnSubscribedChanged()
        End Set
    End Property

    ''' <summary>
    ''' Get the arguments for dataset subscription from the control on the form, or set them to the control.
    ''' </summary>
    Private Property SubscribeDataSetArguments() As UASubscribeDataSetArguments
        Get
            Return CType(subscribeDataSetArgumentsControl.GetControlValue(), UASubscribeDataSetArguments)
        End Get
        Set(ByVal value As UASubscribeDataSetArguments)
            subscribeDataSetArgumentsControl.SetControlValue(value)
        End Set
    End Property

    ''' <summary>
    ''' The user has pressed the "Unsubscribe" button.
    ''' </summary>
    Private Sub unsubscribeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles unsubscribeButton.Click
        Subscribed = False
    End Sub

End Class
