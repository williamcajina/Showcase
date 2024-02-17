#include "stdafx.h"
#include "World.h"

void World::highlight(bool pos, ConsoleColor _col, int _x, int _y)
{
	if (pos == true)
	{
		world[_x][_y] = true;
		Console::ForegroundColor(_col);
		Console::DrawBox(_x*cellW, _y*cellH, cellW, cellH, 0);
		Console::ForegroundColor(color);
	}
	else
	{
		world[_x][_y] = false;
		Console::ForegroundColor(color);
		Console::DrawBox(_x*cellW, _y*cellH, cellW, cellH, 0);
		Console::ForegroundColor(color);

	}
}

void World::render()
{
	for (int x = 0; x < 6; x++)
	{
		for (int y = 0; y < 6; y++)
		{
			if (world[x][y] == true) {
				Console::ForegroundColor(Yellow);
				Console::DrawBox(x*cellW, y*cellH, cellW, cellH, 0);
				Console::ForegroundColor(color);
			}
			else
				Console::DrawBox(x*cellW, y*cellH, cellW, cellH, 0);
		}
	}
}

World::World()
{
	
	color = Gray;
	Console::ForegroundColor(color);
	Console::SetWindowSize(90, 60);
	Console::SetBufferSize(90, 60);

}
