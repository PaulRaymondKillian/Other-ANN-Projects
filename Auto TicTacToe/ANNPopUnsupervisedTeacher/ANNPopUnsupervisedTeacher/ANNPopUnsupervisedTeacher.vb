Imports ANN_Network

Public Class ANNPopUnsupervisedTeacher
    Inherits ANNPopulationBaseClass.ANNPopulationBaseClass

    Public Sub New(pInputNeurons As Integer, pHiddenNeurons As Integer, pHiddenLayers As Integer, pOutputNeurons As Integer, _ 
                   Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, Optional pOutputStatus As Boolean = False)
        MyBase.New(pInputNeurons, pHiddenNeurons, pHiddenLayers, pOutputNeurons, pInitWeightLimit, pPopulationCount, pOutputStatus, _
                   "UnsupTeach")
    End Sub

    Public Sub Teach(pTeacherIndex As Integer, pStudentIndex As Integer, pLearningRate As Double, Optional pPercentWeightsTaught As Double = 1)
        Teach(_Population(pTeacherIndex), _Population(pStudentIndex), pLearningRate, pPercentWeightsTaught)
    End Sub

    Public Sub Teach(pTeacher As ANN_Network.Network, ByRef pStudent As ANN_Network.Network, pLearningRate As Double, pPercentWeightsTaught As Double)
        If Not CongruentNetworks(pTeacher, pStudent) Then
            Throw New Exception("Non-congruent networks entered. Unable to perform teaching.")
        End If
        If pLearningRate > 1 Or pLearningRate < 0 Then
            Throw New Exception("Invalid Learning Rate value: " & pLearningRate)
        End If
        If pPercentWeightsTaught > 1 Or pPercentWeightsTaught < 0 Then
            Throw New Exception("Invalid Percent Weights Taught value: " & pPercentWeightsTaught)
        End If

        Dim Delta As Double
        Dim TeacherWeights() = pTeacher.exportWeightsToArray
        Dim StudentWeights() = pStudent.exportWeightsToArray
        Dim RandTeachWeight As Integer

        For i As Integer = 0 to TeacherWeights.count -1
            RandTeachWeight = Random.Next Mod 100
            'Only teach the weight if the random number is less than teach percentage (guarentees teaching near desired percent of weights)
            If RandTeachWeight <= (pPercentWeightsTaught * 100)
                'Delta is the difference of the weights between the teacher and student tempered by a learning rate
                Delta = pLearningRate * (TeacherWeights(i) - StudentWeights(i))
                StudentWeights(i) += Delta  'This will push the student's weight value towards the teacher's weight value
            End If
        Next

        pStudent.importWeightsFromArray(StudentWeights)

    End Sub

    Public Sub GenerateNewPopulation(Optional pClonePercent As Double = 0.7, Optional pMutationRate As Double = 0.01, _
                                     Optional pMutationMagnitudePercent As Double = 0.20, Optional pOutputStatusToConsole As Boolean = False)
        If pOutputStatusToConsole
            Console.Writeline("Generating New Population... ")
        End If
        Dim NewPopulation(GetPopulationCount-1) As ANN_Network.Network
        Dim index As Integer = 0
        Dim CloneIndexLimit As Integer = pClonePercent * NewPopulation.Count

        'Retain best network
        if RetainBestNetwork()
            OutputBestNetworkToFile(_OutputDirectory, "UnsupTeacher", "", True)
        End If

        'Clone the best networks
        Do
            NewPopulation(index) = _Population(index)
            index += 1
        Loop Until index = CloneIndexLimit

        'Randomize the rest of the networks
        Do
            Dim NewRandNetwork As New ANN_Network.Network(NewPopulation(0).countInputNeurons(False), NewPopulation(0).countHiddenNeurons(False), _ 
                                                            NewPopulation(0).countHiddenLayers, NewPopulation(0).countOutputNeurons, True, 10)
            NewRandNetwork.ID = "R" & _CountRandomNetworks
            _CountRandomNetworks += 1
            NewPopulation(index) = NewRandNetwork
            index += 1
        Loop Until index = NewPopulation.Count

        'Set Population to New Population
        _Population = NewPopulation
        If pOutputStatusToConsole
            Console.Writeline("New Population Generation Complete!")
        End If
    End Sub

End Class
