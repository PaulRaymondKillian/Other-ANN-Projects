<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGame
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
        Me.r1c1 = New System.Windows.Forms.Button()
        Me.r1c2 = New System.Windows.Forms.Button()
        Me.r1c3 = New System.Windows.Forms.Button()
        Me.r2c1 = New System.Windows.Forms.Button()
        Me.r2c2 = New System.Windows.Forms.Button()
        Me.r2c3 = New System.Windows.Forms.Button()
        Me.r3c1 = New System.Windows.Forms.Button()
        Me.r3c2 = New System.Windows.Forms.Button()
        Me.r3c3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPlayer1Score = New System.Windows.Forms.Label()
        Me.lblPlayer2Score = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCurrentPlayer = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbAIBot = New System.Windows.Forms.CheckBox()
        Me.btnLoadANN = New System.Windows.Forms.Button()
        Me.ofdLoadANN = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'r1c1
        '
        Me.r1c1.Location = New System.Drawing.Point(13, 19)
        Me.r1c1.Name = "r1c1"
        Me.r1c1.Size = New System.Drawing.Size(27, 23)
        Me.r1c1.TabIndex = 0
        Me.r1c1.Text = "X"
        Me.r1c1.UseVisualStyleBackColor = True
        '
        'r1c2
        '
        Me.r1c2.Location = New System.Drawing.Point(46, 19)
        Me.r1c2.Name = "r1c2"
        Me.r1c2.Size = New System.Drawing.Size(27, 23)
        Me.r1c2.TabIndex = 1
        Me.r1c2.Text = "X"
        Me.r1c2.UseVisualStyleBackColor = True
        '
        'r1c3
        '
        Me.r1c3.Location = New System.Drawing.Point(79, 19)
        Me.r1c3.Name = "r1c3"
        Me.r1c3.Size = New System.Drawing.Size(27, 23)
        Me.r1c3.TabIndex = 2
        Me.r1c3.Text = "X"
        Me.r1c3.UseVisualStyleBackColor = True
        '
        'r2c1
        '
        Me.r2c1.Location = New System.Drawing.Point(13, 48)
        Me.r2c1.Name = "r2c1"
        Me.r2c1.Size = New System.Drawing.Size(27, 23)
        Me.r2c1.TabIndex = 3
        Me.r2c1.Text = "X"
        Me.r2c1.UseVisualStyleBackColor = True
        '
        'r2c2
        '
        Me.r2c2.Location = New System.Drawing.Point(46, 48)
        Me.r2c2.Name = "r2c2"
        Me.r2c2.Size = New System.Drawing.Size(27, 23)
        Me.r2c2.TabIndex = 4
        Me.r2c2.Text = "X"
        Me.r2c2.UseVisualStyleBackColor = True
        '
        'r2c3
        '
        Me.r2c3.Location = New System.Drawing.Point(79, 48)
        Me.r2c3.Name = "r2c3"
        Me.r2c3.Size = New System.Drawing.Size(27, 23)
        Me.r2c3.TabIndex = 5
        Me.r2c3.Text = "X"
        Me.r2c3.UseVisualStyleBackColor = True
        '
        'r3c1
        '
        Me.r3c1.Location = New System.Drawing.Point(13, 77)
        Me.r3c1.Name = "r3c1"
        Me.r3c1.Size = New System.Drawing.Size(27, 23)
        Me.r3c1.TabIndex = 6
        Me.r3c1.Text = "X"
        Me.r3c1.UseVisualStyleBackColor = True
        '
        'r3c2
        '
        Me.r3c2.Location = New System.Drawing.Point(46, 77)
        Me.r3c2.Name = "r3c2"
        Me.r3c2.Size = New System.Drawing.Size(27, 23)
        Me.r3c2.TabIndex = 7
        Me.r3c2.Text = "X"
        Me.r3c2.UseVisualStyleBackColor = True
        '
        'r3c3
        '
        Me.r3c3.Location = New System.Drawing.Point(79, 77)
        Me.r3c3.Name = "r3c3"
        Me.r3c3.Size = New System.Drawing.Size(27, 23)
        Me.r3c3.TabIndex = 8
        Me.r3c3.Text = "X"
        Me.r3c3.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Player 1:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(89, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Player 2:"
        '
        'lblPlayer1Score
        '
        Me.lblPlayer1Score.AutoSize = True
        Me.lblPlayer1Score.Location = New System.Drawing.Point(65, 9)
        Me.lblPlayer1Score.Name = "lblPlayer1Score"
        Me.lblPlayer1Score.Size = New System.Drawing.Size(13, 13)
        Me.lblPlayer1Score.TabIndex = 11
        Me.lblPlayer1Score.Text = "0"
        '
        'lblPlayer2Score
        '
        Me.lblPlayer2Score.AutoSize = True
        Me.lblPlayer2Score.Location = New System.Drawing.Point(143, 9)
        Me.lblPlayer2Score.Name = "lblPlayer2Score"
        Me.lblPlayer2Score.Size = New System.Drawing.Size(13, 13)
        Me.lblPlayer2Score.TabIndex = 12
        Me.lblPlayer2Score.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Current Player:"
        '
        'lblCurrentPlayer
        '
        Me.lblCurrentPlayer.AutoSize = True
        Me.lblCurrentPlayer.ForeColor = System.Drawing.Color.Red
        Me.lblCurrentPlayer.Location = New System.Drawing.Point(117, 51)
        Me.lblCurrentPlayer.Name = "lblCurrentPlayer"
        Me.lblCurrentPlayer.Size = New System.Drawing.Size(13, 13)
        Me.lblCurrentPlayer.TabIndex = 14
        Me.lblCurrentPlayer.Text = "0"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.r1c1)
        Me.GroupBox1.Controls.Add(Me.r1c2)
        Me.GroupBox1.Controls.Add(Me.r1c3)
        Me.GroupBox1.Controls.Add(Me.r2c1)
        Me.GroupBox1.Controls.Add(Me.r2c2)
        Me.GroupBox1.Controls.Add(Me.r2c3)
        Me.GroupBox1.Controls.Add(Me.r3c1)
        Me.GroupBox1.Controls.Add(Me.r3c3)
        Me.GroupBox1.Controls.Add(Me.r3c2)
        Me.GroupBox1.Location = New System.Drawing.Point(25, 67)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(122, 111)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Gameboard"
        '
        'cbAIBot
        '
        Me.cbAIBot.AutoSize = True
        Me.cbAIBot.Checked = True
        Me.cbAIBot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbAIBot.Location = New System.Drawing.Point(92, 25)
        Me.cbAIBot.Name = "cbAIBot"
        Me.cbAIBot.Size = New System.Drawing.Size(55, 17)
        Me.cbAIBot.TabIndex = 16
        Me.cbAIBot.Text = "AI Bot"
        Me.cbAIBot.UseVisualStyleBackColor = True
        '
        'btnLoadANN
        '
        Me.btnLoadANN.Location = New System.Drawing.Point(38, 184)
        Me.btnLoadANN.Name = "btnLoadANN"
        Me.btnLoadANN.Size = New System.Drawing.Size(92, 23)
        Me.btnLoadANN.TabIndex = 17
        Me.btnLoadANN.Text = "Load ANN"
        Me.btnLoadANN.UseVisualStyleBackColor = True
        '
        'ofdLoadANN
        '
        Me.ofdLoadANN.Filter = "ANN Files|*.ANN"
        '
        'frmGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(172, 216)
        Me.Controls.Add(Me.btnLoadANN)
        Me.Controls.Add(Me.cbAIBot)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblCurrentPlayer)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblPlayer2Score)
        Me.Controls.Add(Me.lblPlayer1Score)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmGame"
        Me.Text = "Tic Tac Toe Game"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents r1c1 As System.Windows.Forms.Button
    Friend WithEvents r1c2 As System.Windows.Forms.Button
    Friend WithEvents r1c3 As System.Windows.Forms.Button
    Friend WithEvents r2c1 As System.Windows.Forms.Button
    Friend WithEvents r2c2 As System.Windows.Forms.Button
    Friend WithEvents r2c3 As System.Windows.Forms.Button
    Friend WithEvents r3c1 As System.Windows.Forms.Button
    Friend WithEvents r3c2 As System.Windows.Forms.Button
    Friend WithEvents r3c3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPlayer1Score As System.Windows.Forms.Label
    Friend WithEvents lblPlayer2Score As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentPlayer As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbAIBot As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadANN As System.Windows.Forms.Button
    Friend WithEvents ofdLoadANN As System.Windows.Forms.OpenFileDialog

End Class
