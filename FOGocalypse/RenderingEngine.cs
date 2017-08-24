using System;
using System.Drawing;
using System.Windows.Forms;

namespace FOGocalypse
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap player = FOGocalypse.Properties.Resources.player;
        Bitmap grass = FOGocalypse.Properties.Resources.grass;
        Bitmap dirt = FOGocalypse.Properties.Resources.dirt;
        Bitmap wood = FOGocalypse.Properties.Resources.wood;
        Bitmap waterDrop = FOGocalypse.Properties.Resources.waterDrop;
        Bitmap heart = FOGocalypse.Properties.Resources.heart;
        Bitmap food = FOGocalypse.Properties.Resources.food;
        Bitmap flashlightIcon = FOGocalypse.Properties.Resources.flashlightIcon;
        Bitmap flashlight = FOGocalypse.Properties.Resources.flashlight;

        //constructor
        public RenderingEngine()
        {

        }

        //draw screen
        public void DrawScreen(int width, int height, Graphics g)
        {
            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                //draw tiles
                foreach (Tile t in Game.worldTiles)
                {
                    int x = t.x - Game.player.playerX;
                    int y = t.y - Game.player.playerY;

                    if (x > 0 - Game.tileSize && x < width + Game.tileSize)
                    {
                        if (y > 0 - Game.tileSize && y < height + Game.tileSize)
                        {
                            if (t.type.Equals(EnumHandler.TileTypes.Grass)) g.DrawImage(grass, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Dirt)) g.DrawImage(dirt, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Wood)) g.DrawImage(wood, x, y, Game.tileSize, Game.tileSize);
                        }
                    }  
                }

                //draw player
                if (Game.player.direction.Equals(EnumHandler.Directions.Right)) { player.RotateFlip(RotateFlipType.RotateNoneFlipX); }
                else if (Game.player.direction.Equals(EnumHandler.Directions.Up)) { player.RotateFlip(RotateFlipType.Rotate90FlipNone); }
                else if (Game.player.direction.Equals(EnumHandler.Directions.Down)) { player.RotateFlip(RotateFlipType.Rotate270FlipNone); }

                g.DrawImage(player, width / 2 - player.Width / 2, height / 2 - player.Height / 2, Game.tileSize, Game.tileSize);

                player = FOGocalypse.Properties.Resources.player;

                //draw item player is holding
                EnumHandler.Items selectedItem = Game.itemsInHotbar[Game.selectedHotbar - 1];

                switch (selectedItem)
                {
                    case EnumHandler.Items.Flashlight:
                        if (Game.player.direction.Equals(EnumHandler.Directions.Up))
                        {
                            g.DrawImage(flashlight, width / 2 - player.Height / 2 + Game.tileSize - 10, height / 2 - player.Height / 2 - 10, 10, 10);
                        }
                        if (Game.player.direction.Equals(EnumHandler.Directions.Down))
                        {
                            flashlight.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            g.DrawImage(flashlight, width / 2 - player.Height / 2 + Game.tileSize - 10, height / 2, 10, 10);
                            flashlight.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        }
                        if (Game.player.direction.Equals(EnumHandler.Directions.Left))
                        {
                            flashlight.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            g.DrawImage(flashlight, width / 2 - player.Height / 2 - 10, height / 2 - player.Height / 2, 10, 10);
                            flashlight.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        }
                        if (Game.player.direction.Equals(EnumHandler.Directions.Right))
                        {
                            flashlight.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImage(flashlight, width / 2 - player.Height / 2 + Game.tileSize, height / 2 - player.Height / 2, 10, 10);
                            flashlight.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        break;
                }

                //draw health/thirst/hunger
                g.DrawRectangle(Pens.Black, 10, 10, 200, 30);
                g.FillRectangle(Brushes.Red, 11, 11, (Game.player.playerHealth * 2) - 1, 29);
                g.DrawImage(heart, 200 - 25, 11, 25, 25);

                g.DrawRectangle(Pens.Black, 10, 50, 200, 30);
                g.FillRectangle(Brushes.Blue, 11, 51, (200 - Game.player.playerWaterNeed * 2) - 1, 29);
                g.DrawImage(waterDrop, 200 - 30, 51, 25, 25);

                g.DrawRectangle(Pens.Black, 10, 90, 200, 30);
                g.FillRectangle(Brushes.Brown, 11, 91, (200 - Game.player.playerFoodNeed * 2) - 1, 29);
                g.DrawImage(food, 200 - 30, 93, 25, 25);

                //draw hotbar
                for (int i = 0; i < Game.numberOfhotBarSlots; i++)
                {
                    int x = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + i * 60;
                    int y = height - 60;
                    Color c = Color.Brown;

                    if (Game.selectedHotbar - 1 == i) c = Color.White;

                    g.DrawRectangle(new Pen(c, 4), x, y, 50, 50);
                }

                int index = 0;
                foreach (EnumHandler.Items item in Game.itemsInHotbar)
                {
                    int xToDraw = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + index * 60;

                    if (item.Equals(EnumHandler.Items.Flashlight)) g.DrawImage(flashlightIcon, xToDraw, height - 60, 50, 50);
                    index++;
                }

                //draw cursor
                if (!Game.inPauseMenu)
                {
                    Cursor.Hide();
                    g.DrawEllipse(Pens.Black, MouseHandler.mouseX - 5, MouseHandler.mouseY - 5, 10, 10);
                }
                else
                {
                    Cursor.Show();
                }
            }
            #endregion
        }
    }
}
