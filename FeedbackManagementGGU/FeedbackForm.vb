Imports MySql.Data.MySqlClient

Public Class FeedbackForm
    Public Property teacher_id As String
    Public Property teacher_name As String
    Public Property subject_id As String
    Public Property subject_name As String
    Public Property conn As MySql.Data.MySqlClient.MySqlConnection

    Dim current_page As Integer = 1
    Dim total_page As Integer = 1

    Dim prop1(100) As String
    Dim prop2(100) As String
    Dim prop3(100) As String
    Dim prop4(100) As String
    Dim prop5(100) As String
    Dim prop6(100) As String
    Dim prop7(100) As String
    Dim prop8(100) As String
    Dim prop9(100) As String
    Dim prop10(100) As String
    Dim prop11(100) As String

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
        'save the current page content
        SaveValues(current_page)

        'start entering data page by page
        For I As Integer = 0 To total_page - 1
            Try
                Dim SQLquery As String =
                    "insert into feedbacks (
                    prop1_" & MS(prop1(I)) & ",
                prop2_" & MS(prop2(I)) & ",
                    prop3_" & MS(prop3(I)) & ",
                prop4_" & MS(prop4(I)) & ",
                    prop5_" & MS(prop5(I)) & ",
                prop6_" & MS(prop6(I)) & ",
                    prop7_" & MS(prop7(I)) & ",
                prop8_" & MS(prop8(I)) & ",
                    prop9_" & MS(prop9(I)) & ",
                prop10_" & MS(prop10(I)) & ",
                    prop11_" & MS(prop11(I)) & ",
                teacher_id, subject_id
                ) values (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, " & teacher_id & ", " & subject_id & ")
                on duplicate key update
                prop1_" & MS(prop1(I)) & " = prop1_" & MS(prop1(I)) & " + 1 ,
                prop2_" & MS(prop2(I)) & " = prop2_" & MS(prop2(I)) & " + 1 ,
                prop3_" & MS(prop3(I)) & " = prop3_" & MS(prop3(I)) & " + 1 ,
                prop4_" & MS(prop4(I)) & " = prop4_" & MS(prop4(I)) & " + 1 ,
                prop5_" & MS(prop5(I)) & " = prop5_" & MS(prop5(I)) & " + 1 ,
                prop6_" & MS(prop6(I)) & " = prop6_" & MS(prop6(I)) & " + 1 ,
                prop7_" & MS(prop7(I)) & " = prop7_" & MS(prop7(I)) & " + 1 ,
                prop8_" & MS(prop8(I)) & " = prop8_" & MS(prop8(I)) & " + 1 ,
                prop9_" & MS(prop9(I)) & " = prop9_" & MS(prop9(I)) & " + 1 ,
                prop10_" & MS(prop10(I)) & " = prop10_" & MS(prop10(I)) & " + 1 ,
                prop11_" & MS(prop11(I)) & " = prop11_" & MS(prop11(I)) & " + 1;"

                Console.WriteLine("Executing ....")
                Console.WriteLine(SQLquery)

                Dim command As New MySqlCommand(SQLquery, Me.conn)
                command.ExecuteNonQuery()
                Console.WriteLine("Executed ....")
            Catch ex As Exception
                MessageBox.Show("Unknown error occured while adding feedback of page " & (I + 1).ToString & ". Exiting. Error: " & ex.Message)
                'log errors here
                Me.Close()
                Exit Sub
            End Try
        Next

        MessageBox.Show("Feedbacks added successfully.")
        Me.Close()
        SelectTeacher.Show()
    End Sub

    Private Sub FeedbackForm_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Me.teacher_name & " - " & Me.subject_name
        Console.WriteLine("Form loading")
        Console.WriteLine(Me.teacher_name)
        Console.WriteLine(teacher_id)

        TextBox12.Text = current_page.ToString
        TextBox13.Text = total_page.ToString
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
            e.SuppressKeyPress = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Sub SaveValues(pageNumber As Integer)
        'add current properties to the list
        Dim index As Integer = pageNumber - 1
        If TextBox1.Text <> "" Then
            prop1(index) = (TextBox1.Text)
        Else
            prop1(index) = ("0")
        End If

        If TextBox2.Text <> "" Then
            prop2(index) = ((TextBox2.Text))
        Else
            prop2(index) = ("0")
        End If

        If TextBox3.Text <> "" Then
            prop3(index) = ((TextBox3.Text))
        Else
            prop3(index) = ("0")
        End If

        If TextBox4.Text <> "" Then
            prop4(index) = ((TextBox4.Text))
        Else
            prop4(index) = ("0")
        End If

        If TextBox5.Text <> "" Then
            prop5(index) = ((TextBox5.Text))
        Else
            prop5(index) = ("0")
        End If

        If TextBox6.Text <> "" Then
            prop6(index) = ((TextBox6.Text))
        Else
            prop6(index) = ("0")
        End If

        If TextBox7.Text <> "" Then
            prop7(index) = ((TextBox7.Text))
        Else
            prop7(index) = ("0")
        End If

        If TextBox8.Text <> "" Then
            prop8(index) = ((TextBox8.Text))
        Else
            prop8(index) = ("0")
        End If

        If TextBox9.Text <> "" Then
            prop9(index) = ((TextBox9.Text))
        Else
            prop9(index) = ("0")
        End If

        If TextBox10.Text <> "" Then
            prop10(index) = ((TextBox10.Text))
        Else
            prop10(index) = ("0")
        End If

        If TextBox11.Text <> "" Then
            prop11(index) = ((TextBox11.Text))
        Else
            prop11(index) = ("0")
        End If
    End Sub

    Sub GetValues(forPageNo As Integer)
        Dim Index As Integer = forPageNo - 1
        'and get that index values
        TextBox1.Text = prop1(Index)
        TextBox2.Text = prop2(Index)
        TextBox3.Text = prop3(Index)
        TextBox4.Text = prop4(Index)
        TextBox5.Text = prop5(Index)
        TextBox6.Text = prop6(Index)
        TextBox7.Text = prop7(Index)
        TextBox8.Text = prop8(Index)
        TextBox9.Text = prop9(Index)
        TextBox10.Text = prop10(Index)
        TextBox11.Text = prop11(Index)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If current_page > 1 Then
            'first save the current values
            SaveValues(current_page)
            'now lower the page number
            current_page -= 1
            'update the textbox
            TextBox12.Text = current_page
            GetValues(current_page)
        End If
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        'if current page is the last page (i.e total number)
        'then first save the contents at current page
        SaveValues(current_page)

        'increase current page no
        If current_page = total_page Then
            If total_page = 100 Then
                MessageBox.Show("More than 100 entries are not allowed at a time")
                Exit Sub
            End If
            total_page += 1
            current_page += 1

            'then reset the textboxes
            For Each ctrl In TableLayoutPanel3.Controls
                If TypeOf ctrl Is TextBox Then
                    ctrl.ResetText()
                End If
            Next
        Else
            current_page += 1

            'get values
            GetValues(current_page)
        End If

        'update it in the textbox
        TextBox12.Text = current_page.ToString
        TextBox13.Text = total_page.ToString

        'finally send focus to textbox1
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If current_page > 1 Then
            current_page -= 1
            TextBox12.Text = current_page.ToString
            GetValues(current_page)

            total_page = current_page
            TextBox13.Text = total_page.ToString
        Else
            MessageBox.Show("Cannot remove only one entry remaining.")
        End If
    End Sub
End Class