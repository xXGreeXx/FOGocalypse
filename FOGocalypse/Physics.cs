using System;
using System.Drawing;

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
            Game.player.hitbox = new Rectangle(Game.player.playerX, Game.player.playerY, Game.tileSize, Game.tileSize);

            foreach (Tile t in Game.worldTiles)
            {
                if (t.type.Equals(EnumHandler.TileTypes.Wood))
                {
                    t.hitbox = new Rectangle(t.x - Game.player.playerX, t.y - Game.player.playerY, Game.tileSize, Game.tileSize);

                    if (Game.player.hitbox.IntersectsWith(t.hitbox))
                    {
                        //Game.player.playerX -= Game.player.playerXVelocity;
                        //Game.player.playerY -= Game.player.playerYVelocity;
                        break;
                    }
                }
            }
        }
    }
}
