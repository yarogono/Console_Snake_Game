using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Map
    {
        const char CIRCLE = '\u25cf';

        public int Size { get; private set; }

        public void Initialize(int size)
        {
            Size = size;
        }

        public void Render(List<Pos> snakePoints, Pos foodPos)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (foodPos.Y == y && foodPos.X == x)
                        Console.ForegroundColor = ConsoleColor.Red;

                    foreach (Pos pos in snakePoints)
                        if (pos.Y == y && pos.X == x)
                            Console.ForegroundColor = ConsoleColor.Green;

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }
    }
}
