Imports ANN_Network

Public Class ANNPopulationBaseClass

    Protected _Population() As ANN_Network.Network
    Protected _BestNetwork As ANN_Network.Network
    Protected _CountRandomNetworks As Integer = 0
    Protected Random As New Random(System.DateTime.Now.Millisecond)
    Private _AvgFitnessAll(), _AvgFitnessTop50Percent(), _AvgFitnessTop10Percent(), _BestFitness() As Double
    Private _ArchivedFitnessIndexCount As Integer = -1
    Protected _OutputDirectory As String
    Private _OutputDirectoryPrefix As String
    Private _OutputDirectorySuffix As String

    Public Sub New(pInputNeurons As Integer, pHiddenNeurons As Integer, pHiddenLayers As Integer, pOutputNeurons As Integer, _ 
                   Optional pInitWeightLimit As Double = 10, Optional pPopulationCount As Integer = 100, Optional pOutputStatus As Boolean = False, _ 
                   Optional pOutputDirPrefix As String = "")
        _OutputDirectoryPrefix = pOutputDirPrefix
        _OutputDirectorySuffix = "#DS#" & Now.Year & "-" & Now.Month & "-" & _ 
                                                            Now.Day & "-" & Now.Hour & "." & Now.Minute & "." & Now.Second & _
                                                            "#(" & pInputNeurons & "-" & pHiddenNeurons & "-" & pHiddenLayers & _
                                                            "-" & pOutputNeurons & ")"
        _OutputDirectory = System.IO.Directory.GetCurrentDirectory() & "\" & pOutputDirPrefix & _OutputDirectorySuffix
        If pOutputStatus
            Console.Write("Creating initial population... ")
        End If
        ReDim _Population(pPopulationCount-1)
        _BestNetwork = New ANN_Network.Network(pInputNeurons, pHiddenNeurons, pHiddenLayers, pOutputNeurons)
        _BestNetwork.fitness = -1
        For i As Integer = 0 to _Population.Count-1
            _Population(i) = New ANN_Network.Network(pInputNeurons, pHiddenNeurons, pHiddenLayers, pOutputNeurons)
            _Population(i).randomizeWeights(pInitWeightLimit)
            _Population(i).ID = i
        Next
        If pOutputStatus
            Console.Writeline("Complete!")
        End If
    End Sub

    Public Sub UpdateOutputDirectory()
        FileIO.FileSystem.RenameDirectory(_OutputDirectory, _ 
                                          _OutputDirectoryPrefix & _ 
                                          "#" & _BestNetwork.fitness & _OutputDirectorySuffix)
    End Sub

    'Give access to an ANN member of the population
    Public Function GetMember(p0Index As Integer) As ANN_Network.Network
        If p0Index >= 0 AndAlso p0Index <= _Population.Count-1
            If _Population(p0Index) IsNot Nothing
                Return _Population(p0Index)
            Else
                Throw New Exception("Error - Population member at index is not initialized: " & p0Index)
            End If
        Else
            Throw New Exception("Error - Invalid population member index inputted: " & p0Index)
        End If
    End Function

    Public Sub SetMemberFitness(p0Index As Integer, pFitness As Double)
        If p0Index >= 0 AndAlso p0Index <= _Population.Count-1
            If _Population(p0Index) IsNot Nothing
                _Population(p0Index).fitness = pFitness
            Else
                Throw New Exception("Error - Population member at index is not initialized: " & p0Index)
            End If
        Else
            Throw New Exception("Error - Invalid population member index inputted: " & p0Index)
        End If
    End Sub

    'This sorting algorithm uses selection sort because it is quick to implement.
     Public Sub SortPopulation()
        Dim NewPopulation(GetPopulationCount-1) As ANN_Network.Network
        Dim CurrentHighFitness As Double
        Dim CurrentHighIndex As Integer

        For i As Integer = 0 to GetPopulationCount-1
            CurrentHighFitness = Double.MinValue
            CurrentHighIndex = -1
            For j As Integer = 0 to GetPopulationCount-1
                If _Population(j) IsNot Nothing AndAlso GetMember(j).fitness > CurrentHighFitness
                    CurrentHighFitness = GetMember(j).fitness
                    CurrentHighIndex = j
                End If
            Next
            If CurrentHighIndex <> -1
                NewPopulation(i) = _Population(CurrentHighIndex)    'Set member into new population
                _Population(CurrentHighIndex) = Nothing             'Remove member from old population
            Else
                Throw New Exception("Error - No highest fitness member selected during sorting.")
            End If
        Next

        'Set the new sorted population
        _Population = NewPopulation
    End Sub

    Public Function GetPopulationCount() As Integer
        Return _Population.Count
    End Function

    Public Sub OutputBestNetworkToFile(pFilePath As String, Optional pPrefixFileName As String = "", _ 
                                       Optional pSuffixFileName As String = "BestNetwork", Optional pOutputStatus As Boolean = False)
        Try
            Dim FileName As String = pPrefixFileName & "=" & _BestNetwork.fitness & "(" & _BestNetwork.countInputNeurons(False) & "-" & _BestNetwork.countHiddenNeurons(False) & _ 
                                    "-" & _BestNetwork.countHiddenLayers & "-" & _BestNetwork.countOutputNeurons & ")" &  pSuffixFileName
            Dim AdjustedFilePath As String = pFilePath & "\" & FileName
            If pOutputStatus
                Console.Write("Writing Best Network to File: " & pFilePath & "\" & FileName & "... ")
            End If
            
            'Check if output directory exists. If it doesnt, create it.
            If(Not System.IO.Directory.Exists(AdjustedFilePath))
                System.IO.Directory.CreateDirectory(AdjustedFilePath) 
            End If

            _BestNetwork.exportNetworkToFile(AdjustedFilePath & "\" & FileName & ".ANN")
            If pOutputStatus
                Console.WriteLine("Complete!")
            End If

        Catch ex As Exception
            If pOutputStatus
                Console.WriteLine("FAILED")
            End If
            Throw New Exception("Error outputting best network to file: " & pFilePath & ", Desc: " & ex.Message)
        End Try
    End Sub

    Public Sub OutputArchivedFitnessToFile(pFilePath As String, Optional pPrefixFileName As String = "", _ 
                                      Optional pSuffixFileName As String = "", Optional pOutputStatus As Boolean = False)
        Try
            Dim FileName As String = pPrefixFileName & "ArchivedFitnessValues" &  pSuffixFileName
            If pOutputStatus
                Console.Write("Writing ArchivedFitness to File: " & pFilePath & "\" & FileName & "... ")
            End If
            
            'Check if output directory exists. If it doesnt, create it.
            If(Not System.IO.Directory.Exists(pFilePath))
                System.IO.Directory.CreateDirectory(pFilePath) 
            End If

            Dim sw As System.IO.StreamWriter = System.IO.File.CreateText(pFilePath & "\" & FileName & ".txt")
            sw.WriteLine("Archived Fitness Values = All, Top 50% of Pop, Top 10% of Pop, Best of Pop")
            For i As Integer = 0 to _ArchivedFitnessIndexCount
                sw.WriteLine(_AvgFitnessAll(i) & "," & _AvgFitnessTop50Percent(i) & "," & _AvgFitnessTop10Percent(i) & _ 
                    "," & _BestFitness(i))
            Next
            sw.Close()

            If pOutputStatus
                Console.WriteLine("Complete!")
            End If

        Catch ex As Exception
            If pOutputStatus
                Console.WriteLine("FAILED")
            End If
            Throw New Exception("Error outputting best network to file: " & pFilePath & ", Desc: " & ex.Message)
        End Try
    End Sub

    Protected Function RetainBestNetwork() As Boolean
        If _Population(0).fitness > _BestNetwork.fitness
            Dim BestNetWeights() As Double = _Population(0).exportWeightsToArray
            _BestNetwork.importWeightsFromArray(_Population(0).exportWeightsToArray)
            _BestNetwork.fitness = _Population(0).fitness
            _BestNetwork.ID = "B(" & _Population(0).ID & ")"
            UpdateOutputDirectory()
            Console.WriteLine("New Best Network Retained - ID: " & _BestNetwork.ID & ", F: " & _BestNetwork.fitness)
            Return True
        Else
            Return False
        End If
    End Function

    Protected Function CongruentNetworks(pANN1 As ANN_Network.Network, pANN2 As ANN_Network.Network) As Boolean
        If pANN1.countInputNeurons(False) = pANN2.countInputNeurons(False) _ 
                AndAlso pANN1.countHiddenNeurons(False) = pANN2.countHiddenNeurons(False) _ 
                AndAlso pANN1.countHiddenLayers = pANN2.countHiddenLayers _ 
                AndAlso pANN1.countOutputNeurons = pANN2.countOutputNeurons Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ArchiveFitnessValues()
        _ArchivedFitnessIndexCount += 1
        ReDim Preserve _AvgFitnessAll(_ArchivedFitnessIndexCount)
        ReDim Preserve _AvgFitnessTop50Percent(_ArchivedFitnessIndexCount)
        ReDim Preserve _AvgFitnessTop10Percent(_ArchivedFitnessIndexCount)
        ReDim Preserve _BestFitness(_ArchivedFitnessIndexCount)

        Dim TotalAllFitness As Integer = 0
        Dim TotalTop50Fitness As Integer = 0
        Dim TotalTop10Fitness As Integer = 0

        For i As Integer = 0 to GetPopulationCount-1
            With GetMember(i)
                TotalAllFitness += .fitness
                If i < 50 Then
                    TotalTop50Fitness += .fitness
                End If
                If i < 10 Then
                    TotalTop10Fitness += .fitness
                End If
            End With
        Next
        _AvgFitnessAll(_ArchivedFitnessIndexCount) = Convert.ToInt32(TotalAllFitness/GetPopulationCount)
        _AvgFitnessTop50Percent(_ArchivedFitnessIndexCount) = Convert.ToInt32(TotalTop50Fitness/(GetPopulationCount/2))
        _AvgFitnessTop10Percent(_ArchivedFitnessIndexCount) = Convert.ToInt32(TotalTop10Fitness/(GetPopulationCount/10))
        _BestFitness(_ArchivedFitnessIndexCount) = Convert.ToInt32(GetMember(0).fitness)
    End Sub
    
    Public Function OutputArchivedFitnessValues() As String
        Dim s As String = ""
        
        For i As Integer = 0 to _ArchivedFitnessIndexCount
            s += "Fitness - All: " & _AvgFitnessAll(i) & "   Top50: " & _AvgFitnessTop50Percent(i) & "   Top10: " & _AvgFitnessTop10Percent(i) & _ 
                    "   Best: " & _BestFitness(i) & vbCrLf
        Next

        OutputArchivedFitnessToFile(_OutputDirectory, "", _ 
                                      "",True)

        Return s
    End Function

End Class
