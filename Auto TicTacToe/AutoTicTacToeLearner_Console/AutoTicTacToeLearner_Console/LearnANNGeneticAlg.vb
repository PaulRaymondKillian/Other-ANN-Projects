Imports ANNGeneticAlgFramework
Imports TicTacToeAIANN
Imports TicTacToeAIExpertSystem
Imports TicTacToeGame

Module LearnANNGeneticAlg

    Const BOARDSIZE As Integer = 9
    Dim ValidMove As Boolean
    Dim Winner As Player
    Dim Game As ITicTacToeGame
    Dim P1ANN, P2 As ITicTacToeAI.ITicTacToeAI
    Dim GamesToPlay As Integer = 5
    Dim Generations As Integer = 50
    Dim ShowBoard As Boolean = False

    Friend Sub Play(Optional pHiddenNeurons As Integer = 10, Optional pHiddenLayers As Integer = 1, _ 
                    Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, _ 
                    Optional pClonePercent As Double = 0.2, Optional pCrossPercent As Double = 0.50, _ 
                    Optional pCrossBreedPopLimPercent As Double = 0.50, Optional pCrossPointsMax as Integer = 1, _ 
                    Optional pMutationRate As Double = 0.01, Optional pMutationMagnitudePercent As Double = 0.20)

        Console.WriteLine("### ANN Genetic Algorithm Learner (2011) - TicTacToe ###" & vbCrLf & _ 
                          "Author: Paul Killian" & vbCrLf & _
                          "Contact: PaulKillian@DecodedCortex.com" & vbCrLf & _ 
                          "www.DecodedCortex.com" & vbCrLf & vbCrLf)

        Console.Write("Generations: ")
        Generations = Console.ReadLine
        Console.Write("Games Per Generation: ")
        GamesToPlay = Console.ReadLine
        Console.WriteLine()

        InputUserParameters(pHiddenNeurons, pHiddenLayers, pInitWeightLimit, pPopulationCount, pClonePercent, _ 
                            pCrossPercent, pCrossBreedPopLimPercent, pCrossPointsMax, pMutationRate, pMutationMagnitudePercent)

        P2 = New TicTacToeAIExpertSystem.TicTacToeAIESRandom

        Dim Population As New ANNGeneticAlgFramework.ANNGeneticAlgFramework(BOARDSIZE, pHiddenNeurons, pHiddenLayers, _
                                                                            BOARDSIZE, pInitWeightLimit, pPopulationCount, True)

        'Do each generation
        For generation As Integer = 0 to Generations-1

            'Record/Output Population Analysis Metrics
            Population.Analysis_RecordPopWeightVariance(True)
            Console.WriteLine(Population.GetRecordedWeightVariances(10))

            'Do each member of the population for each generation
            For index As Integer = 0 to Population.GetPopulationCount-1
                P1ANN = New TicTacToeAIANN.TicTacToeAIANN(Population.GetMember(index))
                Game = New TicTacToeGame.Game

                'Play multiplate games of TicTacToe with the population member
                Do
                    CheckAIPlayerTurn
                    If Game.CheckForWinner <> Player.NoPlayer Or Game.CheckForStalemate = True Then
                        Game.NewGame
                    End If
                Loop Until Game.GetGamesPlayed = GamesToPlay

                Dim P1Wins, P2Wins, Stalemates, Total As Integer
                P1Wins = Game.GetPlayerScore(Player.Player1)
                P2Wins = Game.GetPlayerScore(Player.Player2)
                Total = Game.GetGamesPlayed
                Stalemates = Total - (P1Wins + P2Wins)

                'Set fitness value for network to be the network's wins + stalements (learning not to lose)
                Population.GetMember(index).fitness = P1Wins + Stalemates
                With Population.GetMember(index)
                    Console.Writeline("G:" & generation & ", I:" & index & ", ID: " & .ID & " - fitness: " & .fitness)
                End With
            Next

            'Sort the population based on fitness values
            Population.SortPopulation
            OutputPopulationFitnessValues(Population)

            Population.ArchiveFitnessValues()
            Console.WriteLine(Population.OutputArchivedFitnessValues())

            'Generate a new population
            Population.GenerateNewPopulation(pClonePercent, pCrossPercent, pCrossBreedPopLimPercent, pCrossPointsMax, _ 
                                     pMutationRate, pMutationMagnitudePercent, True)


        Next

    End Sub

    Private Sub CheckAIPlayerTurn()
        If Game.IsPlayer1Turn
            'Play Player 1 AI Bot Turn
            Play(Player.Player1, P1ANN.ChooseMove(Player.Player1, Game.GetBoard))
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

    Private Sub OutputPopulationFitnessValues(pPopulation As ANNGeneticAlgFramework.ANNGeneticAlgFramework)
        Console.WriteLine(vbCrLf & "### SORTED POPULATION FITNESS ###")
        For i As Integer = 0 to pPopulation.GetPopulationCount-1
            Console.WriteLine(i & "- " & pPopulation.GetMember(i).ID & "=" & pPopulation.GetMember(i).fitness)
        Next
    End Sub

    Private Sub InputUserParameters(ByRef pHiddenNeurons As Integer,ByRef pHiddenLayers As Integer , _ 
                                ByRef pInitWeightLimit As Double, ByRef pPopulationCount As Integer, _ 
                                ByRef pClonePercent As Double, ByRef pCrossPercent As Double, _ 
                                ByRef pCrossBreedPopLimPercent As Double, ByRef pCrossPointsMax as Integer, _ 
                                ByRef pMutationRate As Double, ByRef pMutationMagnitudePercent As Double)
        Dim InputIndex As String = ""
        Dim InputtedValue As String
        Do
            Console.WriteLine("Current Genetic Algorith Parameters:" & vbCrLf & _
                              "[1] Hidden Neurons: " & pHiddenNeurons & vbCrLf & _
                              "[2] Hidden Layers: " & pHiddenLayers & vbCrLf & _
                              "[3] Init Weight Limit: " & pInitWeightLimit & vbCrLf & _
                              "[4] Population Count: " & pPopulationCount & vbCrLf & _
                              "[5] New Generation Clone Percent: " & pClonePercent & vbCrLf & _
                              "[6] New Generation Cross Breed Percent: " & pCrossPercent & vbCrLf & _
                              "[7] New Generation Cross Breed Population Limit Percent: " & pCrossBreedPopLimPercent & vbCrLf & _
                              "[8] New Generation Cross Breed Points Count Max: " & pCrossPointsMax & vbCrLf & _
                              "[9] New Generation Mutation Rate: " & pMutationRate & vbCrLf & _
                              "[10] New Generation Mutation Magnitude Percent: " & pMutationMagnitudePercent & vbCrLf &  _ 
                              "[0] No Changes" & vbCrLf & vbCrLf & _
                              "Please enter the integer value of the parameter you would like to change. Enter 0 for no changes")

            Console.Write("Inputted Index = ")
            InputIndex = Console.ReadLine
            
            Select Case InputIndex
                Case "1"
                    Console.WriteLine("Hidden Neurons: " & pHiddenNeurons)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pHiddenNeurons = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "2"
                    Console.WriteLine("Hidden Layers: " & pHiddenLayers)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pHiddenLayers = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "3"
                    Console.WriteLine("Init Weight Limit: " & pInitWeightLimit)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pInitWeightLimit = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "4"
                    Console.WriteLine("Population Count: " & pPopulationCount)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pPopulationCount = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "5"
                    Console.WriteLine("New Generation Clone Percent: " & pClonePercent)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pClonePercent = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "6"
                    Console.WriteLine("New Generation Cross Breed Percent: " & pCrossPercent)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pCrossPercent = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "7"
                    Console.WriteLine("New Generation Cross Breed Population Limit Percent: " & pCrossBreedPopLimPercent)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pCrossBreedPopLimPercent = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "8"
                    Console.WriteLine("New Generation Cross Breed Points Count Max: " & pCrossPointsMax)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pCrossPointsMax = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "9"
                    Console.WriteLine("New Generation Mutation Rate: " & pMutationRate)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pMutationRate = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "10"
                    Console.WriteLine("New Generation Mutation Magnitude Percent: " & pMutationMagnitudePercent)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pMutationMagnitudePercent = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
            End Select

        Loop Until InputIndex = "0" or InputIndex = ""
    End Sub
End Module
