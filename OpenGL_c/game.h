#pragma once

enum Way
{
	left = 1,
	right = 2,
	up = 3,
	down = 4
};

struct Point
{
	int x;
	int y;
};

void increase(void);
int getScore(void);
int checkCollision(void);
int belongsToPlayer(int x, int y);
void placeFood(void);
void placePlayer(void);
void move(void);
void init(int argc, char** argv);
void prepareWindow(void);
void display(void);
void timerCallback(void);
void keyboardCallback(unsigned char symbol, int x, int y);