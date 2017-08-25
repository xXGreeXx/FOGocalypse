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
        Bitmap title1 = FOGocalypse.Properties.Resources.title1;
        Bitmap title2 = FOGocalypse.Properties.Resources.title2;
        Bitmap fog = FOGocalypse.Properties.Resources.fog;

        //constructor
        public RenderingEngine()
        {
            player.RotateFlip(RotateFlipType.RotateNoneFlipX);
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

                int positionX = width / 2 - player.Width / 2;
                int positionY = height / 2 - player.Height / 2;
                float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - positionY, (double)MouseHandler.mouseX - positionX)) * (180/Math.PI));
                
                g.DrawImage(RotateImage(player, angle), width / 2 - player.Width / 2, height / 2 - player.Height / 2, Game.tileSize, Game.tileSize);

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

                //draw fog
                //fogGenerator(width, height, g);

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
                    g.DrawImage(title1, width / 2 - title1.Width / 2, height / 2 - 200, title1.Width, title1.Height);
                    g.DrawImage(title2, width / 2 - title1.Width / 2, height / 2 + 100, title1.Width, title1.Height);

                    Font font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);

                    g.DrawString("Return To Game", font, Brushes.Black, width / 2 - g.MeasureString("Return To Game", font).Width / 2, height / 2 - 100);
                    g.DrawString("Return To Menu", font, Brushes.Black, width / 2 - g.MeasureString("Return To Menu", font).Width / 2, height / 2 - 75);

                }
            }
            #endregion
        }


        //fog generator
        //TODO\\
        private void fogGenerator(int width, int height, Graphics g)
        {
            int cycle = 2;

            for (int x = 0; x < width; x += Game.tileSize)
            {
                for (int y = 0; y < height; y += Game.tileSize)
                {
                    Boolean pass = true;

                    if (x >= (width / 2 - player.Width / 2) - (((Game.playerViewDistance / 2F) - cycle) * Game.tileSize) && x <= (width / 2 - player.Width / 2) + (((Game.playerViewDistance / 2F) - cycle) * Game.tileSize))
                    {
                        if (y >= (height / 2 - player.Height / 2) - ((Game.playerViewDistance / 2F) * Game.tileSize) && y <= (height / 2 - player.Height / 2) + ((Game.playerViewDistance / 2F) * Game.tileSize))
                        {
                            if (cycle <= 2)
                            {
                                cycle++;
                            }
                            else if (cycle >= 6)
                            {
                                cycle--;
                            }

                            pass = false;
                        }
                    }

                    if (pass)
                    {
                        g.DrawImage(fog, x, y, Game.tileSize, Game.tileSize);
                    }
                }
            }
        }

        //rotate image
        private static Bitmap RotateImage(Image image, float angle)
        {
            PointF offset = new PointF((float)image.Width / 2, (float)image.Height / 2);
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics g = Graphics.FromImage(rotatedBmp);
            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
    }
}
