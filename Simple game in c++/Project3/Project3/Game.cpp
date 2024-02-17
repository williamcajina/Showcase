#include "stdafx.h"
#include "Game.h"


Game::~Game()
{
	for (int i = 0; i < 3; i++)
	{
		delete Player1[i];
		delete Player2[i];
		
	}
}

void Game::randomize(vector<Character*> &temp)
{
	Character* tem;
	srand(time(0));
	for (int i = 0; i < temp.size(); i++)
	{
		int ran = rand() % temp.size();
		tem = temp[i];
		temp[i] = temp[ran];
		temp[ran] = tem;


	}
}

void Game::status(vector<Character*> temp )
{
	Console::SetCursorPosition(0, 1 + world.Height() * 6);

	cout << "Player 1: ";
	for (int i = 0; i < Player1.size(); i++)
	{

		Console::ForegroundColor(Player1[i]->getColor());
		cout << Player1[i]->GetSymbol();
		Console::ForegroundColor(Gray);
		cout << ":" << Player1[i]->getHealth() << "/20 ";
	}

	cout << "\n\n";

	cout << "Player 2: ";
	for (int i = 0; i < Player2.size(); i++)
	{

		Console::ForegroundColor(Player2[i]->getColor());
		cout << Player2[i]->GetSymbol();
		Console::ForegroundColor(Gray);
		cout << ":" << Player2[i]->getHealth() << "/20 ";
	}
	

	Console::SetCursorPosition(world.Width() * 8, 1 );
	cout << "Turn order ";
	for (int i = temp.size()-1; i >-1 ; i--)
	{
		Console::SetCursorPosition(world.Width() * 8, 2*temp.size()-2*i+1);
		Console::ForegroundColor(temp[i]->getColor());
		if (i == temp.size()-1)
		{
			cout << temp[i]->GetSymbol();

			Console::ForegroundColor(Gray);
			cout << " <-- ";
		}
		else
			cout << temp[i]->GetSymbol();
		
		Console::ForegroundColor(Gray);
	}
	

}

void Game::render()
{


	world.render();
	for (int i = 0; i < 3; i++)
	{
		Player1[i]->render(world);
		Player2[i]->render(world);
	}


	if (round == false) {
		randomize(temp);
		round = true;
	}
	
	status(temp);
	
	Character* turn = temp[temp.size() - 1];
	
	turn->update(world);
	
	

	

	

}

void Game::SetGame()
{
	round = false;

	Player1.push_back(new Character(20, "W", Red, 1, 1, 0, true));
	Player1.push_back(new Character(20, "A", Red, 4, 0, 0, true));
	Player1.push_back(new Character(20, "S", Red, 2, 0, 1, true));

	Player2.push_back(new Character(20, "W", Blue, 1, 4, 5, true));
	Player2.push_back(new Character(20, "A", Blue, 4, 5, 5, true));
	Player2.push_back(new Character(20, "S", Blue, 2, 5, 4, true));

	for (int i = 0; i < Player1.size(); i++)
	{
		temp.push_back(Player1[i]);
		temp.push_back(Player2[i]);
	}
}


