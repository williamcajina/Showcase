#pragma once
#include "World.h"
class Character
{
	

	int health;
	char* symbol;
	ConsoleColor color;
	int range;
	int x;
	int y;
	bool Alive;
	bool Attacking;
	bool Moving;
	int moveDistance;

public:
	bool isAlive() { return Alive;}
	bool isAttacking(){ return Attacking; }
	bool isMoving() { return Moving; }

	void setAlive(bool _a) { Alive = _a; }
	void setAttack(bool _b) { Attacking = _b; }
	void setMoving(bool _c) { Moving = _c; }

	void update(World &_w);
	void setPosition(int _x, int _y) { x = _x; y = _y; }
	int getHealth() { return health; }
	ConsoleColor getColor() { return color; }
	const char* const GetSymbol() const { return symbol; }
	Character(int _health, char* _sym, ConsoleColor _color, int _range, int _x, int _y, bool _alive);
	void attack(Character _player,int min, int max);
	void render(World& w);
	
	Character();

	~Character();
};

