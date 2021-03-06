﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace FOGocalypse
{
    public class CombatSystem
    {
        //define global variables
        public static Boolean canAttack { get; set; } = true;

        //constructor
        public CombatSystem()
        {

        }

        //swing item
        public void SwingItem(int damage)
        {
            if (canAttack)
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
                            int directionToPushZombieBackX = 0;
                            int directionToPushZombieBackY = 0;

                            if (MouseHandler.mouseX < zombieX)
                            {
                                directionToPushZombieBackX = damage / 2;
                            }
                            if (MouseHandler.mouseX > zombieX)
                            {
                                directionToPushZombieBackX = -damage / 2;
                            }

                            if (MouseHandler.mouseY < zombieY)
                            {
                                directionToPushZombieBackY = damage / 2;
                            }
                            if (MouseHandler.mouseY > zombieY)
                            {
                                directionToPushZombieBackY = -damage / 2;
                            }

                            Game.zombies[index].health -= damage;
                            Game.zombies[index].x -= directionToPushZombieBackX;
                            Game.zombies[index].y -= directionToPushZombieBackY;
                            if (Game.blood) Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x, Game.zombies[index].y, 10, Color.Red, 8);
                        }
                    }
                }

                Game.attackSpeedLimit = 450;
                RenderingEngine.weaponPositionOffsetX = 5;
                RenderingEngine.weaponPositionOffsetY = 2;
                canAttack = false;
            }
        }

        //stab with item
        public void StabWithItem(int damage)
        {
            if (canAttack)
            {
                int playerPositionX = Game.canvasWidth / 2 - Game.tileSize / 2;
                int playerPositionY = Game.canvasHeight / 2 - Game.tileSize / 2;

                float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - playerPositionX, (double)MouseHandler.mouseX - playerPositionY)) * (180 / Math.PI));
                angle /= (float)(180 / Math.PI);

                int rotationX = (int)(Math.Cos(angle) * Game.tileSize);
                int rotationY = (int)(Math.Sin(angle) * Game.tileSize);

                for (int index = 0; index < Game.zombies.Count; index++)
                {
                    int zombieX = Game.zombies[index].x - Game.player.playerX;
                    int zombieY = Game.zombies[index].y - Game.player.playerY;

                    if (zombieX >= playerPositionX - 60 && zombieX <= playerPositionX + 60 + Game.tileSize)
                    {
                        if (zombieY >= playerPositionY - 60 && zombieY <= playerPositionY + 60 + Game.tileSize)
                        {
                            Game.zombies[index].health -= damage;
                            if (Game.blood) Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x, Game.zombies[index].y, 10, Color.Red, 8);
                            break;
                        }
                    }
                }

                Game.attackSpeedLimit = 400;
                RenderingEngine.weaponPositionOffsetY = 5;
                canAttack = false;
            }
        }

        //throw item
        public void ThrowItem(int damage, Point destination, Item thrownItem)
        {
            Item i = new Item(Game.canvasWidth / 2 - Game.tileSize / 2 + Game.player.playerX, Game.canvasHeight / 2 - Game.tileSize / 2 + Game.player.playerY, thrownItem.type);
            i.ammo = thrownItem.ammo;

            RenderingEngine.itemsBeingThrown.Add(i);
            RenderingEngine.destinationsOfItemsBeingThrown.Add(new int[] { destination.X, destination.Y, damage } );
        }

        //fire item
        public void FireItem(int damage, Point destination)
        {
            if (canAttack)
            {
                int positionX = Game.canvasWidth / 2 - Game.tileSize / 2;
                int positionY = Game.canvasHeight / 2 - Game.tileSize / 2;
                Boolean hit = false;

                float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - positionY, (double)MouseHandler.mouseX - positionX)) * (180 / Math.PI));
                angle /= (float)(180 / Math.PI);

                int rotationX = (int)(Math.Cos(angle) * Game.tileSize);
                int rotationY = (int)(Math.Sin(angle) * Game.tileSize);

                for (int index = 0; index < Game.zombies.Count; index++)
                {
                    int zombieX = Game.zombies[index].x - Game.player.playerX;
                    int zombieY = Game.zombies[index].y - Game.player.playerY;

                    if (zombieX >= destination.X - 30 && zombieX <= destination.X + 30)
                    {
                        if (zombieY >= destination.Y - 30 && zombieY <= destination.Y + 30)
                        {
                            Game.zombies[index].health -= damage;
                            if (Game.blood) Game.particleGenerator.CreateBloodEffect(Game.zombies[index].x + Game.tileSize / 2, Game.zombies[index].y + Game.tileSize / 2, damage / 4, Color.Red, 8);
                            hit = true;
                            break;
                        }
                    }
                }

                if (!hit)
                {
                    Game.particleGenerator.CreateBloodEffect(destination.X + Game.player.playerX, destination.Y + Game.player.playerY, 1, Color.Brown, 6);
                }

                Game.particleGenerator.CreateBloodEffect(Game.canvasWidth / 2 - Game.tileSize / 2 + Game.player.playerX + rotationX, Game.canvasHeight / 2 - Game.tileSize / 2 + Game.player.playerY + rotationY, 2, Color.Yellow, 4);

                canAttack = false;
                Game.attackSpeedLimit = 1300;

                Game.itemsInHotbar[Game.selectedHotbar - 1].ammo--;
                RenderingEngine.weaponPositionOffsetY = 5;
            }
        }
    }
}
