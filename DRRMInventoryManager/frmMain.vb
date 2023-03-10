Public Class frmMain
    Private Sub ItemToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim frmAddItem As New frmAddItem
        frmAddItem.TopLevel = False
        mainPanel.Controls.Add(frmAddItem)
        frmAddItem.Show()
    End Sub

    Private Sub ItemsListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsListToolStripMenuItem.Click
        'Check If the form Is already open
        If Not Application.OpenForms().OfType(Of frmItemsList).Any Then
            Dim frmItemsList As New frmItemsList
            frmItemsList.TopLevel = False
            mainPanel.Controls.Add(frmItemsList)
            frmItemsList.Show()
        Else
            '' Display an error message
            MessageBox.Show("The form is already open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutMessage As String = "Project Name: DRRM Disbursement System" & vbCrLf &
        "--------" & vbCrLf &
        "Team Members:" & vbCrLf &
        "Eric Tierra (Lead Developer)" & vbCrLf &
        "Angelo Lagar (Database Developer)" & vbCrLf &
        "Christel Formaran (UI Design & Tester)" & vbCrLf &
        "Khen Bryan Estrada (UI Design & Tester)" & vbCrLf &
        "Ann Shiela Austria (UI Design & Tester)" & vbCrLf &
        "Yeysha Alva(UI Design & Tester)" & vbCrLf &
        "Alvil Villareal (UI Design & Tester)" & vbCrLf &
        "Klim Son Guevarra (UI Design & Tester)" & vbCrLf &
        "--------" & vbCrLf &
        "Course/Year/Section: BSCS II-C" & vbCrLf &
        "Subject: CC103 - Data Structures and Algorithms"
        MessageBox.Show(aboutMessage, "About", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub tsbAddItem_Click(sender As Object, e As EventArgs)
        ' Check if the form is already open
        If Not Application.OpenForms().OfType(Of frmAddItem).Any Then
            Dim frmAddItem As New frmAddItem
            frmAddItem.Show()
        Else
            ' Display an error message
            MessageBox.Show("The form is already open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs)
        mainPanel.Controls.Clear()
        Dim frm As New frmItemsList()
        frm.TopLevel = False
        mainPanel.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.Screen.Bounds.Width < 1080 Then
            Me.Size = New System.Drawing.Size(800, 600)
        Else
            Me.WindowState = FormWindowState.Maximized
        End If

        'Load the Items List
        Dim frm As New frmItemsList()
        frm.TopLevel = False
        mainPanel.Controls.Add(frm)
        frm.Show()
    End Sub
    Private Sub DisbursedItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisbursedItemsToolStripMenuItem.Click
        Dim frm As New frmDisbursedList()
        frm.TopLevel = False
        mainPanel.Controls.Add(frm)
        frm.Show()
    End Sub

    Private Sub tsbLogoff_Click(sender As Object, e As EventArgs) Handles tsbLogoff.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Dim frmlogout As New frmLogin()
            frmlogout.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub tsmBorrowedList_Click(sender As Object, e As EventArgs) Handles tsmBorrowedList.Click
        Dim frm As New frmBorrowedList()
        frm.TopLevel = False
        mainPanel.Controls.Add(frm)
        frm.Show()
    End Sub
End Class
