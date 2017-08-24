using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOGocalypse
{
    public class Player
    {
        //define global variables
        public int playerX { get; set; }
        public int playerY { get; set; }
        public int playerXVelocity { get; set; } = 0;
        public int playerYVelocity { get; set; } = 0;
        public EnumHandler.Directions playerDirection { get; set; }

        //constructor
        public Player(int playerX, int playerY, EnumHandler.Directions playerDirection)
        {
            this.playerX = playerX;
            this.playerY = playerY;
            this.playerDirection = playerDirection;
        }
    }
}
