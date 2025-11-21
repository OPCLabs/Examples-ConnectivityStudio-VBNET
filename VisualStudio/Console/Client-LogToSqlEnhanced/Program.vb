' LogToSqlEnhanced: Logs OPC Data Access item changes into an SQL database, using a subscription. Item values and qualities
' are stored in their respective columns. Notifications with the same timestamp are merged into a single row.
'
' The database creation script is in the ExamplesNet\MSSQL\QuickOPCExamples.sql file under the product installation 
' directory. The example assumes that the database is already created.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports OpcLabs.BaseLib.Runtime.InteropServices
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.LogToSqlEnhanced

    Friend Class Program
        Shared WithEvents _client As New EasyDAClient
        Shared _adapter As SqlDataAdapter
        Shared _dataSet As DataSet
        Shared _table As DataTable

        <MTAThread> ' needed for COM security initialization to succeed
        Shared Sub Main()
            ComManagement.Instance.AssureSecurityInitialization()

            Const connectionString As String = "Data Source=(local);Initial Catalog=QuickOPCExamples;Integrated Security=true"

            Console.WriteLine("Starting up...")
            Using connection = New SqlConnection(connectionString)
                connection.Open()

                ' Create all necessary ADO.NET objects.
                _adapter = New SqlDataAdapter("SELECT * FROM ColumnarLog", connection)
                _dataSet = New DataSet()
                _adapter.FillSchema(_dataSet, SchemaType.Source, "ColumnarLog")
                _adapter.InsertCommand = (New SqlCommandBuilder(_adapter)).GetInsertCommand()
                _adapter.UpdateCommand = (New SqlCommandBuilder(_adapter)).GetUpdateCommand()
                _table = _dataSet.Tables("ColumnarLog")

                Console.WriteLine("Logging for 30 seconds...")
                ' Subscribe to OPC items, using an anonymous method to process the notifications.
                Dim [handles]() As Integer = _client.SubscribeMultipleItems(New DAItemGroupArguments() {
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 s)", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (10 s)", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (1 min)", 1000, Nothing),
                    New DAItemGroupArguments("", "OPCLabs.KitServer.2", "Trends.Ramp (10 min)", 1000, Nothing)
                })
                Threading.Thread.Sleep(30 * 1000)
                Console.WriteLine()

                Console.WriteLine("Shutting down...")
                _client.UnsubscribeMultipleItems([handles])
            End Using

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub ItemChanged(ByVal sender As Object, ByVal eventArgs As EasyDAItemChangedEventArgs) Handles _client.ItemChanged
            Console.Write(".")
            ' In this example, we only log valid data. Production logger would also log errors.
            If eventArgs.Vtq IsNot Nothing Then
                ' Fill a DataRow with the OPC data, and add it to a DataTable.
                Dim timestamp As Date = If(
                eventArgs.Vtq.Timestamp < CDate(SqlDateTime.MinValue),
                CDate(SqlDateTime.MinValue),
                eventArgs.Vtq.Timestamp)

                Dim row As DataRow = _dataSet.Tables("ColumnarLog").Rows.Find(timestamp)
                Dim adding As Boolean = (row Is Nothing)
                If adding Then
                    row = _table.NewRow()
                    row("Timestamp") = timestamp
                End If

                Select Case eventArgs.Arguments.ItemDescriptor.ItemId
                    Case "Trends.Ramp (1 s)"
                        row("Ramp1Value") = If(eventArgs.Vtq.Value, DBNull.Value)
                        row("Ramp1Quality") = CShort(Fix(eventArgs.Vtq.Quality))
                    Case "Trends.Ramp (10 s)"
                        row("Ramp2Value") = If(eventArgs.Vtq.Value, DBNull.Value)
                        row("Ramp2Quality") = CShort(Fix(eventArgs.Vtq.Quality))
                    Case "Trends.Ramp (1 min)"
                        row("Ramp3Value") = If(eventArgs.Vtq.Value, DBNull.Value)
                        row("Ramp3Quality") = CShort(Fix(eventArgs.Vtq.Quality))
                    Case "Trends.Ramp (10 min)"
                        row("Ramp4Value") = If(eventArgs.Vtq.Value, DBNull.Value)
                        row("Ramp4Quality") = CShort(Fix(eventArgs.Vtq.Quality))
                End Select

                If adding Then
                    _table.Rows.Add(row)
                End If

                ' Update the underlying DataSet using an insert command.
                _adapter.Update(_dataSet, "ColumnarLog")

                ' IMPROVE: For long-running logs, you may have to remove old DataTable.Rows from memory.
            End If
        End Sub
    End Class
End Namespace