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
        public static int maxPlayerHealth { get; set; } = 100;
        public static int playerHealth { get; set; } = 100;
        public static int maxPlayerWaterNeed { get; set; } = 100;
        public static int playerWaterNeed { get; set; } = 0;
        public static int maxPlayerFoodNeed { get; set; } = 100;
        public static int playerFoodNeed { get; set; } = 0;

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
