using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOGocalypse
{
    public class Physics
    {
        //constrcutor
        public Physics()
        {

        }

        //simulate physics
        public void SimulatePhysics()
        {
            simulatePlayerPhysics();
        }

        //player physics
        private void simulatePlayerPhysics()
        {
            Game.player.playerX += Game.player.playerXVelocity;
            Game.player.playerY += Game.player.playerYVelocity;
        }
    }
}
