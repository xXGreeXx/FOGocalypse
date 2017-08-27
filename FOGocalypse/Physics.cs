﻿using System;
using System.Drawing;
using System.Collections.Generic;

namespace FOGocalypse
{
    public class Physics
    {
        int waterCycle = 0;
        int hungerCycle = 0;
        Random generator = new Random();
        int cycle = 0;

        //constrcutor
        public Physics()
        {

        }

        //simulate physics
        public void SimulatePhysics()
        {
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                if (!Game.inInventory)
                {
                    simulatePlayerPhysics();
                }
                simulatePlayerNeeds();
                simulateParticles();
                simulateZombies();
                spawnZombie();
            }
        }

        //player physics
        private void simulatePlayerPhysics()
        {
            //player movement
            Point mousePosition = new Point(MouseHandler.mouseX, MouseHandler.mouseY);
            Point playerPosition = new Point(Game.canvasWidth / 2 - FOGocalypse.Properties.Resources.player.Width / 2, Game.canvasHeight / 2 - FOGocalypse.Properties.Resources.player.Height / 2);
            Point oldPosition = new Point(Game.player.playerX, Game.player.playerY);
            float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - playerPosition.Y, (double)MouseHandler.mouseX - playerPosition.X)));
            float directionX = (float)Math.Cos(angle);
            float directionY = (float)Math.Sin(angle);
            int velocityX = Game.playerMoveSpeed;
            int velocityY = Game.playerMoveSpeed;

            Game.player.playerX += (int)(directionX * velocityX);
            Game.player.playerY += (int)(directionY * velocityY);

            //handle collsions
            //TODO\\
            Game.player.hitbox = new Rectangle(playerPosition.X, playerPosition.Y, Game.tileSize, Game.tileSize);

            foreach (Tile t in Game.worldTiles)
            {
                if (t.type.Equals(EnumHandler.TileTypes.Wood))
                {
                    int newX = t.x - Game.player.playerX;
                    int newY = t.y - Game.player.playerY;

                    t.hitbox = new Rectangle(newX, newY, Game.tileSize, Game.tileSize);

                    if (Game.player.hitbox.IntersectsWith(t.hitbox))
                    {
                        Game.player.playerX = oldPosition.X;
                        Game.player.playerY = oldPosition.Y;
                        break;
                    }
                }
            }
        }

        //player hunger thirst mechanics
        private void simulatePlayerNeeds()
        {
            waterCycle++;
            if (waterCycle >= 50 - (Game.player.playerXVelocity + Game.player.playerYVelocity))
            {
                Game.player.playerWaterNeed++;
                waterCycle = 0;

                if (Game.player.playerWaterNeed >= 100)
                {
                    Game.player.playerHealth -= 5;
                }
            }

            hungerCycle++;
            if (hungerCycle >= 80 - (Game.player.playerXVelocity + Game.player.playerYVelocity))
            {
                Game.player.playerFoodNeed++;
                hungerCycle = 0;

                if (Game.player.playerFoodNeed >= 100)
                {
                    Game.player.playerHealth -= 5;
                }
            }
        }

        //simulate particles
        private void simulateParticles()
        {
            List<int> particlesToRemove = new List<int>();

            for (int index = 0; index < Game.bloodParticles.Count; index++)
            {
                Game.bloodParticles[index].x += generator.Next(-4, 5);
                Game.bloodParticles[index].y += generator.Next(-4, 5);

                if (Game.bloodParticles[index].size < 0)
                {
                    particlesToRemove.Add(index);
                }
                Game.bloodParticles[index].size--;
            }

            particlesToRemove.Sort();
            particlesToRemove.Reverse();

            foreach (int index in particlesToRemove)
            {
                Game.bloodParticles.RemoveAt(index);
            }
        }

        //simulate zombie physics
        private void simulateZombies()
        {
            List<int> zombiesToRemove = new List<int>();

            for (int index = 0; index < Game.zombies.Count; index++)
            {
                Boolean needsRemoved = Game.zombies[index].SimulateAI();

                if (needsRemoved)
                {
                    zombiesToRemove.Add(index);
                }
            }

            zombiesToRemove.Sort();
            zombiesToRemove.Reverse();

            foreach (int index in zombiesToRemove)
            {
                Game.zombies.RemoveAt(index);
            }
        }

        //spawn zombies
        private void spawnZombie()
        {
            cycle++;
            if (cycle >= 10)
            {
                int chance = generator.Next(1, 5);
                if (chance == Game.zombieSpawnChance / 10)
                {
                    int x = generator.Next(Game.player.playerX - (20 * Game.tileSize), Game.player.playerX + (20 * Game.tileSize));
                    int y = generator.Next(Game.player.playerY - (20 * Game.tileSize), Game.player.playerY + (20 * Game.tileSize));

                    Game.zombies.Add(new Zombie(x, y));
                }

                cycle = 0;
            }
        }
    }
}
