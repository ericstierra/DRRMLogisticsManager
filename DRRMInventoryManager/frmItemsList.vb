Imports System.Data.OleDb
Public Class frmItemsList
    Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db_drrmlogistics.accdb"
    Dim conn As New OleDbConnection(connString)


    Private Sub frmItemsList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoaddataGrid()
        DefineButtonCells()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Check if the clicked cell is a button cell
        ' If e.ColumnIndex = DataGridView1.Columns(9).Index AndAlso e.RowIndex >= 0 Then
        If e.ColumnIndex = DataGridView1.Columns(9).Index Then
            Dim rowIndex As Integer = e.RowIndex
            Dim itemID As String = DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
            Dim editForm As New frmEditItem(itemID)
            editForm.Show()
        End If
        'End If
    End Sub

    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        Dim cell As DataGridViewButtonCell = TryCast(DataGridView1.CurrentCell, DataGridViewButtonCell)
        If cell IsNot Nothing Then
            Dim button As Button = TryCast(e.Control, Button)
            If button IsNot Nothing Then
                button.Image = Image.FromFile("C:\Users\MDRRMO-PC Monitoring\Downloads\green.png")
                button.ImageAlign = ContentAlignment.MiddleLeft
            End If
        End If
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete

    End Sub

    Private Sub tsbAddItem_Click(sender As Object, e As EventArgs) Handles tsbAddItem.Click
        ' Check if the form is already open
        If Not Application.OpenForms().OfType(Of frmAddItem).Any Then
            Dim frmAddItem As New frmAddItem
            frmAddItem.Show()
        Else
            ' Display an error message
            MessageBox.Show("The form is already open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        LoaddataGrid()
    End Sub

    Public Sub LoaddataGrid()
        conn.Open()
        Dim cmd As New OleDbCommand("SELECT * FROM tbl_items", conn)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        Dim table As New DataTable()
        table.Load(reader)
        reader.Close()
        conn.Close()

        DataGridView1.AutoGenerateColumns = False
        DataGridView1.DataSource = table
        DataGridView1.Columns(0).Width = 150
        DataGridView1.Columns(1).Width = 250
        DataGridView1.Columns(2).Width = 180
        DataGridView1.Columns(3).Width = 500
        DataGridView1.Columns(4).Width = 80
        DataGridView1.Columns(5).Width = 150
        DataGridView1.Columns(6).Width = 150
        DataGridView1.Columns(7).Width = 150
        DataGridView1.Columns(8).Width = 150


        'Cells Allignment
        DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'ColumnHeader Allignment
        DataGridView1.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub DefineButtonCells()
        Dim column As New DataGridViewButtonColumn()
        column.HeaderText = "Action" ' Set the header text for the button column
        column.Text = "Edit" ' Set the text for the Edit button
        column.Name = "edit" ' Set the name for the Edit button
        column.UseColumnTextForButtonValue = True
        column.DataPropertyName = "btnEdit"
        column.DefaultCellStyle.BackColor = Color.FromArgb(189, 195, 199)
        column.FlatStyle = FlatStyle.Popup
        DataGridView1.Columns.Insert(9, column) ' Add the button column to the DataGridView

        Dim deleteColumn As New DataGridViewButtonColumn()
        deleteColumn.HeaderText = "" ' Set the header text for the button column
        deleteColumn.Text = "Delete" ' Set the text for the Delete button
        deleteColumn.Name = "delete" ' Set the name for the Delete button
        deleteColumn.UseColumnTextForButtonValue = True
        deleteColumn.DataPropertyName = "btnDelete"
        deleteColumn.DefaultCellStyle.BackColor = Color.FromArgb(231, 76, 60)
        deleteColumn.FlatStyle = FlatStyle.Popup
        DataGridView1.Columns.Insert(10, deleteColumn) ' Add the button column to the DataGridView

        DataGridView1.Columns("edit").Width = 80
        DataGridView1.Columns("delete").Width = 80

    End Sub

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
End Class