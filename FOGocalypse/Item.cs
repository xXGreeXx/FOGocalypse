using System;

namespace FOGocalypse
{
    public class Item
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public EnumHandler.Items type { get; set; }

        //constructor
        public Item(int x, int y, EnumHandler.Items type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
