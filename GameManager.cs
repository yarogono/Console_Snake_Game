using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class GameManager
    {
        private Map _map;
        private Snake _snake;

        private int _mapSize = 25;

        public void StartGame()
        {
            _map = new Map();
            _snake = new Snake();
            _map.Initialize(_mapSize);
            _snake.Initialize(10, 12);

            Console.CursorVisible = false;
 
            while (true)
            {
                if (IsGameOver())
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\nGame Over");
                    break;
                }

                ConsoleKeyPressCheck();

                Console.SetCursorPosition(0, 0);
                _snake.MoveSnake();
                _map.Render(_snake.Positions);

                Thread.Sleep(100);
            }
        }

        private void ConsoleKeyPressCheck()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();

                switch (consoleKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _snake.SwitchDirection(Snake.Dir.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        _snake.SwitchDirection(Snake.Dir.Right);
                        break;
                    case ConsoleKey.UpArrow:
                        _snake.SwitchDirection(Snake.Dir.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        _snake.SwitchDirection(Snake.Dir.Down);
                        break;
                    case ConsoleKey.Q:
                        return;
                }
            }
        }

        private bool IsGameOver()
        {
            if (_snake.Positions[0].Y <= -1 || _snake.Positions[0].Y >= _map.Size || _snake.Positions[0].X <= -1 || _snake.Positions[0].X >= _map.Size)
                return true;

            return false;
        }
    }
}
