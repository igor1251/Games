#pragma once
#include <cstdbool>

enum Way
{
	left,
	right,
	up,
	down
};

struct Point
{
	int x;
	int y;
};

void createGame(void);
void createPlayer(void);
void createFood(void);
bool belongsToPlayer(int x, int y);
