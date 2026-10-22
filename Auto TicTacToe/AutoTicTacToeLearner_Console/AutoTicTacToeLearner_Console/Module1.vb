Imports ANNGeneticAlgFramework
Imports TicTacToeAIANN
Imports TicTacToeAIExpertSystem
Imports TicTacToeGame

Module Module1

    
    Sub Main()
        Console.WriteLine("### Auto TicTacToe Learner ###" & vbCrLf & _ 
                          "Author: Paul Killian" & vbCrLf & _
                          "Contact: PaulKillian@DecodedCortex.com" & vbCrLf & _ 
                          "www.DecodedCortex.com" & vbCrLf & vbCrLf)

        Console.WriteLine("[1] Use ANN Genetic Algorithm" & vbCrLf & _ 
                          "[2] Use ANN Population Unsupervised Teacher Algorithm" & vbCrLf & _
                          "[3] Use ANN Population Hebbian Algorithm" & vbCrLf)
        Console.Write("Selected Index = ")

        Dim UserInput As String = Console.ReadLine
        Dim StartTime As DateTime = Now
        Select UserInput
            Case "1"
                LearnANNGeneticAlg.Play()
            Case "2"
                LearnANNPopUnsupervisedTeacher.Play()
            Case "3"
                LearnANNPopHebbian.Play()
        End Select
        Dim Timespan As New TimeSpan(Now.Ticks - StartTime.Ticks)
        Console.WriteLine("Learning Finished in " & Timespan.Hours & " Hours, " & Timespan.Minutes & " Minutes, " & Timespan.Seconds & " Seconds.")
        Console.WriteLine(vbCrLf & "Press Enter to Exit...")
        Dim c As Char = Console.ReadLine

    End Sub

    
End Module
