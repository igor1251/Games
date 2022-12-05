using System;

namespace Snake_cli
{
    internal class Program
    {
        const ushort _DEFAULT_TIMER_TICK_INTERVAL = 8000;
        const byte _DEFAULT_WIDTH = 40;
        const byte _DEFAULT_HEIGHT = 40;

        static Game game;

        static void Main()
        {
            Console.Title = "Snake game";

            Console.Write($"Game field height: ({_DEFAULT_HEIGHT}) ");
            string buf = Console.ReadLine();
            byte height = string.IsNullOrEmpty(buf) ? _DEFAULT_HEIGHT : byte.Parse(buf);

            Console.Write($"Game field width: ({_DEFAULT_WIDTH}) ");
            buf = Console.ReadLine();
            byte width = string.IsNullOrEmpty(buf) ? _DEFAULT_WIDTH : byte.Parse(buf);

            Console.Write($"Game speed: ({_DEFAULT_TIMER_TICK_INTERVAL}) ");
            buf = Console.ReadLine();
            ushort tickInterval = string.IsNullOrEmpty(buf) ? _DEFAULT_TIMER_TICK_INTERVAL : ushort.Parse(buf);

            Console.Clear();

            game = new Game(width, height);

            ushort cyclesLeft = 0;

            while (!game.GameOver())
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            game.MoveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            game.MoveDown();
                            break;
                        case ConsoleKey.LeftArrow:
                            game.MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            game.MoveRight();
                            break;
                    }
                }
                cyclesLeft++;
                if (cyclesLeft > tickInterval)
                {
                    game.Redraw();
                    cyclesLeft = 0;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine($"\n\nScore: {game.GetScore()}");
            Console.Beep(120, 400);
            Console.ReadKey();
        }
    }
}
