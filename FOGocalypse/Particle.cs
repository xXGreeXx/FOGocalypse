using System;
using System.Drawing;

namespace FOGocalypse
{
    public class Particle
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public Color color { get; set; }
        public int size { get; set; }

        //constructor
        public Particle(int x, int y, Color color, int size)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.size = size;
        }
    }
}
