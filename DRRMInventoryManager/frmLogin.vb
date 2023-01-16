Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username = "Admin"
        Dim password = "12345"

        If tsbUsername.Text = username And tsbPassword.Text = password Then
            Dim frm1 As New frmMain
            frm1.Show()
            Me.Hide()
        Else
            MessageBox.Show("Sorry wrong username or password. Please try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tsbUsername.Clear()
            tsbPassword.Clear()
            tsbUsername.Focus()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class