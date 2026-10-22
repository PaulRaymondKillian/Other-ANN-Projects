Imports ITicTacToeAI
Imports TicTacToeGame

Public Class TicTacToeAIESRandom : Implements ITicTacToeAI.ITicTacToeAI

    Public Sub New()

    End Sub

    Function ChooseMove(pPlayer As Player, pBoardState() As Player) As Position Implements ITicTacToeAI.ITicTacToeAI.ChooseMove
        Return ChooseRandomMove(pBoardState)
    End Function

    Private Function ChooseRandomMove(pBoardState() As Player) As Position
        Dim Move As Position
            Dim Random As New Random(System.DateTime.Now.Millisecond)
            Do
                'Pos = (Random.Next Mod 8) + 1
                'If BoardState(Pos-1) = Player.NoPlayer
                '    'Move.Col = Pos Mod 3
                '    Move.Row = Math.Ceiling(Pos / 3)
                '    Move.Col = Pos - ((Move.Row-1) * 3)
                'End If
                Move.Row = (Random.Next Mod 3) + 1
                Move.Col = (Random.Next mod 3) + 1
            Loop Until pBoardState(Move.CalcSingleCoor) = Player.NoPlayer
        Return Move
    End Function

End Class
