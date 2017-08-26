using System;
using System.Drawing;
using System.Windows.Forms;

namespace FOGocalypse
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap player = FOGocalypse.Properties.Resources.player;
        Bitmap zombie = FOGocalypse.Properties.Resources.zombie;
        Bitmap grass = FOGocalypse.Properties.Resources.grass;
        Bitmap dirt = FOGocalypse.Properties.Resources.dirt;
        Bitmap wood = FOGocalypse.Properties.Resources.wood;
        Bitmap waterDrop = FOGocalypse.Properties.Resources.waterDrop;
        Bitmap heart = FOGocalypse.Properties.Resources.heart;
        Bitmap food = FOGocalypse.Properties.Resources.food;
        Bitmap flashlightIcon = FOGocalypse.Properties.Resources.flashlightIcon;
        Bitmap flashlight = FOGocalypse.Properties.Resources.flashlight;
        Bitmap waterBottleIcon = FOGocalypse.Properties.Resources.waterBottleIcon;
        Bitmap waterBottle = FOGocalypse.Properties.Resources.waterBottle;
        Bitmap emptyBottleIcon = FOGocalypse.Properties.Resources.emptyBottleIcon;
        Bitmap emptyBottle = FOGocalypse.Properties.Resources.emptyBottle;
        Bitmap knifeIcon = FOGocalypse.Properties.Resources.knifeIcon;
        Bitmap knife = FOGocalypse.Properties.Resources.knife;
        Bitmap peanutButterIcon = FOGocalypse.Properties.Resources.peanutButterIcon;
        Bitmap peanutButter = FOGocalypse.Properties.Resources.peanutButter;
        Bitmap breadIcon = FOGocalypse.Properties.Resources.breadIcon;
        Bitmap bread = FOGocalypse.Properties.Resources.bread;
        Bitmap pistolIcon = FOGocalypse.Properties.Resources.pistolIcon;
        Bitmap pistol = FOGocalypse.Properties.Resources.pistol;
        Bitmap title1 = FOGocalypse.Properties.Resources.title1;
        Bitmap title2 = FOGocalypse.Properties.Resources.title2;
        Bitmap fog = FOGocalypse.Properties.Resources.fog;
        Bitmap fogBackground = FOGocalypse.Properties.Resources.fogBackground;

        float fogFrame = 0.0F;

        //constructor
        public RenderingEngine()
        {
            player.RotateFlip(RotateFlipType.RotateNoneFlipX);
            zombie.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        //draw screen
        public void DrawScreen(int width, int height, Graphics g)
        {
            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameStates.MainMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                int baseOfText = 200;

                g.DrawImage(fogBackground, 0, 0, width, height - 250);
                g.FillRectangle(Brushes.DarkGray, 0, height - 250, width, 250);
                g.DrawImage(title1, width / 2 - title1.Width / 2, 0, title1.Width, title1.Height);
                g.DrawImage(title2, width / 2 - title2.Width / 2, title1.Height, title2.Width, title2.Height);

                g.DrawString("Play", f, Brushes.Black, width / 2 - g.MeasureString("Play", f).Width / 2, baseOfText);
                g.DrawString("Options", f, Brushes.Black, width / 2 - g.MeasureString("Options", f).Width / 2, baseOfText + 75);
                g.DrawString("Quit", f, Brushes.Black, width / 2 - g.MeasureString("Quit", f).Width / 2, baseOfText + 225);

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Play", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Play", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= baseOfText && MouseHandler.mouseY <= baseOfText + g.MeasureString("Play", f).Height)
                    {
                        g.DrawString("Play", f, Brushes.White, width / 2 - g.MeasureString("Play", f).Width / 2, baseOfText);
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Options", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Options", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= baseOfText + 75 && MouseHandler.mouseY <= baseOfText + 75 + g.MeasureString("Options", f).Height)
                    {
                        g.DrawString("Options", f, Brushes.White, width / 2 - g.MeasureString("Options", f).Width / 2, baseOfText + 75);
                    }
                }

                if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Quit", f).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Quit", f).Width / 2)
                {
                    if (MouseHandler.mouseY >= baseOfText + 225 && MouseHandler.mouseY <= baseOfText + 225 + g.MeasureString("Quit", f).Height)
                    {
                        g.DrawString("Quit", f, Brushes.White, width / 2 - g.MeasureString("Quit", f).Width / 2, baseOfText + 225);
                    }
                }
            }

            #endregion

            #region OptionsMenu
            if (Game.state.Equals(EnumHandler.GameStates.OptionsMenu))
            {
                g.DrawImage(fogBackground, 0, 0, width, height - 250);
                g.FillRectangle(Brushes.DarkGray, 0, height - 250, width, 250);
                g.DrawImage(title1, width / 2 - title1.Width / 2, 0, title1.Width, title1.Height);
                g.DrawImage(title2, width / 2 - title2.Width / 2, title1.Height, title2.Width, title2.Height);

                g.FillRectangle(Brushes.Brown, width / 2 - 250, height / 2 - 250, 500, 500);
            }
            #endregion

            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 6, FontStyle.Bold);
                Font f2 = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);

                #region Tiles/Items in the world
                //draw tiles
                foreach (Tile t in Game.worldTiles)
                {
                    int x = t.x - Game.player.playerX;
                    int y = t.y - Game.player.playerY;
                    int distance = Game.playerViewDistance * Game.tileSize;

                    if (x > width / 2 - player.Width / 2 - distance && x < width / 2 - player.Width / 2 + distance)
                    {
                        if (y > height / 2 - player.Height / 2 - distance && y < height / 2 - player.Height / 2 + distance)
                        {
                            if (t.type.Equals(EnumHandler.TileTypes.Grass)) g.DrawImage(grass, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Dirt)) g.DrawImage(dirt, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Wood)) g.DrawImage(wood, x, y, Game.tileSize, Game.tileSize);
                        }
                    }  
                }

                //draw items in the world
                Boolean tooltipDrawn = false;

                foreach (Item i in Game.itemsInWorld)
                {
                    int newX = i.x - Game.player.playerX;
                    int newY = i.y - Game.player.playerY;
                    int distance = Game.playerViewDistance * Game.tileSize;

                    if (newX > width / 2 - player.Width / 2 - distance && newX < width / 2 - player.Width / 2 + distance)
                    {
                        if (newY > height / 2 - player.Height / 2 - distance && newY < height / 2 - player.Height / 2 + distance)
                        {
                            if (newX >= width / 2 - player.Width / 2 - 80 && newX <= width / 2 - player.Width / 2 + 80 && !tooltipDrawn)
                            {
                                if (newY >= height / 2 - player.Height / 2 - 80 && newY <= height / 2 - player.Height / 2 + 80)
                                {
                                    g.FillRectangle(Brushes.Gray, newX, newY - 15, 70, 20);
                                    g.DrawString(i.type.ToString() + "\n Press <f> to equip", f, Brushes.Black, newX, newY - 15);
                                    tooltipDrawn = true;
                                }
                            }

                            switch (i.type)
                            {
                                case EnumHandler.Items.Flashlight:
                                    g.DrawImage(flashlight, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Waterbottle:
                                    g.DrawImage(waterBottle, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Knife:
                                    g.DrawImage(knife, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Peanutbutter:
                                    g.DrawImage(peanutButter, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Bread:
                                    g.DrawImage(bread, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Emptybottle:
                                    g.DrawImage(emptyBottle, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                                case EnumHandler.Items.Pistol:
                                    g.DrawImage(pistol, newX + Game.tileSize / 2, newY + Game.tileSize / 2, 15, 15);
                                    break;
                            }
                        }
                    }
                }
                #endregion

                //draw player
                int positionX = width / 2 - player.Width / 2;
                int positionY = height / 2 - player.Height / 2;
                float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - positionY, (double)MouseHandler.mouseX - positionX)) * (180/Math.PI));
                
                g.DrawImage(RotateImage(player, angle), width / 2 - player.Width / 2, height / 2 - player.Height / 2, Game.tileSize, Game.tileSize);

                //draw item player is holding
                //TODO\\
                EnumHandler.Items selectedItem = Game.itemsInHotbar[Game.selectedHotbar - 1];


                #region DrawZombies
                foreach (Zombie z in Game.zombies)
                {
                    int newX = z.x - Game.player.playerX;
                    int newY = z.y - Game.player.playerY;
                    float zombieAngle = (float)((Math.Atan2((double)z.lookingToward.Y - newY, (double)z.lookingToward.X - newX)) * (180 / Math.PI));

                    g.DrawImage(RotateImage(zombie, zombieAngle), newX, newY, Game.tileSize, Game.tileSize);
                }
                #endregion

                //draw fog
                fogGenerator(width, height, g);

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


                #region Hotbar
                //draw hotbar
                for (int i = 0; i < Game.numberOfhotBarSlots; i++)
                {
                    int x = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + i * 60;
                    int y = height - 60;
                    Color c = Color.Brown;

                    if (Game.selectedHotbar - 1 == i) c = Color.White;

                    g.DrawRectangle(new Pen(c, 4), x, y, 50, 50);
                }

                //draw hotbar items
                int index = 0;
                foreach (EnumHandler.Items item in Game.itemsInHotbar)
                {
                    int xToDraw = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + index * 60;

                    drawItem(xToDraw, height - 60, item, g);

                    index++;
                }

                //draw hotbar tooltips
                EnumHandler.Items selectedItemInHotbar = Game.itemsInHotbar[Game.selectedHotbar - 1];

                if (!selectedItemInHotbar.Equals(EnumHandler.Items.None))
                {
                    g.FillRectangle(Brushes.Gray, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115, 300, 50);
                    g.DrawString("Press <q> to drop", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 85);
                }

                switch (selectedItemInHotbar)
                {
                    case EnumHandler.Items.Flashlight:
                        g.DrawString("Left click, toggle on/off", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, melee swing", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Waterbottle:
                        g.DrawString("Left click, drink(50 water)", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, pour on ground", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Knife:
                        g.DrawString("Left click, melee stab", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, melee swing", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Peanutbutter:
                        g.DrawString("Left click, eat(10 food, 15 thirst)", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, ranged throw", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Bread:
                        g.DrawString("Left click, eat(25 food, 15 thirst)", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, melee swing", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Emptybottle:
                        g.DrawString("Left click, melee swing", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, ranged throw", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.Pistol:
                        g.DrawString("Left click, ranged shoot", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, reload", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                }

                #endregion


                #region Inventory
                if (Game.inInventory)
                {
                    //draw slots
                    g.FillRectangle(Brushes.DarkGray, width / 2 - 150, height / 2 - 150, 300, 300);
                    g.FillRectangle(Brushes.LightGray, width / 2 + 150, height / 2 - 150, 178, 178);

                    for (int x = width / 2 - 150; x <width / 2 + 150; x += 60)
                    {
                        for (int y = height / 2 - 150; y < height / 2 + 150; y += 60)
                        {
                            g.DrawRectangle(new Pen(Color.Brown, 4), x + 4, y + 4, 50, 50);
                        }
                    }

                    for (int x = width / 2 + 150; x < width / 2 + 300; x += 60)
                    {
                        for (int y = height / 2 - 150; y < height / 2 + 10; y += 60)
                        {
                            g.DrawRectangle(new Pen(Color.Orange, 4), x + 4, y + 4, 50, 50);
                        }
                    }

                    //draw items in inventory
                    int xOfItemToDraw = width / 2 - 145;
                    int yOfItemToDraw = height / 2 - 145;
                    foreach (EnumHandler.Items itemInInventory in Game.itemsInInventory)
                    {
                        drawItem(xOfItemToDraw, yOfItemToDraw, itemInInventory, g);

                        xOfItemToDraw += 60;
                        if (xOfItemToDraw >= width / 2 + 145)
                        {
                            xOfItemToDraw = width / 2 - 145;
                            yOfItemToDraw += 60;
                        }
                    }

                    //draw held item
                    if (!MouseHandler.itemHeldByMouse.Equals(EnumHandler.Items.None))
                    {
                        drawItem(MouseHandler.mouseX - 25, MouseHandler.mouseY - 25, MouseHandler.itemHeldByMouse, g);
                    }
                }
                #endregion


                //draw cursor/pause menu
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
                    g.DrawString("Exit To Desktop", font, Brushes.Black, width / 2 - g.MeasureString("Exit To Desktop", font).Width / 2, height / 2 + 50);

                }
            }
            #endregion
        }

        //fog generator
        private void fogGenerator(int width, int height, Graphics g)
        {
            int maxCycle = 4;
            int cycle = maxCycle;
            int distance = Game.playerViewDistance;

            foreach (Tile t in Game.worldTiles)
            {
                int newX = t.x - Game.player.playerX;
                int newY = t.y - Game.player.playerY;
                if (newX > 0 - Game.tileSize && newX < width)
                {
                    if (newY > 0 - Game.tileSize && newY < height)
                    {
                        Boolean pass = true;
                        int newDistance = distance * Game.tileSize;

                        if (newX >= width / 2 - player.Width / 2 - newDistance && newX <= width / 2 - player.Width / 2 + newDistance)
                        {
                            if (newY >= height / 2 - player.Height / 2 - newDistance && newY <= height / 2 - player.Height / 2 + newDistance)
                            {
                                pass = false;
                            }
                        }

                        if (pass)
                        {
                            g.DrawImage(fog, newX, newY, 25, 25);
                        }
                    }
                }
            }

           //fogAnimator();
        }

        //animate fog
        private void fogAnimator()
        {
            fogFrame += 0.1F;

            if (fogFrame >= fog.GetFrameCount(new System.Drawing.Imaging.FrameDimension(fog.FrameDimensionsList[0])) - 1)
            {
                fogFrame = 0.0F;
            }
            System.Drawing.Imaging.FrameDimension dim = new System.Drawing.Imaging.FrameDimension(fog.FrameDimensionsList[0]);
            fog.SelectActiveFrame(dim, Convert.ToInt32(fogFrame));
        }

        //draw item
        private void drawItem(int x, int y, EnumHandler.Items itemType, Graphics g)
        {

            switch (itemType)
            {
                case EnumHandler.Items.Flashlight:
                    g.DrawImage(flashlightIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Waterbottle:
                    g.DrawImage(waterBottleIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Knife:
                    g.DrawImage(knifeIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Peanutbutter:
                    g.DrawImage(peanutButterIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Bread:
                    g.DrawImage(breadIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Emptybottle:
                    g.DrawImage(emptyBottleIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Pistol:
                    g.DrawImage(pistolIcon, x, y, 50, 50);
                    break;
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
