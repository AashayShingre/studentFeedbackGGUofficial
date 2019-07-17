Imports MySql.Data.MySqlClient

Public Class Form5
    Public Property conn As MySqlConnection
    Public Property type As String
    Public Property current_name As String
    Public Property school_name As String
    Public Property dept_name As String
    Public Property course_name As String
    Public Property current_id As Int32

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Console.WriteLine("current_name is " & current_name)
        Label1.Text = "Edit " & type

        Try
            'load the school names
            Dim SchoolQuery = "select * from schools"
            Dim da As New MySqlDataAdapter(SchoolQuery, Me.conn)
            Dim dt As New DataTable

            da.Fill(dt)
            ComboBox1.DataSource = dt
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With

        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database; please reopen the page.")
            Me.Close()
        End Try

        ComboBox1.Text = school_name
        ComboBox2.Text = dept_name
        ComboBox3.Text = course_name
        TextBox1.Text = current_name

        If type = "School" Then
            ComboBox1.Visible = False
            ComboBox2.Visible = False
            ComboBox3.Visible = False
        ElseIf type = "Department" Then
            ComboBox2.Visible = False
            ComboBox3.Visible = False
        ElseIf type = "Course" Then
            ComboBox3.Visible = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        Form4.Show()
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
            Dim sqlquery As String = "update departments set dept_name = """ & TextBox1.Text & """, school_id = " & school_id.ToString("00") & " where dept_id = " & current_id.ToString("00") & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        ElseIf type = "Course" Then
            Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update courses set course_name = """ & TextBox1.Text & """, dept_id = " & dept_id.ToString("00") & " where course_id = " & current_id.ToString("00") & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        ElseIf type = "Teacher" Then
            Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            Dim sqlquery As String = "update teachers set teacher_name = """ & TextBox1.Text & """, course_id = " & course_id.ToString("00") & " where teacher_id = " & current_id.ToString("00") & " ;"
            Console.WriteLine(sqlquery)
            Dim cmd As New MySqlCommand(sqlquery, conn)
            cmd.ExecuteNonQuery()
        End If

        MessageBox.Show("Successfully updated")
        Me.Close()
        Form4.Show()
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