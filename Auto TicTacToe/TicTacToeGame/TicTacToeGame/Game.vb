Public Class Game : Implements ITicTacToeGame

    Private _Board As ITicTacToeBoard
    Private _IsPlayer1Turn As Boolean
    Private _Player1Score As Integer
    Private _Player2Score As Integer
    Private _GamesPlayed As Integer
    Private _GameActive As Boolean

    'Default Constructor - Create a new Tic Tac Toe Game controller object
    Public Sub New()
        InitNewGameSession()
    End Sub

    Private Function Play(pPlayer As Player, pX As ColPosition, pY As RowPosition) As Boolean Implements ITicTacToeGame.Play
        Dim ValidPlay As Boolean
        If PlayerTurn() = pPlayer
            ValidPlay = _Board.PlayerPlaceSymbol(pPlayer, pX, pY)
            If ValidPlay
                SwitchPlayerTurn()
            End If
        Else
            ValidPlay = False
        End If
        Return ValidPlay
    End Function

    Function Play(pPlayer As Player, pPosition As Position) As Boolean Implements ITicTacToeGame.Play
        Return Play(pPlayer, pPosition.Col, pPosition.Row)
    End Function

    Public Function IsPlayer1Turn() As Boolean Implements ITicTacToeGame.IsPlayer1Turn
        If _IsPlayer1Turn
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsPlayer2Turn() As Boolean Implements ITicTacToeGame.IsPlayer2Turn
        Return Not IsPlayer1Turn
    End Function

    Public Function PlayerTurn() As Player Implements ITicTacToeGame.PlayerTurn
        If _IsPlayer1Turn
            Return Player.Player1
        Else
            Return Player.Player2
        End If
    End Function

    Public Function CheckForWinner() As Player Implements ITicTacToeGame.CheckForWinner
        Dim Winner As Player = Player.NoPlayer

        'Check Horizontal Winners
        For i As Integer = 1 to 3
            Winner = _Board.CheckHorizontalWinner(i)
            If Winner <> Player.NoPlayer
                If _GameActive
                    IncreaseScore(Winner)
                    _GameActive = False
                End If
                Return Winner
            End If
        Next

        'Check Vertical Winners
        For i As Integer = 1 to 3
            Winner = _Board.CheckVerticalWinner(i)
            If Winner <> Player.NoPlayer
                If _GameActive
                    IncreaseScore(Winner)
                    _GameActive = False
                End If
                Return Winner
            End If
        Next

        'Check Diagonal Winners
        For i As Integer = 1 to 2
            Winner = _Board.CheckDiagonalWinner(i)
            If Winner <> Player.NoPlayer
                If _GameActive
                    IncreaseScore(Winner)
                    _GameActive = False
                End If
                Return Winner
            End If
        Next

        Return Winner   'We will get down here if there is no winner.
    End Function

    Public Function CheckForStalemate() As Boolean Implements ITicTacToeGame.CheckForStalemate
        Dim Stalemate As Boolean = True
        For Row As TicTacToeGame.RowPosition = TicTacToeGame.RowPosition.Row1 to TicTacToeGame.RowPosition.Row3
            For Col As TicTacToeGame.ColPosition = TicTacToeGame.ColPosition.Col1 to TicTacToeGame.ColPosition.Col3
                If _Board.GetPositionState(Row, Col) = Player.NoPlayer
                    Stalemate = False
                End If
            Next
        Next
        If _GameActive AndAlso Stalemate
            IncreaseGamesPlayed
            _GameActive = False
        End If
        Return Stalemate
    End Function

    Public Overrides Function toString() As String
        Return toString(","c)
    End Function

    Public Overloads Function toString(pDelimiter As Char) As String Implements ITicTacToeGame.toString
        Dim sb As New System.Text.StringBuilder
        For x As Integer = 1 to 3
            For y As Integer = 1 to 3
                sb.Append(_Board.GetPositionState(x, y))
                If x <> 3 Or y <> 3 Then
                    sb.Append(pDelimiter)
                End If
            Next
        Next
        Return sb.ToString
    End Function

    Public Function toStringFormatted( Optional pDelimiter As Char = ","c) As String Implements ITicTacToeGame.toStringFormatted
        Dim sb As New System.Text.StringBuilder
        For x As Integer = 1 to 3
            For y As Integer = 1 to 3
                sb.Append(_Board.GetPositionState(x, y))
                If y <> 3 Then
                    sb.Append(pDelimiter)
                Else If x <> 3
                    sb.Append(vbCrLf)
                End If
            Next
        Next
        Return sb.ToString
    End Function

    Public Function GetBoardPosition(pX As RowPosition, pY As ColPosition) As Player Implements ITicTacToeGame.GetBoardPosition
        Return _Board.GetPositionState(pX, pY)
    End Function

    Function GetBoardPosition(pPosition As Position) As Player Implements ITicTacToeGame.GetBoardPosition
        Return GetBoardPosition(pPosition.Row, pPosition.Col)
    End Function

    Function GetBoard() As Player() Implements ITicTacToeGame.GetBoard
        Return _Board.GetBoardState
    End Function

    Public Function GetPlayerScore(pPlayer As Player) As Integer Implements ITicTacToeGame.GetPlayerScore
        If pPlayer = Player.Player1
            Return _Player1Score
        Else If pPlayer = Player.Player2
            Return _Player2Score
        Else
            Throw New Exception("Invalid Player Value Passed")
        End If
    End Function

    Public Function GetGamesPlayed() As Integer Implements ITicTacToeGame.GetGamesPlayed
        Return _GamesPlayed
    End Function

    Public Sub NewGame() Implements ITicTacToeGame.NewGame
        _Board.ResetBoard
        _GameActive = True
    End Sub

    Public Sub ResetScore() Implements ITicTacToeGame.ResetScore
        _Player1Score = 0
        _Player2Score = 0
        _GamesPlayed = 0
    End Sub

End Class
