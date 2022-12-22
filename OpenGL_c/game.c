#include "game.h"
#include <GL/glut.h>

#define _FULL_BODY_LENGTH 400 //sceneWidth * sceneHeight

const int margin = 5;
const int edge = 25;
const int sceneWidth = 20;
const int sceneHeight = 20;

struct Point body[_FULL_BODY_LENGTH];

int bodyHeadId = 0;
int bodyTailId = 0;
int x = 0;
int y = 0;
int width = 0;
int height = 0;
int score = 0;
int length = 5;

struct Point food;
struct Point player;

enum Way way = right;

void move(void)
{
	switch (way)
	{
	case left:
		player.x--;
		break;
	case right:
		player.x++;
		break;
	case up:
		player.y--;
		break;
	case down:
		player.y++;
		break;
	}
	if (player.y < 0) player.y = sceneHeight - 1;
	else if (player.y == sceneHeight) player.y = 0;

	if (player.x < 0) player.x = sceneWidth - 1;
	if (player.x == sceneWidth) player.x = 0;
}

void placeFood(void)
{
	food.x += 2;
	food.y += 1;
}

int belongsToPlayer(int x, int y)
{
	for (int i = bodyHeadId; i <= bodyTailId; i++)
	{
		if (x == body[i].x && y == body[i].y) return 1;
	}
	return 0;
}

void placePlayer(void)
{
	for (int i = 0; i < length; i++)
	{
		body[i].x = i;
		body[i].y = 0;
	}
	bodyHeadId = 0;
	bodyTailId = length - 1;
	player.x = 0;
	player.y = 0;
}

void increase(void)
{
	length++;
}

int checkCollision(void)
{
	if (player.x == food.x && player.y == food.y)
	{
		score++;
		increase();
		placeFood();
	}
}

void prepareWindow(void)
{
	glClearColor(0.0, 0.0, 0.0, 1.0);

	glPointSize(1.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();

	GLdouble left = -1.0 * margin;
	GLdouble right = width + margin;
	GLdouble top = -1.0 * margin;
	GLdouble bottom = height + margin;

	gluOrtho2D(left, right, bottom, top);
}

void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT);
	glBegin(GL_LINES);

	for (int i = 0; i < sceneWidth; i++)
	{
		for (int j = 0; j < sceneHeight; j++)
		{
			x = (edge + margin) * i;
			y = (edge + margin) * j;

			if (belongsToPlayer(i, j)) glColor3f(1.0, 1.0, 0.3);
			else if (i == food.x && j == food.y) glColor3f(1.0, 0.0, 0.0);
			else glColor3f(0.0, 1.0, 0.0);

			glVertex2i(x, y);
			glVertex2i(x + edge, y);

			glVertex2i(x + edge, y);
			glVertex2i(x + edge, y + edge);

			glVertex2i(x + edge, y + edge);
			glVertex2i(x, y + edge);

			glVertex2i(x, y + edge);
			glVertex2i(x, y);
		}
	}

	glEnd();
	glFlush();
}

void timerCallback(void)
{
	move();
	checkCollision();
	display();
	glutTimerFunc(500, timerCallback, 0);
}

void keyboardCallback(unsigned char symbol, int mouse_x, int mouse_y)
{
	if (symbol == 'w') way = up;
	else if (symbol == 's') way = down;
	else if (symbol == 'a') way = left;
	else if (symbol == 'd') way = right;
}

void init(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

	width = (edge + margin) * sceneWidth - margin;
	height = (edge + margin) * sceneHeight - margin;

	glutInitWindowSize(width, height);
	glutInitWindowPosition(0, 0);
	glutCreateWindow("Snake");

	prepareWindow();
	placeFood();
	placePlayer();


	glutKeyboardUpFunc(keyboardCallback);
	glutDisplayFunc(display);
	glutTimerFunc(500, timerCallback, 0);

	glutMainLoop();
}