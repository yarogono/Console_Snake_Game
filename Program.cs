using System.Diagnostics.Tracing;
using System.Security;

namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();
            Snake snake = new Snake();
            map.Initialize(25, snake);
            snake.Initialize(5, 5);

            Console.CursorVisible = false;
            ConsoleKeyInfo consoleKey;


            Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    consoleKey = Console.ReadKey();


                    switch (consoleKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            snake.SwitchDirection(Snake.Dir.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            snake.SwitchDirection(Snake.Dir.Right);
                            break;
                        case ConsoleKey.UpArrow:
                            snake.SwitchDirection(Snake.Dir.Up);
                            break;
                        case ConsoleKey.DownArrow:
                            snake.SwitchDirection(Snake.Dir.Down);
                            break;
                        case ConsoleKey.Q:
                            return;
                    }
                }
            });

            while (true)
            {


                Console.SetCursorPosition(0, 0);
                map.Render();
                snake.MoveSnake();

                Thread.Sleep(200);
            }
        }
    }
}