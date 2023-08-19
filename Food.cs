using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    public class Food
    {
        public Pos Position { get { return _position; } }
        private Pos _position;

        private Random _random = new Random();

        public void CreateFood(int posY, int posX)
        {
            _position = new Pos(posY, posX);
        }

        public void RespawnFood()
        {
            int foodPosY = _random.Next(0, 25);
            int foodPosX = _random.Next(0, 25);
            
            _position.Y = foodPosY;
            _position.X = foodPosX;
        }
    }
}
