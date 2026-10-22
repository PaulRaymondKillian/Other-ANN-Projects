Imports System.IO
Imports MatrixLib.Matrix
Imports MatrixLib.Operations

Public Class Form1
    Private DataFolder As String
    Private ImgWidth As Integer
    Private ImgHeight As Integer
    Private CurrentColor As Color
    Private InputMatrix As New MatrixLib.Matrix("testMatrix.txt")
    Private InputMatrixT As MatrixLib.Matrix = Transpose(InputMatrix)
    Private countInputVectors As Integer
    Private WeightMatrix As MatrixLib.Matrix = Multiply(Multiply(InputMatrix, InputMatrixT), 1 / InputMatrix.cols)
    Private inputVector As MatrixLib.Matrix
    Private resultVector As MatrixLib.Matrix
    Dim r As New Random(System.DateTime.Now.Millisecond)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ImgWidth = 5
        ImgHeight = 8
        InputPixels.Image = New Bitmap(InputPixels.Width, InputPixels.Height)
        OutputPixels.Image = New Bitmap(OutputPixels.Width, OutputPixels.Height)
        DrawImageGrid(InputPixels.Image, ImgWidth, ImgHeight)
        DrawImageGrid(OutputPixels.Image, ImgWidth, ImgHeight)
        SetInputPixelsVector(0)
        If InputMatrix.rows < InputMatrix.cols Then
            countInputVectors = InputMatrix.rows - 1
        Else
            countInputVectors = InputMatrix.cols - 1
        End If
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
        If TrackBar1.Value > countInputVectors Then
            TrackBar1.Value = countInputVectors
        End If
        SetInputPixelsVector(TrackBar1.Value)
    End Sub

    Public Sub SetInputPixelsVector(ByVal vectorIndex As Integer)
        Dim m As MatrixLib.Matrix = InputMatrix.RetrieveVector(vectorIndex)
        OutputVectorToInputPixels(m)
        Reset()
    End Sub

    Public Sub OutputVectorToInputPixels(ByVal m As MatrixLib.Matrix)
        If m.cols * m.rows <> ImgHeight * ImgWidth Then
            Throw New Exception("Error - Method: OutputMatrixToInputPixels, Descr: Inputted matrix is invalid for size of InputPixels")
        End If

        For i As Integer = 0 To ImgHeight - 1
            For j As Integer = 0 To ImgWidth - 1
                If m.alignment = vectorAlignment.vectorRows Then
                    If m.Matrix(0, (i * ImgWidth) + j) > 0 Then
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                    Else
                        SetGridCellColor(InputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                    End If
                Else
                    If m.Matrix((i * ImgWidth) + j, 0) > 0 Then
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
                    If m.Matrix(0, (i * ImgWidth) + j) > 0 Then
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.Black)
                        txtOutputPixels.Text = txtOutputPixels.Text & "1"
                    Else
                        SetGridCellColor(OutputPixels.Image, ImgWidth, ImgHeight, j, i, Color.White)
                        txtOutputPixels.Text = txtOutputPixels.Text & "0"
                    End If
                Else
                    If m.Matrix((i * ImgWidth) + j, 0) > 0 Then
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
        CreateVectorFromInputPixels()
        For i As Integer = 0 To Convert.ToInt32(txtIterations.Text)
            AsyncUpdate()
            OutputVectorToOutputPixels(inputVector)
            lblIterations.Text = i
            lblIterations.Refresh()
        Next
        'If inputVector Is Nothing Then
        '    CreateVectorFromInputPixels()
        '    OutputVectorToOutputPixels(inputVector)
        'Else
        '    CreateVectorFromOutputPixels()
        '    RunHopfield()
        'End If
    End Sub

    'Private Sub RunHopfield()
    '    inputVector = SignFunc(inputVector)
    '    resultVector = Multiply(WeightMatrix, inputVector)
    '    resultVector.alignment = vectorAlignment.vectorColumns
    '    OutputVectorToOutputPixels(resultVector)
    'End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Reset()
    End Sub

    Public Sub Reset()
        inputVector = Nothing
        resultVector = Nothing
        ClearOutputPixels()
        txtOutputPixels.Text = ""
    End Sub

    Public Sub AsyncUpdate()
        Dim random As Integer = RandomNumber(WeightMatrix.cols) - 1
        Dim vectorValue As Double = inputVector.Matrix(random, 0)
        Dim Sum As Double = 0

        If inputVector.alignment = vectorAlignment.vectorColumns Then
            For i As Integer = 0 To WeightMatrix.cols - 1
                Sum += vectorValue * WeightMatrix.Matrix(random, i)
            Next
        End If

        If Sum < 0 Then
            vectorValue = -1
        Else
            vectorValue = 1
        End If

        inputVector.Matrix(random, 0) = vectorValue
    End Sub

    Public Function RandomNumber(ByVal MaxNumber As Integer) As Integer
        Return r.Next(1, MaxNumber)
    End Function

End Class
