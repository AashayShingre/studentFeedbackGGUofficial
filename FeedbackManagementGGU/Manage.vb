Imports MySql.Data.MySqlClient

Public Class Manage
    Public Property conn As New MySqlConnection

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Manage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.SelectedIndex = 3
        Try
            'load the school names
            Dim SchoolQuery = "select * from schools"
            Dim da As New MySqlDataAdapter(SchoolQuery, Me.conn)
            Dim dt As New DataTable
            Dim dt1 As New DataTable

            da.Fill(dt)
            dt1 = dt.Copy()

            ComboBox1.DataSource = dt
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With


            ComboBox5.DataSource = dt1
            With ComboBox5
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With


        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database; please reopen the page.")
            Me.Close()

        Finally
            ComboBox1.Text = "Select School"
            ComboBox2.Text = "Select Department"
            ComboBox3.Text = "Select Course"
            ComboBox4.Text = "Select Teacher"

            ComboBox5.Text = "Select School"
            ComboBox6.Text = "Select Department"
            ComboBox7.Text = "Select Course"

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim school_id As Int32 = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())

        Dim DeptQuery As String = "select dept_id, dept_name from departments where school_id = " + school_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(DeptQuery, conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox2.DataSource = dt
        With ComboBox2
            .DisplayMember = "dept_name"
            .ValueMember = "dept_id"
        End With

        ComboBox2.Text = "Select Department"
        ComboBox3.Text = "Select Course"
        ComboBox4.Text = "Select " + ListBox1.Text

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())

        Dim courseQuery As String = "select course_id, course_name from courses where dept_id = " + dept_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(courseQuery, conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox3.DataSource = dt
        With ComboBox3
            .DisplayMember = "course_name"
            .ValueMember = "course_id"
        End With

        ComboBox3.Text = "Select Course"

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())

        If ListBox1.Text = "Teacher" Then
            Dim teacherQuery As String = "select teacher_id, teacher_name from teachers where course_id = " + course_id.ToString("00") + ";"
            Dim da As New MySqlDataAdapter(teacherQuery, conn)
            Dim dt As New DataTable

            da.Fill(dt)
            ComboBox4.DataSource = dt
            With ComboBox4
                .DisplayMember = "teacher_name"
                .ValueMember = "teacher_id"
            End With
        Else
            Dim subjectQuery As String = "select subject_id, subject_name from subjects where course_id = " + course_id.ToString("00") + ";"
            Dim da As New MySqlDataAdapter(subjectQuery, conn)
            Dim dt As New DataTable

            da.Fill(dt)
            ComboBox4.DataSource = dt
            With ComboBox4
                .DisplayMember = "subject_name"
                .ValueMember = "subject_id"
            End With
        End If

        ComboBox4.Text = "Select " + ListBox1.Text
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim school_id As Int32 = Convert.ToInt32(ComboBox5.SelectedValue.GetHashCode())

        Dim DeptQuery As String = "select dept_id, dept_name from departments where school_id = " + school_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(DeptQuery, conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox6.DataSource = dt
        With ComboBox6
            .DisplayMember = "dept_name"
            .ValueMember = "dept_id"
        End With

        ComboBox6.Text = "Select Department"
        ComboBox7.Text = "Select Course"
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim dept_id As Int32 = Convert.ToInt32(ComboBox6.SelectedValue.GetHashCode())

        Dim courseQuery As String = "select course_id, course_name from courses where dept_id = " + dept_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(courseQuery, conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox7.DataSource = dt
        With ComboBox7
            .DisplayMember = "course_name"
            .ValueMember = "course_id"
        End With

        ComboBox7.Text = "Select Course"
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Label6.Text = "Add " + ListBox1.Text
        If ListBox1.Text = "School" Then
            'Department
            ComboBox2.Visible = False
            Label3.Visible = False
            'Course
            ComboBox3.Visible = False
            Label4.Visible = False
            'Teacher-Subject
            ComboBox4.Visible = False
            Label5.Visible = False

            'School
            TableLayoutPanel5.Visible = False
            'Department
            TableLayoutPanel6.Visible = False
            'Course
            TableLayoutPanel7.Visible = False
        ElseIf ListBox1.Text = "Department" Then
            'Department
            ComboBox2.Visible = True
            Label3.Visible = True
            'Course
            ComboBox3.Visible = False
            Label4.Visible = False
            'Teacher-s
            ComboBox4.Visible = False
            Label5.Visible = False

            'School
            TableLayoutPanel5.Visible = True
            'Department
            TableLayoutPanel6.Visible = False
            'Course
            TableLayoutPanel7.Visible = False
        ElseIf ListBox1.Text = "Course" Then
            'Department
            ComboBox2.Visible = True
            Label3.Visible = True
            'Course
            ComboBox3.Visible = True
            Label4.Visible = True
            'Teacher-s
            ComboBox4.Visible = False
            Label5.Visible = False

            'School
            TableLayoutPanel5.Visible = True
            'Department
            TableLayoutPanel6.Visible = True
            'Course
            TableLayoutPanel7.Visible = False

        Else
            'Department
            ComboBox2.Visible = True
            Label3.Visible = True
            'Course
            ComboBox3.Visible = True
            Label4.Visible = True
            'Teacher-s
            ComboBox4.Visible = True
            ComboBox4.Text = "Select " + ListBox1.Text
            Label5.Visible = True
            Label5.Text = ListBox1.Text

            'School
            TableLayoutPanel5.Visible = True
            'Department
            TableLayoutPanel6.Visible = True
            'Course
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Enter names in the box")
            Exit Sub
        End If

        Dim strarr() As String
        strarr = TextBox1.Text.Replace(Chr(13), "").Split(Chr(10))

        Try
            If ListBox1.Text = "School" Then
                Dim contstr As String = "(""" + strarr(0) + """)"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """)"
                Next
                Dim cmd As New MySqlCommand("insert into schools (school_name) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            ElseIf ListBox1.Text = "Department" Then
                Dim school_id As Int32 = Convert.ToInt32(ComboBox5.SelectedValue.GetHashCode())
                Dim contstr As String = "(""" + strarr(0) + """, " & school_id.ToString("00") & ")"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """, " & school_id.ToString("00") & ")"
                Next
                Dim cmd As New MySqlCommand("insert into departments (dept_name, school_id) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            ElseIf ListBox1.Text = "Course" Then
                Dim dept_id As Int32 = Convert.ToInt32(ComboBox6.SelectedValue.GetHashCode())
                Dim contstr As String = "(""" + strarr(0) + """, " & dept_id.ToString("00") & ")"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """, " & dept_id.ToString("00") & ")"
                Next
                Dim cmd As New MySqlCommand("insert into courses (course_name, dept_id) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            ElseIf ListBox1.Text = "Teacher" Then
                Dim course_id As Int32 = Convert.ToInt32(ComboBox7.SelectedValue.GetHashCode())
                Dim contstr As String = "(""" + strarr(0) + """, " & course_id.ToString("00") & ")"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """, " & course_id.ToString("00") & ")"
                Next
                Dim cmd As New MySqlCommand("insert into teachers (teacher_name, course_id) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            ElseIf ListBox1.Text = "Subject" Then
                Dim course_id As Int32 = Convert.ToInt32(ComboBox7.SelectedValue.GetHashCode())
                Dim contstr As String = "(""" + strarr(0) + """, " & course_id.ToString("00") & ")"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """, " & course_id.ToString("00") & ")"
                Next
                Dim cmd As New MySqlCommand("insert into subjects (subject_name, course_id) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            End If

            MessageBox.Show(ListBox1.Text & "(s) added successfully.")
        Catch excep As Exception
            MessageBox.Show("Error adding " & ListBox1.Text)
        End Try

        TextBox1.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form5.type = ListBox1.Text

        If ComboBox1.DataSource IsNot Nothing Then
            Form5.school_table = ComboBox1.DataSource
        End If
        Form5.school_name = ComboBox1.Text

        If ComboBox2.DataSource IsNot Nothing Then
            Form5.dept_table = ComboBox2.DataSource
        End If
        Form5.dept_name = ComboBox2.Text

        If ComboBox3.DataSource IsNot Nothing Then
            Form5.course_table = ComboBox3.DataSource
        End If
        Form5.course_name = ComboBox3.Text

        Form5.conn = Me.conn

        Try
            If ListBox1.Text = "School" Then
                Form5.current_name = ComboBox1.Text
                Form5.current_id = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())
            ElseIf ListBox1.Text = "Department" Then
                Form5.current_name = ComboBox2.Text
                Form5.current_id = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
            ElseIf ListBox1.Text = "Course" Then
                Form5.current_name = ComboBox3.Text
                Form5.current_id = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            Else
                Form5.current_name = ComboBox4.Text
                Form5.current_id = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
            End If

            Me.Hide()
            Form5.Show()
        Catch except As Exception
            MessageBox.Show("Can't select " & ListBox1.Text & ". Please make sure you have all the selections.")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim d As DialogResult = MessageBox.Show("Are you sure you want to delete? This will also remove all the associated data. This change cannot be reversed.", "Comfirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = DialogResult.Yes Then
            Try
                If ListBox1.Text = "School" Then
                    Dim school_id As Int32 = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())
                    Dim cmd As New MySqlCommand("delete from schools where school_id = " + school_id.ToString() + ";", conn)
                    cmd.ExecuteNonQuery()

                ElseIf ListBox1.Text = "Department" Then
                    Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
                    Dim cmd As New MySqlCommand("delete from departments where dept_id = " + dept_id.ToString() + ";", conn)
                    cmd.ExecuteNonQuery()
                ElseIf ListBox1.Text = "Course" Then
                    Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
                    Dim cmd As New MySqlCommand("delete from courses where course_id = " + course_id.ToString() + ";", conn)
                    cmd.ExecuteNonQuery()
                ElseIf ListBox1.Text = "Teacher" Then
                    Dim teacher_id As Int32 = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
                    Dim cmd As New MySqlCommand("delete from teachers where teacher_id = " + teacher_id.ToString() + ";", conn)
                    cmd.ExecuteNonQuery()
                ElseIf ListBox1.Text = "Subject" Then
                    Dim subject_id As Int32 = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
                    Dim cmd As New MySqlCommand("delete from subjects where subject_id = " + subject_id.ToString() + ";", conn)
                    cmd.ExecuteNonQuery()
                End If
                Me.Manage_Load(sender, e)
            Catch ex As Exception
                MessageBox.Show("Error occurred while deleting. Please make sure you have all the selections.")
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        viewstats.type = ListBox1.Text
        viewstats.school_name = ComboBox1.Text
        viewstats.dept_name = ComboBox2.Text
        viewstats.course_name = ComboBox3.Text
        viewstats.Conn = Me.conn

        Try
            If ListBox1.Text = "School" Then
                viewstats.current_name = ComboBox1.Text
                viewstats.current_id = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())
            ElseIf ListBox1.Text = "Department" Then
                viewstats.current_name = ComboBox2.Text
                viewstats.current_id = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
            ElseIf ListBox1.Text = "Course" Then
                viewstats.current_name = ComboBox3.Text
                viewstats.current_id = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            ElseIf ListBox1.Text = "Teacher" Then
                viewstats.current_name = ComboBox4.Text
                viewstats.current_id = Convert.ToInt32(ComboBox4.SelectedValue.GetHashCode())
            End If

            Me.Hide()
            viewstats.Show()
        Catch except As Exception
            MessageBox.Show("Can't select " & ListBox1.Text & ". Please make sure you have all the selections.")
        End Try
    End Sub
End Class