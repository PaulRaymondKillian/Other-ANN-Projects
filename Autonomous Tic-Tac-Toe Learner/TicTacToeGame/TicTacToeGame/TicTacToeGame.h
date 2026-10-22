// TicTacToeGame.h

#include <iostream>
#include <string>

#pragma once

using namespace std;

namespace TicTacToeGame {

	enum GameState
	{
		GameActive = -1,
		GameDraw = 0,
		Player1Winner= 1,
		Player2Winner = 2
	};

	class Game
	{
		// TODO: Add your methods for this class here.
		public:
			//Public Methods
			Game(void);
			Game(char Player1Symbol, char Player2Symbol);
			Game(char EmptySpaceSymbol, char Player1Symbol, char Player2Symbol);
			char* GetBoardState(void);
			char GetBoardPosition(int x, int y);
			char GetBoardPosition(int xy);
			bool Player1Play(int x, int y);
			bool Player2Play(int x, int y);
			int CurrentPlayersTurn(void);
			GameState GetGameState(void);
			
			//Move these methods to PRIVATE later...
			void UpdateGameState(void);
			bool BoardFull(void);
			GameState CheckForWinnerComplicated(void);
			GameState CheckForWinnerSimple(void);

			//Public Variables

		private:
			//Private Methods
			void Init(char EmptySpaceSymbol, char Player1Symbol, char Player2Symbol);
			void SetBoardPosition(int x, int y, char Symbol);
			void SetBoardPosition(int xy, char Symbol);
			void PlayerPlaceSymbol(int PlayerID, int x, int y);
			void SwitchCurrentPlayer();

			//Private Variables
			string static _CLASS;
			char PlayerSymbol[3];
			char BoardState[10];	//3x3 grid using a 0-index, the last position holding a null-terminating character
			bool Player1Turn;
			int ReturnPlayerNumberFromSymbol(char PlayerSymbol);
			GameState CurrentGameState;
	};
	string Game::_CLASS = "TicTacToeGame::Game";
}
