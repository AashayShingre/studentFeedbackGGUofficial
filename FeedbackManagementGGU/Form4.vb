Imports MySql.Data.MySqlClient

Public Class Form4
    Public Property conn As New MySqlConnection

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Text = "Teacher"
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
        ComboBox4.Text = "Select Teacher"

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
        ComboBox4.Text = "Select Teacher"
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())

        Dim teacherQuery As String = "select teacher_id, teacher_name from teachers where course_id = " + course_id.ToString("00") + ";"

        Dim da As New MySqlDataAdapter(teacherQuery, conn)
        Dim dt As New DataTable

        da.Fill(dt)
        ComboBox4.DataSource = dt
        With ComboBox4
            .DisplayMember = "teacher_name"
            .ValueMember = "teacher_id"
        End With

        ComboBox4.Text = "Select Teacher"
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Console.WriteLine("Combobox 5 changed")

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
        Console.WriteLine("combo6 selected index changed")

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
        Label2.Text = "Add " + ListBox1.Text

        If ListBox1.Text = "School" Then
            ComboBox2.Visible = False
            ComboBox3.Visible = False
            ComboBox4.Visible = False
            ComboBox5.Visible = False
            ComboBox6.Visible = False
            ComboBox7.Visible = False
        ElseIf ListBox1.Text = "Department" Then
            ComboBox2.Visible = True
            ComboBox5.Visible = True

            ComboBox3.Visible = False
            ComboBox4.Visible = False
            ComboBox6.Visible = False
            ComboBox7.Visible = False
        ElseIf ListBox1.Text = "Course" Then
            ComboBox2.Visible = True
            ComboBox3.Visible = True
            ComboBox5.Visible = True
            ComboBox6.Visible = True

            ComboBox4.Visible = False
            ComboBox7.Visible = False

        Else
            ComboBox2.Visible = True
            ComboBox3.Visible = True
            ComboBox5.Visible = True
            ComboBox6.Visible = True
            ComboBox4.Visible = True
            ComboBox7.Visible = True

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If RichTextBox1.Text = "" Then
            MessageBox.Show("Enter names in the box")
            Exit Sub
        End If

        Dim strarr() As String
        strarr = RichTextBox1.Text.Replace(Chr(13), "").Split(Chr(10))

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
            Else
                Dim course_id As Int32 = Convert.ToInt32(ComboBox7.SelectedValue.GetHashCode())
                Dim contstr As String = "(""" + strarr(0) + """, " & course_id.ToString("00") & ")"
                For i = 1 To strarr.Length - 1
                    contstr = contstr & ", (""" + strarr(i) + """, " & course_id.ToString("00") & ")"
                Next
                Dim cmd As New MySqlCommand("insert into teachers (teacher_name, course_id) values " & contstr & ";", Me.conn)
                cmd.ExecuteNonQuery()
            End If

            MessageBox.Show(ListBox1.Text & "(s) added successfully.")
        Catch excep As Exception
            MessageBox.Show("Error adding " & ListBox1.Text)
        End Try

        RichTextBox1.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form5.type = ListBox1.Text
        Console.WriteLine("combobox1 selection:" & ComboBox1.Text)
        Form5.school_name = ComboBox1.Text
        Form5.dept_name = ComboBox2.Text
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
            ElseIf ListBox1.Text = "Teacher" Then
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
        Dim d As DialogResult = MessageBox.Show("are you sure?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)

        If d = DialogResult.Yes Then
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
            End If
            Me.Form4_Load(sender, e)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        viewstats.Show()
    End Sub
End Class