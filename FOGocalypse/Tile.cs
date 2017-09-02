using System;
using System.Drawing;

namespace FOGocalypse
{
    public class Tile
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public EnumHandler.TileTypes type { get; set; }
        public Rectangle hitbox { get; set; }
        public Boolean roofed { get; set; }
        public int fogValue { get; set; }
        public Boolean swapFog { get; set; } = false;

        //constructor
        public Tile(int x, int y, EnumHandler.TileTypes type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.hitbox = new Rectangle(x, y, Game.tileSize, Game.tileSize);
            this.roofed = false;

            fogValue = Game.r.Next(200, 255);
        }
    }
}
