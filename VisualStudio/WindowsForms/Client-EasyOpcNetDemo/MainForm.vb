' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComInterop
Imports OpcLabs.EasyOpc.DataAccess
Imports System.Globalization
Imports OpcLabs.EasyOpc.OperationModel
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

' ReSharper disable CheckNamespace
' ReSharper disable InconsistentNaming

<Assembly:CLSCompliant(True)>
Namespace EasyOpcNetDemo

    Partial Public Class MainForm
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub


        Private _isItemSubscribed As Boolean ' = false

        ''' <summary>
        ''' A handle for the OPC item subscription, returned by the client component. The handle is used to change the
        ''' parameters of the subscription, Or to unsubscribe.
        ''' </summary>
        Private _itemHandle As Integer ' = 0


        ''' <summary>
        ''' The user has pressed the "About" button. Show a message box with information about the executing assembly.
        ''' </summary>
        Private Sub aboutButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutButton.Click

            MessageBox.Show(Me, Reflection.Assembly.GetExecutingAssembly().FullName, "Assembly Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        ''' <summary>
        ''' The user has pressed the "Browse items" button. Let the user select the OPC item (from items available in the
        ''' given OPC server) in a modal dialog.
        ''' </summary>
        Private Sub browseItemsButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles browseItemsButton.Click

            opcDAItemDialog1.ServerDescriptor.MachineName = machineNameTextBox.Text
            opcDAItemDialog1.ServerDescriptor.ServerClass = serverClassTextBox.Text
            If opcDAItemDialog1.ShowDialog() = DialogResult.OK Then
                itemIdTextBox.Text = opcDAItemDialog1.NodeElement.ItemId
            End If
        End Sub

        ''' <summary>
        ''' The user has pressed the "Browse machines" button. Let the user select a machine (from computers available on
        ''' the network) in a modal dialog.
        ''' </summary>
        Private Sub browseMachinesButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles browseMachinesButton.Click

            If computerBrowserDialog1.ShowDialog() = DialogResult.OK Then
                machineNameTextBox.Text = computerBrowserDialog1.SelectedName
            End If
        End Sub

        ''' <summary>
        ''' The user has pressed the "Browse properties" button. Let the user select an OPC property (from properties
        ''' available on the given OPC item) in a modal dialog.
        ''' </summary>
        Private Sub browsePropertiesButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles browsePropertiesButton.Click

            opcDAPropertyDialog1.ServerDescriptor.MachineName = machineNameTextBox.Text
            opcDAPropertyDialog1.ServerDescriptor.ServerClass = serverClassTextBox.Text
            opcDAPropertyDialog1.NodeDescriptor = itemIdTextBox.Text
            If opcDAPropertyDialog1.ShowDialog() = DialogResult.OK Then
                propertyIdMaskedTextBox.Text = opcDAPropertyDialog1.PropertyElement.PropertyId.ToString()
            End If
        End Sub

        ''' <summary>
        ''' The user has pressed the "Browse servers" button. Let the user select the OPC server (from servers available on
        ''' the given machine) in a modal dialog.
        ''' </summary>
        Private Sub browseServersButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles browseServersButton.Click

            opcServerDialog1.Location = machineNameTextBox.Text
            If opcServerDialog1.ShowDialog(Me) = DialogResult.OK Then
                serverClassTextBox.Text = opcServerDialog1.ServerElement.ServerClass
            End If
        End Sub

        ''' <summary>
        ''' The user has pressed the "Change item subscription" button. Change the parameters of the current subscription to
        ''' the requested update rate and percent deadband currently given on the form.
        ''' </summary>
        Private Sub changeItemSubscriptionButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles changeItemSubscriptionButton.Click

            Dim groupParameters = New DAGroupParameters(CInt(Fix(requestedUpdateRateNumericUpDown.Value)), CSng(percentDeadbandNumericUpDown.Value))
            easyDAClient1.ChangeItemSubscription(_itemHandle, groupParameters)
        End Sub

        ''' <summary>
        ''' The user has pressed the "Close" button. Close the form.
        ''' </summary>
        Private Sub closeButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles closeButton.Click
            Close()
        End Sub

        ''' <summary>
        ''' Updates the "Exception" text box with the text of the error, or clears it if there Is no exception.
        ''' </summary>
        Private Sub DisplayException(ByVal exception As Exception)
            If CObj(exception) Is CObj(Nothing) Then
                exceptionTextBox.Text = ""
            Else
                exceptionTextBox.Text = exception.GetBaseException().Message
            End If
        End Sub

        ''' <summary>
        ''' Updates the "Value", "Timestamp" And "Quality" text boxes with data from the OPC server, or clears them if no
        ''' data is available.
        ''' </summary>
        Private Sub DisplayVtq(ByVal vtq As DAVtq)
            If vtq Is Nothing Then
                valueTextBox.Text = ""
                timestampTextBox.Text = ""
                qualityTextBox.Text = ""
            Else
                valueTextBox.Text = vtq.DisplayValue()
                timestampTextBox.Text = vtq.Timestamp.ToString()
                qualityTextBox.Text = vtq.Quality.ToString()
            End If
        End Sub

        ''' <summary>
        ''' Event handler for the <see cref="EasyDAClient.ItemChanged"/> event. It Is invoked for every significant change
        ''' related to the OPC items subscribed. We display the data received (or the error) on the form.
        ''' </summary>
        Private Sub easyDAClient1_ItemChanged(ByVal sender As Object, ByVal e As EasyDAItemChangedEventArgs) Handles easyDAClient1.ItemChanged

            DisplayVtq(e.Vtq)
            DisplayException(e.Exception)
        End Sub

        ''' <summary>
        ''' The user has pressed the "Get property value" button. Attempt to get the value of the given OPC property, and
        ''' display either the value, or the error received.
        ''' </summary>
        Private Sub getPropertyValueButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles getPropertyValueButton.Click

            Dim propertyId As Integer = Convert.ToInt32(propertyIdMaskedTextBox.Text, CultureInfo.CurrentCulture)
            Dim value As Object = Nothing
            Dim exception As Exception = Nothing
            Try
                value = easyDAClient1.GetPropertyValue(machineNameTextBox.Text, serverClassTextBox.Text, itemIdTextBox.Text, propertyId)
            Catch ex As OpcException
                exception = ex
            End Try
            propertyValueTextBox.Text = (If(value Is Nothing, "(null)", String.Format(CultureInfo.CurrentCulture, "{0}", value)))
            DisplayException(exception)
        End Sub

        ''' <summary>
        ''' Gets Or sets whether there Is currently a subscription to an OPC item.
        ''' </summary>
        ''' <remarks>
        ''' <para>
        ''' The method enables Or disables corresponding controls on the form.</para>
        ''' </remarks>
        Public Property IsItemSubscribed() As Boolean
            Get
                Return _isItemSubscribed
            End Get
            Set(ByVal value As Boolean)
                _isItemSubscribed = value
                subscribeItemButton.Enabled = Not _isItemSubscribed
                changeItemSubscriptionButton.Enabled = _isItemSubscribed
                unsubscribeItemButton.Enabled = _isItemSubscribed
            End Set
        End Property

        ''' <summary>
        ''' The user has pressed the "Read item" button. Attempt to read the given OPC item from the given OPC server, and
        ''' display either the value-timestamp-quality, or the error received.
        ''' </summary>
        Private Sub readItemButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles readItemButton.Click

            Dim vtq As DAVtq = Nothing
            Dim exception As Exception = Nothing
            Try
                vtq = easyDAClient1.ReadItem(machineNameTextBox.Text, serverClassTextBox.Text, itemIdTextBox.Text)
            Catch ex As OpcException
                exception = ex
            End Try
            DisplayVtq(vtq)
            DisplayException(exception)
        End Sub

        ''' <summary>
        ''' The user has pressed the "Subscribe item" button. Subscribe to the given OPC item. Data will flow into the form
        ''' by means of the <see cref="easyDAClient1_ItemChanged"/> event handler.
        ''' </summary>
        Private Sub subscribeItemButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles subscribeItemButton.Click

            Const dataType As Short = VarTypes.Empty
            Dim requestedUpdateRate As Integer = CInt(Fix(requestedUpdateRateNumericUpDown.Value))
            Dim percentDeadband As Single = CSng(percentDeadbandNumericUpDown.Value)
            _itemHandle = easyDAClient1.SubscribeItem(machineNameTextBox.Text, serverClassTextBox.Text, itemIdTextBox.Text, dataType, requestedUpdateRate, percentDeadband)
            IsItemSubscribed = True
        End Sub

        ''' <summary>
        ''' The user has pressed the "Unsubscribe item" button. Unsubscribe from OPC item we have subscribed earlier in
        ''' <see cref="subscribeItemButton_Click"/>. Data will no longer flow through the
        ''' <see cref="easyDAClient1_ItemChanged"/> event handler.
        ''' </summary>
        Private Sub unsubscribeItemButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles unsubscribeItemButton.Click

            easyDAClient1.UnsubscribeItem(_itemHandle)
            _itemHandle = 0
            IsItemSubscribed = False
        End Sub

        ''' <summary>
        ''' The user has pressed the "Write item" button. Attempt to write the value entered on the form to the given OPC
        ''' item. If an error was received, display it.
        ''' </summary>
        Private Sub writeItemValueButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles writeItemValueButton.Click

            Dim value As Object = valueToWriteTextBox.Text
            Dim exception As Exception = Nothing
            Try
                easyDAClient1.WriteItemValue(machineNameTextBox.Text, serverClassTextBox.Text, itemIdTextBox.Text, value)
            Catch ex As OpcException
                exception = ex
            End Try
            DisplayException(exception)
        End Sub
    End Class
End Namespace
