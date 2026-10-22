Public Partial Class ANNPopulationBaseClass

    Private _PopulationWeightsVariance(,) As Double
    Private _PopulationWeightVarianceIndex As Integer = -1

    Public Sub Analysis_RecordPopWeightVariance(Optional pOutputStatusToConsole As Boolean = False)
        If pOutputStatusToConsole
            Console.Write("Recording Population Weight Variances... ")
        End If
        _PopulationWeightVarianceIndex += 1
        Dim Delta As Double
        Dim BaseWeightsArray(), CurrentWeightsArray() As Double
        
        'The Array is (WeightIndex, TimeStepIndex) - Because a Redim can only change the right-most dimension
        ReDim Preserve _PopulationWeightsVariance(_Population(0).countWeights-1, _PopulationWeightVarianceIndex)
        
        BaseWeightsArray = _Population(0).exportWeightsToArray

        'Set all values of array row to 0
        For i As Integer = 0 to _Population(0).countWeights-1
            _PopulationWeightsVariance(i, _PopulationWeightVarianceIndex) = 0
        Next

        'Go through each member of the population
        For PopIndex As Integer = 1 to GetPopulationCount-1
            CurrentWeightsArray = _Population(PopIndex).exportWeightsToArray

            'Go through each weight of the current population member and add the variance to the variance array
            For WeightIndex As Integer = 0 to _Population(PopIndex).countWeights-1
                Delta = Math.Abs(BaseWeightsArray(WeightIndex) - CurrentWeightsArray(WeightIndex))
                _PopulationWeightsVariance(WeightIndex, _PopulationWeightVarianceIndex) += Delta
            Next
        Next
        If pOutputStatusToConsole
            Console.Writeline("Complete!")
        End If
    End Sub

    Public Function GetRecordedWeightVariances(pCountWeightsIncluded As Integer, Optional pStartFromBack As Boolean = False) As String
        If pCountWeightsIncluded > 0 AndAlso pCountWeightsIncluded <= _Population(0).countWeights
            Dim sb As New System.Text.StringBuilder
            For VarianceIndex As Integer = 0 to _PopulationWeightVarianceIndex
                sb.Append("Weight Variances - " & VarianceIndex & ": ")
                For WeightIndex As Integer = 0 to pCountWeightsIncluded-1
                    sb.Append(Math.Round(_PopulationWeightsVariance(WeightIndex, VarianceIndex), 0))
                    If WeightIndex <> pCountWeightsIncluded-1
                        sb.Append(", ")
                    End If
                Next
                sb.Append(vbCrLf)
            Next
            Return sb.ToString
        Else
            Throw New Exception("Error - invalid Count Weights Included value inputted: " & pCountWeightsIncluded)
        End If
    End Function

End Class
