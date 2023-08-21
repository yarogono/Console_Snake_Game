using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    public class Snake
    {
        public List<Pos> Positions { get { return _positions; } }
        private List<Pos> _positions = new List<Pos>();

        public int PosY { get; private set; }
        public int PosX { get; private set; }

        private int _headPosY;
        private int _headPosX;

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
            _positions.Add(new Pos(PosY, PosX));
        }

        public void MoveSnake()
        {
            _headPosY = _positions[0].Y;
            _headPosX = _positions[0].X;

            switch (_dir)
            {
                case (int)Dir.Up:
                    _positions[0].Y--;
                    MoveSnakeBody();
                    break;
                case (int)Dir.Down:
                    _positions[0].Y++;
                    MoveSnakeBody();
                    break;
                case (int)Dir.Left:
                    _positions[0].X--;
                    MoveSnakeBody();
                    break;
                case (int)Dir.Right:
                    _positions[0].X++;
                    MoveSnakeBody();
                    break;
            }
        }

        private void MoveSnakeBody()
        {
            if (_positions.Count > 1)
            {
                Pos snakeNewBody = new Pos(_headPosY, _headPosX);
                _positions.Insert(1, snakeNewBody);
                _positions.RemoveAt(_positions.Count - 1);
            }
        }

        public void SwitchDirection(Dir dir)
        {
            if ((int)Dir.Left == _dir && dir == Dir.Right)
                return;
            else if ((int)Dir.Right == _dir && dir == Dir.Left)
                return;
            else if ((int)Dir.Up == _dir && dir == Dir.Down)
                return;
            else if ((int)Dir.Down == _dir && dir == Dir.Up)
                return;

            _dir = (int)dir;
        }
        

        public void EatFood(Pos foodPos)
        {
            Pos snakeBodyPos = new Pos(foodPos.Y, foodPos.X);
            _positions.Add(snakeBodyPos);
        }
    }
}
