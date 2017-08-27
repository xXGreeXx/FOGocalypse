using System;
using System.Drawing;

namespace FOGocalypse
{
    public class Furniture
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public EnumHandler.FurnitureTypes type { get; set; }
        public int rotation { get; set; }

        //constructor
        public Furniture(int x, int y, EnumHandler.FurnitureTypes type, int rotation)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.rotation = rotation;
        }
    }
}
