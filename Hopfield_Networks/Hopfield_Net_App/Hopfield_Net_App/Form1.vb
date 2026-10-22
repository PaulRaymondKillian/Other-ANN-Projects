Imports System.IO
Imports MatrixLib.Matrix
Imports MatrixLib.Operations

Public Class Form1
    Private DataFolder As String
    Private ImgWidth As Integer
    Private ImgHeight As Integer
    Private CurrentColor As Color
    Private InputMatrix1 As MatrixLib.Matrix
    Private InputMatrixT1 As MatrixLib.Matrix
    Private InputMatrix2 As MatrixLib.Matrix
    Private InputMatrixT2 As MatrixLib.Matrix
    Private countInput1Vectors As Integer
    Private countInput2Vectors As Integer
    Private WeightMatrix1LAM As MatrixLib.Matrix
    Private WeightMatrix1OLAM As MatrixLib.Matrix
    Private WeightMatrix2LAM As MatrixLib.Matrix
    Private WeightMatrix2OLAM As MatrixLib.Matrix
    Private inputVector As MatrixLib.Matrix
    Private resultVector As MatrixLib.Matrix

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InitMatricies()
            ImgWidth = 5
            ImgHeight = 8
            InputPixels.Image = New Bitmap(InputPixels.Width, InputPixels.Height)
            OutputPixels.Image = New Bitmap(OutputPixels.Width, OutputPixels.Height)
            DrawImageGrid(InputPixels.Image, ImgWidth, ImgHeight)
            DrawImageGrid(OutputPixels.Image, ImgWidth, ImgHeight)
            SetInputPixelsVector(0)
            If InputMatrix2.rows < InputMatrix2.cols Then
                countInput2Vectors = InputMatrix2.rows - 1
            Else
                countInput2Vectors = InputMatrix2.cols - 1
            End If

            If InputMatrix1.rows < InputMatrix1.cols Then
                countInput1Vectors = InputMatrix1.rows - 1
            Else
                countInput1Vectors = InputMatrix1.cols - 1
            End If
        Catch ex As Exception
            MsgBox("Error Loading Program: " & ex.Message & ", Input matrix files are probably missing or corrupt. Exiting program...")
            Close()
        End Try
    End Sub

    Public Sub InitMatricies()
        Try
            InputMatrix1 = New MatrixLib.Matrix("Matrix1.txt")
            InputMatrixT1 = Transpose(InputMatrix1)
            InputMatrix2 = New MatrixLib.Matrix("Matrix2.txt")
            InputMatrixT2 = Transpose(InputMatrix2)
            WeightMatrix1LAM = Multiply(Multiply(InputMatrix1, InputMatrixT1), 1 / InputMatrix1.cols)
            WeightMatrix1OLAM = Multiply(Multiply(InputMatrix1, Inverse(Multiply(InputMatrixT1, InputMatrix1))), InputMatrixT1)
            WeightMatrix2LAM = Multiply(Multiply(InputMatrix2, InputMatrixT2), 1 / InputMatrix2.cols)
            WeightMatrix2OLAM = Multiply(Multiply(InputMatrix2, Inverse(Multiply(InputMatrixT2, InputMatrix2))), InputMatrixT2)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DrawImageGrid(ByVal Img As Image, ByVal Width As Integer, ByVal Height As Integer)
        Dim UnitWidth As Integer, UnitHeight As Integer
        Dim X As Integer, Y As Integer
        Dim G As Graphics, P As Pen

        UnitWidth = (Img.Width - 1) \ Width
        UnitHeight = (Img.Height - 1) \ Height

        G = CreateGraphics.FromImage(Img)
        P = Pens.LightGray

        G.FillRectangle(Brushes.White, 0, 0, Width * UnitWidth, Height * UnitHeight)
        For X = 0 To Width
            G.DrawLine(P, X * UnitWidth, 0, X * UnitWidth, Height * UnitHeight)
        Next
        For Y = 0 To Height
            G.DrawLine(P, 0, Y * UnitHeight, Width * UnitWidth, Y * UnitHeight)
        Next
    End Sub

    Private Sub SetGridCellColor(ByVal Img As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal Color As Color)
        Dim UnitWidth As Integer, UnitHeight As Integer
        Dim G As Graphics, B As Brush

        UnitWidth = (Img.Width - 1) \ Width
        UnitHeight = (Img.Height - 1) \ Height

        G = CreateGraphics.FromImage(Img)
        B = New SolidBrush(Color)

        G.FillRectangle(B, X * UnitWidth + 1, Y * UnitHeight + 1, UnitWidth - 1, UnitHeight - 1)
    End Sub

    Private Function GetGridCellColor(ByVal Img As Bitmap, ByVal Width As Integer, ByVal Height As Integer, ByVal X As Integer, ByVal Y As Integer) As Color
        Dim UnitWidth As Integer, UnitHeight As Integer

        UnitWidth = (Img.Width - 1) \ Width
        UnitHeight = (Img.Height - 1) \ Height

        Return Img.GetPixel(X * UnitWidth + UnitWidth * 0.5, Y * UnitHeight + UnitHeight * 0.5)
    End Function

    Private Function WhichGridCell(ByVal Img As Bitmap, ByVal Width As Integer, ByVal Height As Integer, ByRef X As Integer, ByRef Y As Integer) As Boolean
        Dim UnitWidth As Integer, UnitHeight As Integer, C As Color

        UnitWidth = Img.Width \ Width
        UnitHeight = Img.Height \ Height

        X = X \ UnitWidth
        Y = Y \ UnitWidth

        If X < 0 Then Return False
        If Y < 0 Then Return False
        If X >= Width Then Return False
        If Y >= Height Then Return False
        Return True
    End Function

    Private Sub InputPixels_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles InputPixels.MouseDown
        Dim X As Integer, Y As Integer
        X = e.X
        Y = e.Y
        If Not WhichGridCell(InputPixels.Image, ImgWidth, ImgHeight, X, Y) Then
            CurrentColor = Color.White
        Else
            CurrentColor = GetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, X, Y)
            If CurrentColor.ToArgb = Color.Black.ToArgb Then
                CurrentColor = Color.White
            Else
                CurrentColor = Color.Black
            End If
            InputPixels_MouseMove(Nothing, e)
        End If
        Reset()
    End Sub

    Private Sub InputPixels_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles InputPixels.MouseMove
        Dim X As Integer, Y As Integer

        If Not e.Button = MouseButtons.Left Then Return

        X = e.X
        Y = e.Y
        If Not WhichGridCell(InputPixels.Image, ImgWidth, ImgHeight, X, Y) Then Return

        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, X, Y, CurrentColor)
        InputPixels.Refresh()
    End Sub

    'Private Sub OutputPixels_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OutputPixels.MouseDown
    '    Dim X As Integer, Y As Integer
    '    X = e.X
    '    Y = e.Y
    '    If Not WhichGridCell(OutputPixels.Image, ImgWidth, ImgHeight, X, Y) Then
    '        CurrentColor = Color.White
    '    Else
    '        CurrentColor = GetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, X, Y)
    '        If CurrentColor.ToArgb = Color.Black.ToArgb Then
    '            CurrentColor = Color.White
    '        Else
    '            CurrentColor = Color.Black
    '        End If
    '        OutputPixels_MouseMove(Nothing, e)
    '    End If
    'End Sub

    'Private Sub OutputPixels_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles OutputPixels.MouseMove
    '    Dim X As Integer, Y As Integer

    '    If Not e.Button = MouseButtons.Left Then Return

    '    X = e.X
    '    Y = e.Y
    '    If Not WhichGridCell(OutputPixels.Image, ImgWidth, ImgHeight, X, Y) Then Return

    '    SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, X, Y, CurrentColor)
    '    OutputPixels.Refresh()
    'End Sub


    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        If rdoComplex.Checked Then
            If TrackBar1.Value > countInput2Vectors Then
                TrackBar1.Value = countInput2Vectors
            End If
        Else
            If TrackBar1.Value > countInput1Vectors Then
                TrackBar1.Value = countInput1Vectors
            End If
        End If

        SetInputPixelsVector(TrackBar1.Value)

    End Sub

    Public Sub SetInputPixelsVector(ByVal vectorIndex As Integer)
        If rdoSimple.Checked Then
            Dim m As MatrixLib.Matrix = InputMatrix1.RetrieveVector(vectorIndex)
            OutputVectorToInputPixels(m)
        Else
            Dim m As MatrixLib.Matrix = InputMatrix2.RetrieveVector(vectorIndex)
            OutputVectorToInputPixels(m)
        End If
        Reset()
    End Sub

    Public Sub OutputVectorToInputPixels(ByVal m As MatrixLib.Matrix)
        If m.cols * m.rows <> ImgHeight * ImgWidth Then
            Throw New Exception("Error - Method: OutputMatrixToInputPixels, Descr: Inputted matrix is invalid for size of InputPixels")
        End If

        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                If m.alignment = vectorAlignment.vectorRows Then
                    If m.Matrix(0)((i * ImgWidth) + j) > 0 Then
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                    Else
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                    End If
                Else
                    If m.Matrix((i * ImgWidth) + j)(0) > 0 Then
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                    Else
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                    End If
                End If
            Next
        Next

        InputPixels.Refresh()

    End Sub

    Public Sub OutputVectorToOutputPixels(ByVal m As MatrixLib.Matrix)
        If m.cols * m.rows <> ImgHeight * ImgWidth Then
            Throw New Exception("Error - Method: OutputMatrixToOutputPixels, Descr: Inputted matrix is invalid for size of InputPixels")
        End If

        txtOutputPixels.Text = ""
        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                If m.alignment = vectorAlignment.vectorRows Then
                    If m.Matrix(0)((i * ImgWidth) + j) > 0 Then
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                        txtOutputPixels.Text = txtOutputPixels.Text & "1"
                    Else
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                        txtOutputPixels.Text = txtOutputPixels.Text & "0"
                    End If
                Else
                    If m.Matrix((i * ImgWidth) + j)(0) > 0 Then
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                        txtOutputPixels.Text = txtOutputPixels.Text & "1"
                    Else
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                        txtOutputPixels.Text = txtOutputPixels.Text & "0"
                    End If
                End If
                If Not (i = ImgHeight - 1 And j = ImgWidth - 1) Then
                    txtOutputPixels.Text = txtOutputPixels.Text & ","
                End If
                If j = ImgWidth - 1 Then
                    txtOutputPixels.Text += vbCrLf
                End If
            Next
        Next
        OutputPixels.Refresh()

    End Sub

    Public Sub ClearOutputPixels()

        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
            Next
        Next

        OutputPixels.Refresh()

    End Sub

    Public Sub ClearInputPixels()

        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
            Next
        Next

        InputPixels.Refresh()

    End Sub

    Public Sub CreateVectorFromInputPixels()
        Dim m As MatrixLib.Matrix
        Dim s As String = "["
        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                CurrentColor = GetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i)
                If CurrentColor.ToArgb = Color.Black.ToArgb Then
                    s += "1"
                Else
                    s += "-1"
                End If

                If i <> ImgHeight - 1 Or j <> ImgWidth - 1 Then
                    s += ","
                End If
            Next
        Next
        s += "]"
        m = New MatrixLib.Matrix(s, vectorAlignment.vectorColumns)
        inputVector = m
    End Sub

    Public Sub CreateVectorFromOutputPixels()
        Dim m As MatrixLib.Matrix
        Dim s As String = "["
        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                CurrentColor = GetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i)
                If CurrentColor.ToArgb = Color.Black.ToArgb Then
                    s += "1"
                Else
                    s += "-1"
                End If

                If i <> ImgHeight - 1 Or j <> ImgWidth - 1 Then
                    s += ","
                End If
            Next
        Next
        s += "]"
        m = New MatrixLib.Matrix(s, vectorAlignment.vectorColumns)
        inputVector = m
    End Sub

    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        If inputVector Is Nothing Then
            CreateVectorFromInputPixels()
            OutputVectorToOutputPixels(inputVector)
        Else
            CreateVectorFromOutputPixels()
            RunHopfield()
        End If
    End Sub

    Private Sub RunHopfield()
        inputVector = SignFunc(inputVector)
        If rdoSimple.Checked Then
            If rdoOLAM.Checked Then
                resultVector = Multiply(WeightMatrix1OLAM, inputVector)
            Else
                resultVector = Multiply(WeightMatrix1LAM, inputVector)
            End If
        Else
            If rdoOLAM.Checked Then
                resultVector = Multiply(WeightMatrix2OLAM, inputVector)
            Else
                resultVector = Multiply(WeightMatrix2LAM, inputVector)
            End If
        End If
        resultVector = SignFunc(resultVector)
        resultVector.alignment = vectorAlignment.vectorColumns
        OutputVectorToOutputPixels(resultVector)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Reset()
        ClearInputPixels()
    End Sub

    Public Sub Reset()
        inputVector = Nothing
        resultVector = Nothing
        ClearOutputPixels()
        txtOutputPixels.Text = ""
    End Sub

    Private Sub rdoOrthogonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoSimple.CheckedChanged
        If rdoSimple.Checked Then
            TrackBar1.Maximum = countInput1Vectors
        Else
            TrackBar1.Maximum = countInput2Vectors
        End If
        SetInputPixelsVector(TrackBar1.Value)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        MsgBox("There are 2 very simple memories the network has to learn in the simple input matrix." & vbCrLf & vbCrLf & "This is a simple set of input matrices (memories) for the network to learn. The Linear Associative Memory (LAM) memory technique is able to learn this set of data fairly well. A noisy version of these matricies can be recovered with decent accuracy using either the LAM or OLAM memory techniques.", MsgBoxStyle.OkOnly, "Information - Simple Input Matrix")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        MsgBox("There are 9 more complex memories the network has to learn in the complex input matrix" & vbCrLf & vbCrLf & "This is a complex set of input matrices (memories) for the network to learn. If you play around with this program, you will notice that while the Optimal Linear Associative Memory (OLAM) technique can recover noisy images, you will also notice that the simple Linear Associative Memory (LAM) technique does not do nearly as well. Although the LAM technique can recover slightly noisy images to their original condition, it is very easy for too much noise to force the network to converge to an undesired memory; these undesired memories are called 'spurious' memories." & vbCrLf & vbCrLf & "Note: The component vectors of this complex set of input data are linearly independant. To utilize our Optimal Linear Associative (OLAM) memory technique, we must be able to create an Inverse of the Outter Product of the Input Matrix with the Tranpose of the Input Matrix. For the Outter Product matrix to be Invertible (Non-Singular), the Determinant must be non-zero. Therefore, the vector components of the matrix must be Linearly Independant. If the vectors are not Linearly Independant then no Inverse exists and the network cannot learn that set of memories.", MsgBoxStyle.OkOnly, "Information - Complex Input Matrix")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox("Linear Associative Memory (LAM)" & vbCrLf & vbCrLf & "We use a set of bi-polar (-1 or 1) column vectors as our Input Vectors (Memories). The set of Input Vectors form an Input Matrix ('X').  Therefore, if we have input column vectors of length N (A memory is a Nx1 Vector), and we have L memories (L<N), then our Input Matrix will be NxL. We will call the Tranpose of the Input Matrix X^t. Let 'L' be the number of input vectors we are using in our Input Matrix. We then create a Weight Matrix ('W'). We calculate the Weight Matrix using the following formula:" & vbCrLf & vbCrLf & "W = 1/L * (I * I^t)" & vbCrLf & vbCrLf & "Then, given a single noisy input vector Y, to use our memory matrix to calculate a less noisy Y', we perform the following calculation: " & vbCrLf & vbCrLf & "Y' = W * Y" & vbCrLf & vbCrLf & "We can then feed Y' back into our network in a recurrent fashion until we arrive at a stable solution. If the input vector was not too noisy, hopefully our solution settles on an accurate memory from our Input Matrix; otherwise the network may settle on a spurious memory or may oscillate between two intermediate memories. To guarentee accurate memory retrieval using the LAM method, the input vectors must be Orthgonal (inner product = be zero).", MsgBoxStyle.OkOnly, "Information - Linear Associative Memory")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MsgBox("Optimal Linear Associative Memory (OLAM)" & vbCrLf & vbCrLf & "We use a set of bi-polar (-1 or 1) column vectors as our Input Vectors (Memories). The set of Input Vectors form an Input Matrix ('X'). Therefore, if we have input column vectors of length N (A memory is a Nx1 Vector), and we have L memories (L<N), then our Input Matrix will be NxL. We will call the Tranpose of the Input Matrix X^t, and the Inverse of a Matrix to be X^-1. We then create a Weight Matrix ('W'). We calculate the Weight Matrix using the following formula:" & vbCrLf & vbCrLf & "W = X * (X^t * X)^-1 * X^t" & vbCrLf & vbCrLf & "Then, given a single noisy input vector Y, to use our memory matrix to calculate a less noisy Y', we perform the following calculation: " & vbCrLf & vbCrLf & "Y' = W * Y" & vbCrLf & vbCrLf & "We can then feed Y' back into our network in a recurrent fashion until we arrive at a stable solution. If the input vector was not too noisy, hopefully our solution settles on an accurate memory from our Input Matrix; otherwise the network may settle on a spurious memory or may oscillate between two intermediate memories. To guarentee accurate memory retrieval using the OLAM method, the input vectors must be Linearly Independant.", MsgBoxStyle.OkOnly, "Information - Optimal Linear Associative Memory")
    End Sub

    Private Sub btnHowToUse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHowToUse.Click
        HowToUse()
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        MsgBox("Simple Associative Memory App" & vbCrLf & vbCrLf & "Created by Paul Killian (2010)" & vbCrLf & vbCrLf & "Contact: Mutiny34@gmail.com" & vbCrLf & vbCrLf & "About The Program: This is a simple program that illustrates simple neural network memory techniques. However, the implementation actually uses matricies rather than an actual neural network framework. I also created the matrix library that was used by this program and is probably also available whereever you found this program. This program only showcases Linear Associative Memory techniques and does not include any of the more complex dynamic methods." & vbCrLf & vbCrLf & "Note: The program uses the files Matrix1.txt and Matrix2.txt to retrieve the input vectors. Although I didnt include software to easily create your own matrix vectors for use with this program, if you are so inclined you can simply open and edit the matrix files to include any memory vectors you want to try. Keep in mind that although the memories are column vectors, they are stored in the textfile as a row and converted into a column vector by the matrix library.", MsgBoxStyle.OkOnly, "About This Awesome Program")
    End Sub

    Private Sub HowToUseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HowToUseToolStripMenuItem.Click
        HowToUse()
    End Sub

    Public Sub HowToUse()
        MsgBox("Use the Trackbar to select a memory from the Input Matrix. Then, click on the left-hand set of pixels to create a 'noisy' input vector to feed into the network. Click 'Run 1 Iteration' to run that input vector through the network. Some noisy images may require multiple passes through the network to recover the full memory." & vbCrLf & vbCrLf & "Note: An image that has too much noise may stabalize on a 'spurious' memory, or even a memory you didnt even intend to recover. Play around with the program and get a better understanding of how it recovers memories, how well it can recover memories, and the limits of memory recovery. Remember, its all just math! Awesome, Awesome Math!", MsgBoxStyle.OkOnly, "How To Use")
    End Sub
End Class
