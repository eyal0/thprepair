Public Class Form1
    Private gcm_image As System.IO.MemoryStream
    Private t As Thp

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim fo As New OpenFileDialog
        fo.Filter = "GameCube Images (*.gcm,*.iso)|*.gcm;*.iso|All Files|*.*"
        If fo.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtLoadFile.Text = fo.FileName
            Dim br As New System.IO.BinaryReader(New System.IO.FileStream(txtLoadFile.Text, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
            For Each g As GcmFile In GetGcmFiles(br)
                If g.filename.EndsWith(".thp") Then
                    lstThpFiles.Items.Add(g)
                End If
            Next


            t = Thp.Read(br)
            CurrentFrameTrackBar.Minimum = 0
            CurrentFrameTrackBar.Maximum = CInt(t.MyThpHeader.numFrames - 1)
            br.Close()
            t.Repair()
            btnPlay.Enabled = True
            btnPause.Enabled = True
            fp10sTrackBar.Enabled = True
            CurrentFrameTrackBar.Enabled = True
            btnRepairFrame.Enabled = True
        End If
    End Sub

    Dim direction As Integer

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click
        fp10sTrackBar.Value = CInt(t.MyThpHeader.fps * 10)
        fp10sTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub PlayTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayTimer.Tick
        Dim current_frame As Integer = CurrentFrameTrackBar.Value + direction
        If current_frame < 0 Then current_frame = 0
        If current_frame >= t.MyThpHeader.numFrames Then current_frame = CInt(t.MyThpHeader.numFrames - 1)
        CurrentFrameTrackBar.Value = current_frame
        CurrentFrameTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub fp10sTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fp10sTrackBar.Scroll
        If fp10sTrackBar.Value = 0 Then
            PlayTimer.Stop()
        Else
            direction = Math.Sign(fp10sTrackBar.Value)
            PlayTimer.Interval = CInt(Math.Abs(10000 / fp10sTrackBar.Value))
            PlayTimer.Start()
        End If
        lblFrame.Text = "FPS: " & fp10sTrackBar.Value / 10 & " Frame: " & CurrentFrameTrackBar.Value & "/" & t.MyThpHeader.numFrames
    End Sub

    Private Sub CurrentFrameTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrentFrameTrackBar.Scroll
        PictureBox1.Image = t.MyThpFrames(CurrentFrameTrackBar.Value).MyThpVideoFrame.ToImage
        lblFrame.Text = "FPS: " & fp10sTrackBar.Value / 10 & " Frame: " & CurrentFrameTrackBar.Value & "/" & t.MyThpHeader.numFrames
    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPause.Click
        fp10sTrackBar.Value = 0
        fp10sTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub btnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim current_frame As Integer = CurrentFrameTrackBar.Value - 1
        If current_frame < 0 Then current_frame = 0
        If current_frame >= t.MyThpHeader.numFrames Then current_frame = CInt(t.MyThpHeader.numFrames - 1)
        CurrentFrameTrackBar.Value = current_frame
        CurrentFrameTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim current_frame As Integer = CurrentFrameTrackBar.Value + 1
        If current_frame < 0 Then current_frame = 0
        If current_frame >= t.MyThpHeader.numFrames Then current_frame = CInt(t.MyThpHeader.numFrames - 1)
        CurrentFrameTrackBar.Value = current_frame
        CurrentFrameTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub btnRepairFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepairFrame.Click
        t.RepairFrame(CurrentFrameTrackBar.Value)
        CurrentFrameTrackBar_Scroll(Me, Nothing)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim fs As New SaveFileDialog
        fs.Filter = "GameCube Images (*.gcm,*.iso)|*.gcm;*.iso|All Files|*.*"
        If fs.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSaveFile.Text = fs.FileName
            Dim bw As New System.IO.BinaryWriter(New System.IO.FileStream(txtSaveFile.Text, IO.FileMode.Create, IO.FileAccess.Write))
            t.Write(bw)
            bw.Close()
        End If
    End Sub

    Private Sub txtLoadFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLoadFile.TextChanged

    End Sub
End Class
