' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Forms.Application
Imports OpcLabs.EasyOpc.UA.OperationModel

<Assembly: CLSCompliant(True)>

' ReSharper disable InconsistentNaming

Partial Public Class MainForm
    Inherits Form
    Public Sub New()
        InitializeComponent()
    End Sub


    ''' <summary>
    ''' A handle for the OPC subscription, returned by the client component. The handle is used to change the
    ''' parameters of the subscription, or to unsubscribe.
    ''' </summary>
    Private _handle As Integer ' = 0

    Private _isSubscribed As Boolean ' = false


    ''' <summary>
    ''' The user has pressed the "About" button. Show a message box with information about the executing assembly.
    ''' </summary>
    Private Sub aboutButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutButton.Click
        MessageBox.Show(Me, Reflection.Assembly.GetExecutingAssembly().FullName, My.Resources.MainForm_AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' The user has pressed the "Browse" button. Let the user select the OPC data node (from nodes available in the
    ''' given OPC server) in a modal dialog.
    ''' </summary>
    Private Sub browseDataButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles browseDataButton.Click

        uaDataDialog1.EndpointDescriptor = serverUriTextBox.Text
        If uaDataDialog1.ShowDialog() = DialogResult.OK Then
            nodeIdTextBox.Text = uaDataDialog1.NodeDescriptor.NodeId
        End If
    End Sub

    ''' <summary>
    ''' The user has pressed the "Change subscription" button. Change the parameters of the current subscription to
    ''' the sampling interval currently given on the form.
    ''' </summary>
    Private Sub changeSubscriptionButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles changeMonitoredItemSubscriptionButton.Click
        easyUAClient1.ChangeMonitoredItemSubscription(_handle, CInt(Fix(samplingIntervalNumericUpDown.Value)))
    End Sub

    ''' <summary>
    ''' The user has pressed the "Close" button. Close the form.
    ''' </summary>
    Private Sub closeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles closeButton.Click
        If IsSubscribed Then
            Unsubscribe()
        End If
        Close()
    End Sub

    ''' <summary>
    ''' The user has pressed the "Discover servers" button. Let the user enter or select the host and an OPC server in
    ''' a modal dialog.
    ''' </summary>
    Private Sub discoverServersButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles discoverServersButton.Click

        uaHostAndEndpointDialog1.EndpointDescriptor = serverUriTextBox.Text
        If uaHostAndEndpointDialog1.ShowDialog() = DialogResult.OK Then
            serverUriTextBox.Text = uaHostAndEndpointDialog1.EndpointDescriptor.UrlString
        End If
    End Sub

    ''' <summary>
    ''' Updates the "Value", "Status", "Source timestamp" And "Server timestamp" text boxes with data from the OPC
    ''' server, or clears them if no data Is available.
    ''' </summary>
    Private Sub DisplayAttributeData(ByVal attributeData As UAAttributeData)
        If attributeData Is Nothing Then
            valueTextBox.Text = ""
            statusTextBox.Text = ""
            sourceTimestampTextBox.Text = ""
            serverTimestampTextBox.Text = ""
        Else
            valueTextBox.Text = attributeData.DisplayValue()
            statusTextBox.Text = attributeData.StatusCode.ToString()
            sourceTimestampTextBox.Text = attributeData.SourceTimestamp.ToString()
            serverTimestampTextBox.Text = attributeData.ServerTimestamp.ToString()
        End If
    End Sub

    ''' <summary>
    ''' Updates the "Exception" text box with the text of the error, or clears it if there is no exception.
    ''' </summary>
    Private Sub DisplayException(ByVal exception As Exception)
        exceptionTextBox.Text = If(exception Is Nothing, "", exception.GetBaseException().Message)
    End Sub

    ''' <summary>
    ''' Event handler for the <see cref="EasyUAClient.DataChangeNotification"/> event. It is invoked for every
    ''' significant change related to the OPC attribute subscribed. We display the data received (or the error) on the
    ''' form.
    ''' </summary>
    Private Sub easyUAClient1_DataChangeNotification(ByVal sender As Object, ByVal e As EasyUADataChangeNotificationEventArgs) Handles easyUAClient1.DataChangeNotification
        DisplayAttributeData(If(e.Exception Is Nothing, e.AttributeData, Nothing))
        DisplayException(e.Exception)
    End Sub

    ''' <summary>
    ''' Gets Or sets whether there is currently a subscription to an OPC monitored item.
    ''' </summary>
    ''' <remarks>
    ''' <para>
    ''' The method enables or disables corresponding controls on the form.</para>
    ''' </remarks>
    Public Property IsSubscribed() As Boolean
        Get
            Return _isSubscribed
        End Get
        Set(ByVal value As Boolean)
            _isSubscribed = value
            subscribeMonitoredItemButton.Enabled = Not _isSubscribed
            changeMonitoredItemSubscriptionButton.Enabled = _isSubscribed
            unsubscribeMonitoredItemButton.Enabled = _isSubscribed
        End Set
    End Property

    ''' <summary>
    ''' When the form loads, extend its system menu by a command for OPC UA application management.
    ''' </summary>
    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        EasyUAFormsApplication.Instance.AddToSystemMenu(Me)
    End Sub

    ''' <summary>
    ''' The user has pressed the "Read" button. Attempt to read the Value attribute of the given OPC node from the given
    ''' OPC server, And display either the attribute data, or the error received.
    ''' </summary>
    Private Sub readButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readButton.Click
        Dim attributeData As UAAttributeData = Nothing
        Dim exception As Exception = Nothing
        Try
            attributeData = easyUAClient1.Read(serverUriTextBox.Text, nodeIdTextBox.Text)
        Catch ex As UAException
            exception = ex
        End Try
        DisplayAttributeData(attributeData)
        DisplayException(exception)
    End Sub

    ''' <summary>
    ''' The user has pressed the "Subscribe" button. Subscribe to the Value attribute of the given OPC node. Data will
    ''' flow into the form by means of the <see cref="easyUAClient1_DataChangeNotification"/> event handler.
    ''' </summary>
    Private Sub subscribeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles subscribeMonitoredItemButton.Click

        _handle = easyUAClient1.SubscribeDataChange(serverUriTextBox.Text, nodeIdTextBox.Text, CInt(Fix(samplingIntervalNumericUpDown.Value)))
        IsSubscribed = True
    End Sub

    ''' <summary>
    ''' Unsubscribe from OPC monitored item we have subscribed earlier in <see cref="subscribeButton_Click"/>. Data will
    ''' no longer flow through the <see cref="easyUAClient1_DataChangeNotification"/> event handler.
    ''' </summary>
    Private Sub Unsubscribe()
        easyUAClient1.UnsubscribeMonitoredItem(_handle)
        _handle = 0
        IsSubscribed = False
    End Sub

    ''' <summary>
    ''' The user has pressed the "Unsubscribe" button.
    ''' </summary>
    Private Sub unsubscribeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles unsubscribeMonitoredItemButton.Click
        Unsubscribe()
    End Sub

    ''' <summary>
    ''' The user has pressed the "Write value" button. Attempt to write the value entered on the form to the given OPC
    ''' node. If an error was received, display it.
    ''' </summary>
    Private Sub writeValueButton_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles writeValueButton.Click

        Dim exception As Exception = Nothing
        Try
            easyUAClient1.WriteValue(serverUriTextBox.Text, nodeIdTextBox.Text, valueToWriteTextBox.Text)
        Catch ex As UAException
            exception = ex
        End Try
        DisplayException(exception)
    End Sub
End Class