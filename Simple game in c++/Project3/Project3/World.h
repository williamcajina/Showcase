#pragma once

#include "stdafx.h"
class World 
{
	int cellW = 8;
	int cellH = 6;
	bool world[6][6];
	ConsoleColor color;

public:
	
	ConsoleColor getColor() { return color; }
	int Width() { return cellW; }
	int Height() { return cellH; }
	void setOcc(bool occ, int _x, int _y) { world[_x][_y] = occ; }
	void highlight(bool pos,ConsoleColor _col, int _x, int _y);
	
	void render();
	World();

};