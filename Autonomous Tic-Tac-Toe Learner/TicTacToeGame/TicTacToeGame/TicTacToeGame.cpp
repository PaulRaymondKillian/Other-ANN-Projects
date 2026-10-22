// This is the main DLL file.

#include "stdafx.h"
#include "TicTacToeGame.h"

using namespace TicTacToeGame;

Game::Game(void)
{
	Init(' ', 'X', 'O');
}//Default Constructor

Game::Game(char Player1Symbol, char Player2Symbol)
{
	Init(' ', Player1Symbol, Player2Symbol);
}//Constructor

Game::Game(char EmptySpaceSymbol, char Player1Symbol, char Player2Symbol)
{
	Init(EmptySpaceSymbol, Player1Symbol, Player2Symbol);
}//Constructor

void Game::Init(char EmptySpaceSymbol, char Player1Symbol, char Player2Symbol)
{
	PlayerSymbol[0] = EmptySpaceSymbol;
	PlayerSymbol[1] = Player1Symbol;
	PlayerSymbol[2] = Player2Symbol;

	for(int i=0; i<9; i++)
		BoardState[i] = PlayerSymbol[0];
	BoardState[9] = '\0';

	Player1Turn = true;
	CurrentGameState = GameActive;
}//Init

char* Game::GetBoardState(void)
{
	//char* a = new char[9];
	//return a;
	return BoardState;
}

char Game::GetBoardPosition(int x, int y)
{
	string _METHOD = "GetBoardPosition(int x, int y)";

	if(x > 0 && x <= 3 && y > 0 && y <= 3)
		return GetBoardPosition(x*y);
	else
		throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Invalid (x,y) board position.";
}//GetBoardPosition(int x, int y)

char Game::GetBoardPosition(int xy)
{
	//Subtract 1 because c++ arrays are 0-indexed and we use the board as a 1-index on both axis
	return BoardState[xy-1];
}//GetBoardPosition(int xy)

void Game::SetBoardPosition(int x, int y, char Symbol)
{
	string _METHOD = "SetBoardPosition(int x, int y)";

	if(x > 0 && x <= 3 && y > 0 && y <= 3)
		SetBoardPosition(y+(3*(x-1)), Symbol);
	else
		throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Invalid (x,y) board position.";
}//SetBoardPosition(int x, int y, char Symbol)

void Game::SetBoardPosition(int xy, char Symbol)
{
	//Subtract 1 because c++ arrays are 0-indexed and we use the board as a 1-index on both axis
	BoardState[xy-1] = Symbol;
}//SetBoardPosition(int xy, char Symbol)

void Game::PlayerPlaceSymbol(int PlayerID, int x, int y)
{
	string _METHOD = "PlayerPlaceSymbol(int PlayerID, int x, int y)";

	if(CurrentGameState != GameActive)	//Ensure players can only place symbols on active games. Once a winner or draw is declared, no more placing symbols allowed!
		throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Unable to place player symbol. Game is not active!";

	if(PlayerID == CurrentPlayersTurn())
	{
		if(GetBoardPosition(x,y) == PlayerSymbol[0])
		{
			SetBoardPosition(x, y, PlayerSymbol[PlayerID]);
			SwitchCurrentPlayer();
		}
		else
			throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Symbol already exists at board position.";
	}
	else
		throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Invalid PlayerID. Not that player's turn.";
}//PlayerPlaceSymbol

bool Game::Player1Play(int x, int y)
{
	bool SuccessfulMove;
	try
	{
		PlayerPlaceSymbol(1, x, y);
		UpdateGameState();
		SuccessfulMove = true;
	}//try
	catch(...)
	{
		SuccessfulMove = false;
	}//catch

	return SuccessfulMove;
}//Player1Play

bool Game::Player2Play(int x, int y)
{
	bool SuccessfulMove;
	try
	{
		PlayerPlaceSymbol(2, x, y);
		UpdateGameState();
		SuccessfulMove = true;
	}//try
	catch(...)
	{
		SuccessfulMove = false;
	}//catch

	return SuccessfulMove;
}//Player2Play

int Game::CurrentPlayersTurn(void)
{
	if(Player1Turn)
		return 1;
	else
		return 2;
}//CurrentPlayersTurn

void Game::SwitchCurrentPlayer()
{
	Player1Turn = !Player1Turn;
}//SwitchCurrentPlayer

void Game::UpdateGameState(void)
{
	enum GameState CurrentState = GameActive;	//Let GameActive be the default game state to ensure variable is initialized to a value.
	
	//Check if there is a winner
	CurrentState = CheckForWinnerSimple();
	//CurrentState = CheckForWinnerSimple();
	if(CurrentState == Player1Winner || CurrentState == Player2Winner)
	{
		CurrentGameState = CurrentState;
		return;	//We know the current state of the game, now exit the method
	}

	//There is no winner yet, now find out if it is a draw. If the board is full, the game is a draw. If board is not full, the game is still active.

	//Check for empty positions on the board
	if(BoardFull())
		CurrentState = GameDraw;	//No winner, board is full, game ends in a draw
	else
		CurrentState = GameActive;	//No winner, board is not full, game is still going

	return;
}

bool Game::BoardFull(void)
{
	bool EmptySpotExists = false;
	for(int i=1; i<10; i++)
		if(GetBoardPosition(i) == PlayerSymbol[0])
		{
			EmptySpotExists = true;
			break;	//We know an empty spot exists, the board is therefore not full, break out of loop now, no need to continue.
		}

	return !EmptySpotExists;	//If an empty spot exists on the board, it is not full. Return the opposite of EmptySpotExists boolean.
	
}//BoardFull

GameState Game::CheckForWinnerSimple(void)
{

	int Winner = -1;
	GameState CurrentGameState = GameActive;

	//First Row
	if( BoardState[0] != PlayerSymbol[0] && (BoardState[0] == BoardState[1] && BoardState[1] == BoardState[2]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[0]);
		goto EndMethod;
	}

	//Second Row
	if( BoardState[3] != PlayerSymbol[0] && (BoardState[3] == BoardState[4] && BoardState[4] == BoardState[5]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[3]);
		goto EndMethod;
	}

	//Third Row
	if( BoardState[6] != PlayerSymbol[0] && (BoardState[6] == BoardState[7] && BoardState[7] == BoardState[8]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[6]);
		goto EndMethod;
	}

	//First Column
	if( BoardState[0] != PlayerSymbol[0] && (BoardState[0] == BoardState[3] && BoardState[3] == BoardState[6]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[0]);
		goto EndMethod;
	}

	//Second Column
	if( BoardState[1] != PlayerSymbol[0] && (BoardState[1] == BoardState[4] && BoardState[4] == BoardState[7]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[1]);
		goto EndMethod;
	}

	//Third Column
	if( BoardState[2] != PlayerSymbol[0] && (BoardState[2] == BoardState[5] && BoardState[5] == BoardState[8]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[2]);
		goto EndMethod;
	}

	//L-R Diagonal
	if( BoardState[0] != PlayerSymbol[0] && (BoardState[0] == BoardState[4] && BoardState[4] == BoardState[8]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[0]);
		goto EndMethod;
	}

	//R-L Diagonal
	//First Row
	if( BoardState[2] != PlayerSymbol[0] && (BoardState[2] == BoardState[4] && BoardState[4] == BoardState[6]))
	{
		Winner = ReturnPlayerNumberFromSymbol(BoardState[0]);
		goto EndMethod;
	}


EndMethod:
	if( Winner == Player1Winner)
		CurrentGameState = Player1Winner;
	else if( Winner == Player2Winner)
		CurrentGameState = Player2Winner;

	return CurrentGameState;
}//CheckForWinnerSimple

GameState Game::CheckForWinnerComplicated(void)
{
	string _METHOD = "CheckForWinner(void)";

	GameState CurrentGameState = GameActive;	//By default, lets set game to active. We are only checking if a winner exists, not if there is a draw...	

	//Note: There probably are better algorithms to solve for a winner on a TicTacToe board, but for now we will use this algorithm
	char CurrentSymbol;
	int PlayerSymbolCounter;


	//
	//Check for a winner on the horizonal squares
	//
	for(int i=0; i<=9; i=i+3)
	{
		PlayerSymbolCounter = 0;
		for(int k=0; k<3; k++)
		{
			if(k==0)
				CurrentSymbol = BoardState[k+i];

			if(CurrentSymbol == PlayerSymbol[0])	//If current symbol is the empty space symbol
				break;	//Exit loop, no need to continue if there exists an empty symbol in the row
			else
				if(CurrentSymbol == BoardState[k+i])
					PlayerSymbolCounter++;			//If symbols match, increase counter (even if first square of the row)
				else
					break;	//If symbols do not match, this row has no winner and we can break the loop right away
		}//for
		
		if(PlayerSymbolCounter == 3)	//If we had 3 matching symbols, we have a winner!
		{
			int Winner = ReturnPlayerNumberFromSymbol(CurrentSymbol);
			if( Winner == Player1Winner)
				CurrentGameState = Player1Winner;
			else if( Winner == Player2Winner)
				CurrentGameState = Player2Winner;
			else
				throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Invalid player winner calculated.";
			
			goto EndMethod;	//We have a winner, so skip to the end of the method
		}
		
	}//for


	//
	//Check for a winner on the vertical squares
	//
	for(int i=0; i<3; i++)
	{
		PlayerSymbolCounter = 0;
		for(int k=0; k<=9; k=k+3)
		{
			if(k==0)
				CurrentSymbol = BoardState[k+i];

			if(CurrentSymbol == PlayerSymbol[0])	//If current symbol is the empty space symbol
				break;	//Exit loop, no need to continue if there exists an empty symbol in the row
			else
				if(CurrentSymbol == BoardState[k+i])
					PlayerSymbolCounter++;			//If symbols match, increase counter (even if first square of the row)
				else
					break;	//If symbols do not match, this row has no winner and we can break the loop right away
		}//for
		
		if(PlayerSymbolCounter == 3)	//If we had 3 matching symbols, we have a winner!
		{
			int Winner = ReturnPlayerNumberFromSymbol(CurrentSymbol);
			if( Winner == Player1Winner)
				CurrentGameState = Player1Winner;
			else if( Winner == Player2Winner)
				CurrentGameState = Player2Winner;
			else
				throw "Error - Class: " + _CLASS + ", Method:" + _METHOD + ", Description: Invalid player winner calculated.";
			
			goto EndMethod;	//We have a winner, so skip to the end of the method
		}
		
	}//for


	//
	//Check for a winner on the diagonal squares
	//
	//XXXXXXXXXXXXXXX  NEED TO DO THIS PART STILL!!!! XXXXXXXXXXXXXXXXXXXXX

EndMethod:
	return CurrentGameState;
}//CheckforWinner


int Game::ReturnPlayerNumberFromSymbol(char pPlayerSymbol)
{
	int SymbolIndex = -1;
	for(int i=0; i<3; i++)
		if(PlayerSymbol[i] == pPlayerSymbol)
		{
			SymbolIndex = i;
			break;
		}
	
	return SymbolIndex;
}//ReturnPlayerNumberFromSymbol

GameState Game::GetGameState(void)
{
	return CurrentGameState;
}//GetGameState