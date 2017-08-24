using System;
using System.Drawing;

namespace FOGocalypse
{
    public class Player
    {
        //define global variables
        public int playerX { get; set; }
        public int playerY { get; set; }
        public int playerXVelocity { get; set; } = 0;
        public int playerYVelocity { get; set; } = 0;
        public EnumHandler.Directions direction { get; set; }
        public Rectangle hitbox { get; set; }

        //constructor
        public Player(int playerX, int playerY, EnumHandler.Directions playerDirection)
        {
            this.playerX = playerX;
            this.playerY = playerY;
            this.direction = playerDirection;
            this.hitbox = new Rectangle(playerX, playerY, Game.tileSize, Game.tileSize);
        }
    }
}
