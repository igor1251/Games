using System;

namespace Snake_cli.ndp48
{
    internal class Program
    {
        const sbyte _DEFAULT_TIMER_TICK_INTERVAL = 100;
        const sbyte _DEFAULT_WIDTH = 15;
        const sbyte _DEFAULT_HEIGHT = 15;

        static Game game;

        static void Main(string[] args)
        {
            Console.Title = "Snake game";

            Console.Write($"Game field height: ({_DEFAULT_HEIGHT}) ");
            string buf = Console.ReadLine();
            sbyte height = string.IsNullOrEmpty(buf) ? _DEFAULT_HEIGHT : sbyte.Parse(buf);

            Console.Write($"Game field width: ({_DEFAULT_WIDTH}) ");
            buf = Console.ReadLine();
            sbyte width = string.IsNullOrEmpty(buf) ? _DEFAULT_WIDTH : sbyte.Parse(buf);

            Console.Write($"Game speed: ({_DEFAULT_TIMER_TICK_INTERVAL}) ");
            buf = Console.ReadLine();
            sbyte tickInterval = string.IsNullOrEmpty(buf) ? _DEFAULT_TIMER_TICK_INTERVAL : sbyte.Parse(buf);

            Console.Clear();

            game = new Game(width, height);

            DateTime timestamp = DateTime.Now;
            sbyte millisLeft = 0;

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

                millisLeft = (sbyte)(DateTime.Now - timestamp).Milliseconds;
                if (millisLeft > tickInterval)
                {
                    game.Redraw();
                    millisLeft = 0;
                    timestamp = DateTime.Now;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine($"\n\nScore: {game.GetScore()}");
            Console.ReadKey();
        }
    }
}
