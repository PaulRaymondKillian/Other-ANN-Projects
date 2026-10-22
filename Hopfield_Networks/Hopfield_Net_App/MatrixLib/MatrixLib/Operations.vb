Imports System.Math

Public Class Operations

    'Returns a Copy of the Matrix
    Public Shared Function CopyMatrix(ByVal oldMatrix As Matrix) As Matrix
        Dim newMatrix As New Matrix(oldMatrix.rows, oldMatrix.cols)

        For i As Integer = 0 To oldMatrix.rows - 1
            For j As Integer = 0 To oldMatrix.cols - 1
                newMatrix.Matrix(i)(j) = oldMatrix.Matrix(i)(j)
            Next
        Next

        Return newMatrix
    End Function

    Private Shared Function CopyDblArray(ByVal arr()() As Double, ByVal rows As Integer, ByVal cols As Integer) As Double()()
        Dim arr1()() As Double
        ReDim arr1(rows - 1)
        For i As Integer = 0 To rows - 1
            Dim arr2(cols - 1) As Double
            arr1(i) = arr2
        Next
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                arr1(row)(col) = arr(row)(col)
            Next
        Next
        Return arr1
    End Function

    'Return a Tranposed Matrix of the Parameter Matrix
    Public Shared Function Transpose(ByVal oldMatrix As Matrix) As Matrix
        'Its Transposed, so switch the cols and rows around for the new Matrix
        Dim newMatrix As New Matrix(oldMatrix.cols, oldMatrix.rows)

        'Input the values from Matrix to newMatrix in Tranposed fashion
        For i As Integer = 0 To oldMatrix.rows - 1
            For j As Integer = 0 To oldMatrix.cols - 1
                newMatrix.Matrix(j)(i) = oldMatrix.Matrix(i)(j)
            Next
        Next

        Return newMatrix
    End Function

    'Returns a Matrix equal to the two Matricies inputted Multiplied in Matrix1*Matrix2 format
    Public Shared Function Multiply(ByVal Matrix1 As Matrix, ByVal Matrix2 As Matrix) As Matrix
        Dim resultMatrix As New Matrix(Matrix1.rows, Matrix2.cols)
        Try
            If Matrix1.cols <> Matrix2.rows Then
                Throw New Exception("Cannot Multiply Matricies: Inputted matrix structures unsuitable for multiplication. Matrix1=" & Matrix1.rows & "x" & Matrix1.cols & ", Matrix2=" & Matrix2.rows & "x" & Matrix2.cols & ". Valid structure is 'MxN * NxP = MxP'")
            End If

            For resultRow As Integer = 0 To resultMatrix.rows - 1
                For resultCol As Integer = 0 To resultMatrix.cols - 1
                    For i As Integer = 0 To Matrix1.cols - 1
                        resultMatrix.Matrix(resultRow)(resultCol) += Matrix1.Matrix(resultRow)(i) * Matrix2.Matrix(i)(resultCol)
                    Next
                Next
            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw ex
        End Try

        Return resultMatrix
    End Function

    Public Shared Function Multiply(ByVal Matrix1 As Matrix, ByVal Scaler As Double) As Matrix
        Dim resultMatrix As New Matrix(Matrix1.rows, Matrix1.cols)

        For i As Integer = 0 To Matrix1.rows - 1
            For j As Integer = 0 To Matrix1.cols - 1
                resultMatrix.Matrix(i)(j) = Matrix1.Matrix(i)(j) * Scaler
            Next
        Next

        Return resultMatrix
    End Function

    Public Shared Function SignFunc(ByVal m As Matrix) As Matrix
        Dim resultMatrix As New Matrix(m.rows, m.cols)

        For i As Integer = 0 To m.rows - 1
            For j As Integer = 0 To m.cols - 1
                If m.Matrix(i)(j) > 0 Then
                    resultMatrix.Matrix(i)(j) = 1
                ElseIf m.Matrix(i)(j) <= 0 Then
                    resultMatrix.Matrix(i)(j) = -1
                End If
            Next
        Next

        Return resultMatrix
    End Function

    Public Shared Function Determinant(ByVal Matrix As Matrix) As Double
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Determinant of a matrix should be (nxn)
        ' Function returns the solution or errors due to
        ' dimensions incompatibility
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim Det As Double
        Dim Mat()() As Double = CopyDblArray(Matrix.Matrix, Matrix.rows, Matrix.cols)
        Dim DArray()() As Double, S As Integer
        Dim k, k1, i, j As Integer
        Dim save, ArrayK As Double
        Dim M1 As String
        Dim Rows, Cols As Integer

        On Error GoTo Error_Handler

        Rows = Matrix.rows
        Cols = Matrix.cols

        If Rows <> Cols Then GoTo Error_Dimension

        S = Rows - 1
        Det = 1
        DArray = Mat.Clone()

        For k = 0 To S
            If DArray(k)(k) = 0 Then
                j = k
                Do While ((j < S) And (DArray(k)(j) = 0))
                    j = j + 1
                Loop
                If DArray(k)(j) = 0 Then
                    Det = 0
                    Exit Function
                Else
                    For i = k To S
                        save = DArray(i)(j)
                        DArray(i)(j) = DArray(i)(k)
                        DArray(i)(k) = save
                    Next i
                End If

                Det = -Det
            End If
            ArrayK = DArray(k)(k)
            Det = Det * ArrayK
            If k < S Then
                k1 = k + 1
                For i = k1 To S
                    For j = k1 To S
                        DArray(i)(j) = DArray(i)(j) - DArray(i)(k) * (DArray(k)(j) / ArrayK)
                    Next j
                Next i
            End If
        Next

        Return Det

        Exit Function

Error_Dimension:
        Err.Raise("5011", , "Matrix should be a square matrix !")

Error_Handler:
        If Err.Number = 5011 Then
            Err.Raise("5011", , "Matrix should be a square matrix !")
        Else
            Err.Raise("5022", , "In order to do this operation values must be assigned to the matrix !!")
        End If
    End Function

    Public Shared Function Determinant2(ByVal Matrix()() As Double, ByVal Norder As Integer) As Double
        Dim k, k1, i, j As Integer
        Dim save As Single
        Dim check As Boolean
        Dim deter As Double
        deter = 1
        For k = 1 To Norder
            If Matrix(k)(k) = 0 Then
                j = k
                Do
                    check = True
                    If Matrix(k)(j) = 0 Then
                        If j = Norder Then
                            deter = 0
                            Exit Function
                        End If
                        check = False
                        j = j + 1
                    End If
                    If Matrix(k)(j) <> 0 Then
                        For i = k To Norder
                            save = Matrix(i)(j)
                            Matrix(i)(j) = Matrix(i)(k)
                            Matrix(i)(k) = save
                        Next i
                        deter = -deter
                    End If
                Loop While check = False
            End If
            deter = deter * Matrix(k)(k)
            If k - Norder < 0 Then
                k1 = k + 1
                For i = k1 To Norder
                    For j = k1 To Norder
                        Matrix(i)(j) = Matrix(i)(j) - (Matrix(i)(k) * Matrix(k)(j) / Matrix(k)(k))
                    Next j
                Next i
            End If
        Next k

        Return deter
    End Function

    Public Shared Function Determinant3(ByVal sourceMatrix(,) As Double) As Double
        ' ----- Calculate the determinant of a matrix.
        Dim result As Double
        Dim pivots As Integer
        Dim count As Integer

        ' ----- Only calculate the determinants of square matrices.
        If (UBound(sourceMatrix, 1) <> UBound(sourceMatrix, 2)) Then
            Throw New Exception( _
                "Determinant only calculated for square matrices.")
        End If
        Dim rank As Integer = UBound(sourceMatrix, 1)

        ' ----- Make a copy of the matrix so we can work inside of it.
        Dim workMatrix(rank, rank) As Double
        Array.Copy(sourceMatrix, workMatrix, sourceMatrix.Length)

        ' ----- Use LU decomposition to form a triangular matrix.
        Dim rowPivots(rank) As Integer
        Dim colPivots(rank) As Integer
        workMatrix = FormLU(workMatrix, rowPivots, colPivots, count)

        ' ----- Get the product at each of the pivot points.
        result = 1
        For pivots = 0 To rank
            result *= workMatrix(rowPivots(pivots), colPivots(pivots))
        Next pivots

        ' ----- Determine the sign of the result using LaPlace's formula.
        result = (-1) ^ count * result
        Return result
    End Function

    Private Shared Function FormLU(ByVal sourceMatrix(,) As Double, _
           ByRef rowPivots() As Integer, ByRef colPivots() As Integer, _
           ByRef rowsAndCols As Integer) As Double(,)
        ' ----- Perform an LU (lower and upper) decomposition of a matrix,
        '       a modified form of Gaussian elimination.
        Dim eachRow As Integer
        Dim eachCol As Integer
        Dim pivot As Integer
        Dim rowIndex As Integer
        Dim colIndex As Integer
        Dim bestRow As Integer
        Dim bestCol As Integer
        Dim rowToPivot As Integer
        Dim colToPivot As Integer
        Dim maxValue As Double
        Dim testValue As Double
        Dim oldMax As Double
        Const Deps As Double = 0.0000000000000001

        ' ----- Determine the size of the array.
        Dim rank As Integer = UBound(sourceMatrix, 1)
        Dim destMatrix(rank, rank) As Double
        Dim rowNorm(rank) As Double
        ReDim rowPivots(rank)
        ReDim colPivots(rank)

        ' ----- Make a copy of the array so we don't mess it up.
        Array.Copy(sourceMatrix, destMatrix, sourceMatrix.Length)

        ' ----- Initialize row and column pivot arrays.
        For eachRow = 0 To rank
            rowPivots(eachRow) = eachRow
            colPivots(eachRow) = eachRow
            For eachCol = 0 To rank
                rowNorm(eachRow) += Math.Abs(destMatrix(eachRow, eachCol))
            Next eachCol
            If (rowNorm(eachRow) = 0) Then
                Throw New Exception("Cannot invert a singular matrix.")
            End If
        Next eachRow

        ' ----- Use Gauss-Jordan elimination on the matrix rows.
        For pivot = 0 To rank - 1
            maxValue = 0
            For eachRow = pivot To rank
                rowIndex = rowPivots(eachRow)
                For eachCol = pivot To rank
                    colIndex = colPivots(eachCol)
                    testValue = Math.Abs(destMatrix(rowIndex, colIndex)) _
                        / rowNorm(rowIndex)
                    If (testValue > maxValue) Then
                        maxValue = testValue
                        bestRow = eachRow
                        bestCol = eachCol
                    End If
                Next eachCol
            Next eachRow

            ' ----- Detect a singular, or very nearly singular, matrix.
            If (maxValue = 0) Then
                Throw New Exception("Singular matrix used for LU.")
            ElseIf (pivot > 1) Then
                If (maxValue < (Deps * oldMax)) Then
                    Throw New Exception("Non-invertible matrix used for LU.")
                End If
            End If
            oldMax = maxValue

            ' ----- Swap row pivot values for the best row.
            If (rowPivots(pivot) <> rowPivots(bestRow)) Then
                rowsAndCols += 1
                Swap(rowPivots(pivot), rowPivots(bestRow))
            End If

            ' ----- Swap column pivot values for the best column.
            If (colPivots(pivot) <> colPivots(bestCol)) Then
                rowsAndCols += 1
                Swap(colPivots(pivot), colPivots(bestCol))
            End If

            ' ----- Work with the current pivot points.
            rowToPivot = rowPivots(pivot)
            colToPivot = colPivots(pivot)

            ' ----- Modify the remaining rows from the pivot points.
            For eachRow = (pivot + 1) To rank
                rowIndex = rowPivots(eachRow)
                destMatrix(rowIndex, colToPivot) = _
                    -destMatrix(rowIndex, colToPivot) / _
                    destMatrix(rowToPivot, colToPivot)
                For eachCol = (pivot + 1) To rank
                    colIndex = colPivots(eachCol)
                    destMatrix(rowIndex, colIndex) += _
                        destMatrix(rowIndex, colToPivot) * _
                        destMatrix(rowToPivot, colIndex)
                Next eachCol
            Next eachRow
        Next pivot

        ' ----- Detect a non-invertible matrix.
        If (destMatrix(rowPivots(rank), colPivots(rank)) = 0) Then
            Throw New Exception("Non-invertible matrix used for LU.")
        ElseIf (Math.Abs(destMatrix(rowPivots(rank), colPivots(rank))) / _
                rowNorm(rowPivots(rank))) < (Deps * oldMax) Then
            Throw New Exception("Non-invertible matrix used for LU.")
        End If

        ' ----- Success. Return the LU triangular matrix.
        Return destMatrix
    End Function

    Private Shared Sub Swap(ByRef firstValue As Integer, ByRef secondValue As Integer)
        ' ----- Reverse the values of two reference integers.
        Dim holdValue As Integer
        holdValue = firstValue
        firstValue = secondValue
        secondValue = holdValue
    End Sub

    Public Shared Function Inverse(ByVal Matrix As Matrix) As Matrix

        Dim Inv As New Matrix(Matrix.rows, Matrix.cols) 'Should be Square Matrix Anyway
        Dim Mat()() As Double = Matrix.Matrix

        Dim AI()() As Double, AIN As Double, AF As Double, _
                    Mat1()() As Double
        Dim LL As Integer, LLM As Integer, L1 As Integer, _
            L2 As Integer, LC As Integer, LCA As Integer, _
            LCB As Integer, i As Integer, j As Integer
        Dim Rows, Cols As Integer

        On Error GoTo Error_Handler

        Rows = Matrix.rows
        Cols = Matrix.cols
        If Rows <> Cols Then GoTo Error_Dimension

        If Determinant(Matrix) = 0 Then GoTo Error_Zero

        LL = Rows - 1
        LLM = Cols - 1
        Mat1 = Mat.Clone()
        'ReDim AI(LL)(LL)
        ReDim AI(LL)
        For index As Integer = 0 To LL
            Dim arr(LL) As Double
            AI(index) = arr
        Next

        For L2 = 0 To LL
            For L1 = 0 To LL
                AI(L1)(L2) = 0
            Next
            AI(L2)(L2) = 1
        Next

        For LC = 0 To LL
            If Abs(Mat1(LC)(LC)) < 0.0000000001 Then
                For LCA = LC + 1 To LL
                    If LCA = LC Then GoTo 1090
                    If Abs(Mat1(LC)(LCA)) > 0.0000000001 Then
                        For LCB = 0 To LL
                            Mat1(LCB)(LC) = Mat1(LCB)(LC) + Mat1(LCB)(LCA)
                            AI(LCB)(LC) = AI(LCB)(LC) + AI(LCB)(LCA)
                        Next
                        GoTo 1100
                    End If
1090:           Next
            End If

1100:
            AIN = 1 / Mat1(LC)(LC)
            For LCA = 0 To LL
                Mat1(LCA)(LC) = AIN * Mat1(LCA)(LC)
                AI(LCA)(LC) = AIN * AI(LCA)(LC)
            Next

            For LCA = 0 To LL
                If LCA = LC Then GoTo 1150
                AF = Mat1(LC)(LCA)
                For LCB = 0 To LL
                    Mat1(LCB)(LCA) = Mat1(LCB)(LCA) - AF * Mat1(LCB)(LC)
                    AI(LCB)(LCA) = AI(LCB)(LCA) - AF * AI(LCB)(LC)
                Next
1150:       Next

        Next

        Inv.Matrix = AI
        Return Inv

Error_Zero:
        Err.Raise("5012", , "Determinent equals zero, inverse can't be found !")

Error_Dimension:
        Err.Raise("5014", , "Matrix should be a square matrix !")

Error_Handler:
        If Err.Number = 5012 Then
            Err.Raise("5012", , "Determinent equals zero, inverse can't be found !")
        ElseIf Err.Number = 5014 Then
            Err.Raise("5014", , "Matrix should be a square matrix !")
        End If


        Return Inv
    End Function

End Class
