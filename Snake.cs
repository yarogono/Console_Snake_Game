using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }



    public class Snake
    {

        public int PosY { get; private set; }
        public int PosX { get; private set; }

        List<Pos> _points = new List<Pos>();

        public enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int _dir = (int)Dir.Up;

        public void Initialize(int posY, int posX)
        {
            PosY = posY;
            PosX = posX;
        }

        public void MoveSnake()
        {
            switch (_dir)
            {
                case (int)Dir.Up:
                    PosX--;
                    break;
                case (int)Dir.Down:
                    PosX++;
                    break;
                case (int)Dir.Left:
                    PosY--;
                    break;
                case (int)Dir.Right:
                    PosY++;
                    break;
            }
        }

        public void SwitchDirection(Dir dir)
        {
            _dir = (int)dir;
        }
    }
}
