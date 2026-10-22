Imports ITicTacToeAI
Imports TicTacToeGame

Public Class TicTacToeAIESCount : Implements ITicTacToeAI.ITicTacToeAI
    Private Const COUNTHIGH As Integer = 2
    Private Const COUNTLOW As Integer = 1

    Public Sub New()
    End Sub

    Function ChooseMove(pPlayer As Player, pBoardState() As Player) As Position Implements ITicTacToeAI.ITicTacToeAI.ChooseMove
        Return ChooseCountMove(pPlayer, pBoardState)
    End Function

    Private Function ChooseCountMove(pPlayer As Player, pBoardState() As Player) As Position
        Dim Move As Position
        Dim CurrentCount, HighRow, HighCol As Integer
        Dim HighCount As Integer = -100
        For Row As Integer = 1 to 3
            For Col As Integer = 1 to 3
                If pBoardState(CalcSingleCoord(Col, Row)) = Player.NoPlayer
                    CurrentCount = PositionCount(pPlayer, pBoardState, Col, Row)
                    If CurrentCount > HighCount
                        HighCount = CurrentCount
                        HighRow = Row
                        HighCol = Col
                    End If
                End If
            Next
        Next
        Move.Col = HighCol
        Move.Row = HighRow
        Return Move
    End Function

    Public Function CalcSingleCoord(pCol As ColPosition, pRow As RowPosition) As Integer
        If pCol > 0 AndAlso pCol <= 3 AndAlso pRow > 0 AndAlso pRow <= 3
            Return (pCol-1) + ((pRow-1)*3)
        Else
            Throw New Exception("Invalid Board Coordinates Entered. - Col: " & pCol & ", Row: " & pRow)
        End If
    End Function

    Private Function PositionCount(pPlayer As Player, pBoardState() As Player, Col As ColPosition, Row As RowPosition) As Integer
        Dim Pos = CalcSingleCoord(Col, Row)
        Dim Count As Integer = -100
        Select Pos
            Case 0
                Count = Pos0Val(pPlayer, pBoardState) + Pos1Val(pPlayer, pBoardState) + Pos2Val(pPlayer, pBoardState) _ 
                    + Pos3Val(pPlayer, pBoardState) + Pos6Val(pPlayer, pBoardState) _ 
                    + Pos4Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState)
            Case 1
                Count = Pos0Val(pPlayer, pBoardState) + Pos1Val(pPlayer, pBoardState) + Pos2Val(pPlayer, pBoardState) _ 
                    + Pos4Val(pPlayer, pBoardState) + Pos7Val(pPlayer, pBoardState)
            Case 2
                Count = Pos0Val(pPlayer, pBoardState) + Pos1Val(pPlayer, pBoardState) + Pos2Val(pPlayer, pBoardState) _ 
                    + Pos5Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState) _ 
                    + Pos4Val(pPlayer, pBoardState) + Pos6Val(pPlayer, pBoardState)
            Case 3
                Count = Pos3Val(pPlayer, pBoardState) + Pos4Val(pPlayer, pBoardState) + Pos5Val(pPlayer, pBoardState) _ 
                    + Pos0Val(pPlayer, pBoardState) + Pos6Val(pPlayer, pBoardState)
            Case 4
                Count = Pos3Val(pPlayer, pBoardState) + Pos4Val(pPlayer, pBoardState) + Pos5Val(pPlayer, pBoardState) _ 
                    + Pos1Val(pPlayer, pBoardState) + Pos7Val(pPlayer, pBoardState) _ 
                    + Pos0Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState) _
                    + Pos2Val(pPlayer, pBoardState) + Pos6Val(pPlayer, pBoardState)
            Case 5
                Count = Pos3Val(pPlayer, pBoardState) + Pos4Val(pPlayer, pBoardState) + Pos5Val(pPlayer, pBoardState) _ 
                    + Pos2Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState)
                Exit Select
            Case 6
                Count = Pos6Val(pPlayer, pBoardState) + Pos7Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState) _ 
                    + Pos0Val(pPlayer, pBoardState) + Pos3Val(pPlayer, pBoardState) _
                    + Pos4Val(pPlayer, pBoardState) + Pos2Val(pPlayer, pBoardState)
                Exit Select
            Case 7
                Count = Pos6Val(pPlayer, pBoardState) + Pos7Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState) _ 
                    + Pos1Val(pPlayer, pBoardState) + Pos4Val(pPlayer, pBoardState)
                Exit Select
            Case 8
                Count = Pos6Val(pPlayer, pBoardState) + Pos7Val(pPlayer, pBoardState) + Pos8Val(pPlayer, pBoardState) _ 
                    + Pos2Val(pPlayer, pBoardState) + Pos5Val(pPlayer, pBoardState) _
                    + Pos0Val(pPlayer, pBoardState) + Pos4Val(pPlayer, pBoardState)
                Exit Select
        End Select
        Return Count
    End Function

    Private Function Pos0Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetCornerOrMiddlePosValue(0, pPlayer, pBoardState)
    End Function

    Private Function Pos1Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetNonCornerPosValue(1, pPlayer, pBoardState)
    End Function

    Private Function Pos2Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetCornerOrMiddlePosValue(2, pPlayer, pBoardState)
    End Function

    Private Function Pos3Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetNonCornerPosValue(3, pPlayer, pBoardState)
    End Function

    Private Function Pos4Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetCornerOrMiddlePosValue(4, pPlayer, pBoardState)
    End Function

    Private Function Pos5Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetNonCornerPosValue(5, pPlayer, pBoardState)
    End Function

    Private Function Pos6Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetCornerOrMiddlePosValue(6, pPlayer, pBoardState)
    End Function

    Private Function Pos7Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetNonCornerPosValue(7, pPlayer, pBoardState)
    End Function

    Private Function Pos8Val(pPlayer As Player, pBoardState() As Player) As Integer
        Return GetCornerOrMiddlePosValue(8, pPlayer, pBoardState)
    End Function

    Private Function GetCornerOrMiddlePosValue(pPos As Integer, pPlayer As Player, pBoardState() As Player) As Integer
        If pBoardState(pPos) = pPlayer
            Return COUNTHIGH + 1                'Current player holds position
        ElseIf pBoardState(pPos) = Player.NoPlayer 
            Return COUNTHIGH                    'No player holds position
        Else
            Return -1                     'Opponent player holds position
        End If
    End Function

    Private Function GetNonCornerPosValue(pPos As Integer, pPlayer As Player, pBoardState() As Player) As Integer
        If pBoardState(pPos) = pPlayer
            Return COUNTLOW + 1                'Current player holds position
        ElseIf pBoardState(pPos) = Player.NoPlayer 
            Return COUNTLOW                    'No player holds position
        Else
            Return -1                     'Opponent player holds position
        End If
    End Function



End Class
