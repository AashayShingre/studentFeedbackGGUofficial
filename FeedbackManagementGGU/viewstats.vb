Public Class viewstats
    Private Sub viewstats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.Series("Voice modulation").Points.AddXY("Very Good", 26)
        Chart1.Series("Voice modulation").Points.AddXY("Good", 32)
        Chart1.Series("Voice modulation").Points.AddXY("Satisfactory", 13)
        Chart1.Series("Voice modulation").Points.AddXY("Bad", 5)

        Chart1.Series("Speed of Delivery").Points.AddXY("Very Good", 15)
        Chart1.Series("Speed of Delivery").Points.AddXY("Good", 21)
        Chart1.Series("Speed of Delivery").Points.AddXY("Satisfactory", 20)
        Chart1.Series("Speed of Delivery").Points.AddXY("Bad", 10)
    End Sub
End Class