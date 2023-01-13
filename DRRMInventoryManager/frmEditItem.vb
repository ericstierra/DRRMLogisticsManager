Imports System.Data.OleDb
Public Class frmEditItem
    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
    Dim conn As New OleDbConnection(connString)
    Private itemID As String
    Public Sub New(itemID As String)
        InitializeComponent()
        Me.itemID = itemID
    End Sub

    Private Sub frmEditItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmd As New OleDbCommand("SELECT item_code, item_name, category, qty, unit, condition, description, status, location FROM tbl_items WHERE item_code = ?", conn)
        cmd.Parameters.AddWithValue("@item_code", itemID)

        Try
            conn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                txtItemCode.Text = reader("item_code").ToString()
                txtItemName.Text = reader("item_name").ToString()
                cmbCategory.Text = reader("category").ToString()
                txtQuantity.Text = reader("qty").ToString()
                cmbUnit.Text = reader("unit").ToString()
                cmbCondition.Text = reader("condition").ToString()
                txtDescription.Text = reader("description").ToString()
                cmbStatus.Text = reader("status").ToString()
                cmbLocation.Text = reader("location").ToString()
            Else
                MessageBox.Show("Item code not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            reader.Close()
        Catch ex As OleDbException
            MessageBox.Show("Error retrieving data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub tsbUpdate_Click(sender As Object, e As EventArgs) Handles tsbUpdate.Click
        Dim cmd As New OleDbCommand("UPDATE tbl_items SET item_code = @item_code, item_name = @item_name, category = @category, qty = @qty, unit = @unit, condition = @condition, description = @description, location = @location, status = @status WHERE item_code = @item_code", conn)

        cmd.Parameters.AddWithValue("@item_code", itemID)
        cmd.Parameters.AddWithValue("@item_name", txtItemName.Text)
        cmd.Parameters.AddWithValue("@category", cmbCategory.Text)
        cmd.Parameters.AddWithValue("@qty", txtQuantity.Text)
        cmd.Parameters.AddWithValue("@unit", cmbUnit.Text)
        cmd.Parameters.AddWithValue("@condition", cmbCondition.Text)
        cmd.Parameters.AddWithValue("@description", txtDescription.Text)
        cmd.Parameters.AddWithValue("@location", cmbLocation.Text)
        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            MessageBox.Show("Error updating record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub
End Class