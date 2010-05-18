<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtLoadFile = New System.Windows.Forms.TextBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.PlayTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnPause = New System.Windows.Forms.Button()
        Me.fp10sTrackBar = New System.Windows.Forms.TrackBar()
        Me.lblFrame = New System.Windows.Forms.Label()
        Me.CurrentFrameTrackBar = New System.Windows.Forms.TrackBar()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnRepairFrame = New System.Windows.Forms.Button()
        Me.txtSaveFile = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fp10sTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CurrentFrameTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtLoadFile
        '
        Me.txtLoadFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLoadFile.Location = New System.Drawing.Point(12, 12)
        Me.txtLoadFile.Name = "txtLoadFile"
        Me.txtLoadFile.Size = New System.Drawing.Size(728, 20)
        Me.txtLoadFile.TabIndex = 0
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(746, 12)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(52, 23)
        Me.btnLoad.TabIndex = 2
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 109)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(786, 484)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'btnPlay
        '
        Me.btnPlay.Enabled = False
        Me.btnPlay.Location = New System.Drawing.Point(12, 51)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(75, 23)
        Me.btnPlay.TabIndex = 5
        Me.btnPlay.Text = "Play"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'PlayTimer
        '
        '
        'btnPause
        '
        Me.btnPause.Enabled = False
        Me.btnPause.Location = New System.Drawing.Point(93, 51)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(75, 23)
        Me.btnPause.TabIndex = 6
        Me.btnPause.Text = "Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'fp10sTrackBar
        '
        Me.fp10sTrackBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.fp10sTrackBar.AutoSize = False
        Me.fp10sTrackBar.Enabled = False
        Me.fp10sTrackBar.Location = New System.Drawing.Point(174, 51)
        Me.fp10sTrackBar.Maximum = 1000
        Me.fp10sTrackBar.Minimum = -1000
        Me.fp10sTrackBar.Name = "fp10sTrackBar"
        Me.fp10sTrackBar.Size = New System.Drawing.Size(535, 23)
        Me.fp10sTrackBar.TabIndex = 7
        Me.fp10sTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'lblFrame
        '
        Me.lblFrame.AutoSize = True
        Me.lblFrame.Location = New System.Drawing.Point(12, 35)
        Me.lblFrame.Name = "lblFrame"
        Me.lblFrame.Size = New System.Drawing.Size(39, 13)
        Me.lblFrame.TabIndex = 8
        Me.lblFrame.Text = "Label1"
        '
        'CurrentFrameTrackBar
        '
        Me.CurrentFrameTrackBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrentFrameTrackBar.AutoSize = False
        Me.CurrentFrameTrackBar.Enabled = False
        Me.CurrentFrameTrackBar.Location = New System.Drawing.Point(94, 80)
        Me.CurrentFrameTrackBar.Maximum = 1000
        Me.CurrentFrameTrackBar.Minimum = -1000
        Me.CurrentFrameTrackBar.Name = "CurrentFrameTrackBar"
        Me.CurrentFrameTrackBar.Size = New System.Drawing.Size(704, 23)
        Me.CurrentFrameTrackBar.TabIndex = 9
        Me.CurrentFrameTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnPrev
        '
        Me.btnPrev.Location = New System.Drawing.Point(12, 80)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(35, 23)
        Me.btnPrev.TabIndex = 10
        Me.btnPrev.Text = "<"
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(53, 80)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(35, 23)
        Me.btnNext.TabIndex = 11
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnRepairFrame
        '
        Me.btnRepairFrame.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRepairFrame.Enabled = False
        Me.btnRepairFrame.Location = New System.Drawing.Point(715, 51)
        Me.btnRepairFrame.Name = "btnRepairFrame"
        Me.btnRepairFrame.Size = New System.Drawing.Size(83, 23)
        Me.btnRepairFrame.TabIndex = 12
        Me.btnRepairFrame.Text = "Repair Frame"
        Me.btnRepairFrame.UseVisualStyleBackColor = True
        '
        'txtSaveFile
        '
        Me.txtSaveFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSaveFile.Location = New System.Drawing.Point(12, 599)
        Me.txtSaveFile.Name = "txtSaveFile"
        Me.txtSaveFile.Size = New System.Drawing.Size(728, 20)
        Me.txtSaveFile.TabIndex = 13
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(746, 599)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(52, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 631)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtSaveFile)
        Me.Controls.Add(Me.btnRepairFrame)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.CurrentFrameTrackBar)
        Me.Controls.Add(Me.lblFrame)
        Me.Controls.Add(Me.fp10sTrackBar)
        Me.Controls.Add(Me.btnPause)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.txtLoadFile)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fp10sTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CurrentFrameTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtLoadFile As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents PlayTimer As System.Windows.Forms.Timer
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents fp10sTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents lblFrame As System.Windows.Forms.Label
    Friend WithEvents CurrentFrameTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnRepairFrame As System.Windows.Forms.Button
    Friend WithEvents txtSaveFile As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button

End Class
