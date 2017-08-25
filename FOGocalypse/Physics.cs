﻿using System;
using System.Drawing;

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
        public void SimulatePhysics(int width, int height)
        {
            if (!Game.inInventory)
            {
                simulatePlayerPhysics();
            }
            simulatePlayerNeeds();
        }

        //player physics
        private void simulatePlayerPhysics()
        {
            //player movement
            Point mousePosition = new Point(MouseHandler.mouseX, MouseHandler.mouseY);
            Point playerPosition = new Point(Game.canvasWidth / 2 - FOGocalypse.Properties.Resources.player.Width / 2, Game.canvasHeight / 2 - FOGocalypse.Properties.Resources.player.Height / 2);
            String keyPressed = "";
            Point oldPosition = new Point(Game.player.playerX, Game.player.playerY);

            if (Game.player.playerXVelocity == Game.playerMoveSpeed) keyPressed = "D";
            else if (Game.player.playerYVelocity == Game.playerMoveSpeed) keyPressed = "S";
            else if (Game.player.playerXVelocity == -Game.playerMoveSpeed) keyPressed = "A";
            else if (Game.player.playerYVelocity == -Game.playerMoveSpeed) keyPressed = "W";

            if (mousePosition.X < playerPosition.X && mousePosition.Y < playerPosition.Y)
            {
                switch (keyPressed)
                {
                    case "D":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "S":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "A":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "W":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                }
            }


            if (mousePosition.X > playerPosition.X && mousePosition.Y < playerPosition.Y)
            {
                switch (keyPressed)
                {
                    case "D":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "S":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "A":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "W":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                }
            }


            if (mousePosition.X < playerPosition.X && mousePosition.Y > playerPosition.Y)
            {
                switch (keyPressed)
                {
                    case "D":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "S":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "A":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "W":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                }
            }


            if (mousePosition.X > playerPosition.X && mousePosition.Y > playerPosition.Y)
            {
                switch (keyPressed)
                {
                    case "D":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "S":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY -= Game.playerMoveSpeed;
                        break;
                    case "A":
                        Game.player.playerX -= Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                    case "W":
                        Game.player.playerX += Game.playerMoveSpeed;
                        Game.player.playerY += Game.playerMoveSpeed;
                        break;
                }
            }

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
            }

            hungerCycle++;
            if (hungerCycle >= 100 - (Game.player.playerXVelocity + Game.player.playerYVelocity))
            {
                Game.player.playerFoodNeed++;
                hungerCycle = 0;
            }
        }

        //simulate zombie physics
        private void simulateZombies()
        {
            foreach (Zombie z in Game.zombies)
            {
                z.SimulateAI();
            }
        }

        //spawn zombies
        private void spawnZombie()
        {
            cycle++;
            if (cycle >= 20)
            {
                int chance = generator.Next(1, 10);

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
