' $Header: $
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable PossibleNullReferenceException
#Region "Example"
' A fully functional OPC UA demo server running in Windows Forms host.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Linq
Imports System.Windows.Forms
Imports Microsoft.Extensions.DependencyInjection
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.Forms.Application
Imports OpcLabs.EasyOpc.UA.OperationModel
Imports OpcLabs.EasyOpc.UA.Services
Imports UAServerDemoLibrary

Public Class MainForm
    Sub New()
        InitializeComponent()

        ' Instantiating the EasyUAServer here, and not inline where the field is declared, is important for it to
        ' acquire the proper synchronization context.
        _server = New EasyUAServer()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Extend the form's system menu by a command for OPC UA application management.
        EasyUAFormsApplication.Instance.AddToSystemMenu(Me)

        ' Define various nodes.
        DataNodes.AddToParent(_server.Objects)
        DemoNodes.AddToParent(_server.Objects)

        ' Hook events to the server object.
        AddHandler _server.EndpointStateChanged, AddressOf ServerOnEndpointStateChanged

        ' Obtain the server connection monitoring service.
        Dim serverConnectionMonitoring As IEasyUAServerConnectionMonitoring = _server.GetService(Of IEasyUAServerConnectionMonitoring)()
        If (Not (serverConnectionMonitoring Is Nothing)) Then
            ' Hook events to the connection monitoring service.
            AddHandler serverConnectionMonitoring.ClientSessionConnected, AddressOf ServerConnectionMonitoringOnClientSessionConnected
            AddHandler serverConnectionMonitoring.ClientSessionDisconnected, AddressOf ServerConnectionMonitoringOnClientSessionDisconnected
        End If

        _server.Start()
    End Sub

    Private ReadOnly _server As EasyUAServer

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _server.Stop()
    End Sub

    Private Sub ServerOnEndpointStateChanged(sender As Object, e As EasyUAServerEndpointStateChangedEventArgs)
        ' Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
        ' event handlers directly, because the events are raised using the synchronization context acquired at time of
        ' the creation.

        Dim endpointUrlString As String = e.EndpointUrlString
        Dim listViewItem As ListViewItem = endpointStateListView.Items.Cast(Of ListViewItem)().FirstOrDefault(Function(item) _
            item.Text = endpointUrlString)
        If (listViewItem Is Nothing) Then
            listViewItem = New ListViewItem(endpointUrlString)
            endpointStateListView.Items.Add(listViewItem)
            listViewItem.SubItems.Add("")
            listViewItem.SubItems.Add("")
        End If
        listViewItem.SubItems(1).Text = e.ConnectionState.ToString()
        listViewItem.SubItems(2).Text = If(e.Exception?.Message, "")
    End Sub

    Private Sub ServerConnectionMonitoringOnClientSessionConnected(sender As Object, e As EasyUAClientSessionConnectionEventArgs)
        ' Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
        ' event handlers directly, because the events are raised using the synchronization context acquired at time of
        ' the creation.

        Dim listViewItem = New ListViewItem(e.SessionId.Identifier.ToString())
        listViewItem.SubItems.Add(e.SessionName)
        connectionsListView.Items.Add(listViewItem)
    End Sub

    Private Sub ServerConnectionMonitoringOnClientSessionDisconnected(sender As Object, e As EasyUAClientSessionConnectionEventArgs)
        ' Note that since we have created the EasyUAServer on the UI thread, we can access the UI controls in its
        ' event handlers directly, because the events are raised using the synchronization context acquired at time of
        ' the creation.

        Dim identifierString As String = e.SessionId.Identifier.ToString()
        Dim listViewItem As ListViewItem = connectionsListView.Items.Cast(Of ListViewItem)().FirstOrDefault(Function(item) _
        item.Text = identifierString)
        If (Not (listViewItem Is Nothing)) Then
            connectionsListView.Items.Remove(listViewItem)
        End If
    End Sub

End Class
#End Region