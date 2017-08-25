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
        public void SimulatePhysics(int width, int height)
        {
            simulatePlayerPhysics(width, height);
        }

        //player physics
        private void simulatePlayerPhysics(int width, int height)
        {
            //player movement
            Point mousePosition = new Point(MouseHandler.mouseX, MouseHandler.mouseY);
            Point playerPosition = new Point(width / 2 - FOGocalypse.Properties.Resources.player.Width / 2, height / 2 - FOGocalypse.Properties.Resources.player.Height / 2);
            String keyPressed = "";

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
