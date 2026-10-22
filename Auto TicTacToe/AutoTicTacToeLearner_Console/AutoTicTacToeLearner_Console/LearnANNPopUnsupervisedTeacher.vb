Imports ANNPopUnsupervisedTeacher
Imports TicTacToeAIANN
Imports TicTacToeGame

Module LearnANNPopUnsupervisedTeacher

    Const BOARDSIZE As Integer = 9
    Dim ValidMove As Boolean
    Dim Winner As Player
    Dim Game As ITicTacToeGame
    Dim P1ANN, P2ANN As ITicTacToeAI.ITicTacToeAI
    'Dim GamesToPlayForFitness As Integer = 10
    'Dim GamesToPlay As Integer = 4
    Dim TeachingSessions As Integer = 10
    Dim TeachingCyclesPerSession As Integer = 10
    Dim ShowBoard As Boolean = False
    Dim P1Wins, P2Wins, Stalemates, Total As Integer
    'Dim LearningRate As Double = 0.01
    'Dim PercentWeightsTaught As Double = 0.5


    Friend Sub Play(Optional pHiddenNeurons As Integer = 10, Optional pHiddenLayers As Integer = 1, _ 
                    Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, _ 
                    Optional pLearningRate As Double = 0.01, Optional pPercentWeightsTaught As Double = 0.50, _ 
                    Optional pGamesToPlay As Integer = 5, Optional pGamesToPlayForFitness as Integer = 100, _ 
                    Optional pClonePercent As Double = 0.7, Optional pMutationRate As Double = 0.01, _ 
                    Optional pMutationMagnitudePercent As Double = 0.1)
        
        Console.WriteLine("### ANN Unsupervised Population Teacher Learner (2011) - TicTacToe ###" & vbCrLf & _ 
                          "Author: Paul Killian" & vbCrLf & _
                          "Contact: PaulKillian@DecodedCortex.com" & vbCrLf & _ 
                          "www.DecodedCortex.com" & vbCrLf & vbCrLf)

        Console.Write("Teaching Sessions: ")
        TeachingSessions = Console.ReadLine
        Console.Write("Teaching Cycles Per Sessions: ")
        TeachingCyclesPerSession = Console.ReadLine
        Console.WriteLine()

        InputUserParameters(pHiddenNeurons, pHiddenLayers, pInitWeightLimit, pPopulationCount, pLearningRate, pPercentWeightsTaught, _ 
                            pGamesToPlay, pGamesToPlayForFitness, pClonePercent, pMutationRate, pMutationMagnitudePercent)

        Console.WriteLine("Creating initial population...")
        Dim Population As New ANNPopUnsupervisedTeacher.ANNPopUnsupervisedTeacher(BOARDSIZE, pHiddenNeurons, pHiddenLayers, _
                                                                                  BOARDSIZE, pInitWeightLimit, pPopulationCount)

        CalcPopFitness(Population, 0, pGamesToPlayForFitness)
        Population.SortPopulation
        Population.ArchiveFitnessValues()
        Console.WriteLine(Population.OutputArchivedFitnessValues())

        'Do each generation
        For session As Integer = 0 to TeachingSessions-1
            
            'Record/Output Population Analysis Metrics
            Population.Analysis_RecordPopWeightVariance(True)
            Console.WriteLine(Population.GetRecordedWeightVariances(10))
            Console.Write("Cycles: ")
            For cycle As Integer = 0 to TeachingCyclesPerSession-1
                Console.Write(cycle)
                'Do each member of the population for each generation
                For index As Integer = 0 to Population.GetPopulationCount-1
                'Console.Write(".")
                    'Steps for Teacher Learning Move
                    ' 1) For each member of the population
                    ' 2) play against each other member of the population 'x' times
                    ' 3) for each win, teach the other ANN, for each loss, let the other ANN teach 
                    ' 3.1) Each teaching only includes a random fraction of the number of genes/weights
                    ' 3.2) The weights are adjusted only a small amount
                    ' 4) Generate New Population, add new randomized networks, to create diversity in population

                    P1ANN = New TicTacToeAIANN.TicTacToeAIANN(Population.GetMember(index))

                    For subindex As Integer = 0 to Population.GetPopulationCount-1
                        'Console.Write(".")
                        If subindex <> index
                            Game = New TicTacToeGame.Game
                            P2ANN = New TicTacToeAIANN.TicTacToeAIANN(Population.GetMember(subindex))
                
                            'Play a game with both population members
                            Do
                                CheckAIPlayerTurn
                                If Game.CheckForWinner <> Player.NoPlayer Or Game.CheckForStalemate = True Then
                                    Game.NewGame
                                End If
                            Loop Until Game.GetGamesPlayed = pGamesToPlay

                            P1Wins = Game.GetPlayerScore(Player.Player1)
                            P2Wins = Game.GetPlayerScore(Player.Player2)
                            Total = Game.GetGamesPlayed
                            Stalemates = Total - (P1Wins + P2Wins)

                            'Have the better player teach the other player - do nothing if wins are equal
                            If P1Wins > P2Wins
                                Population.Teach(index, subindex, pLearningRate, pPercentWeightsTaught)
                            ElseIf P2Wins > P1Wins
                                Population.Teach(subindex, index, pLearningRate, pPercentWeightsTaught)
                            End If
                        End If
                    Next 'For each sub-member of the population
                Next 'For each member of the population
            Next 'Cycles per Sessions

            'Play games against an AI Bot to gauge networks fitness at this point
            CalcPopFitness(Population, session+1, pGamesToPlayForFitness)
            Population.SortPopulation
            'Generate a new population
            Console.WriteLine("Generating New Population...")
            Population.GenerateNewPopulation(pClonePercent, pMutationRate, pMutationMagnitudePercent, True)

            Population.ArchiveFitnessValues()
            Console.WriteLine(Population.OutputArchivedFitnessValues())

        Next'Sessions

    End Sub

    Private Sub CheckAIPlayerTurn()
        If Game.IsPlayer1Turn
            'Play Player 1 AI Bot Turn
            Play(Player.Player1, P1ANN.ChooseMove(Player.Player1, Game.GetBoard))
        ElseIf Game.IsPlayer2Turn
            'Play Player 2 AI Bot Turn
            Play(Player.Player2, P2ANN.ChooseMove(Player.Player2, Game.GetBoard))
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

    Private Sub CalcPopFitness(pPopulation As ANNPopUnsupervisedTeacher.ANNPopUnsupervisedTeacher, pSession As Integer, pGamesToPlayForFitness As Integer)
            Console.WriteLine("Calculating population fitness values...")
            'Do each member of the population
            P2ANN = New TicTacToeAIExpertSystem.TicTacToeAIESRandom
            For index As Integer = 0 to pPopulation.GetPopulationCount-1
                P1ANN = New TicTacToeAIANN.TicTacToeAIANN(pPopulation.GetMember(index))
                Game = New TicTacToeGame.Game

                'Play multiplate games of TicTacToe with the population member
                Do
                    CheckAIPlayerTurn
                    If Game.CheckForWinner <> Player.NoPlayer Or Game.CheckForStalemate = True Then
                        Game.NewGame
                    End If
                Loop Until Game.GetGamesPlayed = pGamesToPlayForFitness

                Dim P1Wins, P2Wins, Stalemates, Total As Integer
                P1Wins = Game.GetPlayerScore(Player.Player1)
                P2Wins = Game.GetPlayerScore(Player.Player2)
                Total = Game.GetGamesPlayed
                Stalemates = Total - (P1Wins + P2Wins)

                'Set fitness value for network to be the network's wins + stalements (learning not to lose)
                pPopulation.GetMember(index).fitness = P1Wins + Stalemates
                With pPopulation.GetMember(index)
                    Console.Writeline("S:" & pSession & ", I:" & index & ", ID: " & .ID & " - fitness: " & .fitness)
                End With
            Next
    End Sub



    Private Sub InputUserParameters(ByRef pHiddenNeurons As Integer, pHiddenLayers As Integer, _ 
                    ByRef pInitWeightLimit As Double, ByRef pPopulationCount As Integer, _ 
                    ByRef pLearningRate As Double, ByRef pPercentWeightsTaught As Double, _ 
                    ByRef pGamesToPlay As Integer, ByRef pGamesToPlayForFitness As Integer, _
                    ByRef pClonePercent As Double, ByRef pMutationRate As Double, _ 
                    ByRef pMutationMagnitudePercent As Double)
        Dim InputIndex As String = ""
        Dim InputtedValue As String
        Do
            Console.WriteLine("Current Unsupervised Population Teacher Parameters:" & vbCrLf & _
                              "[1] Hidden Neurons: " & pHiddenNeurons & vbCrLf & _
                              "[2] Hidden Layers: " & pHiddenLayers & vbCrLf & _
                              "[3] Init Weight Limit: " & pInitWeightLimit & vbCrLf & _
                              "[4] Population Count: " & pPopulationCount & vbCrLf & _
                              "[5] Learning Rate: " & pLearningRate & vbCrLf & _
                              "[6] Percent Weights To Be Taught: " & pPercentWeightsTaught & vbCrLf & _
                              "[7] Games To Play Per Teaching: " & pGamesToPlay & vbCrLf & _
                              "[8] Games To Play Against Random To Determine Fitness: " & pGamesToPlayForFitness & vbCrLf & _
                              "[9] New Generation Clone Percent: " & pClonePercent & vbCrLf & _
                              "[10] New Generation Mutation Rate: " & pMutationRate & vbCrLf & _
                              "[11] New Generation Mutation Magnitude Percent: " & pMutationMagnitudePercent & vbCrLf &  _
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
                    Console.WriteLine("Learning Rate: " & pLearningRate)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pLearningRate = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "6"
                    Console.WriteLine("Percent Weights to be Taught: " & pPercentWeightsTaught)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pPercentWeightsTaught = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "7"
                    Console.WriteLine("Games To Play Per Teaching: " & pGamesToPlay)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pGamesToPlay = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "8"
                    Console.WriteLine("Games To Play Against Random To Determine Fitness: " & pGamesToPlayForFitness)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pGamesToPlayForFitness = Convert.ToInt32(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "9"
                    Console.WriteLine("New Generation Clone Percent: " & pClonePercent)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pClonePercent = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "10"
                    Console.WriteLine("New Generation Mutation Rate: " & pMutationRate)
                    Console.Write("New Value = ")
                    InputtedValue = Console.ReadLine
                    If IsNumeric(InputtedValue)
                        pMutationRate = Convert.ToDouble(InputtedValue)
                    Else
                        Console.WriteLine("Invalid Value Inputted")
                    End If
                Case "11"
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
