Imports ANN_Hebbian

Public Class ANNPopHebbian
    'Inherits ANNPopulationBaseClass.ANNPopulationBaseClass

    Private _Population() As ANN_Hebbian.ANN_Hebbian

    Public Sub New(pInputNeurons As Integer, pHiddenNeurons As Integer, pHiddenLayers As Integer, pOutputNeurons As Integer, _ 
                   Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, Optional pOutputStatus As Boolean = False)
        For i As Integer = 0 to pPopulationCount-1
            _Population(i) = New ANN_Hebbian.ANN_Hebbian(pInputNeurons, pHiddenNeurons, pHiddenLayers, pOutputNeurons)
        Next
    End Sub
End Class
