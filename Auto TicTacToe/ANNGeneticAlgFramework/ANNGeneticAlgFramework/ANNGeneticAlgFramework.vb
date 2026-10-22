Imports ANN_Network

Public Class ANNGeneticAlgFramework
    Inherits ANNPopulationBaseClass.ANNPopulationBaseClass

    Private _CountCrossNetworks As Integer = 0

    Public Sub New(pInputNeurons As Integer, pHiddenNeurons As Integer, pHiddenLayers As Integer, pOutputNeurons As Integer, _ 
                   Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, Optional pOutputStatus As Boolean = False)
        MyBase.New(pInputNeurons, pHiddenNeurons, pHiddenLayers, pOutputNeurons, pInitWeightLimit, pPopulationCount, pOutputStatus, _ 
                   "GeneticAlg")
    End Sub

    Public Sub GenerateNewPopulation(Optional pClonePercent As Double = 0.2, Optional pCrossPercent As Double = 0.50, _ 
                                     Optional pCrossBreedPopLimPercent As Double = 0.50, Optional pCrossPointsMax as Integer = 1, _ 
                                     Optional pMutationRate As Double = 0.01, Optional pMutationMagnitudePercent As Double = 0.20, _ 
                                     Optional pOutputStatusToConsole As Boolean = False)
        If pOutputStatusToConsole
            Console.Writeline("Generating New Population... ")
        End If
        Dim NewPopulation(GetPopulationCount-1) As ANN_Network.Network
        Dim index As Integer = 0
        Dim CloneIndexLimit As Integer = pClonePercent * NewPopulation.Count
        Dim CrossIndexLimit As Integer = CloneIndexLimit + (pCrossPercent * NewPopulation.Count)

        'Retain best network
        If RetainBestNetwork()
            OutputBestNetworkToFile(_OutputDirectory, "GeneticAlg", "", True)
        End If

        'Clone the best networks
        Do
            NewPopulation(index) = _Population(index)
            index += 1
        Loop Until index = CloneIndexLimit

        'Crossbreed some networks
        Dim Rand1, Rand2, RandCrossPoints As Integer
        Do
            Rand1 = Random.Next Mod (pCrossBreedPopLimPercent * GetPopulationCount)
            Rand2 = Random.Next Mod (pCrossBreedPopLimPercent * GetPopulationCount)
            RandCrossPoints = (Random.Next Mod pCrossPointsMax) + 1
            NewPopulation(index) = CrossBreedNetworks(_Population(Rand1), _Population(Rand2), RandCrossPoints)
            index += 1
        Loop Until index = CrossIndexLimit

        'Randomize the rest of the networks
        Do
            Dim NewRandNetwork As New ANN_Network.Network(NewPopulation(0).countInputNeurons(False), NewPopulation(0).countHiddenNeurons(False), _ 
                                                            NewPopulation(0).countHiddenLayers, NewPopulation(0).countOutputNeurons, True, 10)
            NewRandNetwork.ID = "R" & _CountRandomNetworks
            _CountRandomNetworks += 1
            NewPopulation(index) = NewRandNetwork
            index += 1
        Loop Until index = NewPopulation.Count

        'Allow possible mutation on all networks of new population
        For i As Integer = 0 to NewPopulation.Count-1
            If (Random.Next Mod 10000) < (pMutationRate * 10000)
                MutateNetwork(NewPopulation(i), pMutationMagnitudePercent)
            End If
        Next

        'Set Population to New Population
        _Population = NewPopulation

        If pOutputStatusToConsole
            Console.Writeline("New Population Generation Complete!")
        End If
    End Sub

    Private Function CrossBreedNetworks(pANN1 As ANN_Network.Network, pANN2 As ANN_Network.Network, Optional pCrossPoints As Integer = 1) As ANN_Network.Network
        If pCrossPoints < 1 Or pCrossPoints > pANN1.countWeights
            Throw New Exception("Error cross breeding networks. Number of cross points invalid: " & pCrossPoints)
        End If
        If CongruentNetworks(pANN1, pANN2)
            Dim RandCrossPoints(pCrossPoints-1) As Integer
            Dim FlipCross As Boolean = True
            Dim ChildANN As New ANN_Network.Network(pANN1.countInputNeurons(False), pANN1.countHiddenNeurons(False), pANN1.countHiddenLayers, pANN1.countOutputNeurons)
            Dim WeightsArray1() As Double = pANN1.exportWeightsToArray
            Dim WeightsArray2() As Double = pANN2.exportWeightsToArray
            Dim WeightsArrayChild(WeightsArray1.Count-1) As Double
            For i As Integer = 0 to RandCrossPoints.Count-1
                RandCrossPoints(i) = Random.Next Mod pANN1.countWeights
            Next
            For i As Integer = 0 to WeightsArrayChild.Count-1
                For j As Integer = 0 to RandCrossPoints.Count-1
                    If RandCrossPoints(j) = i
                        FlipCross = Not FlipCross
                        Exit For
                    End If
                Next
                If FlipCross
                    WeightsArrayChild(i) = WeightsArray1(i) 
                Else
                    WeightsArrayChild(i) = WeightsArray2(i) 
                End If
            Next
            ChildANN.importWeightsFromArray(WeightsArrayChild)
            ChildANN.ID = "C" & _CountCrossNetworks
            _CountCrossNetworks += 1
            Return ChildANN
        Else
            Throw New Exception("Error - Unable to cross breed inputted networks. They are not congruent")
        End If
    End Function

    Private Sub MutateNetwork(ByRef pANN As ANN_Network.Network,  pMutationMagnitudePercent As Double)
        Dim ANNWeightsArray() As Double = pANN.exportWeightsToArray
        Dim RandIndex As Integer = Random.Next Mod pANN.countWeights-1
        Dim RandMutationMag As Double = (Random.Next Mod (pMutationMagnitudePercent*1000)) / 1000
        Dim RandPosNeg As Integer = Random.Next Mod 100
        If RandPosNeg < 50 Then
            RandMutationMag = -RandMutationMag
        End If
        ANNWeightsArray(RandIndex) += RandMutationMag * ANNWeightsArray(RandIndex)
        pANN.importWeightsFromArray(ANNWeightsArray)
    End Sub

End Class
