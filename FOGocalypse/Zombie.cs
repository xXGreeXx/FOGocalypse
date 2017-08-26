﻿using System;
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
        public Boolean SimulateAI()
        {
            int newX = x - Game.player.playerX;
            int newY = y - Game.player.playerY;
            int positionOfPlayerX = Game.canvasWidth / 2 - Game.tileSize / 2;
            int positionOfPlayerY = Game.canvasHeight / 2 - Game.tileSize / 2;
            int viewDistance = Game.zombieViewDistance * Game.tileSize;
            Boolean needsRemoved = false;

            if (newX >= positionOfPlayerX - viewDistance && newX < positionOfPlayerX + viewDistance)
            {
                if (newY >= positionOfPlayerY - viewDistance && newY < positionOfPlayerY + viewDistance)
                {
                    lookingToward = new Point(positionOfPlayerX, positionOfPlayerY);

                    if (positionOfPlayerX < newX)
                    {
                        x -= Game.zombieMoveSpeed;
                    }
                    if (positionOfPlayerX > newX)
                    {
                        x += Game.zombieMoveSpeed;
                    }

                    if (positionOfPlayerY < newY)
                    {
                        y -= Game.zombieMoveSpeed;
                    }
                    if (positionOfPlayerY > newY)
                    {
                        y += Game.zombieMoveSpeed;
                    }
                }
            }

            if (newX <= -50 || newX >= Game.canvasWidth + 50)
            {
                needsRemoved = true;
            }
            if (newY <= -50 || newY >= Game.canvasHeight + 50)
            {
                needsRemoved = true;
            }

            if (health <= 0)
            {
                needsRemoved = true;
            }

            return needsRemoved;
        }
    }
}
