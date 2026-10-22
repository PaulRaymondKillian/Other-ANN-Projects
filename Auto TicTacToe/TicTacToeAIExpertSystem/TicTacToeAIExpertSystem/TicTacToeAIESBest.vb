Imports ITicTacToeAI
Imports TicTacToeGame

Public Class TicTacToeAIESBest : Implements ITicTacToeAI.ITicTacToeAI

    Public Sub New()

    End Sub

    Function ChooseMove(pPlayer As Player, pBoardState() As Player) As Position Implements ITicTacToeAI.ITicTacToeAI.ChooseMove
        Return ChooseBestMove(pPlayer, pBoardState)
    End Function

    Private Function ChooseBestMove(pPlayer As Player, pBoardState() As Player) As Position
        'Best Move Logic will consist of the following ordered steps:
        ' 1) Check if empty board, if so, choose center
        ' 2) Check for win, if so, choose winning position
        ' 3) Check for block, if so, choose blocking position
        ' 4) Check for best available move on the board

        Dim Move As Position
        
        ' 1) Check if empty board
        If CheckForEmptyBoard(pBoardState, Move)
            Return Move                         'If so, Move = Center Position (2,2)

        ' 2) Check for an available winning move
        Else If CheckForWin(pPlayer, pBoardState, Move)
            Return Move                         'If so, choose that winning move
        
        ' 3) Check for a move blocking an opponents win
        Else If CheckForBlock(pPlayer, pBoardState, Move)
            Return Move                         'If so, choose the blocking position
        
        ' 4) Check for best available move
        Else If CheckForBestAvailableMove(pPlayer, pBoardState, Move)
            Return Move
        'Oh no, no move was chosen! Bad bad bad!
        Else
            Throw New Exception("No move was selected. This should never happen.")
        End If
    End Function

    Private Function CheckForEmptyBoard(pBoardState() As Player, ByRef pPosition As Position) As Boolean
        Dim BoardEmpty As Boolean = True
        For i As Integer = 0 to pBoardState.Count-1
            If pBoardState(i) <> Player.NoPlayer
                BoardEmpty = False
            End If
        Next
        If BoardEmpty
            pPosition.Col = 2
            pPosition.Row = 2
        End If
        Return BoardEmpty
    End Function

    Private Function CheckForWin(pPlayer As Player, pBoardState() As Player, ByRef pPosition As Position) As Boolean
        Dim WinningIndex As Integer = -1

        'Check Rows for a winning move
        For row As Integer = 1 to 3
            If CheckForWin(pPlayer, ReturnBoardRow(row, pBoardState), WinningIndex)
                pPosition.Row = row
                pPosition.Col = WinningIndex
                Return True 'We have a winning move
            End If
        Next

        'Check Cols for a winning move
        For col As Integer = 1 to 3
            If CheckForWin(pPlayer, ReturnBoardCol(col, pBoardState), WinningIndex)
                pPosition.Row = WinningIndex
                pPosition.Col = col
                Return True ' We have a winning move
            End If
        Next

        'Check Diagonals for a winning move
        For dia As Integer = 1 to 2
            If CheckForWin(pPlayer, ReturnBoardDia(dia, pBoardState), WinningIndex)
                If WinningIndex = 2
                    pPosition.Row = 2
                    pPosition.Col = 2
                    Return True
                ElseIf WinningIndex = 1
                    If dia = 1
                        pPosition.Row = 1
                        pPosition.Col = 1
                        Return True
                    ElseIf dia = 2
                        pPosition.Row = 1
                        pPosition.Col = 3
                        Return True
                    End If
                ElseIf WinningIndex = 3
                    If dia = 1
                        pPosition.Row = 3
                        pPosition.Col = 3
                        Return True
                    ElseIf dia = 2
                        pPosition.Row = 3
                        pPosition.Col = 1
                        Return True
                    End If
                End If
            End If
        Next

        Return False 'We dont have a winning move
    End Function

    Private Function CheckForWin(pPlayer As Player, pSubSet() As Player, ByRef pIndex As Integer) As Boolean
        If pSubSet.Count <> 3 Then
            Throw New Exception("Invalid subset passed. Subsets should only contain 3 items. - Items: " & pSubSet.Count)
        End If
        Dim WinningMove As Boolean = False
        Dim PlayerCount As Integer = 0
        Dim PlayIndex As Integer = -1
        'Scan through all 3 items, check for the number of times pPlayer exists, keep track of last open-space
        For i As Integer = 0 to pSubSet.Count - 1
            If pSubSet(i) = pPlayer
                PlayerCount += 1
            Else If pSubSet(i) = Player.NoPlayer
                PlayIndex = i
            End If
        Next
        If PlayerCount = 2 AndAlso PlayIndex <> -1
            WinningMove = True
            pIndex = PlayIndex + 1 '0-based index, need to increment by 1
        End If
        Return WinningMove
    End Function

    Private Function CheckForBlock(pPlayer As Player, pBoardState As Player(), ByRef pPosition As Position) As Boolean
        Dim BlockingIndex As Integer = -1

        'Check Rows for a blocking move
        For row As Integer = 1 to 3
            If CheckForBlock(pPlayer, ReturnBoardRow(row, pBoardState), BlockingIndex)
                pPosition.Row = row
                pPosition.Col = BlockingIndex
                Return True 'We have a blocking move
            End If
        Next

        'Check Cols for a winning move
        For col As Integer = 1 to 3
            If CheckForBlock(pPlayer, ReturnBoardCol(col, pBoardState), BlockingIndex)
                pPosition.Row = BlockingIndex
                pPosition.Col = col
                Return True ' We have a blocking move
            End If
        Next

        'Check Diagonals for a winning move
        For dia As Integer = 1 to 2
            If CheckForBlock(pPlayer, ReturnBoardDia(dia, pBoardState), BlockingIndex)
                If BlockingIndex = 2
                    pPosition.Row = 2
                    pPosition.Col = 2
                    Return True
                ElseIf BlockingIndex = 1
                    If dia = 1
                        pPosition.Row = 1
                        pPosition.Col = 1
                        Return True
                    ElseIf dia = 2
                        pPosition.Row = 1
                        pPosition.Col = 3
                        Return True
                    End If
                ElseIf BlockingIndex = 3
                    If dia = 1
                        pPosition.Row = 3
                        pPosition.Col = 3
                        Return True
                    ElseIf dia = 2
                        pPosition.Row = 3
                        pPosition.Col = 1
                        Return True
                    End If
                End If
            End If
        Next

        Return False 'We dont have a blocking move
    End Function

    Private Function CheckForBlock(pPlayer As Player, pSubset As Player(), ByRef pBlockingIndex As Integer) As Boolean
        If pSubSet.Count <> 3 Then
            Throw New Exception("Invalid subset passed. Subsets should only contain 3 items. - Items: " & pSubSet.Count)
        End If
        Dim BlockingMove As Boolean = False
        Dim OpponentPlayerCount As Integer = 0
        Dim PlayIndex As Integer = -1
        Dim OpponentPlayer As Player
        If pPlayer = Player.Player1
            OpponentPlayer = Player.Player2
        Else
            OpponentPlayer = Player.Player1
        End If

        'Scan through all 3 items, check for the number of times the Opponent Player exists, keep track of last open-space
        For i As Integer = 0 to pSubSet.Count - 1
            If pSubSet(i) = OpponentPlayer
                OpponentPlayerCount += 1
            Else If pSubSet(i) = Player.NoPlayer
                PlayIndex = i
            End If
        Next
        If OpponentPlayerCount = 2 AndAlso PlayIndex <> -1
            BlockingMove = True
            pBlockingIndex = PlayIndex + 1 '0-based index, need to increment by 1
        End If
        Return BlockingMove
    End Function

    Private Function CheckForBestAvailableMove(pPlayer As Player, pBoardState As Player(), ByRef pPosition As Position) As Boolean
        'I want to store an array or list of 9 items that will represent each position on the board. I then want to keep track of the
        ' number symbols within a 'board line' from this position for both the current player's symbols and the opponents symbols.
        ' I would love to use a struct here, but this is the only place I would use it, and it would be visible to the entire library,
        ' and I dont want that, so I cant use a struct here (cant create a struct privately in a method). I would use an anonymous type,
        ' but you cannot use an array of a anonymous types, so I am resorting what I consider a poor design, though I cant think of a better
        ' alternative. I will use a 2-dimensional table using constants for the second index to keep track of the board symbols info.
        Const UNAVAILABLE_POS As Integer = -1
        Const SYMBOLS_MY As Integer = 0
        Const SYMBOLS_OPP As Integer = 1
        Dim PositionValues(8,1) As Integer
        Dim CurrentPos As Position
        Dim CurrentSubset(2) As Player
        Dim CurrentMy, CurrentOpp As Integer

        'Set PositionValues 2-Dimensional array values to 0
        For i As Integer = 0 to 8
            For j As Integer = 0 to 1
                PositionValues(i,j) = 0
            Next
        Next

        'Go through the board and add up the 'valid' symbol totals
        For Row As Integer = 1 to 3
            For Col As Integer = 1 to 3
                CurrentPos.Row = Row
                CurrentPos.Col = Col

                'Only check the status of this position if it is available
                If pBoardState(CurrentPos.CalcSingleCoor) = Player.NoPlayer then


                    ''''''''''''''''''''''''
                    'Determine Status of Row
                    CurrentSubset = ReturnBoardRow(Row, pBoardState)
                    CurrentMy = 0
                    CurrentOpp = 0
                    For Index As Integer = 0 to 2
                        If CurrentSubset(Index) = pPlayer               'If it is my symbol
                            CurrentMy += 1
                        ElseIf CurrentSubset(Index) <> Player.NoPlayer  'If not my symbol, and not no player, then is opponent's symbol
                            CurrentOpp += 1
                        End If
                    Next
                    'Check if Subset contained 1 of my symbols and 1 of opps
                    ' If so, then it is then we should not count it towards the total at all because the row itself cannot be a winner or blocker
                    If CurrentMy > 0 AndAlso CurrentOpp > 0 Then
                        CurrentMy = 0
                        CurrentOpp = 0
                    End If
                    'Increase the specified symbol values by the subset's total
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) += CurrentMy
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) += CurrentOpp
                


                    'Determine Status of Col
                    CurrentSubset = ReturnBoardCol(Col, pBoardState)
                    CurrentMy = 0
                    CurrentOpp = 0
                    For Index As Integer = 0 to 2
                        If CurrentSubset(Index) = pPlayer               'If it is my symbol
                            CurrentMy += 1
                        ElseIf CurrentSubset(Index) <> Player.NoPlayer  'If not my symbol, and not no player, then is opponent's symbol
                            CurrentOpp += 1
                        End If
                    Next
                    'Check if Subset contained 1 of my symbols and 1 of opps
                    ' If so, then it is then we should not count it towards the total at all because the row itself cannot be a winner or blocker
                    If CurrentMy > 0 AndAlso CurrentOpp > 0 Then
                        CurrentMy = 0
                        CurrentOpp = 0
                    End If
                    'Increase the specified symbol values by the subset's total
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) += CurrentMy
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) += CurrentOpp
                

                    'If Position is a corner, we need to check the pertaining diagonal for symbols as well, if position is center, we need to check both diagonals
                    Dim DiagonalIndex As Integer = -1
                    If CurrentPos.Row = 2 AndAlso CurrentPos.Col = 2
                        'We need to check symbol count for both diagonals on the board
                        '''''''''''''''''''''
                        'Check first diagonal
                        CurrentSubset = ReturnBoardDia(1, pBoardState)
                        CurrentMy = 0
                        CurrentOpp = 0
                        For Index As Integer = 0 to 2
                            If CurrentSubset(Index) = pPlayer               'If it is my symbol
                                CurrentMy += 1
                            ElseIf CurrentSubset(Index) <> Player.NoPlayer  'If not my symbol, and not no player, then is opponent's symbol
                                CurrentOpp += 1
                            End If
                        Next
                        'Check if Subset contained 1 of my symbols and 1 of opps
                        ' If so, then it is then we should not count it towards the total at all because the row itself cannot be a winner or blocker
                        If CurrentMy > 0 AndAlso CurrentOpp > 0 Then
                            CurrentMy = 0
                            CurrentOpp = 0
                        End If
                        'Increase the specified symbol values by the subset's total
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) += CurrentMy
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) += CurrentOpp

                        ''''''''''''''''''''''
                        'Check second diagonal
                        CurrentSubset = ReturnBoardDia(2, pBoardState)
                        CurrentMy = 0
                        CurrentOpp = 0
                        For Index As Integer = 0 to 2
                            If CurrentSubset(Index) = pPlayer               'If it is my symbol
                                CurrentMy += 1
                            ElseIf CurrentSubset(Index) <> Player.NoPlayer  'If not my symbol, and not no player, then is opponent's symbol
                                CurrentOpp += 1
                            End If
                        Next
                        'Check if Subset contained 1 of my symbols and 1 of opps
                        ' If so, then it is then we should not count it towards the total at all because the row itself cannot be a winner or blocker
                        If CurrentMy > 0 AndAlso CurrentOpp > 0 Then
                            CurrentMy = 0
                            CurrentOpp = 0
                        End If
                        'Increase the specified symbol values by the subset's total
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) += CurrentMy
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) += CurrentOpp
                    ElseIf IsBoardPositionACorner(CurrentPos, DiagonalIndex)
                        CurrentSubset = ReturnBoardDia(DiagonalIndex, pBoardState)
                        CurrentMy = 0
                        CurrentOpp = 0
                        For Index As Integer = 0 to 2
                            If CurrentSubset(Index) = pPlayer               'If it is my symbol
                                CurrentMy += 1
                            ElseIf CurrentSubset(Index) <> Player.NoPlayer  'If not my symbol, and not no player, then is opponent's symbol
                                CurrentOpp += 1
                            End If
                        Next
                        'Check if Subset contained 1 of my symbols and 1 of opps
                        ' If so, then it is then we should not count it towards the total at all because the row itself cannot be a winner or blocker
                        If CurrentMy > 0 AndAlso CurrentOpp > 0 Then
                            CurrentMy = 0
                            CurrentOpp = 0
                        End If
                        'Increase the specified symbol values by the subset's total
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) += CurrentMy
                        PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) += CurrentOpp
                    End If
                Else
                    'This current position is already occupied by a player, set the values to -1 so this position is never chosen
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) = UNAVAILABLE_POS
                    PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) = UNAVAILABLE_POS
                End If
            Next
        Next


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Now we have all status values for each board position, go through each one to determine which position is best.
        ' the position that has the best values, is the best move to make, and therefore we should choose it as our move.
        Dim HighSymbols_Value As Integer = -1
        Dim CurrentSymbols_Value As Integer = -1
        Dim HighPosition As Position
        HighPosition.Row = -1
        HighPosition.Col = -1
        For Row As Integer = 1 to 3
            For Col As Integer = 1 to 3
                CurrentPos.Row = Row
                CurrentPos.Col = Col
                'Only consider positions that are available, therefore, skip if unavailable
                If PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY) <> UNAVAILABLE_POS AndAlso PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP) <> UNAVAILABLE_POS Then
                    CurrentSymbols_Value = 3 * Math.Pow(PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_MY),2) + 2 * PositionValues(CurrentPos.CalcSingleCoor, SYMBOLS_OPP)
                    

                    '### EXPERIMENTAL ####
                    '### Give the Center Square a higher priority...
                    If CurrentPos.Row = 2 AndAlso CurrentPos.Col = 2
                        CurrentSymbols_Value += 1
                    End If
                    

                    If CurrentSymbols_Value > HighSymbols_Value
                        HighSymbols_Value = CurrentSymbols_Value
                        HighPosition = CurrentPos
                    End If
                End If
            Next
        Next


        'Verify a move was selected, then return it
        If HighPosition.Row <> -1 AndAlso HighPosition.Col <> -1
            pPosition = HighPosition    'Set the referenced position parameter to the chosen position
            Return True                 'A move was chosen
        Else
            Return False                'No move was chosen
        End If
    End Function

    Private Function IsBoardPositionACorner(pPosition As Position, ByRef pDiagonalIndex As Integer) As Boolean
        Dim CoordValue As Integer = pPosition.CalcSingleCoor
        If CoordValue = 0 Or CoordValue = 2 Or CoordValue = 6 Or CoordValue = 8
            If CoordValue = 0 Or CoordValue = 8
                pDiagonalIndex = 1
            Else
                pDiagonalIndex = 2
            End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ReturnBoardRow(pRow As RowPosition, pBoardState() As Player) As Player()
        Dim SelectedRow(2) As Player
        Dim Pos As Position
        Pos.Row = pRow
        For i As Integer = 1 to SelectedRow.Count
            Pos.Col = i
            SelectedRow(i-1) = pBoardState(Pos.CalcSingleCoor)
        Next
        Return SelectedRow
    End Function

    Private Function ReturnBoardCol(pCol As ColPosition, pBoardState() As Player) As Player()
        Dim SelectedCol(2) As Player
        Dim Pos As Position
        Pos.Col = pCol
        For i As Integer = 1 to SelectedCol.Count
            Pos.Row = i
            SelectedCol(i-1) = pBoardState(Pos.CalcSingleCoor)
        Next
        Return SelectedCol
    End Function

    Private Function ReturnBoardDia(pDia As Integer, pBoardState() As Player) As Player()
        Dim SelectedDia(2) As Player
        If pDia = 1 Then
            SelectedDia(0) = pBoardState(0)
            SelectedDia(1) = pBoardState(4)
            SelectedDia(2) = pBoardState(8)
        ElseIf pDia = 2 Then
            SelectedDia(0) = pBoardState(2)
            SelectedDia(1) = pBoardState(4)
            SelectedDia(2) = pBoardState(6)
        Else
            Throw New Exception("Error - Invalid diagonal value inputted. pDia: " & pDia)
        End If
        Return SelectedDia
    End Function
End Class
