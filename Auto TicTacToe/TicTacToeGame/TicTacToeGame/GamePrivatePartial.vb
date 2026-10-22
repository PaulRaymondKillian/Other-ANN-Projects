Public Partial Class Game
    'This class is to store the Private methods of the Game class.
    'Game.vb holds only the Interface facing methods.
    Private Sub InitNewGameSession()
        _Board = New Board
        _IsPlayer1Turn = True
        _Player1Score = 0
        _Player2Score = 0
        _GamesPlayed = 0
        _GameActive = True
    End Sub

    Private Sub SwitchPlayerTurn()
        _IsPlayer1Turn = Not _IsPlayer1Turn
    End Sub

    Private Sub IncreaseScore(pPlayer As Player)
        If pPlayer = Player.Player1
            _Player1Score += 1
        ElseIf pPlayer = Player.Player2
            _Player2Score += 1
        End If
        IncreaseGamesPlayed
    End Sub

    Private Sub IncreaseGamesPlayed()
        _GamesPlayed += 1
    End Sub
End Class
