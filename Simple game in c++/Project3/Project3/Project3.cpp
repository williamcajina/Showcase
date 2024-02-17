// Project3.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Game.h"

int main()
{

	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
	
	Game game;
	game.SetGame();
	
	
	while (true) {
		Console::Lock(true);
		Console::Clear();
		game.render();
		Console::Lock(false);
		Sleep(10);
	}
	

	/*vector<int> num;
	num.push_back(2);
	num.push_back(3);
	
	for (int i = 0; i < num.size(); i++)
	{
		cout << num[i] << "\n";

	}*/
    return 0;
}

