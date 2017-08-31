using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOGocalypse
{
    public class Plant
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public EnumHandler.PlantTypes type { get; set; }

        //constructor
        public Plant(int x, int y, EnumHandler.PlantTypes type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
