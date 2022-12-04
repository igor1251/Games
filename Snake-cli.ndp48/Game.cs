using System;
using System.Collections.Generic;

namespace Snake_cli.ndp48
{
    internal enum Way
    {
        Left,
        Right,
        Up,
        Down,
    }

    internal struct Point
    {
        public byte X;
        public byte Y;
        public Point(byte x, byte y)
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

        byte length = 5;
        byte score = 0;

        const ConsoleColor playerColor = ConsoleColor.Green;
        const ConsoleColor foodColor = ConsoleColor.Red;
        const ConsoleColor terrainColor = ConsoleColor.Black;
        const ConsoleColor borderColor = ConsoleColor.Blue;

        const char terrainChar = ' ';
        const char playerChar = '*';
        const char foodChar = '&';
        const char borderChar = '^';

        readonly bool colored = true;
        readonly byte padding = 1;
        readonly Point dimentions;
        readonly Random foodPositionRandomizer = new Random();

        public Game(byte width, byte height, bool coloredOutput = true)
        {
            dimentions = new Point(width, height);
            player = new List<Point>(width * height);
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

        bool BelongsToPlayer(byte x, byte y)
        {
            foreach (var point in player)
            {
                if (point.X == x && point.Y == y) return true;
            }
            return false;
        }

        void CreatePlayer()
        {
            for (byte i = 0; i < length; i++)
            {
                player.Add(new Point(i, 0));
            }
        }

        void CreateFood()
        {
            byte x, y;
            do
            {
                x = (byte)foodPositionRandomizer.Next(0, dimentions.X);
                y = (byte)foodPositionRandomizer.Next(0, dimentions.Y);
            }
            while (BelongsToPlayer(x, y));
            food = new Point(x, y);
        }

        void IncreasePlayer()
        {
            length++;
            score++;
            Console.Beep();
        }

        void CalculateNextPlayerStep()
        {
            switch (way)
            {
                case Way.Left:
                    if (nextPlayerStep.X > 0) nextPlayerStep.X--;
                    else nextPlayerStep.X = (byte)(dimentions.X - padding);
                    break;
                case Way.Right:
                    if (nextPlayerStep.X < dimentions.X - padding) nextPlayerStep.X++;
                    else nextPlayerStep.X = 0;
                    break;
                case Way.Up:
                    if (nextPlayerStep.Y > 0) nextPlayerStep.Y--;
                    else nextPlayerStep.Y = (byte)(dimentions.Y - padding);
                    break;
                case Way.Down:
                    if (nextPlayerStep.Y < dimentions.Y - padding) nextPlayerStep.Y++;
                    else nextPlayerStep.Y = 0;
                    break;
            }
        }

        void Move()
        {
            CalculateNextPlayerStep();
            player.Add(nextPlayerStep);
            if (player.Count > length) player.RemoveAt(0);
        }

        void CheckCollision()
        {
            var head = player[player.Count - 1];
            if (head.X == food.X && head.Y == food.Y)
            {
                IncreasePlayer();
                CreateFood();
            }
        }

        bool EatsHimself()
        {
            var head = player[player.Count - 1];
            for (int i = 0; i < player.Count - 2; i++)
            {
                if (head.X == player[i].X && head.Y == player[i].Y) return true;
            }
            return false;
        }

        void DrawBorders()
        {
            if (colored) Console.BackgroundColor = borderColor;
            for (int i = 0; i <= dimentions.Y + padding; i++)
            {
                PaintBlock(i, 0, borderColor, borderChar);
                PaintBlock(i, dimentions.X + padding, borderColor, borderChar);
            }
            for (int i = 0; i <= dimentions.X + padding; i++)
            {
                PaintBlock(0, i, borderColor, borderChar);
                PaintBlock(dimentions.Y + padding, i, borderColor, borderChar);
            }
        }

        void DisableUnusedBlocks()
        {
            PaintBlock(player[0].X + padding, player[0].Y + padding);
            PaintBlock(food.X + padding, food.Y + padding);
        }

        void PaintBlock(int x, int y, ConsoleColor blockColor = terrainColor, char blockChar = terrainChar)
        {
            Console.SetCursorPosition(x, y);
            if (colored) Console.BackgroundColor = blockColor;
            Console.Write(colored ? ' ' : blockChar);
        }

        void DrawFoodAndPlayer()
        {
            PaintBlock(player[player.Count - 1].X + padding, player[player.Count - 1].Y + padding, playerColor, playerChar);
            PaintBlock(food.X + padding, food.Y + padding, foodColor, foodChar);
        }

        public void Redraw()
        {
            DisableUnusedBlocks();
            Move();
            CheckCollision();
            DrawFoodAndPlayer();
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
