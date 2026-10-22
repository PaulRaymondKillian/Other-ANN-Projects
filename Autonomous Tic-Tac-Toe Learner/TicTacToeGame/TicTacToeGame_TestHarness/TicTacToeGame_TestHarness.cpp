// TicTacToeGame_TestHarness.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "..\TicTacToeGame\TicTacToeGame.cpp"

using namespace TicTacToeGame;
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	//Game* a = new Game('X','Y');
	//Game* a = new Game();
	Game* a = new Game('z','x','c');

	cout << a->GetBoardState() << endl;

	//a->PlayerPlaceSymbol(1,1,1);
	a->Player1Play(1,1);
	cout << a->GetBoardState() << endl;

	//a->PlayerPlaceSymbol(2,3,3);
	a->Player2Play(3,3);
	cout << a->GetBoardState() << endl;

	cout << "GameState: " << a->GetGameState() << endl;
	cout << "BoardFull: " << a->BoardFull() << endl;

	a->Player1Play(2,1);
	cout << a->GetBoardState() << endl;

	a->Player2Play(3,2);
	cout << a->GetBoardState() << endl;

	a->Player1Play(3,1);
	cout << a->GetBoardState() << endl;

	cout << "GameState: " << a->GetGameState() << endl;

	a->Player2Play(2,3);
	cout << a->GetBoardState() << endl;

	a->Player1Play(2,2);
	cout << a->GetBoardState() << endl;

	cout << "GameState: " << a->GetGameState() << endl;



	char b;
	cin >> b;
	//getline(cin, a);

	return 0;
}

