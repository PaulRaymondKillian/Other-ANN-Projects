Public Enum Player
        NoPlayer
        Player1
        Player2
End Enum

Public Enum RowPosition
    Row1 = 1
    Row2 = 2
    Row3 = 3
End Enum

Public Enum ColPosition
    Col1 = 1
    Col2 = 2
    Col3 = 3
End Enum

Public Structure Position
    Dim Row As TicTacToeGame.RowPosition
    Dim Col As TicTacToeGame.ColPosition

    Public Function CalcSingleCoor() As Integer
        If Col > 0 AndAlso Col <= 3 AndAlso Row > 0 AndAlso Row <= 3
            Return (Col-1) + ((Row-1)*3)
        Else
            Throw New Exception("Invalid Board Coordinates Entered. - Col: " & Col & ", Row: " & Row)
        End If
    End Function
End Structure
