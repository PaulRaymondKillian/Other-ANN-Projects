// TicTacToeGUI.cpp : main project file.

#include "stdafx.h"
#include "Form1.h"
//#include "TicTacToeGame.h"

using namespace TicTacToeGUI;
//using namespace TicTacToeGame;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	//TicTacToeGame::Game* a;// = new Game('z','x','c');
	TicTacToe

	// Create the main window and run it
	Application::Run(gcnew Form1());
	return 0;
}
