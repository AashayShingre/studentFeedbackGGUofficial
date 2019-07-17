Imports MySql.Data.MySqlClient

Public Class Form3
    Public Property teacher_id As String
    Public Property teacher_name As String
    Public Property conn As MySql.Data.MySqlClient.MySqlConnection
    Public Property student_name As String

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label14.Text = Me.teacher_name
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class