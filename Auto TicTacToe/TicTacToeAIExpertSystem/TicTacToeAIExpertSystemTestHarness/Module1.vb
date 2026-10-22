Imports TicTacToeGame
Imports TicTacToeAIExpertSystem

Module Module1
    Dim ValidMove As Boolean
    Dim Winner As Player
    Dim Game As ITicTacToeGame
    Dim P1, P2 As ITicTacToeAI.ITicTacToeAI
    Dim GamesToPlay, SeriesToPlay As Integer
    Dim ShowBoard As Boolean = False

    Sub Main()
        GamesToPlay = 1000
        SeriesToPlay = 1
        P1 = New TicTacToeAIExpertSystem.TicTacToeAIESRandom
        'P2 = New TicTacToeAIExpertSystem.TicTacToeAIESBest
        P2 = New TicTacToeAIANN.TicTacToeAIANN
        For i As Integer = 1 to SeriesToPlay
            Game = New TicTacToeGame.Game
            If ShowBoard
                Console.WriteLine("############## SERIES" & i & "##############") 
            End If
            Do
                CheckAIPlayerTurn
                If ShowBoard
                    Console.Writeline(Game.toStringFormatted & vbCrLf)
                End If
                If Game.CheckForWinner <> Player.NoPlayer Or Game.CheckForStalemate = True then
                    'If Game.CheckForWinner = Player.Player1
                    '    MsgBox("Player 1 Somehow Won....")
                    'End If
                    Game.NewGame
                    If ShowBoard
                        Console.WriteLine(vbCrLf & vbCrLf & vbCrLf & "################# NEW GAME ######################")
                    End If
                End If
            Loop Until Game.GetGamesPlayed = GamesToPlay
            Dim P1Wins, P2Wins, Stalemates, Total As Integer
            P1Wins = Game.GetPlayerScore(Player.Player1)
            P2Wins = Game.GetPlayerScore(Player.Player2)
            Total = Game.GetGamesPlayed
            Stalemates = Total - (P1Wins + P2Wins)
            Console.WriteLine("Player1:" & P1Wins & " - " & (P1Wins/Total)*100 & "% (" & P1Wins/P2Wins & ")" & vbCrLf _ 
                              & "Player2: " & P2Wins & " - " & (P2Wins/Total)*100 & "% (" & P2Wins/P1Wins & ")" & vbCrLf _ 
                              & "Stalemates: " & Stalemates & " - " & (Stalemates/Total)*100 & "%" & vbCrLf _ 
                              & "Total: " & Total & vbCrLf & vbCrLf)
        Next
        Console.WriteLine("Press Enter to Exit...")
        Dim c As Char = Console.ReadLine
    End Sub

        Private Sub CheckAIPlayerTurn()
        If Game.IsPlayer1Turn
            'Play Player 1 AI Bot Turn
            Play(Player.Player1, P1.ChooseMove(Player.Player1, Game.GetBoard))
        ElseIf Game.IsPlayer2Turn
            'Play Player 2 AI Bot Turn
            Play(Player.Player2, P2.ChooseMove(Player.Player2, Game.GetBoard))
        End If
        'If Human player's turn, nothing happens and we wait for player action
    End Sub

        Private Sub Play(pPlayer As Player, pPosition As Position)
        If pPlayer = Game.PlayerTurn
            Game.Play(pPlayer, pPosition)
        Else
            Throw New Exception("Invalid player attempting to play. Attempted Player: " & pPlayer & ", Current Player: " & Game.PlayerTurn)
        End If
    End Sub

End Module
