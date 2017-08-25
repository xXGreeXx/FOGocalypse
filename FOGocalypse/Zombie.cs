using System;
using System.Drawing;

namespace FOGocalypse
{
    public class Zombie
    {
        //define global variables
        public int x { get; set; }
        public int y { get; set; }
        public int health { get; set; }
        public Point lookingToward { get; set; }

        //constructor
        public Zombie(int x, int y)
        {
            this.x = x;
            this.y = y;
            health = 50;
        }

        //simulate AI
        public void SimulateAI()
        {

        }
    }
}
