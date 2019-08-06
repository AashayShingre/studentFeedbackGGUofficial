Imports MySql.Data.MySqlClient

Public Class SelectCourse
    Public Property Conn As New MySqlConnection

    Private Sub SelectCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            ComboBox2.ResetText()
            ComboBox3.ResetText()

        Catch ex As Exception
            MessageBox.Show("Cannot connect to the database. Please reopen the page.")
            Me.Close()
        End Try
    End Sub

    Private Sub ComboBox1_Selected(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
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
    End Sub

    Private Sub ComboBox2_Selected(sender As Object, e As EventArgs) Handles ComboBox2.SelectionChangeCommitted
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
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'take the selected value to the next page
        Try
            Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
            Console.WriteLine("course id is " & course_id.ToString)
            Me.Hide()

            OverallFeedbackForm.course_id = course_id.ToString("00")
            OverallFeedbackForm.course_name = ComboBox3.Text
            OverallFeedbackForm.conn = Conn
            OverallFeedbackForm.Show()

        Catch ex As Exception
            MessageBox.Show("Course not selected")
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub ComboBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles ComboBox3.KeyUp, ComboBox2.KeyUp, ComboBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class