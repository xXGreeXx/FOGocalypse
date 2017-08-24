using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOGocalypse
{
    public class Tile
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public EnumHandler.TileTypes type { get; set; }

        //constructor
        public Tile(int x, int y, EnumHandler.TileTypes type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
