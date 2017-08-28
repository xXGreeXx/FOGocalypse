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
                        int directionToPushZombieBackX = 0;
                        int directionToPushZombieBackY = 0;

                        if (MouseHandler.mouseX < zombieX)
                        {
                            directionToPushZombieBackX = damage;
                        }
                        if (MouseHandler.mouseX > zombieX)
                        {
                            directionToPushZombieBackX = -damage;
                        }

                        if (MouseHandler.mouseY < zombieY)
                        {
                            directionToPushZombieBackY = damage;
                        }
                        if (MouseHandler.mouseY > zombieY)
                        {
                            directionToPushZombieBackY = -damage;
                        }

                        Game.zombies[index].health -= damage;
                        Game.zombies[index].x -= directionToPushZombieBackX;
                        Game.zombies[index].y -= directionToPushZombieBackY;
                        Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x - directionToPushZombieBackX, Game.zombies[index].y - directionToPushZombieBackX, 10, Color.DarkRed);
                    }
                }
            }

            Game.attackEffect = 1;
        }

        //stab with item
        public void StabWithItem(int damage)
        {
            for (int index = 0; index < Game.zombies.Count; index++)
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
                        Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x, Game.zombies[index].y, 10, Color.DarkRed);
                    }
                }
            }
        }

        //throw item
        public void ThrowItem(int damage, Point destination)
        {

        }

        //fire item
        public void FireItem(int damage, Point destination)
        {
            Boolean hit = false;

            for (int index = 0; index < Game.zombies.Count; index++)
            {
                int zombieX = Game.zombies[index].x - Game.player.playerX;
                int zombieY = Game.zombies[index].y - Game.player.playerY;

                if (zombieX >= destination.X - 30 && zombieX <= destination.X + 30)
                {
                    if (zombieY >= destination.Y - 30 && zombieY <= destination.Y + 30)
                    {
                        Game.zombies[index].health -= damage;
                        Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x, Game.zombies[index].y, damage / 2, Color.DarkRed);
                        hit = true;
                        break;
                    }
                }
            }

            if (!hit)
            {
                Game.particleGenerator.CreateBloodEffect(destination.X + Game.player.playerX, destination.Y + Game.player.playerY, 1, Color.Brown);
            }

            Game.particleGenerator.CreateBloodEffect(Game.canvasWidth / 2 - Game.tileSize / 2 + Game.player.playerX, Game.canvasHeight / 2 - Game.tileSize / 2 + Game.player.playerY, 2, Color.Yellow);
        }
    }
}
