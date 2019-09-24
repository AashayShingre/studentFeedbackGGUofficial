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
            MessageBox.Show("Error connecting Database. Error message: " + exp.Message, "Error Loading...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Conn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SelectTeacher.Conn = Conn
        SelectTeacher.Type = "Subject"
        SelectTeacher.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Manage.conn = Conn
        Manage.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        viewstats.Conn = Conn
        viewstats.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SelectTeacher.Conn = Conn
        SelectTeacher.Type = "Practical"
        SelectTeacher.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ViewLabStats.Conn = Conn
        ViewLabStats.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SelectCourse.Conn = Conn
        SelectCourse.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ViewStatsOverall.Conn = Conn
        ViewStatsOverall.Show()
    End Sub
End Class
