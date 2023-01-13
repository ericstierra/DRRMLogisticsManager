Imports System.Data.OleDb
Public Class frmDisburseItem

    Private Sub tsbProcess_Click(sender As Object, e As EventArgs) Handles tsbProcess.Click
        Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
        Using conn As New OleDbConnection(connString)

            Dim item_code As String = txtItemcode.Text
            Dim newStatus As String = "Disbursed"
            Dim currentStatus As String = ""
            Dim itemExist As Boolean = False

            conn.Open()

            'check if the item code exists in the tbl_items
            Dim cmdExist As New OleDbCommand("SELECT COUNT(*) FROM tbl_items WHERE item_code = @item_code", conn)
            cmdExist.Parameters.AddWithValue("@item_code", item_code)
            itemExist = cmdExist.ExecuteScalar() > 0

            'check if the item code exists in the tbl_items and its status is not disbursed
            If itemExist Then
                'create a command to retrieve the current status
                Dim cmdStatus As New OleDbCommand("SELECT status FROM tbl_items WHERE item_code = @item_code", conn)
                cmdStatus.Parameters.AddWithValue("@item_code", item_code)
                currentStatus = cmdStatus.ExecuteScalar().ToString()

                'check if the current status is "Disbursed"
                If currentStatus = "Disbursed" Then
                    MessageBox.Show("This item has already been disbursed. Please select another item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    'create a command to insert data into tbl_disbursed
                    Dim cmd As New OleDbCommand("INSERT INTO tbl_disbursed (d_date, d_name, department, item_code, item_name, d_status) VALUES (@d_date, @name, @dep, @item_code, @item_name, @d_qty)", conn)
                    cmd.Parameters.AddWithValue("@d_date", Date.Today.ToString("MM/dd/yyyy"))
                    cmd.Parameters.AddWithValue("@name", txtName.Text)
                    cmd.Parameters.AddWithValue("@dep", cmbDep.Text)
                    cmd.Parameters.AddWithValue("@item_code", txtItemcode.Text)
                    cmd.Parameters.AddWithValue("@item_name", txtItemName.Text)
                    cmd.Parameters.AddWithValue("@d_qty", 1)

                    'create a command to update the status
                    Dim cmd1 As New OleDbCommand("UPDATE tbl_items SET status = @status WHERE item_code = @item_code", conn)
                    cmd1.Parameters.AddWithValue("@status", "Disbursed")
                    cmd1.Parameters.AddWithValue("@item_code", item_code)

                    'update the quantity of item disbursed   
                    'Dim cmd2 As New OleDbCommand("UPDATE tbl_items SET quantity = quantity - 1 WHERE item_code = @item_code", conn)
                    'cmd2.Parameters.AddWithValue("@item_code", item_code)

                    Try
                        cmd.ExecuteNonQuery()
                        cmd1.ExecuteNonQuery()
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As OleDbException
                        MessageBox.Show("Error adding record to the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        conn.Close()
                    End Try
                End If
            End If
        End Using
    End Sub

    Private Sub txtItemCode_LostFocus(sender As Object, e As EventArgs) Handles txtItemcode.GotFocus
        Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\est_database\db_drrmlogistics.accdb"
        Using conn As New OleDbConnection(connString)
            Dim item_code As String = txtItemcode.Text
            Dim itemName As String = ""

            conn.Open()
            Dim cmd As New OleDbCommand("SELECT item_name FROM tbl_items WHERE item_code = @item_code", conn)
            cmd.Parameters.AddWithValue("@item_code", item_code)

            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                itemName = reader("item_name").ToString()
            End If
            conn.Close()

            txtItemName.Text = itemName
        End Using
    End Sub
End Class