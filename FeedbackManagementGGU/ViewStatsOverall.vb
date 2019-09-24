﻿Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing

Public Class ViewStatsOverall
    Dim Labels As String() = {"Overall Rating", "Library Facility", "Evaluation Process", "Opportunities for extra co-curricular activities", "Opportunities for leadership", "Safety and Security", "Canteen Facilities", "Washroom Facilities", "Internet Facilities", "Healthcare Facilities", "Support/Coorperation of Non-teaching Staff", "Cleanliness", "Transport Facility", "Ecosystem", "Overall Ambience", "Hostel Facilities"}
    Public Property Conn As MySqlConnection

    Public Property PropCounts As New List(Of Integer)
    Dim x As Integer = 0

    Public Function GetAverage(counts As List(Of Integer))
        Return (5 * counts(0) + 4 * counts(1) + 3 * counts(2) + 0 * counts(3)) / counts.Sum
    End Function

    Public Sub ExecuteAndDraw(sqlQuery As String)
        PropCounts.Clear()

        Dim cmd As New MySqlCommand(sqlQuery, Me.Conn)
        Dim dr As MySqlDataReader
        Try
            dr = cmd.ExecuteReader()
            While dr.Read
                For i As Integer = 0 To (16 * 4 - 1)
                    PropCounts.Add(dr(i))
                    Console.WriteLine(dr(i))
                Next
            End While
            dr.Close()

            Dim ChartCount As Integer = 0
            Dim CumulativeScore As Decimal = 0

            For I As Integer = 0 To TableLayoutPanel1.Controls.Count - 1
                If TypeOf TableLayoutPanel1.Controls.Item(I) Is DataVisualization.Charting.Chart Then
                    Dim Chrt As DataVisualization.Charting.Chart = CType(TableLayoutPanel1.Controls.Item(I), DataVisualization.Charting.Chart)
                    Console.WriteLine("at chart " & Chrt.Text)
                    Chrt.ResetAutoValues()
                    'set all four data points within
                    For k As Integer = 0 To 3
                        Chrt.Series("Series1").Points(k).SetValueY(PropCounts(ChartCount * 4 + k))
                    Next
                    'give average
                    Dim avg As Decimal = GetAverage(PropCounts.GetRange(ChartCount * 4, 4))
                    Chrt.Series("Series1").LegendText = "Avg: " & avg.ToString("N2")
                    CumulativeScore += avg
                    'increment count
                    ChartCount += 1
                End If
            Next

            'calculate cumulative whatnot and put it in label
            Label11.Text = CumulativeScore.ToString(".00")
            Dim percentile As Decimal = CumulativeScore * (100) / 75
            Label7.Text = percentile.ToString("N2")
            If percentile >= 75 Then
                Label9.Text = "A"
            ElseIf percentile > 60 Then
                Label9.Text = "B"
            ElseIf percentile > 50 Then
                Label9.Text = "C"
            Else
                Label9.Text = "Very Poor"
            End If

            Label16.Text = PropCounts.GetRange(0, 4).Sum.ToString()
        Catch ex As Exception
            'clear all chart areas
            For I As Integer = 0 To TableLayoutPanel1.Controls.Count - 1
                If TypeOf TableLayoutPanel1.Controls.Item(I) Is DataVisualization.Charting.Chart Then
                    Dim Chrt As DataVisualization.Charting.Chart = CType(TableLayoutPanel1.Controls.Item(I), DataVisualization.Charting.Chart)

                    Chrt.ResetAutoValues()
                    For k As Integer = 0 To 3
                        Chrt.Series("Series1").Points(k).SetValueY(0)
                    Next
                    Chrt.Series("Series1").LegendText = "Avg: 00.00"
                End If
            Next
            'clear labels
            Label11.Text = "No records."
            Label7.Text = "No records."
            Label9.Text = "No records."
            Label16.Text = "0"
            MessageBox.Show("No records found for particular selection.")
            Console.WriteLine(ex.Message)
        Finally
            dr.Close()
        End Try
    End Sub

    Private Sub ViewStatsOverall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim labelIndex As Integer = 0
        For I As Integer = 0 To TableLayoutPanel1.Controls.Count - 1
            If TypeOf TableLayoutPanel1.Controls.Item(I) Is DataVisualization.Charting.Chart Then
                Dim Chrt As DataVisualization.Charting.Chart = CType(TableLayoutPanel1.Controls.Item(I), DataVisualization.Charting.Chart)
                Console.WriteLine("at chart " & Chrt.Text)
                Chrt.ChartAreas("ChartArea1").AxisX.Title = Labels(labelIndex)
                labelIndex += 1
                Chrt.Series(0).Points(0).Color = Color.Green
                Chrt.Series(0).Points(1).Color = Color.CornflowerBlue
                Chrt.Series(0).Points(2).Color = Color.DarkOrange
                Chrt.Series(0).Points(3).Color = Color.Red
            End If
        Next
        CheckBox1.Checked = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim mPrintBitMap As Bitmap

        ' Copy the form's image into a bitmap.
        mPrintBitMap = New Bitmap(Me.Width, Me.Height)
        Dim lRect As System.Drawing.Rectangle
        lRect.Width = Me.Width
        lRect.Height = Me.Height

        Button2.Visible = False
        If CheckBox1.Checked Then
            ComboBox1.Visible = False
            Label1.Visible = False
            TableLayoutPanel3.Visible = False
            TableLayoutPanel4.Visible = False
        Else
            CheckBox1.Visible = False
            If ComboBox2.Text = "" Then
                TableLayoutPanel3.Visible = False
            End If

            If ComboBox3.Text = "" Then
                TableLayoutPanel4.Visible = False
            End If
        End If
        Me.DrawToBitmap(mPrintBitMap, lRect)

        Dim newBitMap As New Bitmap(mPrintBitMap)
        mPrintBitMap.Dispose()
        mPrintBitMap = Nothing

        If SaveFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            newBitMap.Save(SaveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If

        Button2.Visible = True
        CheckBox1.Visible = True
        ComboBox1.Visible = True
        Label1.Visible = True
        TableLayoutPanel3.Visible = True
        TableLayoutPanel4.Visible = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Console.WriteLine(x.ToString + ". Checkbox1 - checkchanged event triggered :")
        x += 1
        Dim SqlQuery As String = ""
        If CheckBox1.Checked Then
            Console.WriteLine(x.ToString + ". Combobox about to be disabled:")
            x += 1
            ComboBox1.Enabled = False

            Console.WriteLine(x.ToString + ". Combobox 1 disabled:")
            x += 1

            SqlQuery = "select sum(prop1_vg), sum(prop1_g), sum(prop1_s), sum(prop1_b),
                               sum(prop2_vg), sum(prop2_g), sum(prop2_s), sum(prop2_b),
                               sum(prop3_vg), sum(prop3_g), sum(prop3_s), sum(prop3_b),
                               sum(prop4_vg), sum(prop4_g), sum(prop4_s), sum(prop4_b),
                               sum(prop5_vg), sum(prop5_g), sum(prop5_s), sum(prop5_b),
                               sum(prop6_vg), sum(prop6_g), sum(prop6_s), sum(prop6_b),
                               sum(prop7_vg), sum(prop7_g), sum(prop7_s), sum(prop7_b),
                               sum(prop8_vg), sum(prop8_g), sum(prop8_s), sum(prop8_b),
                               sum(prop9_vg), sum(prop9_g), sum(prop9_s), sum(prop9_b),
                               sum(prop10_vg), sum(prop10_g), sum(prop10_s), sum(prop10_b),
                               sum(prop11_vg), sum(prop11_g), sum(prop11_s), sum(prop11_b),
                               sum(prop12_vg), sum(prop12_g), sum(prop12_s), sum(prop12_b),
                               sum(prop13_vg), sum(prop13_g), sum(prop13_s), sum(prop13_b),
                               sum(prop14_vg), sum(prop14_g), sum(prop14_s), sum(prop14_b),
                               sum(prop15_vg), sum(prop15_g), sum(prop15_s), sum(prop15_b),
                               sum(prop16_vg), sum(prop16_g), sum(prop16_s), sum(prop16_b)
                         from overallfeedbacks where course_id in (select course_id from courses where dept_id in (select dept_id from departments where school_id in (select school_id from schools)));"

            ExecuteAndDraw(SqlQuery)

        Else
            ComboBox1.Enabled = True
        End If

        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
    End Sub

    Private Sub ComboBox1_EnabledChanged(sender As Object, e As EventArgs) Handles ComboBox1.EnabledChanged
        Console.WriteLine(x.ToString + ". Combobox enabled change triggered.")
        x += 1
        'School combobox fill
        If ComboBox1.Enabled Then
            Console.WriteLine(x.ToString + ". Combobox found enabled. To be filled with school names and values")
            x += 1
            Dim schoolQuery As String = "select * from schools;"
            Dim da As New MySqlDataAdapter(schoolQuery, Conn)
            Dim dt As New DataTable

            da.Fill(dt)
            ComboBox1.DataSource = dt
            With ComboBox1
                .DisplayMember = "school_name"
                .ValueMember = "school_id"
            End With
            Console.WriteLine(x.ToString + ". Combobox filled")
            x += 1
        End If

        ComboBox1.ResetText()
    End Sub

    Private Sub ComboBox2_EnabledChanged(sender As Object, e As EventArgs) Handles ComboBox2.EnabledChanged
        'fill the departments combobox
        If ComboBox2.Enabled Then
            Try
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
            Catch
                MessageBox.Show("Select School first.")
                ComboBox2.Enabled = False
            End Try
        End If
        ComboBox2.ResetText()
    End Sub

    Private Sub ComboBox3_EnabledChanged(sender As Object, e As EventArgs) Handles ComboBox3.EnabledChanged
        If ComboBox3.Enabled Then
            Try
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
            Catch
                MessageBox.Show("Select Department first.")
                ComboBox3.Enabled = False
            End Try
        End If
        ComboBox3.ResetText()
    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox1.SelectionChangeCommitted
        ComboBox2.Enabled = False
        ComboBox2.Enabled = True
        ComboBox3.Enabled = False

        'school selected
        Dim school_id As Int32 = Convert.ToInt32(ComboBox1.SelectedValue.GetHashCode())
        Console.WriteLine("School_id:" + school_id.ToString)
        Dim sqlQuery As String = "select sum(prop1_vg), sum(prop1_g), sum(prop1_s), sum(prop1_b),
                               sum(prop2_vg), sum(prop2_g), sum(prop2_s), sum(prop2_b),
                               sum(prop3_vg), sum(prop3_g), sum(prop3_s), sum(prop3_b),
                               sum(prop4_vg), sum(prop4_g), sum(prop4_s), sum(prop4_b),
                               sum(prop5_vg), sum(prop5_g), sum(prop5_s), sum(prop5_b),
                               sum(prop6_vg), sum(prop6_g), sum(prop6_s), sum(prop6_b),
                               sum(prop7_vg), sum(prop7_g), sum(prop7_s), sum(prop7_b),
                               sum(prop8_vg), sum(prop8_g), sum(prop8_s), sum(prop8_b),
                               sum(prop9_vg), sum(prop9_g), sum(prop9_s), sum(prop9_b),
                               sum(prop10_vg), sum(prop10_g), sum(prop10_s), sum(prop10_b),
                               sum(prop11_vg), sum(prop11_g), sum(prop11_s), sum(prop11_b),
                               sum(prop12_vg), sum(prop12_g), sum(prop12_s), sum(prop12_b),
                               sum(prop13_vg), sum(prop13_g), sum(prop13_s), sum(prop13_b),
                               sum(prop14_vg), sum(prop14_g), sum(prop14_s), sum(prop14_b),
                               sum(prop15_vg), sum(prop15_g), sum(prop15_s), sum(prop15_b),
                               sum(prop16_vg), sum(prop16_g), sum(prop16_s), sum(prop16_b)
                         from overallfeedbacks where course_id in (select course_id from courses where dept_id in (select dept_id from departments where school_id = " & school_id.ToString() & "));"

        ExecuteAndDraw(sqlQuery)
    End Sub

    Private Sub ComboBox2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox2.SelectionChangeCommitted
        ComboBox3.Enabled = False
        ComboBox3.Enabled = True

        'department selected
        Dim dept_id As Int32 = Convert.ToInt32(ComboBox2.SelectedValue.GetHashCode())
        Dim sqlQuery As String = "select sum(prop1_vg), sum(prop1_g), sum(prop1_s), sum(prop1_b),
                               sum(prop2_vg), sum(prop2_g), sum(prop2_s), sum(prop2_b),
                               sum(prop3_vg), sum(prop3_g), sum(prop3_s), sum(prop3_b),
                               sum(prop4_vg), sum(prop4_g), sum(prop4_s), sum(prop4_b),
                               sum(prop5_vg), sum(prop5_g), sum(prop5_s), sum(prop5_b),
                               sum(prop6_vg), sum(prop6_g), sum(prop6_s), sum(prop6_b),
                               sum(prop7_vg), sum(prop7_g), sum(prop7_s), sum(prop7_b),
                               sum(prop8_vg), sum(prop8_g), sum(prop8_s), sum(prop8_b),
                               sum(prop9_vg), sum(prop9_g), sum(prop9_s), sum(prop9_b),
                               sum(prop10_vg), sum(prop10_g), sum(prop10_s), sum(prop10_b),
                               sum(prop11_vg), sum(prop11_g), sum(prop11_s), sum(prop11_b),
                               sum(prop12_vg), sum(prop12_g), sum(prop12_s), sum(prop12_b),
                               sum(prop13_vg), sum(prop13_g), sum(prop13_s), sum(prop13_b),
                               sum(prop14_vg), sum(prop14_g), sum(prop14_s), sum(prop14_b),
                               sum(prop15_vg), sum(prop15_g), sum(prop15_s), sum(prop15_b),
                               sum(prop16_vg), sum(prop16_g), sum(prop16_s), sum(prop16_b)
                         from overallfeedbacks where course_id in (select course_id from courses where dept_id = " & dept_id.ToString() & ");"

        ExecuteAndDraw(sqlQuery)
    End Sub

    Private Sub ComboBox3_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBox3.SelectionChangeCommitted
        'course selected
        Dim course_id As Int32 = Convert.ToInt32(ComboBox3.SelectedValue.GetHashCode())
        Dim sqlQuery As String = "select prop1_vg, prop1_g, prop1_s, prop1_b,
                               prop2_vg, prop2_g, prop2_s, prop2_b,
                               prop3_vg, prop3_g, prop3_s, prop3_b,
                                prop4_vg, prop4_g, prop4_s, prop4_b,
                                prop5_vg, prop5_g, prop5_s, prop5_b,
                                prop6_vg, prop6_g, prop6_s, prop6_b,
                                prop7_vg, prop7_g, prop7_s, prop7_b,
                                prop8_vg, prop8_g, prop8_s, prop8_b,
                                prop9_vg, prop9_g, prop9_s, prop9_b,
                                prop10_vg, prop10_g, prop10_s, prop10_b,
                                prop11_vg, prop11_g, prop11_s, prop11_b,
                                prop12_vg, prop12_g, prop12_s, prop12_b,
                                prop13_vg, prop13_g, prop13_s, prop13_b,
                                prop14_vg, prop14_g, prop14_s, prop14_b,
                                prop15_vg, prop15_g, prop15_s, prop15_b,
                                prop16_vg, prop16_g, prop16_s, prop16_b
                         from overallfeedbacks where course_id = " & course_id.ToString() & ";"

        ExecuteAndDraw(sqlQuery)
    End Sub
End Class