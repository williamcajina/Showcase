#pragma once
#include "World.h"
#include <vector>
#include "Character.h"
#include <ctime>
using namespace std;
class Game 
{
	World world;
	bool play = false;
	bool round;
	vector<Character*> Player1;
	vector<Character*> Player2;
	vector<Character*> temp;
public:
	~Game();
	void randomize(vector<Character*> &temp);
	void status(vector<Character*> temp);
	void render();
	void SetGame();
};