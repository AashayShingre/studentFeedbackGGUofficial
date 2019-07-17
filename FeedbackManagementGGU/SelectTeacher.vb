Imports MySql.Data.MySqlClient

Public Class SelectTeacher
    Public Property Conn As New MySqlConnection

    Private Sub SelectTeacher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'load the school names
            Dim SchoolQuery = "select * from schools"
            Dim da As New MySqlDataAdapter(SchoolQuery, Me.Conn)
            Dim dt As New DataTable

            da.Fill(dt)
            ComboBox1.DataSource = dt
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With

            ComboBox1.ResetText()

        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database; please reopen the page.")
            Me.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim school_id As Int32 = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())

        Dim DeptQuery As String = "select dept_id, dept_name from departments where school_id = " + school_id.ToString("00") + ";"
        Dim da As New MySqlDataAdapter(DeptQuery, Conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox2.DataSource = dt
        With ComboBox2
            .DisplayMember = "dept_name"
            .ValueMember = "dept_id"
        End With

        ComboBox2.ResetText()
        ComboBox3.ResetText()
        ComboBox4.ResetText()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())

        Dim courseQuery As String = "select course_id, course_name from courses where dept_id = " + dept_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(courseQuery, Conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox3.DataSource = dt
        With ComboBox3
            .DisplayMember = "course_name"
            .ValueMember = "course_id"
        End With

        ComboBox3.ResetText()
        ComboBox4.ResetText()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())

        Dim teacherQuery As String = "select teacher_id, teacher_name from teachers where course_id = " + course_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(teacherQuery, Conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox4.DataSource = dt
        With ComboBox4
            .DisplayMember = "teacher_name"
            .ValueMember = "teacher_id"
        End With

        ComboBox4.ResetText()
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Button1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'take the selected value to the next page
        Try
            Dim teacher_id As Int32 = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
            FeedbackForm.teacher_id = teacher_id.ToString("00")
            FeedbackForm.teacher_name = ComboBox4.Text
            FeedbackForm.conn = Conn
            'FeedbackForm.student_name = TextBox1.Text
            Console.WriteLine("teacher name is " + ComboBox4.Text)
            Console.WriteLine("teacherid is " + teacher_id.ToString("00"))

            Me.Hide()
            FeedbackForm.Show()
        Catch ex As Exception
            MessageBox.Show("Teacher not selected")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub ComboBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox4.KeyUp, ComboBox3.KeyUp, ComboBox2.KeyUp, ComboBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class