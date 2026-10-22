Imports MatrixLib.Matrix
Imports MatrixLib.Operations

Module Module1

    Sub Main()

        'Dim a As New MatrixLib.Matrix("[1,2,3,4,5,6,7,-8],[1,2,3,4,5,6,7,8],[1,2,3,4,1,2,3,4],[1,2,3,4,4,3,2,1]", vectorAlignment.vectorColumns)
        'Dim a As New MatrixLib.Matrix("c:\testMatrix.txt")
        'Dim b As MatrixLib.Matrix = Transpose(a)
        'Dim W As MatrixLib.Matrix = Multiply(Multiply(b, a), 1 / a.cols)
        'Dim c As New MatrixLib.Matrix("[-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,-1,-1]", vectorAlignment.vectorColumns)
        'Dim d As MatrixLib.Matrix = Multiply(W, c)
        'For i As Integer = 0 To d.rows - 1
        '    Console.WriteLine(d.OutoutRow(i, False))
        'Next
        Dim a As New MatrixLib.Matrix("[1,2,3],[1,5,5],[5,2,3]", vectorAlignment.vectorRows)
        Console.WriteLine(a.OutputMatrix)
        Console.WriteLine(Determinant(a))
        Dim ai As MatrixLib.Matrix = Inverse(a)
        Console.WriteLine(Determinant(ai))
        'For i As Integer = 0 To 2
        '    For j As Integer = 0 To 2
        '        Console.Write(ai.Matrix(j, i) & ",")
        '    Next
        '    Console.WriteLine()
        'Next
        Console.WriteLine(ai.OutputMatrix)
        'Dim b As MatrixLib.Matrix = Transpose(a)

        'For i As Integer = 0 To b.rows - 1
        '    Console.WriteLine(b.OutoutRow(i, True))
        'Next

        'Dim c As MatrixLib.Matrix = Multiply(a, b)
        'For i As Integer = 0 To c.rows - 1
        '    Console.WriteLine(c.OutoutRow(i, True))
        'Next

        'Dim d As MatrixLib.Matrix = Multiply(b, a)
        'For i As Integer = 0 To d.rows - 1
        '    Console.WriteLine(d.OutoutRow(i, True))
        'Next

        'Dim e As MatrixLib.Matrix = Multiply(a, 100)
        'For i As Integer = 0 To e.rows - 1
        '    Console.WriteLine(e.OutoutRow(i, True))
        'Next

        'Dim f As MatrixLib.Matrix = CopyMatrix(d)
        'For i As Integer = 0 To f.rows - 1
        '    Console.WriteLine(f.OutoutRow(i, True))
        'Next

        Console.WriteLine("Press Enter to Exit...")
        Dim z As String = Console.ReadLine()
    End Sub

End Module
