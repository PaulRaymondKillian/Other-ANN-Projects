Public Interface ITicTacToeGame
    Function Play(pPlayer As Player, pX As ColPosition, pY As RowPosition) As Boolean
    Function Play(pPlayer As Player, pPosition As Position) As Boolean
    Function IsPlayer1Turn() As Boolean
    Function IsPlayer2Turn() As Boolean
    Function PlayerTurn() As Player
    Function CheckForWinner() As Player
    Function CheckForStalemate() As Boolean
    Function toString(pDelimiter As Char) As String
    Function toStringFormatted(Optional pDelimiter As Char = ","c) As String
    Function GetBoardPosition(pX As RowPosition, pY As ColPosition) As Player
    Function GetBoardPosition(pPosition As Position) As Player
    Function GetBoard() As Player()
    Function GetPlayerScore(pPlayer As Player) As Integer
    Function GetGamesPlayed() As Integer
    Sub NewGame()
    Sub ResetScore()
End Interface
