using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

namespace FOGocalypse
{
    public class Physics
    {
        int waterCycle = 0;
        int hungerCycle = 0;
        int rainCycle = 0;
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
                simulateRain();
                simulateWorldChunks();
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

            int velocityX = 0;
            int velocityY = 0;
            int playerMoveSpeed = Game.playerMoveSpeed;
            
            if (KeyBoardHandler.lastKeyPressed.Equals("W"))
            {
                velocityX = playerMoveSpeed;
                velocityY = playerMoveSpeed;
            }
            if (KeyBoardHandler.lastKeyPressed.Equals("S"))
            {
                velocityX = -playerMoveSpeed;
                velocityY = -playerMoveSpeed;
            }
            if (KeyBoardHandler.lastKeyPressed.Equals("A"))
            {
                velocityX = playerMoveSpeed;
                velocityY = -playerMoveSpeed;
            }
            if (KeyBoardHandler.lastKeyPressed.Equals("D"))
            {
                velocityX = -playerMoveSpeed;
                velocityY = playerMoveSpeed;
            }

            Game.player.playerX += (int)(directionX * velocityX);
            Game.player.playerY += (int)(directionY * velocityY);

            //handle block collsions
            Game.player.hitbox = new Rectangle(playerPosition.X, playerPosition.Y, Game.tileSize, Game.tileSize);

            foreach (Tile t in Game.allocatedTiles)
            {
                if (t.type.Equals(EnumHandler.TileTypes.Wood))
                {
                    int newX = t.x - Game.player.playerX;
                    int newY = t.y - Game.player.playerY;
                    int playerX = Game.canvasWidth / 2 - Game.tileSize / 2;
                    int playerY = Game.canvasHeight / 2 - Game.tileSize / 2;

                    Rectangle topHitbox = new Rectangle(newX, newY + 5, Game.tileSize, 10);
                    Rectangle bottomHitbox = new Rectangle(newX, newY + Game.tileSize - 5, Game.tileSize, 10);
                    Rectangle leftHitbox = new Rectangle(newX, newY, 20, Game.tileSize);
                    Rectangle rightHitbox = new Rectangle(newX + Game.tileSize - 10, newY, 10, Game.tileSize);

                    if (Game.player.hitbox.IntersectsWith(topHitbox) || Game.player.hitbox.IntersectsWith(bottomHitbox)) Game.player.playerY = oldPosition.Y;
                    if (Game.player.hitbox.IntersectsWith(leftHitbox) || Game.player.hitbox.IntersectsWith(rightHitbox)) Game.player.playerX = oldPosition.X;
                }
            }

            //handle furniture collisions
            foreach (Furniture f in Game.furnitureInWorld)
            {
                int newX = f.x - Game.player.playerX;
                int newY = f.y - Game.player.playerY;
                int playerX = Game.canvasWidth / 2 - Game.tileSize / 2;
                int playerY = Game.canvasHeight / 2 - Game.tileSize / 2;

                if (f.type.Equals(EnumHandler.FurnitureTypes.Couch))
                {
                    if (f.rotation == 90)
                    {
                        Rectangle topHitbox = new Rectangle(newX, newY + 5, 50, 10);
                        Rectangle bottomHitbox = new Rectangle(newX, newY + 100 - 5, 50, 10);
                        Rectangle leftHitbox = new Rectangle(newX, newY, 20, 100);
                        Rectangle rightHitbox = new Rectangle(newX + 50 - 10, newY, 10, 100);

                        if (Game.player.hitbox.IntersectsWith(topHitbox) || Game.player.hitbox.IntersectsWith(bottomHitbox)) Game.player.playerY = oldPosition.Y;
                        if (Game.player.hitbox.IntersectsWith(leftHitbox) || Game.player.hitbox.IntersectsWith(rightHitbox)) Game.player.playerX = oldPosition.X;
                    }
                }

                if (f.type.Equals(EnumHandler.FurnitureTypes.Table))
                {
                    Rectangle topHitbox = new Rectangle(newX, newY + 5, 100, 10);
                    Rectangle bottomHitbox = new Rectangle(newX, newY + 50 - 5, 100, 10);
                    Rectangle leftHitbox = new Rectangle(newX, newY, 20, 50);
                    Rectangle rightHitbox = new Rectangle(newX + 100 - 10, newY, 10, 50);

                    if (Game.player.hitbox.IntersectsWith(topHitbox) || Game.player.hitbox.IntersectsWith(bottomHitbox)) Game.player.playerY = oldPosition.Y;
                    if (Game.player.hitbox.IntersectsWith(leftHitbox) || Game.player.hitbox.IntersectsWith(rightHitbox)) Game.player.playerX = oldPosition.X;
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

                if (Game.player.playerFoodNeed < 20 && Game.player.playerWaterNeed < 30)
                {
                    if (Game.player.playerHealth < Game.player.maxPlayerHealth)
                    {
                        Game.player.playerHealth += 5;
                    }
                }
            }

            if (Game.player.playerHealth <= 0)
            {
                Game.inLossScreen = true;
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

        //simulate rain
        private void simulateRain()
        {
            int width = Game.canvasWidth;
            int height = Game.canvasHeight;

            if (Game.weather.Equals(EnumHandler.WeatherType.Rainy) && Game.rainOn)
            {
                if (rainCycle >= 3)
                {
                    for (int i = 0; i < generator.Next(5, 80); i++)
                    {
                        Game.particleGenerator.CreateBloodEffect(generator.Next(width / 2 - 100, width / 2 + 100) + Game.player.playerX, generator.Next(height / 2 - 100, height / 2 + 100) + Game.player.playerY, -3, Color.Blue, 4);
                    }

                    rainCycle = 0;
                }
                rainCycle++;
            }
        }

        //allocate world chunks
        private void simulateWorldChunks()
        {
            Tile tBaseTopLeft = Game.allocatedTiles[0];
            Tile tBaseBottomRight = Game.allocatedTiles[Game.allocatedTiles.Count - 1];

            if (tBaseTopLeft.x - Game.player.playerX >= Game.canvasWidth / 2 - 50 || tBaseTopLeft.y - Game.player.playerY >= Game.canvasHeight / 2 - 50)
            {
                Thread t = new Thread(() =>
                {
                    Game.allocatedTiles = new WorldGenerator().AllocateTiles(Game.worldTiles, 20);
                });

                t.Start();
            }

            if (tBaseBottomRight.x - Game.player.playerX <= Game.canvasWidth / 2 || tBaseBottomRight.y - Game.player.playerY <= Game.canvasHeight / 2)
            {
                Thread t = new Thread(() =>
                {
                    Game.allocatedTiles = new WorldGenerator().AllocateTiles(Game.worldTiles, 20);
                });

                t.Start();
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
                int chance = generator.Next(1, 10 - (9 - (Game.zombieSpawnChance / 10)));
                if (chance == 1)
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
