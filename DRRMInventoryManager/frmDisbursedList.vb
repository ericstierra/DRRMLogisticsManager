Imports System.Data.OleDb
Public Class frmDisbursedList
    Private Sub tsbDisburse_Click(sender As Object, e As EventArgs) Handles tsbDisburse.Click
        ' Check if the form is already open
        If Not Application.OpenForms().OfType(Of frmDisburseItem).Any Then
            Dim frm As New frmDisburseItem
            frm.Show()
        Else
            ' Display an error message
            MessageBox.Show("The form is already open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub frmDisbursedItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDisbursedGrid()

        Dim returnButton As New DataGridViewButtonColumn()
        returnButton.Name = "Return"
        returnButton.HeaderText = "Return"
        returnButton.Text = "Return"
        returnButton.UseColumnTextForButtonValue = True
        DataGridView1.Columns.Add(returnButton)
        returnButton.DefaultCellStyle.BackColor = Color.FromArgb(189, 195, 199)
        returnButton.FlatStyle = FlatStyle.Popup
        DataGridView1.Columns("Return").Width = 100


    End Sub

    Private Sub LoadDisbursedGrid()
        Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
        Using conn As New OleDbConnection(connString)
            conn.Open()

            Dim da As New OleDbDataAdapter("SELECT d_date, d_name, department, item_code, item_name, d_qty FROM tbl_disbursed", conn)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt

            conn.Close()
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = DataGridView1.Columns("Return").Index AndAlso e.RowIndex >= 0 Then
            Dim item_code As String = DataGridView1.Rows(e.RowIndex).Cells("item_code").Value.ToString()
            Dim item_name As String = DataGridView1.Rows(e.RowIndex).Cells("item_name").Value.ToString()

            'Your code here to update the status and delete the row
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to return this item?", "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then

                Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
                Dim cmd As OleDbCommand
                Using conn As New OleDbConnection(connString)
                    conn.Open()
                    cmd = New OleDbCommand("UPDATE tbl_items SET status = 'In-stock' WHERE item_code = @item_code", conn)
                    cmd.Parameters.AddWithValue("@item_code", item_code)
                    cmd.ExecuteNonQuery()
                End Using



                Using conn As New OleDbConnection(connString)
                    conn.Open()
                    cmd = New OleDbCommand("DELETE FROM tbl_disbursed WHERE item_code = @item_code AND item_name = @item_name", conn)
                    cmd.Parameters.AddWithValue("@item_code", item_code)
                    cmd.Parameters.AddWithValue("@item_name", item_name)
                    cmd.ExecuteNonQuery()
                End Using


                LoadDisbursedGrid()
            End If
        End If
    End Sub

    Private Sub DefineButtonCells()
        Dim column As New DataGridViewButtonColumn()
        column.HeaderText = "Return"
        column.Text = "Return"
        column.Name = "Return"
        column.UseColumnTextForButtonValue = True
        column.DataPropertyName = "return"
        column.DefaultCellStyle.BackColor = Color.FromArgb(189, 195, 199)
        column.FlatStyle = FlatStyle.Popup
        DataGridView1.Columns.Insert(9, column)

        DataGridView1.Columns("Return").Width = 50
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        LoadDisbursedGrid()
    End Sub
End Class