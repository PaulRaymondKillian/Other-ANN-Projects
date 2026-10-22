Public Class frmGame

Private Sub frmGame_Load( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles MyBase.Load
        LoadGame()
End Sub

Private Sub cbAIBot_CheckedChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles cbAIBot.CheckedChanged
        If cbAIBot.Checked = True then
            Player2BotActive = True
            LoadAI
        Else
            Player2BotActive = False
            Player2AIBot = Nothing
        End If
        InitNewGame
End Sub

Private Sub btnLoadANN_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnLoadANN.Click
        If ofdLoadANN.ShowDialog = vbOK
            Dim ANN As New ANN_Network.Network(1, 1, 1, 1)
            ANN = ANN.importNetworkFromFile(ofdLoadANN.FileName)
            Player2AIBot = New TicTacToeAIANN.TicTacToeAIANN(ANN)
        End If
        
End Sub
End Class
