using System;
using System.Collections.Generic;
using System.Drawing;

namespace FOGocalypse
{
    public class CombatSystem
    {
        //define globla variables

        //constructor
        public CombatSystem()
        {

        }

        //swing item
        public void SwingItem(int damage)
        {
            for(int index = 0; index < Game.zombies.Count; index++)
            {
                int zombieX = Game.zombies[index].x - Game.player.playerX;
                int zombieY = Game.zombies[index].y - Game.player.playerY;
                int playerPositionX = Game.canvasWidth / 2 - Game.tileSize / 2;
                int playerPositionY = Game.canvasHeight / 2 - Game.tileSize / 2;

                if (zombieX >= playerPositionX - 60 && zombieX <= playerPositionX + 60 + Game.tileSize)
                {
                    if (zombieY >= playerPositionY - 60 && zombieY <= playerPositionY + 60 + Game.tileSize)
                    {
                        Game.zombies[index].health -= damage;
                    }
                }
            }
        }

        //stab with item
        public void StabWithItem(int damage)
        {

        }

        //throw item
        public void ThrowItem(int damage, Point destination)
        {

        }

        //fire item
        public void FireItem(int damage, Point destination)
        {

        }
    }
}
