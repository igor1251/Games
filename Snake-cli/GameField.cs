using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Snake_cli
{
    internal struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Game
    {
        Way way;

        Point food;
        Point nextPlayerStep;

        List<Point> player;

        int length = 5;
        int score = 0;

        bool colored = true;

        readonly char terrainChar = ' ';
        readonly char playerChar = '*';
        readonly char foodChar = '&';
        readonly char borderChar = '^';
        readonly ConsoleColor playerColor = ConsoleColor.Green;
        readonly ConsoleColor foodColor = ConsoleColor.Red;
        readonly ConsoleColor terrainColor = ConsoleColor.Black;
        readonly ConsoleColor borderColor = ConsoleColor.Blue;
        readonly int padding = 1;
        readonly Point dimentions;
        readonly Random foodPositionRandomizer = new();

        public Game(int width, int height, bool coloredOutput = true)
        {
            dimentions = new Point(width, height);
            player = new List<Point>(length);
            colored = coloredOutput;
            SetupConsoleWindow();
            CreatePlayer();
            CreateFood();
            DrawBorders();
        }

        void SetupConsoleWindow()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(dimentions.X + padding * 2, dimentions.Y + padding * 2);
        }

        bool BelongsToPlayer(int x, int y)
        {
            foreach (var point in player)
            {
                if (point.X == x && point.Y == y) return true;
            }
            return false;
        }

        bool BelongsToFood(int x, int y)
        {
            return food.X == x && food.Y == y;
        }


        void CreatePlayer()
        {
            for (int i = 0; i < length; i++)
            {
                player.Add(new Point(i, 0));
            }
        }

        void CreateFood()
        {
            int x, y;
            do
            {
                x = foodPositionRandomizer.Next(0, dimentions.X);
                y = foodPositionRandomizer.Next(0, dimentions.Y);
            }
            while (BelongsToPlayer(x, y));
            food = new Point(x, y);
        }

        void IncreasePlayer()
        {
            length++;
            score++;
        }

        void CalculateNextPlayerStep()
        {
            switch (way)
            {
                case Way.Left:
                    nextPlayerStep.X--;
                    break;
                case Way.Right:
                    nextPlayerStep.X++;
                    break;
                case Way.Up:
                    nextPlayerStep.Y--;
                    break;
                case Way.Down:
                    nextPlayerStep.Y++;
                    break;
            }
        }

        void AdjustPlayerStep()
        {
            if (nextPlayerStep.X < 0) nextPlayerStep.X = dimentions.X - 1;
            else if (nextPlayerStep.X == dimentions.X) nextPlayerStep.X = 0;
            if (nextPlayerStep.Y < 0) nextPlayerStep.Y = dimentions.Y - 1;
            else if (nextPlayerStep.Y == dimentions.Y) nextPlayerStep.Y = 0;
        }

        void Move()
        {
            CalculateNextPlayerStep();
            AdjustPlayerStep();
            player.Add(nextPlayerStep);
            if (player.Count > length) player.RemoveAt(0);
        }

        Point GetPlayerHead()
        {
            return player.Last();
        }

        void CheckCollision()
        {
            var head = GetPlayerHead();
            if (head.X == food.X && head.Y == food.Y)
            {
                IncreasePlayer();
                CreateFood();
            }
        }

        bool EatsHimself()
        {
            var head = GetPlayerHead();
            return player.Where(item => item.X == head.X && item.Y == head.Y).Count() > 1;
        }

        void DrawBorders()
        {
            if (colored) Console.BackgroundColor = borderColor;
            for (int i = 0; i <= dimentions.Y + padding; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(colored ? ' ' : borderChar);
                Console.SetCursorPosition(i, dimentions.X + padding);
                Console.Write(colored ? ' ' : borderChar);
            }
            for (int i = 0; i <= dimentions.X + padding; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(colored ? ' ' : borderChar);
                Console.SetCursorPosition(dimentions.Y + padding, i);
                Console.Write(colored ? ' ' : borderChar);
            }
        }

        public void Redraw()
        {
            Move();
            CheckCollision();
            for (int i = 0; i < dimentions.Y; i++)
            {
                for (int j = 0; j < dimentions.X; j++)
                {
                    Console.SetCursorPosition(i + padding, j + padding);
                    if (colored)
                    {
                        if (BelongsToFood(i, j)) Console.BackgroundColor = foodColor;
                        else if (BelongsToPlayer(i, j)) Console.BackgroundColor = playerColor;
                        else Console.BackgroundColor = terrainColor;
                        Console.Write(' ');
                    }
                    else
                    {
                        if (BelongsToFood(i, j)) Console.Write(foodChar);
                        else if (BelongsToPlayer(i, j)) Console.Write(playerChar);
                        else Console.Write(terrainChar);
                    }
                }
            }
            
        }

        public void MoveLeft()
        {
            way = Way.Left;
        }

        public void MoveRight()
        {
            way = Way.Right;
        }

        public void MoveUp()
        {
            way = Way.Up;
        }

        public void MoveDown()
        {
            way = Way.Down;
        }

        public int GetScore()
        {
            return score;
        }

        public bool GameOver()
        {
            return EatsHimself();
        }
    }
}
