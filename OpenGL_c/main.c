#include <stdio.h>
#include <GL/glut.h>
#include <math.h>

const int margin = 5;
const int edge = 25;
const int sceneWidth = 20;
const int sceneHeight = 15;

int x = 0;
int y = 0;

int width = 0;
int height = 0;

void prepareDimentions(void)
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glColor3f(0.0, 1.0, 0.0);

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

int main(int argc, char** argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

	width = (edge + margin) * sceneWidth - margin;
	height = (edge + margin) * sceneHeight - margin;

	glutInitWindowSize(width, height);
	glutInitWindowPosition(0, 0);
	glutCreateWindow("Snake");
	prepareDimentions();
	glutDisplayFunc(display);
	glutMainLoop();
}