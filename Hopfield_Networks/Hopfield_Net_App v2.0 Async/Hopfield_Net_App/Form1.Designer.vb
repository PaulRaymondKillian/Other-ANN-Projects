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
        Me.components = New System.ComponentModel.Container
        Me.InputPixels = New System.Windows.Forms.PictureBox
        Me.OutputPixels = New System.Windows.Forms.PictureBox
        Me.btnRun = New System.Windows.Forms.Button
        Me.btnStop = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtIterations = New System.Windows.Forms.TextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtOutputPixels = New System.Windows.Forms.TextBox
        Me.lblIterations = New System.Windows.Forms.Label
        CType(Me.InputPixels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OutputPixels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'InputPixels
        '
        Me.InputPixels.BackColor = System.Drawing.SystemColors.Control
        Me.InputPixels.Location = New System.Drawing.Point(23, 68)
        Me.InputPixels.Name = "InputPixels"
        Me.InputPixels.Size = New System.Drawing.Size(75, 120)
        Me.InputPixels.TabIndex = 41
        Me.InputPixels.TabStop = False
        '
        'OutputPixels
        '
        Me.OutputPixels.BackColor = System.Drawing.SystemColors.Control
        Me.OutputPixels.Location = New System.Drawing.Point(237, 68)
        Me.OutputPixels.Name = "OutputPixels"
        Me.OutputPixels.Size = New System.Drawing.Size(75, 120)
        Me.OutputPixels.TabIndex = 42
        Me.OutputPixels.TabStop = False
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(111, 95)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(112, 24)
        Me.btnRun.TabIndex = 43
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(111, 127)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(112, 24)
        Me.btnStop.TabIndex = 44
        Me.btnStop.Text = "Stop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(111, 159)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(112, 24)
        Me.btnClear.TabIndex = 45
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(106, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Iterations:"
        '
        'txtIterations
        '
        Me.txtIterations.Location = New System.Drawing.Point(160, 69)
        Me.txtIterations.Name = "txtIterations"
        Me.txtIterations.Size = New System.Drawing.Size(56, 20)
        Me.txtIterations.TabIndex = 47
        Me.txtIterations.Text = "100"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(419, 24)
        Me.MenuStrip1.TabIndex = 48
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(12, 40)
        Me.TrackBar1.Maximum = 8
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(88, 45)
        Me.TrackBar1.TabIndex = 49
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'txtOutputPixels
        '
        Me.txtOutputPixels.Location = New System.Drawing.Point(318, 68)
        Me.txtOutputPixels.Multiline = True
        Me.txtOutputPixels.Name = "txtOutputPixels"
        Me.txtOutputPixels.Size = New System.Drawing.Size(81, 115)
        Me.txtOutputPixels.TabIndex = 50
        '
        'lblIterations
        '
        Me.lblIterations.AutoSize = True
        Me.lblIterations.Location = New System.Drawing.Point(271, 191)
        Me.lblIterations.Name = "lblIterations"
        Me.lblIterations.Size = New System.Drawing.Size(13, 13)
        Me.lblIterations.TabIndex = 51
        Me.lblIterations.Text = "0"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 208)
        Me.Controls.Add(Me.lblIterations)
        Me.Controls.Add(Me.txtOutputPixels)
        Me.Controls.Add(Me.txtIterations)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.OutputPixels)
        Me.Controls.Add(Me.InputPixels)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TrackBar1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.InputPixels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OutputPixels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InputPixels As System.Windows.Forms.PictureBox
    Friend WithEvents OutputPixels As System.Windows.Forms.PictureBox
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIterations As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtOutputPixels As System.Windows.Forms.TextBox
    Friend WithEvents lblIterations As System.Windows.Forms.Label

End Class
