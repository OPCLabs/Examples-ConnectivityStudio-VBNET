
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Globalization
Imports JetBrains.Annotations
Imports OpcLabs.EasyOpc
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.OperationModel
Imports OpcLabs.EasyOpc.DataAccess.OperationModel


Partial Public Class Form1
    Inherits Form

    Private Const MachineName As String = ""
    Private Const ServerClass As String = "SWToolbox.TOPServer.V5" ' or "Kepware.KEPServerEX.V5"

    Private _fanPower As DAVtq
    Private _heaterPower As DAVtq
    Private _ovenTemperature As DAVtq
    Private _heaterTemperature As DAVtq
    Private _fanSpeed As DAVtq
    Private _temperatureSetpoint As DAVtq


    Public Sub New()
        InitializeComponent()
    End Sub

    Private _defaultBackColor As Color = Color.White


    ' ReSharper disable InconsistentNaming
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' ReSharper restore InconsistentNaming
        SetDefaults()
        _defaultBackColor = txbOvenTemperature.BackColor
    End Sub

    Private Sub SetDefaults()
        nudUpdateRate.Value = 10
    End Sub

    ' ReSharper disable InconsistentNaming
    Private Sub btnStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
        ' ReSharper restore InconsistentNaming
        btnStart.Enabled = False
        timer1.Interval = Decimal.ToInt32(nudUpdateRate.Value * 1000)
        timer1.Start()
        UpdateValuesAndLog()
        btnStop.Enabled = True
    End Sub

    ' ReSharper disable InconsistentNaming
    Private Sub btnStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
        ' ReSharper restore InconsistentNaming
        btnStop.Enabled = False
        timer1.Stop()
        btnStart.Enabled = True
    End Sub

    ' ReSharper disable InconsistentNaming
    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
        ' ReSharper restore InconsistentNaming
        UpdateValuesAndLog()
    End Sub

    Private Function ConvertValueToInt32(ByVal vtq As DAVtq) As Int32?
        If vtq Is Nothing Then
            Return Nothing
        End If
        If Not vtq.HasValue() Then
            Return Nothing
        End If
        Return CType(vtq.Value, Int32?)
    End Function

    Private Function GetTextVtq(ByVal vtq As DAVtq) As String
        If (vtq Is Nothing) OrElse ((Not vtq.HasValue())) Then
            Return "???"
        End If
        Return vtq.DisplayValue()
    End Function

    Private Sub DisplayVtq(ByVal vtq As DAVtq, ByVal txb As TextBox, ByVal colorTestId As Integer)
        If (vtq Is Nothing) OrElse ((Not vtq.HasValue())) Then
            ' ReSharper disable LocalizableElement
            txb.Text = "???"
            ' ReSharper restore LocalizableElement
            txb.BackColor = Color.Magenta
        Else
            txb.Text = vtq.DisplayValue()

            Dim value? As Int32
            Dim aBackColor As Color = _defaultBackColor
            Select Case colorTestId
                Case 0
                    txb.BackColor = _defaultBackColor

                Case 1 ' oven temperature
                    value = ConvertValueToInt32(vtq)
                    If value.HasValue Then
                        Dim range? As Int32 = ConvertValueToInt32(_temperatureSetpoint)
                        If range.HasValue Then
                            If value <= (range - 5) Then
                                aBackColor = Color.Blue
                            Else
                                If value >= (range + 5) Then
                                    aBackColor = Color.Red
                                End If
                            End If
                        End If
                    End If
                    txb.BackColor = aBackColor

                Case 2 ' oven temperature
                    value = ConvertValueToInt32(vtq)
                    If value.HasValue Then
                        If value < 200 Then
                            aBackColor = Color.Red
                        End If
                    End If
                    txb.BackColor = aBackColor
            End Select
        End If
    End Sub

    Private Function ReadVtq(ByVal itemId As String) As DAVtq
        Dim vtq As DAVtq
        Dim exception As Exception = Nothing
        Try
            vtq = easyDAClient1.ReadItem(MachineName, ServerClass, itemId)
        Catch ex As OpcException
            exception = ex
            vtq = Nothing
        End Try
        DisplayException(exception)
        Return vtq
    End Function

    Private Function ReadMultipleVtq(ByVal itemIds() As String) As DAVtqResult()
        Dim results() As DAVtqResult
        Dim exception As Exception = Nothing
        Dim itemDescriptors = New DAItemDescriptor(itemIds.Length - 1) {}
        For i As Integer = 0 To itemIds.Length - 1
            Debug.Assert(itemIds(i) IsNot Nothing)
            itemDescriptors(i) = New DAItemDescriptor(itemIds(i))
        Next i
        Try
            results = easyDAClient1.ReadMultipleItems(New ServerDescriptor(MachineName, ServerClass), itemDescriptors)
        Catch ex As OpcException
            exception = ex
            results = Nothing
        End Try
        DisplayException(exception)
        Return results
    End Function

    Private _lastExceptionMessage As String = ""
    Private Sub DisplayException(ByVal exception As Exception)
        'txbExceptions.Text = exception == null ? "" : exception.GetBaseException().Message;
        If exception IsNot Nothing Then
            Dim newMessage As String = String.Format("{0} {1}", Date.Now, exception.GetBaseException().Message)
            If _lastExceptionMessage <> newMessage Then
                _lastExceptionMessage = newMessage
                txbExceptions.AppendText(newMessage & Environment.NewLine)
            End If
        End If
    End Sub

    Private Sub ReadMultiple()
        Dim results() As DAVtqResult = ReadMultipleVtq(New String() {"Channel1.Device1.FanPower", "Channel1.Device1.FanSpeed", "Channel1.Device1.HeaterPower", "Channel1.Device1.HeaterTemp", "Channel1.Device1.TempSetPoint", "Channel1.Device1.OvenTemp"})

        If results Is Nothing Then
            Return
        End If

        Debug.Assert(results(0) IsNot Nothing)
        Debug.Assert(results(1) IsNot Nothing)
        Debug.Assert(results(2) IsNot Nothing)
        Debug.Assert(results(3) IsNot Nothing)
        Debug.Assert(results(4) IsNot Nothing)
        Debug.Assert(results(5) IsNot Nothing)

        If results(0).Exception Is Nothing Then
            _fanPower = results(0).Vtq
        Else
            _fanPower = Nothing
            DisplayException(results(0).Exception)
        End If
        If results(1).Exception Is Nothing Then
            _fanSpeed = results(1).Vtq
        Else
            _fanSpeed = Nothing
            DisplayException(results(1).Exception)
        End If
        If results(2).Exception Is Nothing Then
            _heaterPower = results(2).Vtq
        Else
            _heaterPower = Nothing
            DisplayException(results(2).Exception)
        End If
        If results(3).Exception Is Nothing Then
            _heaterTemperature = results(3).Vtq
        Else
            _heaterTemperature = Nothing
            DisplayException(results(3).Exception)
        End If
        If results(4).Exception Is Nothing Then
            _temperatureSetpoint = results(4).Vtq
        Else
            _temperatureSetpoint = Nothing
            DisplayException(results(4).Exception)
        End If
        If results(5).Exception Is Nothing Then
            _ovenTemperature = results(5).Vtq
        Else
            _ovenTemperature = Nothing
            DisplayException(results(5).Exception)
        End If
    End Sub


    Private Sub UpdateValuesAndLog()
        ' read multiple item
        ReadMultiple()

        DisplayVtq(_fanPower, txbFanPower, 0)
        DisplayVtq(_fanSpeed, txbFanSpeed, 0)
        DisplayVtq(_heaterPower, txbHeaterPower, 0)
        DisplayVtq(_heaterTemperature, txbHeaterTemperature, 2)
        DisplayVtq(_temperatureSetpoint, txbTemperatureSetpoint, 0)
        DisplayVtq(_ovenTemperature, txbOvenTemperature, 1)

        WriteToLog()
    End Sub

    ' ReSharper disable InconsistentNaming
    Private Sub btnSetTemperatureSetpoint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSetTemperatureSetpoint.Click
        ' ReSharper restore InconsistentNaming
        Dim value As Object = txbNewTemperatureSetpoint.Text
        Dim exception As Exception = Nothing
        Try
            easyDAClient1.WriteItemValue(MachineName, ServerClass, "Channel1.Device1.TempSetPoint", value)
        Catch ex As OpcException
            exception = ex
        End Try
        DisplayException(exception)

        _temperatureSetpoint = ReadVtq("Channel1.Device1.TempSetPoint")
        DisplayVtq(_temperatureSetpoint, txbTemperatureSetpoint, 0)
        DisplayVtq(_ovenTemperature, txbOvenTemperature, 1)

    End Sub

    Private Sub WriteToLog()
        Try
            Dim sw = New IO.StreamWriter(Application.StartupPath & "\OvenControl.csv", True)
            sw.WriteLine(Date.Now.ToString(CultureInfo.InvariantCulture) & "," & GetTextVtq(_fanPower) & "," & GetTextVtq(_heaterPower) & "," & GetTextVtq(_ovenTemperature) & "," & GetTextVtq(_heaterTemperature) & "," & GetTextVtq(_fanSpeed) & "," & GetTextVtq(_temperatureSetpoint))

            sw.Close()
        Catch e As Exception
            DisplayException(e)
        End Try
    End Sub

    ' ReSharper disable InconsistentNaming
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        ' ReSharper restore InconsistentNaming
        Close()
    End Sub
End Class