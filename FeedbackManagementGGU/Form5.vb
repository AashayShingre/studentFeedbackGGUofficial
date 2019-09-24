Imports MySql.Data.MySqlClient

Public Class Form5
    Public Property conn As MySqlConnection
    Public Property type As String
    Public Property current_name As String
    Public Property current_id As Int32

    Public Property school_name As String
    Public Property school_table As DataTable = Nothing

    Public Property dept_name As String
    Public Property dept_table As DataTable = Nothing

    Public Property course_name As String
    Public Property course_table As DataTable = Nothing

    Public Property semester As Integer
    Public Property selected_sem As Integer

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Console.WriteLine("current_name is " & current_name)
        Label1.Text = "Edit " & type

        If Me.school_table IsNot Nothing Then
            ComboBox1.DataSource = Me.school_table
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With
        End If
        ComboBox1.Text = school_name

        If dept_table IsNot Nothing Then
            ComboBox2.DataSource = Me.dept_table
            With ComboBox2
                .DisplayMember = "dept_name"
                .ValueMember = "dept_id"
            End With
        End If
        ComboBox2.Text = dept_name

        If course_table IsNot Nothing Then
            ComboBox3.DataSource = Me.course_table
            With ComboBox3
                .DisplayMember = "course_name"
                .ValueMember = "course_id"
            End With
        End If
        ComboBox3.Text = course_name

        TextBox1.Text = current_name

        If type = "School" Then
            ComboBox1.Visible = False
            Label5.Visible = False
            ComboBox2.Visible = False
            Label4.Visible = False
            ComboBox3.Visible = False
            Label3.Visible = False
            TextBox2.Visible = False
            Label6.Visible = False
            ComboBox4.Visible = False
        ElseIf type = "Department" Then
            ComboBox1.Visible = True
            ComboBox2.Visible = False
            ComboBox3.Visible = False
            TextBox2.Visible = False
            ComboBox4.Visible = False
            Label5.Visible = True
            Label4.Visible = False
            Label3.Visible = False
            Label6.Visible = False
        ElseIf type = "Course" Then
            ComboBox1.Visible = True
            ComboBox2.Visible = True
            ComboBox3.Visible = False
            TextBox2.Visible = True
            TextBox2.Text = semester
            ComboBox4.Visible = False
            Label5.Visible = True
            Label4.Visible = True
            Label3.Visible = False
            Label6.Visible = True
            Label6.Text = "No. of Sems"
        Else
            ComboBox1.Visible = True
            ComboBox2.Visible = True
            ComboBox3.Visible = True
            ComboBox4.Visible = False
            TextBox2.Visible = False
            Label6.Visible = False

            If type = "Subject" Then
                TextBox2.Visible = True
                Label6.Visible = True
                Label6.Text = "Semester"
                ComboBox4.Visible = True
                'now fill the combobox
                Dim Semester_Names() As String = {"I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX"}


                ComboBox4.Items.Clear()
                ComboBox4.MaxDropDownItems = semester

                For i = 0 To semester - 1
                    ComboBox4.Items.Add(Semester_Names(i))
                Next

                ComboBox4.SelectedIndex = selected_sem - 1
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, LinkLabel1.Click
        Me.Close()
        Manage.Show()
        Manage.RefreshPage()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'the id of the last combobox is updated
        If type = "School" Then
            Dim sqlquery As String = "update schools set school_name = """ & TextBox1.Text & """ where school_id = " & current_id.ToString("00") & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        ElseIf type = "Department" Then
            Dim school_id As Int32 = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update departments set dept_name = """ & TextBox1.Text & """, school_id = " & school_id.ToString() & " where dept_id = " & current_id.ToString() & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        ElseIf type = "Course" Then
            Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update courses set course_name = """ & TextBox1.Text & """, semester_count = " & TextBox2.Text & ", dept_id = " & dept_id.ToString() & " where course_id = " & current_id.ToString() & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        ElseIf type = "Teacher" Then
            Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update teachers set teacher_name = """ & TextBox1.Text & """, course_id = " & course_id.ToString() & " where teacher_id = " & current_id.ToString() & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        Else type = "Subject"
            Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update subjects set subject_name = """ & TextBox1.Text & """, semester = " & ComboBox4.SelectedIndex + 1 & ", course_id = " & course_id.ToString() & " where subject_id = " & current_id.ToString() & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        End If

        MessageBox.Show("Successfully updated")
        Me.Close()
        Manage.Show()
        Manage.RefreshPage()
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

        ComboBox2.ResetText()
        ComboBox3.ResetText()
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

        ComboBox3.ResetText()
    End Sub

End Class