Imports System.Text.StringBuilder
Imports System.IO

Public Class Matrix
    Public Matrix()() As Double
    Public rows As Integer
    Public cols As Integer
    Public alignment As vectorAlignment

    Public Enum vectorAlignment
        vectorRows = 0
        vectorColumns = 1
    End Enum

    Public Sub New(ByVal elements As String, ByVal _alignment As vectorAlignment)

        AssembleMatrix(elements)
        alignment = _alignment

        'If the vector input string is in row format, we do not need to do anything special. However, if the vector
        ''input string is in column format, we need to create the matrix and then transpose it to get the same affect.
        If alignment = vectorAlignment.vectorColumns Then
            'Inputted vector string is in column format
            TranposeMatrix()
        End If
    End Sub

    Public Sub New(ByVal Vectors() As Matrix, ByVal Length As Integer)
        'Verify all matrices are of the same alignment
        For i As Integer = 0 To Vectors.Count - 2
            If Not Vectors(i).alignment = Vectors(i + 1).alignment Then
                Throw New Exception("Error - Creating New Matrix, not all vectors inputted are of the same alignment.")
            End If
        Next

        'Verify all vectors inputted are indeed vectors and not matricies
        'For i As Integer = 0 To Vectors.Count - 1
        '    If Vectors(i).alignment = vectorAlignment.vectorRows Then
        '        If Vectors(i).rows > 1 Then
        '            Throw New Exception("Error - Creating New Matrix, inputted vectors are not all vectors - multi-line matrix found")
        '        End If
        '    Else
        '        If Vectors(i).cols > 1 Then
        '            Throw New Exception("Error - Creating New Matrix, inputted vectors are not all vectors - multi-line matrix found")
        '        End If
        '    End If
        'Next

        'For now, all matricies should be in column format
        ''Prep Matrix
        Dim newMatrix(Length - 1)() As Double
        For i As Integer = 0 To newMatrix.Count - 1
            Dim arr(Vectors.Count - 1) As Double
            newMatrix(i) = arr
        Next

        'Populate Matrix with Inputted Vectors

        For j As Integer = 0 To Length - 1
            For i As Integer = 0 To Vectors.Count - 1
                If j < Vectors(i).Matrix.Count Then
                    newMatrix(j)(i) = Vectors(i).Matrix(j)(0)
                Else
                    newMatrix(j)(i) = 0
                    'newMatrix(j)(i) = -1
                End If
            Next
        Next

        cols = newMatrix(0).Count
        rows = newMatrix.Count
        Matrix = newMatrix

    End Sub

    Public Sub New(ByVal filePath As String)
        AssembleMatrixFromFile(filePath)
    End Sub

    Public Sub New(ByVal _rows As Integer, ByVal _cols As Integer)
        rows = _rows
        cols = _cols
        'ReDim Matrix(rows - 1)(cols - 1)
        PrepMatrix()
        'Set all values by default to zero
        For i As Integer = 0 To rows - 1
            For j As Integer = 0 To cols - 1
                Matrix(i)(j) = 0
            Next
        Next
    End Sub

    Private Sub AssembleMatrix(ByVal elements As String)
        Try
            'Separate the vectors of the matrix
            Dim vectors() = Split(elements, "],[")
            rows = vectors.Length

            'Clean up the vector input string to usable vectors
            vectors(0) = vectors(0).Substring(1)
            vectors(vectors.Length - 1) = vectors(vectors.Length - 1).Substring(0, vectors(vectors.Length - 1).Length - 1)

            'Verify vectors are valid
            If Not ValidVectorSpace(vectors) Then
                Throw New Exception("Unable to Create Matrix: Invalid Vectors Inputted to Matrix")
            End If

            'Separate the elements of the vectors
            Dim vectorArray(vectors.Length - 1)() As String
            For i As Integer = 0 To vectors.Length - 1
                vectorArray(i) = Split(vectors(i), ","c)
            Next

            If Not ValidVectorArray(vectorArray) Then
                Throw New Exception("Unable to Create Matrix: Invalid Vector Sizes Inputted to Matrix")
            End If

            CreateMatrix(vectorArray)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw ex
        End Try

    End Sub

    Public Sub AssembleMatrixFromFile(ByVal filePath As String)
        Try
            Dim sr As StreamReader
            Dim elements As New Text.StringBuilder("")
            sr = File.OpenText(filePath)

            If sr.ReadLine = "columns" Then
                alignment = vectorAlignment.vectorColumns
            Else
                alignment = vectorAlignment.vectorRows
            End If

            Do Until sr.EndOfStream
                elements.Append(sr.ReadLine)
                If Not sr.EndOfStream Then
                    elements.Append(",")
                End If
            Loop

            AssembleMatrix(elements.ToString)

            'If the vector input string is in row format, we do not need to do anything special. However, if the vector
            ''input string is in column format, we need to create the matrix and then transpose it to get the same affect.
            If alignment = vectorAlignment.vectorColumns Then
                'Inputted vector string is in column format
                TranposeMatrix()
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw ex
        End Try
    End Sub


    Private Function ValidVectorSpace(ByVal vectors()) As Boolean
        Dim valid As Boolean = True

        'Verify there are not extra bracket characters (extra brackets mean incorrectly structured matrix parameter string)
        For i As Integer = 0 To vectors.Length - 1
            If InStr(vectors(i), "]") <> 0 Or InStr(vectors(i), "[") <> 0 Then
                valid = False
            End If
        Next

        'Verify there are only numbers and commas in the vector-strings
        For i As Integer = 0 To vectors.Length - 1
            For j As Integer = 0 To vectors(i).length - 1
                If vectors(i).Chars(j) <> ","c And vectors(i).Chars(j) <> "-"c And Not IsNumeric(vectors(i).Chars(j)) Then
                    valid = False
                End If
            Next
        Next


        Return valid
    End Function

    Public Function ValidVectorArray(ByVal vectorArray()()) As Boolean
        Dim valid As Boolean = True

        cols = vectorArray(0).Length

        For i As Integer = 1 To vectorArray.Length - 1
            If vectorArray(i).Length <> cols Then
                valid = False
            End If
        Next

        Return valid
    End Function

    Private Sub CreateMatrix(ByVal vectorArray()())
        'Dim Matrix(rows - 1)(cols - 1)
        PrepMatrix()
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                Matrix(row)(col) = vectorArray(row)(col)
            Next
        Next
    End Sub

    Public Sub PrepMatrix()
        ReDim Matrix(rows - 1)
        For i As Integer = 0 To rows - 1
            Dim arr(cols - 1) As Double
            Matrix(i) = arr
        Next
    End Sub

    Public Function OutoutRow(ByVal row As Integer, ByVal includeSpacer As Boolean) As String
        Dim charCountSpacer As Integer
        Dim s As New Text.StringBuilder("[")
        For i As Integer = 0 To cols - 1
            'Used to properly align the outputted columns (Terribly inefficient but works well enough for now)
            charCountSpacer = largestNumCharCount(i, includeSpacer)

            s.Append(Matrix(row)(i).ToString.PadLeft(charCountSpacer, " "))

            If i <> cols - 1 Then
                s.Append(",")
            Else
                s.Append("]")
            End If
        Next

        Return s.ToString
    End Function

    Public Function OutputMatrix() As String
        Dim s As String = ""
        For row As Integer = 0 To rows - 1
            s = s & "["
            For col As Integer = 0 To cols - 1
                s = s & Matrix(row)(col)
                If col <> cols - 1 Then
                    s = s & ","
                End If
            Next
            s = s & "]" & vbCrLf
        Next
        Return s
    End Function

    Public Function largestNumCharCount(ByVal col As Integer, ByVal includeSpacer As Boolean) As Integer
        Dim count As Integer = 0
        Dim currentString As String

        For i As Integer = 0 To rows - 1
            currentString = Matrix(i)(col).ToString
            If currentString.Length > count Then
                count = currentString.Length
            End If
        Next

        If includeSpacer Then
            Return count + 1
        Else
            Return count
        End If
    End Function

    Private Sub TranposeMatrix()
        Dim newMatrix()() As Double
        'ReDim newMatrix(cols - 1)(rows - 1)
        ReDim newMatrix(cols - 1)
        For i As Integer = 0 To cols - 1
            Dim arr(rows - 1) As Double
            newMatrix(i) = arr
        Next

        For i As Integer = 0 To rows - 1
            For j As Integer = 0 To cols - 1
                newMatrix(j)(i) = Matrix(i)(j)
            Next
        Next
        Matrix = newMatrix
        Dim temp As Integer
        temp = rows
        rows = cols
        cols = temp
    End Sub

    Public Function RetrieveVector(ByVal vectorIndex) As Matrix
        Dim vector As Matrix
        Dim s As String
        If alignment = vectorAlignment.vectorRows Then
            If vectorIndex > (rows - 1) Or vectorIndex < 0 Then
                Throw New Exception("Error - Method: RetrieveVector, Desc: Vector Index inputted is invalid/out-of-bounds")
            End If
            s = "["
            For i As Integer = 0 To cols - 1
                s += Matrix(vectorIndex)(i).ToString
                If i <> cols - 1 Then
                    s += ","
                End If
            Next
            s += "]"
            vector = New Matrix(s, vectorAlignment.vectorRows)
        Else
            If vectorIndex > (cols - 1) Or vectorIndex < 0 Then
                Throw New Exception("Error - Method: RetrieveVector, Desc: Vector Index inputted is invalid/out-of-bounds")
            End If
            s = "["
            For i As Integer = 0 To rows - 1
                s += Matrix(i)(vectorIndex).ToString
                If i <> rows - 1 Then
                    s += ","
                End If
            Next
            s += "]"
            vector = New Matrix(s, vectorAlignment.vectorColumns)
        End If

        Return vector
    End Function

End Class
