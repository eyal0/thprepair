Public Class Form1

    Private Sub btnFileSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSelect.Click
        Dim fo As New OpenFileDialog
        fo.Filter = "THP Movies|*.thp|All Files|*.*"
        If fo.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = fo.FileName
        End If
    End Sub

    Private t As Thp

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim br As New System.IO.BinaryReader(New System.IO.FileStream(TextBox1.Text, IO.FileMode.Open, IO.FileAccess.Read))
        t = Thp.Read(br)
        br.Close()
    End Sub

    Private Sub btnRepair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepair.Click
        t.Repair()
    End Sub

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
        For i As Integer = 0 To CInt(t.MyThpHeader.numFrames - 1)
            PictureBox1.Image = t.MyThpFrames(i).MyThpVideoFrame.image
            Application.DoEvents()
            Threading.Thread.Sleep(CInt(1000 / t.MyThpHeader.fps))
        Next
    End Sub
End Class
