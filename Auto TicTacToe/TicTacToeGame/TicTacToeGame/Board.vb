Friend Interface ITicTacToeBoard
    Function PlayerPlaceSymbol(pPlayer As Player, pX As ColPosition, pY As RowPosition) As Boolean
    Function GetPositionState(pX As ColPosition, pY As RowPosition) As Player
    Function GetBoardState() As Player()
    Function CheckHorizontalWinner(pRow As Integer) As Player
    Function CheckVerticalWinner(pCol As Integer) As Player
    Function CheckDiagonalWinner(pDia As Integer) As Player
    Sub ResetBoard()
End Interface

Friend Class Board : Implements ITicTacToeBoard

    Private _BoardState(9) As Player

    'Default Constructor to create a new TicTacToe board object
    Public Sub New()
        ResetBoard
    End Sub

    Function PlayerPlaceSymbol(pPlayer As Player, pX As ColPosition, pY As RowPosition) As Boolean Implements ITicTacToeBoard.PlayerPlaceSymbol
        Dim Position = CalcBoardCoords(pX, pY)
        Dim ValidMove As Boolean
            If _BoardState(Position) = Player.NoPlayer
                _BoardState(Position) = pPlayer
                ValidMove = True
            Else
                ValidMove = False
            End If
        Return ValidMove
    End Function

    Function GetPositionState(pX As ColPosition, pY As RowPosition) As Player Implements ITicTacToeBoard.GetPositionState
        Dim Position = CalcBoardCoords(pX, pY)
        If Position >= 0 AndAlso Position <= 8
            'Board positions are 1-9, but array is 0-index, so subtract 1 from inputted position
            Return _BoardState(Position)
        Else
            Throw New Exception("Invalid Position Entered - Position:" & Position)
        End If
    End Function

    Function GetBoardState() As Player() Implements ITicTacToeBoard.GetBoardState
        Return _BoardState
    End Function

    Private Function CalcBoardCoords(pX As Integer, pY As Integer) As Integer
        If pX > 0 AndAlso pX <= 3 AndAlso pY > 0 AndAlso pY <= 3
            Return (pX-1) + ((pY-1)*3)
        Else
            Throw New Exception("Invalid Board Coordinates Entered. - X: " & pX & ", Y: " & pY)
        End If
    End Function

    Function CheckHorizontalWinner(pRow As Integer) As Player Implements ITicTacToeBoard.CheckHorizontalWinner
        Dim Winner As Player = Player.NoPlayer
        Dim BasePosition = (pRow - 1) * 3
        If _BoardState(BasePosition) = _BoardState(BasePosition+1) AndAlso _BoardState(BasePosition+1) = _BoardState(BasePosition+2)
            Winner = _BoardState(BasePosition)
        End If
        Return Winner
    End Function

    Function CheckVerticalWinner(pCol As Integer) As Player Implements ITicTacToeBoard.CheckVerticalWinner
        Dim Winner As Player = Player.NoPlayer
        If _BoardState(pCol-1) = _BoardState(pCol+2) AndAlso _BoardState(pCol+2) = _BoardState(pCol+5)
            Winner = _BoardState(pCol-1)
        End If
        Return Winner
    End Function

    Function CheckDiagonalWinner(pDia As Integer) As Player Implements ITicTacToeBoard.CheckDiagonalWinner
        Dim Winner As Player = Player.NoPlayer
        If pDia = 1
            If _BoardState(0) = _BoardState(4) AndAlso _BoardState(4) = _BoardState(8)
                Winner = _BoardState(0)
            End If
        Else
            If _BoardState(2) = _BoardState(4) AndAlso _BoardState(4) = _BoardState(6)
                Winner = _BoardState(2)
            End If
        End If
        Return Winner
    End Function

    Public Sub ResetBoard Implements ITicTacToeBoard.ResetBoard
        For i As Integer = 0 to 8
            _BoardState(i) = Player.NoPlayer
        Next
    End Sub

End Class
