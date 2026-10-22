Imports TicTacToeGame

Public Interface ITicTacToeAI
    Function ChooseMove(pPlayer As Player, BoardState() As Player) As Position
End Interface
