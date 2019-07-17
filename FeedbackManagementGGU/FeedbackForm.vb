Imports MySql.Data.MySqlClient

Public Class FeedbackForm
    Public Property teacher_id As String
    Public Property teacher_name As String
    Public Property conn As MySql.Data.MySqlClient.MySqlConnection
    Public Property student_name As String

    Private Function MS(Score As String) As String
        If Score = "5" Then
            Return "vg"
        ElseIf Score = "4" Then
            Return "g"
        ElseIf Score = "3" Then
            Return "s"
        Else
            Return "b"
        End If
    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim SQLquery As String =
                "update teachers set 
                prop1_" & MS(TextBox1.Text) & " = prop1_" & MS(TextBox1.Text) & " + 1 ,
                prop2_" & MS(TextBox2.Text) & " = prop2_" & MS(TextBox2.Text) & " + 1 ,
                prop3_" & MS(TextBox3.Text) & " = prop3_" & MS(TextBox3.Text) & " + 1 ,
                prop4_" & MS(TextBox4.Text) & " = prop4_" & MS(TextBox4.Text) & " + 1 ,
                prop5_" & MS(TextBox5.Text) & " = prop5_" & MS(TextBox5.Text) & " + 1 ,
                prop6_" & MS(TextBox6.Text) & " = prop6_" & MS(TextBox6.Text) & " + 1 ,
                prop7_" & MS(TextBox7.Text) & " = prop7_" & MS(TextBox7.Text) & " + 1 ,
                prop8_" & MS(TextBox8.Text) & " = prop8_" & MS(TextBox8.Text) & " + 1 ,
                prop9_" & MS(TextBox9.Text) & " = prop9_" & MS(TextBox9.Text) & " + 1 ,
                prop10_" & MS(TextBox10.Text) & " = prop10_" & MS(TextBox10.Text) & " + 1 ,
                prop11_" & MS(TextBox11.Text) & " = prop11_" & MS(TextBox11.Text) & " + 1 
                where teacher_id = " + teacher_id + ";"

            Dim command As New MySqlCommand(SQLquery, Me.conn)
            command.ExecuteNonQuery()

            'Done
            Me.Close()
            SelectTeacher.Show()

        Catch ex As Exception
            MessageBox.Show("Unknown error occured while adding feedback\n Error: " & ex.Message)
            'log errors here
        End Try
    End Sub

    Private Sub FeedbackForm_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Me.teacher_name
        Console.WriteLine("Form loading")
        Console.WriteLine(Me.teacher_name)
        Console.WriteLine(teacher_id)
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress, TextBox8.KeyPress, TextBox7.KeyPress, TextBox6.KeyPress, TextBox5.KeyPress, TextBox4.KeyPress, TextBox3.KeyPress, TextBox2.KeyPress, TextBox11.KeyPress, TextBox10.KeyPress, TextBox1.KeyPress
        If Not (Asc(e.KeyChar) = 48 Or Asc(e.KeyChar) = 51 Or Asc(e.KeyChar) = 52 Or Asc(e.KeyChar) = 53) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        SelectTeacher.Show()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox9.KeyDown, TextBox8.KeyDown, TextBox7.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox11.KeyDown, TextBox10.KeyDown, TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class