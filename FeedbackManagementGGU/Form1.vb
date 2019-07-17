Imports MySql.Data.MySqlClient

Public Class Form1
    Dim ConnectionString As String = "server=localhost; database=feedbackmanagement; uid=mysql; pwd=mysql;"
    Dim Conn As MySqlConnection = New MySqlConnection(ConnectionString)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Conn.Open()
            Console.WriteLine("Connection established")
        Catch exp As Exception
            Console.WriteLine("Error occured : " + exp.Message)
            MessageBox.Show("Error connecting Database.\nError message:\n" + exp.Message)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Conn.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        SelectTeacher.Conn = Conn
        SelectTeacher.Show()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Form4.conn = Conn
        Form4.Show()
    End Sub
End Class
