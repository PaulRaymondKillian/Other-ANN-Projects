Imports ITicTacToeAI
Imports TicTacToeGame
Imports ANN_Network

Public Class TicTacToeAIANN : Implements ITicTacToeAI.ITicTacToeAI
    Private _ANN As ANN_Network.Network
    Private _HiddenNeurons As Integer = 10
    Private _HiddenLayers As Integer = 1
    Private _MaxRandomInitWeight As Integer = 10

    Private _SymbolValueMy As Integer = 1
    Private _SymbolValueOpp As Integer = -1
    Private _SymbolValueNoPlayer As Integer = 0

    Public Sub New()
        InitANN
    End Sub

    Public Sub New(pHiddenNeurons As Integer, pHiddenLayers As Integer, Optional pMaxRandomInitWeight As Integer = 10)
        _HiddenNeurons = pHiddenNeurons
        _HiddenLayers = pHiddenLayers
        _MaxRandomInitWeight = pMaxRandomInitWeight
        InitANN
    End Sub

    Public Sub New(pANN As ANN_Network.Network)
        _ANN = pANN
    End Sub

    Function ChooseMove(pPlayer As Player, pBoardState() As Player) As Position Implements ITicTacToeAI.ITicTacToeAI.ChooseMove
        Return ChooseANNMove(pPlayer, pBoardState)
    End Function

    Private Function ChooseANNMove(pPlayer As Player, pBoardState() As Player) As Position
        Dim Move As Position
        '1) Load the board state into the network input neurons
        LoadBoardIntoANN(pPlayer, pBoardState)

        '2)Calculate the ANN output neuron values
        _ANN.calculateAll

        '3)Find winning output neuron that is an available board position
        FindWinningANNMove(pBoardState, Move)

        Return Move
    End Function

    Private Sub InitANN()
        _ANN = New ANN_Network.Network(9,_HiddenNeurons, _HiddenLayers, 9)
        _ANN.randomizeWeights(_MaxRandomInitWeight)
    End Sub

    'Load the current state of the TicTacToe board into the ANN input neurons
    Private Sub LoadBoardIntoANN(pPlayer As Player, pBoardState() As Player)
        For index As Integer = 0 to 8
            If pBoardState(index) = pPlayer                 'If symbol is my symbol
                _ANN.getNeuronFromIndex (index+1).value = _SymbolValueMy        '###NOTE: ANN Neurons are 1-Indexed (Not 0-Indexed)
            ElseIf pBoardState(index) = Player.NoPlayer     'If symbol is NoPlayer and thus spot is open
                _ANN.getNeuronFromIndex (index+1).value = _SymbolValueNoPlayer  '###NOTE: ANN Neurons are 1-Indexed (Not 0-Indexed)
            Else                                            'If not my symbol and not no player, must be opponent symbol
                _ANN.getNeuronFromIndex (index+1).value = _SymbolValueOpp       '###NOTE: ANN Neurons are 1-Indexed (Not 0-Indexed)
            End If
        Next
    End Sub

    'Look through the output neurons of the calculated ANN and choose the highest valued output neuron that corresponds
    ' to an available position on the board. That is the move this ANN will choose for the current boardstate.
    Private Sub FindWinningANNMove(pBoardState() As Player, ByRef pMove As Position)
        Dim HighValue As Double = Double.MinValue
        Dim CurrentPos As Position
        Dim HighPos As Position

        For Row As Integer = 1 to 3
            For Col As Integer = 1 to 3
                CurrentPos.Row = Row
                CurrentPos.Col = Col
                '###NOTE: ANN Output Neurons are 0-Indexed (Not 1-Indexed... Apparently...)
                If pBoardState(CurrentPos.CalcSingleCoor) = Player.NoPlayer _ 
                            AndAlso _ANN.Layer(_ANN.countHiddenLayers+1).Neuron(CurrentPos.CalcSingleCoor).value > HighValue then
                    HighValue = _ANN.Layer(_ANN.countHiddenLayers+1).Neuron(CurrentPos.CalcSingleCoor).value
                    HighPos = CurrentPos
                End If
            Next
        Next

        'Verify a move was selected, then return it
        If HighPos.Row <> -1 AndAlso HighPos.Col <> -1
            pMove = HighPos    'Set the referenced position parameter to the chosen position
        Else
            Throw New Exception("Error - No move was chosen!")
        End If
    End Sub

End Class

