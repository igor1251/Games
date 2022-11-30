namespace Snake_cli
{
    internal class Program
    {
        const int _DEFAULT_TIMER_TICK_INTERVAL = 150;
        const int _DEFAULT_WIDTH = 15;
        const int _DEFAULT_HEIGHT = 15;

        static GameField gameField = new();
        static Way way = Way.Down;

        static void MovePlayer()
        {
            switch (way)
            {
                case Way.Up:
                    gameField.MoveUp();
                    break;
                case Way.Down:
                    gameField.MoveDown();
                    break;
                case Way.Left:
                    gameField.MoveLeft();
                    break;
                case Way.Right:
                    gameField.MoveRight();
                    break;
            }
        }

        static void Main(string[] args)
        {
            Console.Write($"Game field height: ({_DEFAULT_HEIGHT}) ");
            string? buf = Console.ReadLine();
            gameField.Height = string.IsNullOrEmpty(buf) ? _DEFAULT_HEIGHT : int.Parse(buf);
            
            Console.Write($"Game field width: ({_DEFAULT_WIDTH}) ");
            buf = Console.ReadLine();
            gameField.Width = string.IsNullOrEmpty(buf) ? _DEFAULT_WIDTH : int.Parse(buf);
            
            Console.Write($"Game speed: ({_DEFAULT_TIMER_TICK_INTERVAL}) ");
            buf = Console.ReadLine();
            int tickInterval = string.IsNullOrEmpty(buf) ? _DEFAULT_TIMER_TICK_INTERVAL : int.Parse(buf);

            Console.Clear();
            Console.CursorVisible = false;
            gameField.Init();

            DateTime timestamp = DateTime.Now;
            int millisLeft = 0;
            
            while (!gameField.GameOver())
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            way = Way.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            way = Way.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            way = Way.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            way = Way.Right;
                            break;
                    }
                }
                
                millisLeft = (DateTime.Now - timestamp).Milliseconds;  
                if (millisLeft > tickInterval)
                {
                    MovePlayer();
                    gameField.Update();
                    millisLeft = 0;
                    timestamp = DateTime.Now;
                }
            }
            Console.ResetColor();
            Console.WriteLine($"\n\nGame over. Score: {gameField.Score}");
#if RELEASE
            Console.ReadKey();
#endif
        }
    }
}