Imports MySql.Data.MySqlClient

Public Class SelectTeacher
    Public Property Conn As New MySqlConnection
    Public Property Type As String

    Private Sub SelectTeacher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Type = "Subject" Then
            Label7.Text = "Subject Wise Feedback"
        Else
            Label7.Text = "Practical Wise Feedback"
        End If

        Try
            'load the school names
            Dim SchoolQuery = "select * from schools"
            Dim da As New MySqlDataAdapter(SchoolQuery, Me.Conn)
            Dim dt As New DataTable
            Dim dt2 As New DataTable

            da.Fill(dt)
            dt2 = dt.Copy

            ComboBox1.DataSource = dt
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With

            ComboBox6.DataSource = dt2
            With ComboBox6
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With

            ComboBox1.ResetText()
            ComboBox6.ResetText()

        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database. Please reopen the page.")
            Me.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'take the selected value to the next page
        Try
            Dim teacher_id As Int32 = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
            Console.WriteLine("teacher id is " & teacher_id.ToString)
            Dim subject_id As Int32 = Convert.ToInt32(ComboBox10.SelectedValue.GetHashCode())
            Console.WriteLine("subject_id is " & subject_id)
            Me.Hide()

            If Type = "Subject" Then
                FeedbackForm.teacher_id = teacher_id.ToString("00")
                FeedbackForm.subject_id = subject_id.ToString()
                FeedbackForm.teacher_name = ComboBox4.Text
                FeedbackForm.subject_name = ComboBox10.Text
                FeedbackForm.conn = Conn
                FeedbackForm.Show()
            Else
                FeedbackFormLab.teacher_id = teacher_id.ToString("00")
                FeedbackFormLab.subject_id = subject_id.ToString()
                FeedbackFormLab.teacher_name = ComboBox4.Text
                FeedbackFormLab.subject_name = ComboBox10.Text
                FeedbackFormLab.conn = Conn
                FeedbackFormLab.Show()
            End If


        Catch ex As Exception
            MessageBox.Show("Teacher or Subject not selected")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
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

        ComboBox2.Text = "---Select Department---"
    End Sub

    Private Sub ComboBox2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox2.SelectionChangeCommitted
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

        ComboBox3.Text = "---Select Course---"
    End Sub

    Private Sub ComboBox3_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox3.SelectionChangeCommitted
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

        ComboBox4.Text = "---Select Teacher---"
    End Sub

    Private Sub ComboBox6_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox6.SelectionChangeCommitted
        Dim school_id As Int32 = Convert.ToInt32(ComboBox6.SelectedValue.GetHashCode())

        Dim DeptQuery As String = "select dept_id, dept_name from departments where school_id = " + school_id.ToString("00") + ";"
        Dim da As New MySqlDataAdapter(DeptQuery, Conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox7.DataSource = dt
        With ComboBox7
            .DisplayMember = "dept_name"
            .ValueMember = "dept_id"
        End With

        ComboBox7.Text = "---Select Department---"
    End Sub

    Private Sub ComboBox7_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox7.SelectionChangeCommitted
        Dim dept_id As Int32 = Convert.ToInt32(ComboBox7.SelectedValue.GetHashCode())

        Dim courseQuery As String = "select course_id, course_name from courses where dept_id = " + dept_id.ToString() + ";"

        Dim da As New MySqlDataAdapter(courseQuery, Conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox8.DataSource = dt
        With ComboBox8
            .DisplayMember = "course_name"
            .ValueMember = "course_id"
        End With

        ComboBox8.Text = "---Select Course---"
    End Sub

    Private Sub ComboBox8_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox8.SelectionChangeCommitted
        Dim course_id As Int32 = Convert.ToInt32(ComboBox8.SelectedValue.GetHashCode())

        Dim Semester_Names() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX"}

        'fill semester
        Dim semQuery As String = "select semester_count from courses where course_id = " + course_id.ToString() + ";"
        Dim semCmd As New MySqlCommand(semQuery, Conn)

        Dim Count As Int32 = Convert.ToInt32(semCmd.ExecuteScalar())
        Console.WriteLine(Count)

        ComboBox9.Items.Clear()
        ComboBox9.MaxDropDownItems = Count

        For i = 0 To Count - 1
            ComboBox9.Items.Add(Semester_Names(i))
        Next

        ComboBox9.Text = "---Select Semester---"
        'fill subjects
        Dim subjectQuery As String = "select subject_id, subject_name from subjects where course_id = " + course_id.ToString() + ";"

        Dim daS As New MySqlDataAdapter(subjectQuery, Conn)
        Dim dtS As New DataTable

        daS.Fill(dtS)
        ComboBox10.DataSource = dtS
        With ComboBox10
            .DisplayMember = "subject_name"
            .ValueMember = "subject_id"
        End With

        ComboBox10.Text = "---Select Subject---"
    End Sub

    Private Sub ComboBox9_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox9.SelectionChangeCommitted
        Dim course_id As Int32 = Convert.ToInt32(ComboBox8.SelectedValue.GetHashCode())
        Dim sem As Integer = ComboBox9.SelectedIndex + 1

        Dim subjectQuery As String = "select subject_id, subject_name from subjects where course_id = " + course_id.ToString() + " and semester = " + sem.ToString() + ";"
        Dim daS As New MySqlDataAdapter(subjectQuery, Conn)
        Dim dtS As New DataTable

        daS.Fill(dtS)
        ComboBox10.DataSource = dtS
        With ComboBox10
            .DisplayMember = "subject_name"
            .ValueMember = "subject_id"
        End With

        ComboBox10.Text = "---Select Subject---"
    End Sub

    Private Sub ComboBox1_KeyUp_1(sender As Object, e As KeyEventArgs) Handles ComboBox9.KeyUp, ComboBox8.KeyUp, ComboBox7.KeyUp, ComboBox6.KeyUp, ComboBox4.KeyUp, ComboBox3.KeyUp, ComboBox2.KeyUp, ComboBox10.KeyUp, ComboBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class