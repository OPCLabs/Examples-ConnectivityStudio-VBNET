' SimpleLogToSql: Logs OPC Data Access item changes into an SQL database, using a subscription. Values of all data types are
' stored in a single SQL_VARIANT column.
'
' The database creation script is in the ExamplesNet\MSSQL\QuickOPCExamples.sql file under the product installation 
' directory. The example assumes that the database is already created.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports OpcLabs.BaseLib.Runtime.InteropServices
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.OperationModel

Namespace Global.SimpleLogToSql

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
                _adapter = New SqlDataAdapter("SELECT * FROM SimpleLog", connection)
                _dataSet = New DataSet()
                _adapter.FillSchema(_dataSet, SchemaType.Source, "SimpleLog")
                _adapter.InsertCommand = (New SqlCommandBuilder(_adapter)).GetInsertCommand()
                _table = _dataSet.Tables("SimpleLog")
                Debug.Assert(_table IsNot Nothing)

                Console.WriteLine("Logging for 30 seconds...")
                ' Subscribe to an OPC item, using an anonymous method to process the notifications.
                Dim handle As Integer = _client.SubscribeItem(
                "",
                "OPCLabs.KitServer.2",
                "Simulation.Incrementing (1 s)",
                100)
                Threading.Thread.Sleep(30 * 1000)
                Console.WriteLine()

                Console.WriteLine("Shutting down...")
                _client.UnsubscribeItem(handle)
            End Using

            Console.WriteLine("Finished.")
        End Sub

        Private Shared Sub ItemChanged(ByVal sender As Object, ByVal eventArgs As EasyDAItemChangedEventArgs) Handles _client.ItemChanged
            Debug.Assert(eventArgs IsNot Nothing)
            Console.Write(".")

            ' In this example, we only log valid data. Production logger would also log errors.
            If eventArgs.Vtq IsNot Nothing Then
                ' Fill a DataRow with the OPC data, and add it to a DataTable.
                Debug.Assert(_table.Rows IsNot Nothing)
                _table.Rows.Clear()
                Dim row As DataRow = _table.NewRow()
                row("ItemID") = eventArgs.Arguments.ItemDescriptor.ItemId
                row("Value") = eventArgs.Vtq.Value
                row("Timestamp") = If(eventArgs.Vtq.Timestamp < CDate(SqlDateTime.MinValue), CDate(SqlDateTime.MinValue), eventArgs.Vtq.Timestamp)
                row("Quality") = CShort(Fix(eventArgs.Vtq.Quality))

                Debug.Assert(_table.Rows IsNot Nothing)
                _table.Rows.Add(row)

                ' Update the underlying DataSet using an insert command.
                _adapter.Update(_dataSet, "SimpleLog")
            End If
        End Sub
    End Class
End Namespace