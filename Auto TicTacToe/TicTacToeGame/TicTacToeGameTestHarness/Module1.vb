Imports TicTacToeGame

Module Module1

    Sub Main()
        Dim ValidMove As Boolean
        Dim Winner As Player
        Dim Game As ITicTacToeGame
        Game = New TicTacToeGame.Game
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player1,1,1)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player1,2,2)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player2,1,2)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player1,2,2)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player2,2,1)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        ValidMove = Game.Play(Player.Player1,3,3)
        Winner = Game.CheckForWinner
        Console.WriteLine(ValidMove)
        Console.WriteLine(Winner)
        Console.WriteLine(Game.toStringFormatted & vbCrLf)

        Console.WriteLine("Press Enter to Exit...")
        Dim c As Char = Console.ReadLine
    End Sub

End Module
