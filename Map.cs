using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Map
    {
        const char CIRCLE = '\u25cf';

        public int Size { get; private set; }

        Snake _snake;

        public void Initialize(int size, Snake snake)
        {
            Size = size;
            _snake = snake;
        }

        public void Render()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_snake.PosY == j && _snake.PosX == i)
                            Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }

    }
}
