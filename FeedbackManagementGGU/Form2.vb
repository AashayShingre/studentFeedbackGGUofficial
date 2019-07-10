Public Class Form2
    Dim StudentName As String = ""
    Dim ConnectionString As String = "server=localhost;database=feedbackmanagement;uid=mysql;pwd=mysql;"
    Dim Conn As MySql.Data.MySqlClient.MySqlConnection = New MySql.Data.MySqlClient.MySqlConnection(ConnectionString)



    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Conn.Open()
            Console.WriteLine("Connection established")

            'load the school names
            Dim SchoolQuery = "Select"

        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database; please reload")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        StudentName = TextBox1.Text
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class