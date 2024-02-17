#include "stdafx.h"
#include "Character.h"






void Character::update(World &_w)
{
	if (isMoving())
	{
		
		if (GetAsyncKeyState(VK_UP) && abs(moveDistance) < 1 && y>-1) 
		{
			_w.setOcc(true, x, y - abs(moveDistance));
			_w.setOcc(false, x, y + abs(moveDistance));
			moveDistance--;
			
			
			
		}
		if (GetAsyncKeyState(VK_DOWN) && abs(moveDistance) < 1  && y<6)
		{
			_w.setOcc(false, x, y - abs(moveDistance));
			moveDistance++;
			_w.setOcc(true, x, y + abs(moveDistance));
			
			
		}
		
	}
	
}

Character::Character(int _health, char * _sym, ConsoleColor _color, int _range, int _x, int _y, bool _alive)
{
	health = 20;
	x = _x;
	y = _y;
	setAlive(true);
	setMoving(true);
	color = _color;
	symbol = nullptr;
	delete[] symbol;
	symbol = _strdup(_sym);
	moveDistance = 1;
}

void Character::attack(Character _player, int min, int max)
{

}

void Character::render(World &w)
{
	w.highlight(true, color, x, y);
	Console::SetCursorPosition(x*w.Width() + w.Width() / 2 - 2, y*w.Height() + w.Height() / 2);
	Console::ForegroundColor(color);
	cout << symbol;
	Console::ForegroundColor(w.getColor());


}
Character::Character()
{

}


Character::~Character()
{
	delete[] symbol;
}
