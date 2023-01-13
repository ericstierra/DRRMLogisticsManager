Imports System.Data.OleDb

Public Class frmAddItem
    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
    Dim conn As New OleDbConnection(connString)
    Private Sub frmAddItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        If String.IsNullOrEmpty(txtItemName.Text) Then
            MessageBox.Show("Please enter a valid Item Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbCategory.Text) Then
            MessageBox.Show("Please enter a valid Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(txtQuantity.Text) OrElse Not IsNumeric(txtQuantity.Text) Then
            MessageBox.Show("Please enter a valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbUnit.Text) Then
            MessageBox.Show("Please enter a valid Unit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbCondition.Text) Then
            MessageBox.Show("Please enter a valid Condition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(txtDescription.Text) Then
            MessageBox.Show("Please enter a valid Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Replace the MySQL connection string with an Access connection string
        Dim cmd As New OleDbCommand("INSERT INTO tbl_items (item_code, item_name, category, qty, unit, condition, description, date_added) VALUES (@item_code, @item_name, @category, @quantity, @unit, @condition, @description, @date_added)", conn)

        cmd.Parameters.AddWithValue("@item_code", txtItemCode.Text)
        cmd.Parameters.AddWithValue("@item_name", txtItemName.Text)
        cmd.Parameters.AddWithValue("@category", cmbCategory.Text)
        cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text)
        cmd.Parameters.AddWithValue("@unit", cmbUnit.Text)
        cmd.Parameters.AddWithValue("@condition", cmbCondition.Text)
        cmd.Parameters.AddWithValue("@description", txtDescription.Text)
        cmd.Parameters.AddWithValue("@date_added", DateTime.Now)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            MessageBox.Show("Error adding record to the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try

        LoadDataIntoGrid()
    End Sub

    Private Sub txtItemName_TextChanged(sender As Object, e As EventArgs) Handles txtItemName.TextChanged

        Dim rnd As New Random()
        Dim itemCode As String = "DLM" & DateTime.Now.ToString("MMddyy") & rnd.Next(100, 1000).ToString()

        txtItemCode.Text = itemCode

    End Sub
    Private Sub LoadDataIntoGrid()

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Dim cmd As New OleDbCommand("UPDATE tbl_items SET item_name = ?, category = ?, qty = ?, unit = ?, condition = ?, description = ? WHERE item_code = ?", conn)

        cmd.Parameters.AddWithValue("@item_name", txtItemName.Text)
        cmd.Parameters.AddWithValue("@category", cmbCategory.Text)
        cmd.Parameters.AddWithValue("@qty", txtQuantity.Text)
        cmd.Parameters.AddWithValue("@unit", cmbUnit.Text)
        cmd.Parameters.AddWithValue("@condition", cmbCondition.Text)
        cmd.Parameters.AddWithValue("@description", txtDescription.Text)
        cmd.Parameters.AddWithValue("@item_code", txtItemCode.Text)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            MessageBox.Show("Error updating record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try

        LoadDataIntoGrid()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim cmd As New OleDbCommand("SELECT item_name, category, qty, unit, condition, description FROM tbl_items WHERE item_code = ?", conn)
        'cmd.Parameters.AddWithValue("@item_code", txtSearchItemCode.Text)

        Try
            conn.Open()
            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                txtItemName.Text = reader("item_name").ToString()
                cmbCategory.Text = reader("category").ToString()
                txtQuantity.Text = reader("qty").ToString()
                cmbUnit.Text = reader("unit").ToString()
                cmbCondition.Text = reader("condition").ToString()
                txtDescription.Text = reader("description").ToString()
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

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
        If String.IsNullOrEmpty(txtItemName.Text) Then
            MessageBox.Show("Please enter a valid Item Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbCategory.Text) Then
            MessageBox.Show("Please enter a valid Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(txtQuantity.Text) OrElse Not IsNumeric(txtQuantity.Text) Then
            MessageBox.Show("Please enter a valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbUnit.Text) Then
            MessageBox.Show("Please enter a valid Unit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(cmbCondition.Text) Then
            MessageBox.Show("Please enter a valid Condition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If String.IsNullOrEmpty(txtDescription.Text) Then
            MessageBox.Show("Please enter a valid Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        Dim cmd As New OleDbCommand("INSERT INTO tbl_items (item_code, item_name, category, qty, unit, condition, description, status, location, date_added) VALUES (@item_code, @item_name, @category, @quantity, @unit, @condition, @description, @status, @location, @date_added)", conn)

        cmd.Parameters.AddWithValue("@item_code", txtItemCode.Text)
        cmd.Parameters.AddWithValue("@item_name", txtItemName.Text)
        cmd.Parameters.AddWithValue("@category", cmbCategory.Text)
        cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text)
        cmd.Parameters.AddWithValue("@unit", cmbUnit.Text)
        cmd.Parameters.AddWithValue("@condition", cmbCondition.Text)
        cmd.Parameters.AddWithValue("@description", txtDescription.Text)
        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)
        cmd.Parameters.AddWithValue("@location", cmbLocation.Text)
        cmd.Parameters.AddWithValue("@date_added", DateTime.Now)

        Try
            conn.Open()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As OleDbException
            MessageBox.Show("Error adding record to the database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub
End Class