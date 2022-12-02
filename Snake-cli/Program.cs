namespace Snake_cli
{
    internal class Program
    {
        const int _DEFAULT_TIMER_TICK_INTERVAL = 100;
        const int _DEFAULT_WIDTH = 15;
        const int _DEFAULT_HEIGHT = 15;

        static Game game;

        static void Main(string[] args)
        {
            Console.Write($"Game field height: ({_DEFAULT_HEIGHT}) ");
            string? buf = Console.ReadLine();
            int height = string.IsNullOrEmpty(buf) ? _DEFAULT_HEIGHT : int.Parse(buf);
            
            Console.Write($"Game field width: ({_DEFAULT_WIDTH}) ");
            buf = Console.ReadLine();
            int width = string.IsNullOrEmpty(buf) ? _DEFAULT_WIDTH : int.Parse(buf);
            
            Console.Write($"Game speed: ({_DEFAULT_TIMER_TICK_INTERVAL}) ");
            buf = Console.ReadLine();
            int tickInterval = string.IsNullOrEmpty(buf) ? _DEFAULT_TIMER_TICK_INTERVAL : int.Parse(buf);

            Console.Clear();
            Console.CursorVisible = false;
            game = new Game(width, height, false);

            DateTime timestamp = DateTime.Now;
            int millisLeft = 0;

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
                
                millisLeft = (DateTime.Now - timestamp).Milliseconds;  
                if (millisLeft > tickInterval)
                {
                    game.Redraw();
                    millisLeft = 0;
                    timestamp = DateTime.Now;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine($"\n\nGame over. Score: {game.GetScore()}");
#if RELEASE
            Console.ReadKey();
#endif
        }
    }
}