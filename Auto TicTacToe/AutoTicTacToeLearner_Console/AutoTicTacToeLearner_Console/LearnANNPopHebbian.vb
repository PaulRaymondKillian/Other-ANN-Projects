Imports ANNPopHebbian
Imports TicTacToeAIANN
Imports TicTacToeAIExpertSystem
Imports TicTacToeGame

Module LearnANNPopHebbian

    Const BOARDSIZE As Integer = 9
    Dim ValidMove As Boolean
    Dim Winner As Player
    Dim Game As ITicTacToeGame
    Dim P1ANN, P2 As ITicTacToeAI.ITicTacToeAI
    Dim GamesToPlay As Integer = 5
    Dim Generations As Integer = 50
    Dim ShowBoard As Boolean = False

    Friend Sub Play(Optional pHiddenNeurons As Integer = 10, Optional pHiddenLayers As Integer = 1, _ 
                    Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100)
        Dim Population As New ANNPopHebbian.ANNPopHebbian(BOARDSIZE, pHiddenNeurons, pHiddenLayers, _
                                                                            BOARDSIZE, pInitWeightLimit, pPopulationCount, True)
    End Sub
                   
End Module
