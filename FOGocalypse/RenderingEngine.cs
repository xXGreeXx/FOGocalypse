using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

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
        Bitmap carpet = FOGocalypse.Properties.Resources.carpet;
        Bitmap tilledDirt = FOGocalypse.Properties.Resources.tilledDirt;
        Bitmap door = FOGocalypse.Properties.Resources.door;
        Bitmap stone = FOGocalypse.Properties.Resources.stone;
        Bitmap waterDrop = FOGocalypse.Properties.Resources.waterDrop;
        Bitmap heart = FOGocalypse.Properties.Resources.heart;
        Bitmap food = FOGocalypse.Properties.Resources.food;
        Bitmap warningIcon = FOGocalypse.Properties.Resources.warningIcon;
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
        Bitmap pistolAmmo = FOGocalypse.Properties.Resources.pistolAmmo;
        Bitmap pistolAmmoIcon = FOGocalypse.Properties.Resources.pistolAmmoIcon;
        Bitmap berry = FOGocalypse.Properties.Resources.berry;
        Bitmap berryIcon = FOGocalypse.Properties.Resources.berryIcon;
        Bitmap title1 = FOGocalypse.Properties.Resources.title1;
        Bitmap title2 = FOGocalypse.Properties.Resources.title2;
        Bitmap fogBackground = FOGocalypse.Properties.Resources.fogBackground;
        Bitmap upArrow = FOGocalypse.Properties.Resources.upArrow;
        Bitmap downArrow = FOGocalypse.Properties.Resources.downArrow;
        Bitmap hotbarSlot = FOGocalypse.Properties.Resources.hotbarSlot;
        Bitmap couch = FOGocalypse.Properties.Resources.couch;
        Bitmap table = FOGocalypse.Properties.Resources.table;
        Bitmap chair = FOGocalypse.Properties.Resources.chair;
        Bitmap bed = FOGocalypse.Properties.Resources.bed;
        Bitmap smallTable = FOGocalypse.Properties.Resources.smallTable;
        Bitmap counter = FOGocalypse.Properties.Resources.counter;
        Bitmap sink = FOGocalypse.Properties.Resources.sink;
        Bitmap oven = FOGocalypse.Properties.Resources.oven;
        Bitmap tree = FOGocalypse.Properties.Resources.tree;
        Bitmap bush = FOGocalypse.Properties.Resources.bush;

        Bitmap gameSettingsBackground = FOGocalypse.Properties.Resources.gameSettingsBackground;
        Bitmap optionBackground = FOGocalypse.Properties.Resources.optionBackground;
        Bitmap optionBackgroundDown = FOGocalypse.Properties.Resources.optionBackgroundDown;
        Bitmap inventoryBackground = FOGocalypse.Properties.Resources.iventoryBackground;
        Bitmap hotbarTooltipBackground = FOGocalypse.Properties.Resources.hotbarTooltipBackground;

        public static int screenFade { get; set; } = 255;
        public static int weaponPositionOffsetX { get; set; } = 0;
        public static int weaponPositionOffsetY { get; set; } = 0;
        Random r = new Random();

        public static float scale { get; set; } = 1;

        //constructor
        public RenderingEngine()
        {
            player.RotateFlip(RotateFlipType.RotateNoneFlipX);
            zombie.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        //draw screen
        public void DrawScreen(int width, int height, Graphics g)
        {
            //adjust scale based on width/height
            float baseWidth = 1075;
            float baseHeight = 737;

            float currentWidth = Game.canvasWidth;
            float currentHeight = Game.canvasHeight;

            scale = Math.Abs((currentWidth - currentHeight) / (baseWidth - baseHeight));

            if (Game.antialias)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
            }
            else
            {
                g.SmoothingMode = SmoothingMode.Default;
            }

            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameStates.MainMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 30 * scale, FontStyle.Bold);
                float baseOfText = 200 * scale + (20 * scale);

                g.DrawImage(fogBackground, 0, 0, width, height);
                g.DrawImage(title1, width / 2 - title1.Width * scale / 2, 0, title1.Width * scale, title1.Height * scale);
                g.DrawImage(title2, width / 2 - title2.Width * scale / 2, title1.Height * scale, title2.Width * scale, title2.Height * scale);

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

                animateMenuBackground();
            }
            #endregion

            #region OptionsMenu
            if (Game.state.Equals(EnumHandler.GameStates.OptionsMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                Font fSmall = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);

                g.DrawImage(fogBackground, 0, 0, width, height);
                g.DrawImage(title1, width / 2 - title1.Width / 2, 0, title1.Width, title1.Height);
                g.DrawImage(title2, width / 2 - title2.Width / 2, title1.Height, title2.Width, title2.Height);

                g.DrawImage(gameSettingsBackground, width / 2 - 250, height / 2 - 250, 500, 500);

                //resolution
                g.DrawString("Resolution: ", f, Brushes.Black, width / 2 - 250, height / 2 - 250);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width, height / 2 - 250, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width + 110, height / 2 - 250, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width + 220, height / 2 - 250, 110, 30);

                if (Game.resolution.Equals("1240x1440")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width, height / 2 - 250, 110, 30);
                if (Game.resolution.Equals("1920x1080")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width + 110, height / 2 - 250, 110, 30);
                if (Game.resolution.Equals("fullscreen")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width + 220, height / 2 - 250, 110, 30);

                g.DrawString("1240x1440", fSmall, Brushes.Black, width / 2 - 250 + g.MeasureString("Resolution: ", f).Width, height / 2 - 247);
                g.DrawString("1920x1080", fSmall, Brushes.Black, width / 2 - 140 + g.MeasureString("Resolution: ", f).Width, height / 2 - 247);
                g.DrawString("Fullscreen", fSmall, Brushes.Black, width / 2 - 30 + g.MeasureString("Resolution: ", f).Width, height / 2 - 247);

                //framerate
                g.DrawString("Framerate: ", f, Brushes.Black, width / 2 - 250, height / 2 - 200);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width, height / 2 - 200, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width + 110, height / 2 - 200, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width + 220, height / 2 - 200, 110, 30);

                if (Game.frameRate == 30) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width, height / 2 - 200, 110, 30);
                if (Game.frameRate == 60) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width + 110, height / 2 - 200, 110, 30);
                if (Game.frameRate == 120) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width + 220, height / 2 - 200, 110, 30);

                g.DrawString("30/FPS", fSmall, Brushes.Black, width / 2 - 250 + g.MeasureString("Framerate: ", f).Width, height / 2 - 197);
                g.DrawString("60/FPS", fSmall, Brushes.Black, width / 2 - 140 + g.MeasureString("Framerate: ", f).Width, height / 2 - 197);
                g.DrawString("120/FPS", fSmall, Brushes.Black, width / 2 - 30 + g.MeasureString("Framerate: ", f).Width, height / 2 - 197);

                //fog
                g.DrawString("Fog: ", f, Brushes.Black, width / 2 - 250, height / 2 - 150);
                g.DrawImage(optionBackground, width / 2 - 200 + g.MeasureString("Fog: ", f).Width, height / 2 - 150, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 190 + g.MeasureString("Fog: ", f).Width + 110, height / 2 - 150, 110, 30);

                if (Game.fogOn) g.DrawImage(optionBackgroundDown, width / 2 - 200 + g.MeasureString("Fog: ", f).Width, height / 2 - 150, 110, 30);
                else g.DrawImage(optionBackgroundDown, width / 2 - 80 + g.MeasureString("Fog: ", f).Width, height / 2 - 150, 110, 30);

                g.DrawString("On", fSmall, Brushes.Black, width / 2 - 200 + g.MeasureString("Fog: ", f).Width, height / 2 - 147);
                g.DrawString("Off", fSmall, Brushes.Black, width / 2 - 80 + g.MeasureString("Fog: ", f).Width, height / 2 - 147);

                //rain
                g.DrawString("Rain: ", f, Brushes.Black, width / 2 - 250, height / 2 - 100);
                g.DrawImage(optionBackground, width / 2 - 200 + g.MeasureString("Rain: ", f).Width, height / 2 - 100, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 190 + g.MeasureString("Rain: ", f).Width + 110, height / 2 - 100, 110, 30);

                if (Game.rainOn) g.DrawImage(optionBackgroundDown, width / 2 - 200 + g.MeasureString("Rain: ", f).Width, height / 2 - 100, 110, 30);
                else g.DrawImage(optionBackgroundDown, width / 2 - 190 + g.MeasureString("Rain: ", f).Width + 110, height / 2 - 100, 110, 30);

                g.DrawString("On", fSmall, Brushes.Black, width / 2 - 200 + g.MeasureString("Rain: ", f).Width, height / 2 - 97);
                g.DrawString("Off", fSmall, Brushes.Black, width / 2 - 80 + g.MeasureString("Rain: ", f).Width, height / 2 - 97);

                //shadow quality
                g.DrawString("Shadows: ", f, Brushes.Black, width / 2 - 250, height / 2 - 50);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width, height / 2 - 50, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width + 110, height / 2 - 50, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width + 220, height / 2 - 50, 110, 30);

                if (Game.shadowQuality.Equals("low")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width, height / 2 - 50, 110, 30);
                if (Game.shadowQuality.Equals("medium")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width + 110, height / 2 - 50, 110, 30);
                if (Game.shadowQuality.Equals("high")) g.DrawImage(optionBackgroundDown, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width + 220, height / 2 - 50, 110, 30);

                g.DrawString("Low", fSmall, Brushes.Black, width / 2 - 250 + g.MeasureString("Shadows: ", f).Width, height / 2 - 47);
                g.DrawString("Medium", fSmall, Brushes.Black, width / 2 - 140 + g.MeasureString("Shadows: ", f).Width, height / 2 - 47);
                g.DrawString("High", fSmall, Brushes.Black, width / 2 - 30 + g.MeasureString("Shadows: ", f).Width, height / 2 - 47);

                //antialias
                g.DrawString("Antialias: ", f, Brushes.Black, width / 2 - 250, height / 2);
                g.DrawImage(optionBackground, width / 2 - 200 + g.MeasureString("Antialias: ", f).Width, height / 2, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 190 + g.MeasureString("Antialias: ", f).Width + 110, height / 2, 110, 30);

                if (Game.antialias) g.DrawImage(optionBackgroundDown, width / 2 - 200 + g.MeasureString("Antialias: ", f).Width, height / 2, 110, 30);
                else g.DrawImage(optionBackgroundDown, width / 2 - 190 + g.MeasureString("Antialias: ", f).Width + 110, height / 2, 110, 30);

                g.DrawString("On", fSmall, Brushes.Black, width / 2 - 200 + g.MeasureString("Antialias: ", f).Width, height / 2 + 3);
                g.DrawString("Off", fSmall, Brushes.Black, width / 2 - 80 + g.MeasureString("Antialias: ", f).Width, height / 2 + 3);

                //antialias
                g.DrawString("Blood: ", f, Brushes.Black, width / 2 - 250, height / 2 + 50);
                g.DrawImage(optionBackground, width / 2 - 200 + g.MeasureString("Blood: ", f).Width, height / 2 + 50, 110, 30);
                g.DrawImage(optionBackground, width / 2 - 190 + g.MeasureString("Blood: ", f).Width + 110, height / 2 + 50, 110, 30);

                if (Game.blood) g.DrawImage(optionBackgroundDown, width / 2 - 200 + g.MeasureString("Blood: ", f).Width, height / 2 + 50, 110, 30);
                else g.DrawImage(optionBackgroundDown, width / 2 - 190 + g.MeasureString("Blood: ", f).Width + 110, height / 2 + 50, 110, 30);

                g.DrawString("On", fSmall, Brushes.Black, width / 2 - 200 + g.MeasureString("Blood: ", f).Width, height / 2 + 53);
                g.DrawString("Off", fSmall, Brushes.Black, width / 2 - 80 + g.MeasureString("Blood: ", f).Width, height / 2 + 53);

                //sound volume
                g.DrawString("Sound Volume: ", f, Brushes.Black, width / 2 - 250, height / 2 + 100);

                //music volume
                g.DrawString("Music Volume: ", f, Brushes.Black, width / 2 - 250, height / 2 + 150);

                animateMenuBackground();
            }
            #endregion

            #region GameSettingsMenu
            if (Game.state.Equals(EnumHandler.GameStates.GameSettingsMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                Font fSmall = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);

                g.DrawImage(fogBackground, 0, 0, width, height);
                g.DrawImage(title1, width / 2 - title1.Width / 2, 0, title1.Width, title1.Height);
                g.DrawImage(title2, width / 2 - title2.Width / 2, title1.Height, title2.Width, title2.Height);
                g.DrawImage(gameSettingsBackground, width / 2 - 200, height / 2 - 250, 400, 500);

                g.DrawString("Begin!", f, Brushes.Black, width / 2 + 75 - g.MeasureString("Begin!", f).Width, height / 2 + 200);
                if (MouseHandler.mouseX >= width / 2 + 75 - g.MeasureString("Begin!", f).Width && MouseHandler.mouseX <= width / 2 - 75 + g.MeasureString("Begin!", f).Width)
                {
                    if (MouseHandler.mouseY >= height / 2 + 200 && MouseHandler.mouseY <= height / 2 + 200 + g.MeasureString("Begin!", f).Height)
                    {
                        g.DrawString("Begin!", f, Brushes.White, width / 2 + 75 - g.MeasureString("Begin!", f).Width, height / 2 + 200);
                    }
                }

                //world size
                g.DrawString("World Size", fSmall, Brushes.Black, width / 2 - 200, height / 2 - 250);
                g.DrawString(Game.worldSize.ToString(), fSmall, Brushes.Black, width / 2 - 200, height / 2 - 213);
                g.DrawImage(upArrow, width / 2 - 100, height / 2 - 220, 20, 20);
                g.DrawImage(downArrow, width / 2 - 100, height / 2 - 195, 20, 20);

                //zombie sight
                g.DrawString("Zombie Vision", fSmall, Brushes.Black, width / 2, height / 2 - 250);
                g.DrawString(Game.zombieViewDistance.ToString() + "tiles", fSmall, Brushes.Black, width / 2, height / 2 - 213);
                g.DrawImage(upArrow, width / 2 + 100, height / 2 - 220, 20, 20);
                g.DrawImage(downArrow, width / 2 + 100, height / 2 - 195, 20, 20);

                //item rarity
                g.DrawString("Item Rarity", fSmall, Brushes.Black, width / 2 - 200, height / 2 - 170);
                g.DrawString(Game.itemRarity.ToString() + "%", fSmall, Brushes.Black, width / 2 - 200, height / 2 - 133);
                g.DrawImage(upArrow, width / 2 - 100, height / 2 - 140, 20, 20);
                g.DrawImage(downArrow, width / 2 - 100, height / 2 - 115, 20, 20);

                //zombie rarity
                g.DrawString("Zombie Rarity", fSmall, Brushes.Black, width / 2, height / 2 - 170);
                g.DrawString(Game.zombieSpawnChance.ToString() + "%", fSmall, Brushes.Black, width / 2, height / 2 - 133);
                g.DrawImage(upArrow, width / 2 + 100, height / 2 - 140, 20, 20);
                g.DrawImage(downArrow, width / 2 + 100, height / 2 - 115, 20, 20);

                animateMenuBackground();
            }
            #endregion

            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 6, FontStyle.Bold);
                Font f2 = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold);

                #region DrawTiles
                //draw tiles
                foreach (Tile t in Game.worldTiles)
                {
                    int x = t.x - Game.player.playerX;
                    int y = t.y - Game.player.playerY;
                    int distance = Game.playerViewDistance * Game.tileSize;
                    Boolean inView = false;

                    if (x > width / 2 - player.Width / 2 - distance && x < width / 2 - player.Width / 2 + distance)
                    {
                        if (y > height / 2 - player.Height / 2 - distance && y < height / 2 - player.Height / 2 + distance)
                        {
                            if (t.type.Equals(EnumHandler.TileTypes.Grass)) g.DrawImage(grass, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Dirt)) g.DrawImage(dirt, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Wood)) g.DrawImage(wood, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Carpet)) g.DrawImage(carpet, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Stone)) g.DrawImage(stone, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.TilledDirt)) g.DrawImage(tilledDirt, x, y, Game.tileSize, Game.tileSize);

                            else if (t.type.Equals(EnumHandler.TileTypes.Door))
                            {
                                if (t.open)
                                {
                                    door.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                    g.DrawImage(door, x, y, 10, 100);
                                    door.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                }
                                else
                                {
                                    g.DrawImage(door, x, y, 10, 100);
                                }
                            }

                            //tile shading
                            if (!t.roofed)
                            {
                                int timeFromNormal = Game.time;
                                if (Game.time > 1200)
                                {
                                    timeFromNormal = 1200 - (Game.time - 1200);
                                }

                                int alphaForShadow = 200 - (timeFromNormal / 10);

                                if (Game.weather.Equals(EnumHandler.WeatherType.Cloudy))
                                {
                                    alphaForShadow = 180 - (timeFromNormal / 10);
                                }

                                else if (Game.weather.Equals(EnumHandler.WeatherType.Rainy))
                                {
                                    alphaForShadow = 160;
                                }

                                g.FillRectangle(new SolidBrush(Color.FromArgb(alphaForShadow, Color.Black)), x, y, Game.tileSize, Game.tileSize);
                            }

                            inView = true;
                        }
                    }

                    if (!inView && Game.fogOn)
                    {
                        if (x > 0 - Game.tileSize && x < width)
                        {
                            if (y > 0 - Game.tileSize && y < height)
                            {
                                int fogValue = t.fogValue;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(fogValue, Color.DarkGray)), x, y, Game.tileSize, Game.tileSize);

                                if (t.fogValue >= 250)
                                {
                                    t.swapFog = true;
                                }
                                if (t.fogValue <= 180)
                                {
                                    t.swapFog = false;
                                }

                                if (t.swapFog) t.fogValue -= 5;
                                else t.fogValue += 5;
                            }
                        }
                    }
                }
                #endregion

                #region Draw Furniture
                foreach (Furniture furniture in Game.furnitureInWorld)
                {
                    int newX = furniture.x - Game.player.playerX;
                    int newY = furniture.y - Game.player.playerY;
                    int distance = Game.playerViewDistance * Game.tileSize;

                    if (newX > width / 2 - player.Width / 2 - distance - 75 && newX < width / 2 - player.Width / 2 + distance)
                    {
                        if (newY > height / 2 - player.Height / 2 - distance - 50 && newY < height / 2 - player.Height / 2 + distance)
                        {
                            switch (furniture.type)
                            {
                                case EnumHandler.FurnitureTypes.Couch:
                                    if (furniture.rotation == 90)
                                    {
                                        couch.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        g.DrawImage(couch, newX, newY, couch.Width, couch.Height);
                                        couch.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    }
                                    break;
                                case EnumHandler.FurnitureTypes.Table:
                                    g.DrawImage(table, newX, newY, table.Width, table.Height);
                                    break;
                                case EnumHandler.FurnitureTypes.Chair:
                                    g.DrawImage(chair, newX, newY, chair.Width, chair.Height);
                                    break;
                                case EnumHandler.FurnitureTypes.Bed:
                                    g.DrawImage(bed, newX, newY, bed.Width / 2, bed.Height / 2);
                                    break;
                                case EnumHandler.FurnitureTypes.SmallTable:
                                    g.DrawImage(smallTable, newX, newY, smallTable.Width / 2, smallTable.Height / 2);
                                    break;
                                case EnumHandler.FurnitureTypes.Counter:
                                    g.DrawImage(counter, newX, newY, 25, 25);
                                    break;
                                case EnumHandler.FurnitureTypes.Sink:
                                    g.DrawImage(sink, newX, newY, 25, 25);
                                    break;
                                case EnumHandler.FurnitureTypes.Oven:
                                    if (furniture.rotation == 90)
                                    {
                                        oven.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        g.DrawImage(oven, newX, newY, 25, 25);
                                        oven.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                    }
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region Draw Plants
                Boolean plantTooltipDrawn = false;

                foreach (Plant p in Game.plantsInWorld)
                {
                    int newX = p.x - Game.player.playerX;
                    int newY = p.y - Game.player.playerY;
                    int distance = Game.playerViewDistance * Game.tileSize;

                    if (newX > width / 2 - player.Width / 2 - distance - 75 && newX < width / 2 - player.Width / 2 + distance)
                    {
                        if (newY > height / 2 - player.Height / 2 - distance - 50 && newY < height / 2 - player.Height / 2 + distance)
                        {
                            switch (p.type)
                            {
                                case EnumHandler.PlantTypes.Tree:
                                    g.DrawImage(tree, newX, newY, Game.tileSize * 2, Game.tileSize * 2);
                                    break;
                                case EnumHandler.PlantTypes.Bush:
                                    g.DrawImage(bush, newX, newY, Game.tileSize, Game.tileSize);

                                    int xOffset = 0;
                                    int yOffset = 0;
                                    for (int i = 0; i < p.berries * 2; i++)
                                    {
                                        g.FillRectangle(Brushes.Red, newX + xOffset, newY + yOffset, 1, 1);

                                        xOffset += 4;
                                        if (xOffset >= 19)
                                        {
                                            xOffset = 3;
                                            yOffset += 5;
                                        }
                                    }

                                    if (newX >= width / 2 - Game.tileSize * 2 && newX < width / 2 + Game.tileSize / 2 && !plantTooltipDrawn)
                                    {
                                        if (newY >= height / 2 - Game.tileSize * 2 && newY < height / 2 + Game.tileSize / 2)
                                        {
                                            g.FillRectangle(Brushes.Gray, newX, newY - 15, 100, 20);
                                            g.DrawString(p.type.ToString() + "\n Press <f> to gather berries", f, Brushes.Black, newX, newY - 15);
                                            plantTooltipDrawn = true;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region Items in the world

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

                            int xOfItem = newX + Game.tileSize / 2;
                            int yOfItem = newY + Game.tileSize / 2;

                            drawItemInWorld(xOfItem, yOfItem, i.type, g);
                        }
                    }
                }
                #endregion

                //draw player
                int positionX = width / 2 - player.Width / 2;
                int positionY = height / 2 - player.Height / 2;
                float angle = (float)((Math.Atan2((double)MouseHandler.mouseY - positionY, (double)MouseHandler.mouseX - positionX)) * (180 / Math.PI));
                
                g.DrawImage(RotateImage(player, angle), width / 2 - player.Width / 2, height / 2 - player.Height / 2, Game.tileSize, Game.tileSize);

                #region ItemHeld
                EnumHandler.Items selectedItem = Game.itemsInHotbar[Game.selectedHotbar - 1].type;

                angle /= (float)(180 / Math.PI);

                float rotationX = (float)(Math.Cos(angle) * Game.tileSize) - weaponPositionOffsetX;
                float rotationY = (float)(Math.Sin(angle) * Game.tileSize) - weaponPositionOffsetY;

                float angleOfItem = (float)((Math.Atan2((double)MouseHandler.mouseY - positionY + rotationX + 5, (double)MouseHandler.mouseX - positionX + rotationY + 5)) * (180 / Math.PI));

                switch (selectedItem)
                {
                    case EnumHandler.Items.Flashlight:
                        g.DrawImage(RotateImage(flashlight, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Knife:
                        g.DrawImage(RotateImage(knife, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Waterbottle:
                        g.DrawImage(RotateImage(waterBottle, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Emptybottle:
                        g.DrawImage(RotateImage(emptyBottle, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Pistol:
                        g.DrawImage(RotateImage(pistol, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.PistolAmmo:
                        g.DrawImage(RotateImage(pistolAmmo, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Bread:
                        g.DrawImage(RotateImage(bread, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Peanutbutter:
                        g.DrawImage(RotateImage(peanutButter, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                    case EnumHandler.Items.Berry:
                        g.DrawImage(RotateImage(berry, angleOfItem + 90), positionX + rotationX + 5, positionY + rotationY + 5, 15, 15);
                        break;
                }

                if (weaponPositionOffsetX > 0)
                {
                    weaponPositionOffsetX--;
                }
                if (weaponPositionOffsetY > 0)
                {
                    weaponPositionOffsetY--;
                }
                #endregion

                #region DrawZombies
                foreach (Zombie z in Game.zombies)
                {
                    int newX = z.x - Game.player.playerX;
                    int newY = z.y - Game.player.playerY;
                    float zombieAngle = (float)((Math.Atan2((double)z.lookingToward.Y - newY, (double)z.lookingToward.X - newX)) * (180 / Math.PI));
                    int distance = Game.playerViewDistance * Game.tileSize;

                    if (newX > width / 2 - player.Width / 2 - distance && newX < width / 2 - player.Width / 2 + distance)
                    {
                        if (newY > height / 2 - player.Height / 2 - distance && newY < height / 2 - player.Height / 2 + distance)
                        {
                            g.DrawImage(RotateImage(zombie, zombieAngle), newX, newY, Game.tileSize, Game.tileSize);
                        }
                    }
                }
                #endregion


                #region Particles
                //blood particles
                foreach (Particle p in Game.bloodParticles)
                {
                    int newX = p.x - Game.player.playerX;
                    int newY = p.y - Game.player.playerY;

                    g.FillRectangle(new SolidBrush(p.color), newX, newY, p.size, p.size);
                }
                #endregion


                #region PlayerNeeds
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

                //draw thirst/hunger low notification
                if (Game.player.playerWaterNeed >= 70)
                {
                    g.DrawImage(warningIcon, 220, 50, 25, 25);
                }
                if (Game.player.playerFoodNeed >= 70)
                {
                    g.DrawImage(warningIcon, 220, 90, 25, 25);
                }

                Font timeFont = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                String add = Game.time < 1000 ? "0" : "";
                add += Game.time == 0 ? "00" : "";
                g.DrawString(add + Game.time.ToString() + ": " + Game.month + "/"  + Game.day + "/" + Game.year, timeFont, Brushes.Black, 0, 130);

                g.DrawString("Weather: " + Game.weather, new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold), Brushes.Black, 0, 160);
                g.DrawString("Season: " + Game.season, new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold), Brushes.Black, 0, 180);
                #endregion


                #region Hotbar
                //draw hotbar
                for (int i = 0; i < Game.numberOfhotBarSlots; i++)
                {
                    int x = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + i * 60;
                    int y = height - 60;
                    Color c = Color.Brown;

                    if (Game.selectedHotbar - 1 == i) c = Color.White;

                    g.DrawImage(hotbarSlot, x, y, 50, 50);
                    g.DrawRectangle(new Pen(c, 4), x, y, 50, 50);
                }

                //draw hotbar items
                int index = 0;
                foreach (Item item in Game.itemsInHotbar)
                {
                    int xToDraw = width / 2 - (60 * Game.numberOfhotBarSlots / 2) + index * 60;

                    drawItemInHotbar(xToDraw, height - 60, item.type, g);

                    index++;
                }

                //draw hotbar tooltips
                EnumHandler.Items selectedItemInHotbar = Game.itemsInHotbar[Game.selectedHotbar - 1].type;

                if (!selectedItemInHotbar.Equals(EnumHandler.Items.None))
                {
                    g.DrawImage(hotbarTooltipBackground, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115, 300, 50);
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
                        g.DrawString("Right click, reload, <" + Game.itemsInHotbar[Game.selectedHotbar - 1].ammo.ToString() + "shots>", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                    case EnumHandler.Items.PistolAmmo:
                        g.DrawString("Ammo left: " + Game.itemsInHotbar[Game.selectedHotbar - 1].ammo.ToString(), f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        break;
                    case EnumHandler.Items.Berry:
                        g.DrawString("Left click, eat(5 water, 10 food)", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 115);
                        g.DrawString("Right click, ranged throw", f2, Brushes.Black, width / 2 - (60 * Game.numberOfhotBarSlots / 2) - 5, height - 100);
                        break;
                }

                #endregion


                #region Inventory
                if (Game.inInventory)
                {
                    //draw slots
                    g.DrawImage(inventoryBackground, width / 2 - 150, height / 2 - 150, 300, 300);

                    for (int x = width / 2 - 150; x <width / 2 + 150; x += 60)
                    {
                        for (int y = height / 2 - 150; y < height / 2 + 150; y += 60)
                        {
                            g.DrawImage(hotbarSlot, x + 4, y + 4, 50, 50);
                        }
                    }

                    //draw items in inventory
                    int xOfItemToDraw = width / 2 - 145;
                    int yOfItemToDraw = height / 2 - 145;
                    foreach (Item itemInInventory in Game.itemsInInventory)
                    {
                        drawItemInHotbar(xOfItemToDraw, yOfItemToDraw, itemInInventory.type, g);

                        xOfItemToDraw += 60;
                        if (xOfItemToDraw >= width / 2 + 145)
                        {
                            xOfItemToDraw = width / 2 - 145;
                            yOfItemToDraw += 60;
                        }
                    }

                    //draw held item
                    if (!MouseHandler.itemHeldByMouse.type.Equals(EnumHandler.Items.None))
                    {
                        drawItemInHotbar(MouseHandler.mouseX - 25, MouseHandler.mouseY - 25, MouseHandler.itemHeldByMouse.type, g);
                    }
                }
                #endregion


                #region StartingScreen
                if (Game.inStartScreen)
                {
                    if (screenFade >= 3)
                    {
                        String message = "You wake up, unsure of your surroundings, you can hardly see throught the thick fog. \nThere is a faint sound in the distance, it sounds like the groaning of the undead";

                        g.FillRectangle(new SolidBrush(Color.FromArgb(screenFade, Color.Black)), 0, 0, width, height);
                        g.DrawString(message, f2, new SolidBrush(Color.FromArgb(screenFade, Color.White)), width / 2 - g.MeasureString(message, f).Width / 2 - 50, height / 2 - 50);

                        screenFade -= 3;
                    }
                    else
                    {
                        Game.inStartScreen = false;
                    }
                }
                #endregion


                #region lossScreen
                if (Game.inLossScreen)
                {
                    if (screenFade <= 252)
                    {
                        String message = "You fall to the ground, darkness sourrounds you";

                        g.FillRectangle(new SolidBrush(Color.FromArgb(screenFade, Color.Black)), 0, 0, width, height);
                        g.DrawString(message, f2, new SolidBrush(Color.FromArgb(screenFade, Color.White)), width / 2 - g.MeasureString(message, f).Width / 2 - 50, height / 2 - 50);

                        screenFade += 3;
                    }
                    else
                    {
                        Game.state = EnumHandler.GameStates.MainMenu;
                        Game.inLossScreen = false;
                    }
                }
                #endregion


                #region PauseMenu
                //draw cursor/pause menu
                if (!Game.inPauseMenu)
                {
                    //Cursor.Hide();
                    //g.DrawEllipse(Pens.Black, MouseHandler.mouseX - 5, MouseHandler.mouseY - 5, 10, 10);
                }
                else
                {
                    //Cursor.Show();
                    g.DrawImage(title1, width / 2 - title1.Width / 2, height / 2 - 200, title1.Width, title1.Height);
                    g.DrawImage(title2, width / 2 - title1.Width / 2, height / 2 + 100, title1.Width, title1.Height);

                    Font font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);

                    g.DrawString("Return To Game", font, Brushes.Black, width / 2 - g.MeasureString("Return To Game", font).Width / 2, height / 2 - 100);
                    g.DrawString("Return To Menu", font, Brushes.Black, width / 2 - g.MeasureString("Return To Menu", font).Width / 2, height / 2 - 75);
                    g.DrawString("Exit To Desktop", font, Brushes.Black, width / 2 - g.MeasureString("Exit To Desktop", font).Width / 2, height / 2 + 50);

                    if (MouseHandler.mouseX >= width / 2 - g.MeasureString("Exit To Desktop", font).Width / 2 && MouseHandler.mouseX <= width / 2 + g.MeasureString("Exit To Dekstop", font).Width)
                    {
                        if (MouseHandler.mouseY >= height / 2 - 100 && MouseHandler.mouseY <= height / 2 - 100 + g.MeasureString("Return To Game", font).Height)
                        {
                            g.DrawString("Return To Game", font, Brushes.White, width / 2 - g.MeasureString("Return To Game", font).Width / 2, height / 2 - 100);
                        }
                        if (MouseHandler.mouseY >= height / 2 - 75 && MouseHandler.mouseY <= height / 2 - 75 + g.MeasureString("Return To Menu", font).Height)
                        {
                            g.DrawString("Return To Menu", font, Brushes.White, width / 2 - g.MeasureString("Return To Menu", font).Width / 2, height / 2 - 75);
                        }
                        if (MouseHandler.mouseY >= height / 2 + 50 && MouseHandler.mouseY <= height / 2 + 50 + g.MeasureString("Exit To Desktop", font).Height)
                        {
                            g.DrawString("Exit To Desktop", font, Brushes.White, width / 2 - g.MeasureString("Exit To Desktop", font).Width / 2, height / 2 + 50);
                        }
                    }
                }
                #endregion
            }
            #endregion

            g.DrawString(Game.FPS.ToString() + "fps", new Font(FontFamily.GenericSansSerif, 30 * scale, FontStyle.Bold), Brushes.Black, width - 150, 0);

        }

        //draw item in Hotbar
        private void drawItemInHotbar(int x, int y, EnumHandler.Items itemType, Graphics g)
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
                case EnumHandler.Items.PistolAmmo:
                    g.DrawImage(pistolAmmoIcon, x, y, 50, 50);
                    break;
                case EnumHandler.Items.Berry:
                    g.DrawImage(berryIcon, x, y, 50, 50);
                    break;
            }
        }

        //draw item in world
        private void drawItemInWorld(float x, float y, EnumHandler.Items itemType, Graphics g)
        {
            switch (itemType)
            {
                case EnumHandler.Items.Flashlight:
                    g.DrawImage(flashlight, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Waterbottle:
                    g.DrawImage(waterBottle, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Knife:
                    g.DrawImage(knife, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Peanutbutter:
                    g.DrawImage(peanutButter, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Bread:
                    g.DrawImage(bread, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Emptybottle:
                    g.DrawImage(emptyBottle, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Pistol:
                    g.DrawImage(pistol, x, y, 15, 15);
                    break;
                case EnumHandler.Items.Berry:
                    g.DrawImage(berry, x, y, 15, 15);
                    break;
            }
        }

        //background animation
        private void animateMenuBackground()
        {
            
        }

        //rotate image
        private static Bitmap RotateImage(Image image, float angle)
        {
            PointF offset = new PointF((float)image.Width / 2, (float)image.Height / 2);
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics g = Graphics.FromImage(rotatedBmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(angle);
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }
    }
}
