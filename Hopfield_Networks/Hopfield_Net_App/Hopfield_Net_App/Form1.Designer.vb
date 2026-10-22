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
        Me.btnClear = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtOutputPixels = New System.Windows.Forms.TextBox
        Me.rdoLAM = New System.Windows.Forms.RadioButton
        Me.rdoOLAM = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.rdoSimple = New System.Windows.Forms.RadioButton
        Me.rdoComplex = New System.Windows.Forms.RadioButton
        Me.btnHowToUse = New System.Windows.Forms.Button
        Me.HowToUseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.InputPixels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OutputPixels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'InputPixels
        '
        Me.InputPixels.BackColor = System.Drawing.SystemColors.Control
        Me.InputPixels.Location = New System.Drawing.Point(132, 55)
        Me.InputPixels.Name = "InputPixels"
        Me.InputPixels.Size = New System.Drawing.Size(75, 120)
        Me.InputPixels.TabIndex = 41
        Me.InputPixels.TabStop = False
        '
        'OutputPixels
        '
        Me.OutputPixels.BackColor = System.Drawing.SystemColors.Control
        Me.OutputPixels.Location = New System.Drawing.Point(333, 55)
        Me.OutputPixels.Name = "OutputPixels"
        Me.OutputPixels.Size = New System.Drawing.Size(75, 120)
        Me.OutputPixels.TabIndex = 42
        Me.OutputPixels.TabStop = False
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(215, 82)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(112, 24)
        Me.btnRun.TabIndex = 43
        Me.btnRun.Text = "Run One Iteration"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(215, 112)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(112, 24)
        Me.btnClear.TabIndex = 45
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(509, 24)
        Me.MenuStrip1.TabIndex = 48
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HowToUseToolStripMenuItem, Me.InfoToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(121, 27)
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
        Me.txtOutputPixels.Location = New System.Drawing.Point(414, 55)
        Me.txtOutputPixels.Multiline = True
        Me.txtOutputPixels.Name = "txtOutputPixels"
        Me.txtOutputPixels.Size = New System.Drawing.Size(81, 115)
        Me.txtOutputPixels.TabIndex = 50
        '
        'rdoLAM
        '
        Me.rdoLAM.AutoSize = True
        Me.rdoLAM.Location = New System.Drawing.Point(6, 19)
        Me.rdoLAM.Name = "rdoLAM"
        Me.rdoLAM.Size = New System.Drawing.Size(47, 17)
        Me.rdoLAM.TabIndex = 51
        Me.rdoLAM.Text = "LAM"
        Me.rdoLAM.UseVisualStyleBackColor = True
        '
        'rdoOLAM
        '
        Me.rdoOLAM.AutoSize = True
        Me.rdoOLAM.Checked = True
        Me.rdoOLAM.Location = New System.Drawing.Point(6, 45)
        Me.rdoOLAM.Name = "rdoOLAM"
        Me.rdoOLAM.Size = New System.Drawing.Size(55, 17)
        Me.rdoOLAM.TabIndex = 52
        Me.rdoOLAM.TabStop = True
        Me.rdoOLAM.Text = "OLAM"
        Me.rdoOLAM.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.rdoOLAM)
        Me.GroupBox1.Controls.Add(Me.rdoLAM)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 103)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(114, 72)
        Me.GroupBox1.TabIndex = 53
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Memory Method"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(79, 16)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 23)
        Me.Button2.TabIndex = 55
        Me.Button2.Text = "?"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(79, 42)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 23)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "?"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button4)
        Me.GroupBox2.Controls.Add(Me.rdoSimple)
        Me.GroupBox2.Controls.Add(Me.rdoComplex)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(114, 79)
        Me.GroupBox2.TabIndex = 54
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Input Matrix"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(79, 21)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(29, 23)
        Me.Button3.TabIndex = 57
        Me.Button3.Text = "?"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(79, 47)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(29, 23)
        Me.Button4.TabIndex = 56
        Me.Button4.Text = "?"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'rdoSimple
        '
        Me.rdoSimple.AutoSize = True
        Me.rdoSimple.Location = New System.Drawing.Point(6, 24)
        Me.rdoSimple.Name = "rdoSimple"
        Me.rdoSimple.Size = New System.Drawing.Size(56, 17)
        Me.rdoSimple.TabIndex = 56
        Me.rdoSimple.Text = "Simple"
        Me.rdoSimple.UseVisualStyleBackColor = True
        '
        'rdoComplex
        '
        Me.rdoComplex.AutoSize = True
        Me.rdoComplex.Checked = True
        Me.rdoComplex.Location = New System.Drawing.Point(6, 47)
        Me.rdoComplex.Name = "rdoComplex"
        Me.rdoComplex.Size = New System.Drawing.Size(65, 17)
        Me.rdoComplex.TabIndex = 55
        Me.rdoComplex.TabStop = True
        Me.rdoComplex.Text = "Complex"
        Me.rdoComplex.UseVisualStyleBackColor = True
        '
        'btnHowToUse
        '
        Me.btnHowToUse.Location = New System.Drawing.Point(215, 27)
        Me.btnHowToUse.Name = "btnHowToUse"
        Me.btnHowToUse.Size = New System.Drawing.Size(112, 23)
        Me.btnHowToUse.TabIndex = 55
        Me.btnHowToUse.Text = "How To Use"
        Me.btnHowToUse.UseVisualStyleBackColor = True
        '
        'HowToUseToolStripMenuItem
        '
        Me.HowToUseToolStripMenuItem.Name = "HowToUseToolStripMenuItem"
        Me.HowToUseToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.HowToUseToolStripMenuItem.Text = "&How To Use"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.InfoToolStripMenuItem.Text = "&About"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 188)
        Me.Controls.Add(Me.btnHowToUse)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtOutputPixels)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.OutputPixels)
        Me.Controls.Add(Me.InputPixels)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TrackBar1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Simple Associative Memory App"
        CType(Me.InputPixels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OutputPixels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InputPixels As System.Windows.Forms.PictureBox
    Friend WithEvents OutputPixels As System.Windows.Forms.PictureBox
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtOutputPixels As System.Windows.Forms.TextBox
    Friend WithEvents rdoLAM As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOLAM As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents rdoSimple As System.Windows.Forms.RadioButton
    Friend WithEvents rdoComplex As System.Windows.Forms.RadioButton
    Friend WithEvents btnHowToUse As System.Windows.Forms.Button
    Friend WithEvents HowToUseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
