Imports TicTacToeGame
Imports ITicTacToeAI
Imports TicTacToeAIANN
Imports ANN_Network

Public Partial Class frmGame
    Private Game As ITicTacToeGame
    Private Player1AIBot As ITicTacToeAI.ITicTacToeAI
    Private Player2AIBot As ITicTacToeAI.ITicTacToeAI
    Private Player1BotActive As Boolean = False
    Private Player2BotActive As Boolean = False
    Private Sub LoadGame()
        InitNewGame
        LoadAI
        RefreshGUIElements
    End Sub

    Private Sub InitNewGame
        If Game Is Nothing
            Game = New TicTacToeGame.Game
        End If
        Game.NewGame
        Game.ResetScore
        RefreshGUIElements
        LoadAI
        CheckAIPlayerTurn
    End Sub

    Private Sub LoadAI
        If Player1BotActive
            Player1AIBot = New TicTacToeAIExpertSystem.TicTacToeAIESRandom
        End If
        If Player2BotActive
            'Player2AIBot = New TicTacToeAIExpertSystem.TicTacToeAIESRandom
            Player2AIBot = New TicTacToeAIExpertSystem.TicTacToeAIESCount
        End If
    End Sub

    Private Sub RefreshBoard()
        r1c1.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col1, TicTacToeGame.RowPosition.Row1)
        r1c2.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col2, TicTacToeGame.RowPosition.Row1)
        r1c3.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col3, TicTacToeGame.RowPosition.Row1)
        r2c1.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col1, TicTacToeGame.RowPosition.Row2)
        r2c2.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col2, TicTacToeGame.RowPosition.Row2)
        r2c3.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col3, TicTacToeGame.RowPosition.Row2)
        r3c1.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col1, TicTacToeGame.RowPosition.Row3)
        r3c2.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col2, TicTacToeGame.RowPosition.Row3)
        r3c3.Text = Game.GetBoardPosition(TicTacToeGame.ColPosition.Col3, TicTacToeGame.RowPosition.Row3)
    End Sub

    Private Sub RefreshScore()
        lblPlayer1Score.Text = Game.GetPlayerScore(Player.Player1)
        lblPlayer2Score.Text = Game.GetPlayerScore(Player.Player2)
    End Sub

    Private Sub RefreshCurrentPlayer()
        lblCurrentPlayer.Text = Game.PlayerTurn
    End Sub

    Private Sub PlayR1C1 Handles r1c1.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row1
        Position.Col = TicTacToeGame.ColPosition.Col1
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR1C2 Handles r1c2.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row1
        Position.Col = TicTacToeGame.ColPosition.Col2
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR1C3 Handles r1c3.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row1
        Position.Col = TicTacToeGame.ColPosition.Col3
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR2C1 Handles r2c1.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row2
        Position.Col = TicTacToeGame.ColPosition.Col1
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR2C2 Handles r2c2.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row2
        Position.Col = TicTacToeGame.ColPosition.Col2
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR2C3 Handles r2c3.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row2
        Position.Col = TicTacToeGame.ColPosition.Col3
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR3C1 Handles r3c1.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row3
        Position.Col = TicTacToeGame.ColPosition.Col1
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR3C2 Handles r3c2.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row3
        Position.Col = TicTacToeGame.ColPosition.Col2
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub PlayR3C3 Handles r3c3.Click
        Dim Position As Position
        Position.Row = TicTacToeGame.RowPosition.Row3
        Position.Col = TicTacToeGame.ColPosition.Col3
        Play(Game.PlayerTurn, Position)
    End Sub

    Private Sub Play(pPlayer As Player, pPosition As Position)
        If pPlayer = Game.PlayerTurn
            Game.Play(pPlayer, pPosition)
            RefreshGUIElements
            CheckForWinner
            CheckAIPlayerTurn
        Else
            Throw New Exception("Invalid player attempting to play. Attempted Player: " & pPlayer & ", Current Player: " & Game.PlayerTurn)
        End If
    End Sub

    Private Sub CheckForWinner()
        Dim Winner = Game.CheckForWinner
        If Winner <> TicTacToeGame.Player.NoPlayer
            MsgBox("Player " & Winner & " is the winner!", Microsoft.VisualBasic.MsgBoxStyle.OkOnly,"We have a winner!")
            Game.NewGame()
        Elseif Game.CheckForStalemate = True
            MsgBox("Stalemate - No player wins.", Microsoft.VisualBasic.MsgBoxStyle.OkOnly,"No Winner :(")
            Game.NewGame
        End If 
        RefreshGUIElements
    End Sub

    Private Sub RefreshGUIElements
        RefreshBoard
        RefreshScore
        RefreshCurrentPlayer
    End Sub

    Private Sub CheckAIPlayerTurn()
        If Player1BotActive AndAlso Game.IsPlayer1Turn
            'Play Player 1 AI Bot Turn
            Play(Player.Player1, Player1AIBot.ChooseMove(Player.Player1, Game.GetBoard))
        ElseIf Player2BotActive Andalso Game.IsPlayer2Turn
            'Play Player 2 AI Bot Turn
            Play(Player.Player2, Player2AIBot.ChooseMove(Player.Player2, Game.GetBoard))
        End If
        'If Human player's turn, nothing happens and we wait for player action
    End Sub


End Class
