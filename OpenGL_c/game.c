#include "game.h"
#include <GL/glut.h>

const int margin = 5;
const int edge = 25;
const int sceneWidth = 20;
const int sceneHeight = 15;

int x = 0;
int y = 0;

int player_x = 0; player_y = 0;
int food_x = 0; food_y = 0;

int score = 0;

int width = 0;
int height = 0;

unsigned int elapsed = 1000;

enum Way way = right;

void move(void)
{
	switch (way)
	{
	case left:
		player_x--;
		break;
	case right:
		player_x++;
		break;
	case up:
		player_y--;
		break;
	case down:
		player_y++;
		break;
	}
	if (player_y < 0) player_y = sceneHeight - 1;
	else if (player_y == sceneHeight) player_y = 0;

	if (player_x < 0) player_x = sceneWidth - 1;
	if (player_x == sceneWidth) player_x = 0;
}

void placeFood(void)
{
	food_x += 2;
	food_y += 1;
}

void increase(void)
{

}

int checkCollision(void)
{
	if (player_x == food_x && player_y == food_y)
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

			if (i == player_x && j == player_y) glColor3f(1.0, 1.0, 0.3);
			else if (i == food_x && j == food_y) glColor3f(1.0, 0.0, 0.0);
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

	glutKeyboardUpFunc(keyboardCallback);
	glutDisplayFunc(display);
	glutTimerFunc(500, timerCallback, 0);

	glutMainLoop();
}