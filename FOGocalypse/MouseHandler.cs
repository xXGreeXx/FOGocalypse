using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FOGocalypse
{
    public class MouseHandler
    {
        //define global variabels
        public static int mouseX { get; set; }
        public static int mouseY { get; set; }
        public static EnumHandler.Items itemHeldByMouse { get; set; }
        private int indexOfItem { get; set; }
        private Boolean fromHotbar { get; set; } = false;

        //constructor
        public MouseHandler()
        {

        }

        //mouse moved
        public void RegisterMouseMove(int x, int y)
        {
            if (!Game.inPauseMenu && !Game.inLossScreen)
            {
                mouseX = x;
                mouseY = y;
            }
        }

        //mouse down
        public void RegisterMouseDown(int x, int y, MouseButtons button)
        {
            Graphics g = Graphics.FromImage(FOGocalypse.Properties.Resources.player);

            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameStates.MainMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                int baseOfText = 200;

                if (mouseX >= Game.canvasWidth / 2 - g.MeasureString("Play", f).Width / 2 && mouseX <= Game.canvasWidth / 2 + g.MeasureString("Play", f).Width / 2)
                {
                    if (mouseY >= baseOfText && MouseHandler.mouseY <= baseOfText + g.MeasureString("Play", f).Height)
                    {
                        Game.state = EnumHandler.GameStates.GameSettingsMenu;
                    }
                }

                if (mouseX >= Game.canvasWidth / 2 - g.MeasureString("Options", f).Width / 2 && MouseHandler.mouseX <= Game.canvasWidth / 2 + g.MeasureString("Options", f).Width / 2)
                {
                    if (mouseY >= baseOfText + 75 && MouseHandler.mouseY <= baseOfText + 75 + g.MeasureString("Options", f).Height)
                    {
                        Game.state = EnumHandler.GameStates.OptionsMenu;
                    }
                }

                if (mouseX >= Game.canvasWidth / 2 - g.MeasureString("Quit", f).Width / 2 && MouseHandler.mouseX <= Game.canvasWidth / 2 + g.MeasureString("Quit", f).Width / 2)
                {
                    if (mouseY >= baseOfText + 225 && MouseHandler.mouseY <= baseOfText + 225 + g.MeasureString("Quit", f).Height)
                    {
                        Application.Exit();
                    }
                }
            }
            #endregion

            #region OptionsMenu
            if (Game.state.Equals(EnumHandler.GameStates.OptionsMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                float widthOffset = 40 + g.MeasureString("Resolution: ", f).Width;

                if (y >= Game.canvasHeight / 2 - 250 && y <= Game.canvasHeight / 2 - 220)
                {
                    if (x >= Game.canvasWidth / 2 - 250 + widthOffset && x <= Game.canvasWidth / 2 - 140 + widthOffset)
                    {
                        Game.resolution = "1240x1440";
                    }
                    if (x >= Game.canvasWidth / 2 - 140 + widthOffset && x <= Game.canvasWidth / 2 - 30 + widthOffset)
                    {
                        Game.resolution = "1920x1080";
                    }
                    if (x >= Game.canvasWidth / 2 - 30 + widthOffset && x <= Game.canvasWidth / 2 + 80 + widthOffset)
                    {
                        Game.resolution = "fullscreen";
                    }
                }

                widthOffset = 40 + g.MeasureString("Framerate: ", f).Width;
                if (y >= Game.canvasHeight / 2 - 200 && y <= Game.canvasHeight / 2 - 170)
                {
                    if (x >= Game.canvasWidth / 2 - 250 + widthOffset && x <= Game.canvasWidth / 2 - 140 + widthOffset)
                    {
                        Game.frameRate = 30;
                    }
                    if (x >= Game.canvasWidth / 2 - 140 + widthOffset && x <= Game.canvasWidth / 2 - 30 + widthOffset)
                    {
                        Game.frameRate = 60;
                    }
                    if (x >= Game.canvasWidth / 2 - 30 + widthOffset && x <= Game.canvasWidth / 2 + 80 + widthOffset)
                    {
                        Game.frameRate = 120;
                    }
                }
            }
            #endregion

            #region GameSettingsMenu
            if (Game.state.Equals(EnumHandler.GameStates.GameSettingsMenu))
            {
                Font f = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                Font fSmall = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);

                if (MouseHandler.mouseX >= Game.canvasWidth / 2 + 75 - g.MeasureString("Begin!", f).Width && MouseHandler.mouseX <= Game.canvasWidth / 2 - 75 + g.MeasureString("Begin!", f).Width)
                {
                    if (MouseHandler.mouseY >= Game.canvasHeight / 2 + 200 && MouseHandler.mouseY <= Game.canvasHeight / 2 + 200 + g.MeasureString("Begin!", f).Height)
                    {
                        Game.player = new Player(Game.worldSize / 2 * Game.tileSize, Game.worldSize / 2 * Game.tileSize, EnumHandler.Directions.Left);

                        Game.itemsInHotbar[0] = EnumHandler.Items.Flashlight;
                        Game.itemsInHotbar[1] = EnumHandler.Items.Knife;
                        Game.itemsInHotbar[2] = EnumHandler.Items.Waterbottle;
                        Game.itemsInHotbar[3] = EnumHandler.Items.None;
                        Game.itemsInHotbar[4] = EnumHandler.Items.None;

                        for (int i = 0; i < Game.itemsInInventory.Length; i++)
                        {
                            Game.itemsInInventory[i] = EnumHandler.Items.None;
                        }

                        Game.zombies = new List<Zombie>();
                        Game.selectedHotbar = 1;
                        Game.inPauseMenu = false;
                        Game.inInventory = false;
                        Game.inStartScreen = false;
                        Game.inLossScreen = false;

                        Game.time = 800;
                        Game.day = DateTime.Now.Day;
                        Game.month = DateTime.Now.Month;
                        Game.year = DateTime.Now.Year;

                        Game.worldTiles = new WorldGenerator().GenerateWorld(Game.tileSize, Game.worldSize);
                        Game.state = EnumHandler.GameStates.Game;
                        Game.inStartScreen = true;   
                    }
                }
            }
            #endregion

            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                #region Items
                if (!Game.inInventory && !Game.inPauseMenu)
                {
                    EnumHandler.Items selectedItem = Game.itemsInHotbar[Game.selectedHotbar - 1];

                    switch (selectedItem)
                    {
                        case EnumHandler.Items.Flashlight:
                            if (button.Equals(MouseButtons.Right))
                            {
                                Game.combatSystem.SwingItem(10);
                            }
                            break;
                        case EnumHandler.Items.Waterbottle:
                            if (button.Equals(MouseButtons.Left))
                            {
                                Game.player.playerWaterNeed -= 50;
                                Game.itemsInHotbar[Game.selectedHotbar - 1] = EnumHandler.Items.Emptybottle;
                            }
                            if (button.Equals(MouseButtons.Right))
                            {
                                Game.itemsInHotbar[Game.selectedHotbar - 1] = EnumHandler.Items.Emptybottle;
                            }
                            break;
                        case EnumHandler.Items.Peanutbutter:
                            if (button.Equals(MouseButtons.Left))
                            {
                                Game.player.playerFoodNeed -= 10;
                                Game.player.playerWaterNeed += 15;
                                Game.itemsInHotbar[Game.selectedHotbar - 1] = EnumHandler.Items.None;
                            }
                            break;
                        case EnumHandler.Items.Bread:
                            if (button.Equals(MouseButtons.Left))
                            {
                                Game.player.playerFoodNeed -= 10;
                                Game.player.playerWaterNeed += 25;
                                Game.itemsInHotbar[Game.selectedHotbar - 1] = EnumHandler.Items.None;
                            }
                            break;
                        case EnumHandler.Items.Pistol:
                            if (button.Equals(MouseButtons.Left))
                            {
                                Game.combatSystem.FireItem(50, new Point(MouseHandler.mouseX, MouseHandler.mouseY));
                            }
                            break;
                        case EnumHandler.Items.Knife:
                            if (button.Equals(MouseButtons.Left))
                            {
                                Game.combatSystem.SwingItem(35);
                            }
                            if (button.Equals(MouseButtons.Right))
                            {
                                Game.combatSystem.SwingItem(25);
                            }
                            break;
                    }

                    if (Game.player.playerWaterNeed < 0) Game.player.playerWaterNeed = 0;
                    if (Game.player.playerFoodNeed < 0) Game.player.playerFoodNeed = 0;
                }
                #endregion

                #region Iventory/Hotbar
                if (Game.inInventory)
                {
                    for (int i = 0; i < Game.itemsInHotbar.Length; i++)
                    {
                        int xOfItem = Game.canvasWidth / 2 - (60 * Game.numberOfhotBarSlots / 2) + i * 60;
                        int yOfItem = Game.canvasHeight - 60;

                        if (x >= xOfItem && x <= xOfItem + 50)
                        {
                            if (y >= yOfItem && y <= yOfItem + 50)
                            {
                                itemHeldByMouse = Game.itemsInHotbar[i];
                                indexOfItem = i;
                                fromHotbar = true;
                                return;
                            }
                        }
                    }

                    int xOfItem2 = Game.canvasWidth / 2 - 145;
                    int yOfItem2 = Game.canvasHeight / 2 - 145;
                    for (int i = 0; i < Game.itemsInInventory.Length; i++)
                    {
                        if (x >= xOfItem2 && x <= xOfItem2 + 50)
                        {
                            if (y >= yOfItem2 && y <= yOfItem2 + 50)
                            {
                                itemHeldByMouse = Game.itemsInInventory[i];
                                indexOfItem = i;
                                fromHotbar = false;
                            }
                        }

                        xOfItem2 += 60;
                        if (xOfItem2 >= Game.canvasWidth / 2 + 145)
                        {
                            xOfItem2 = Game.canvasWidth / 2 - 145;
                            yOfItem2 += 60;
                        }
                    }


                }
                #endregion
            }
            #endregion
        }

        //mouse up
        public void RegisterMouseUp(int x, int y, MouseButtons button)
        {
            #region Game
            if (Game.inInventory)
            {
                #region Iventory/Hotbar
                if (Game.inInventory)
                {
                    Boolean foundSlot = false;

                    for (int i = 0; i < Game.itemsInHotbar.Length; i++)
                    {
                        int xOfItem = Game.canvasWidth / 2 - (60 * Game.numberOfhotBarSlots / 2) + i * 60;
                        int yOfItem = Game.canvasHeight - 60;

                        if (x >= xOfItem && x <= xOfItem + 50)
                        {
                            if (y >= yOfItem && y <= yOfItem + 50)
                            {
                                if (Game.itemsInHotbar[i].Equals(EnumHandler.Items.None))
                                {
                                    Game.itemsInHotbar[i] = itemHeldByMouse;
                                    itemHeldByMouse = EnumHandler.Items.None;
                                    foundSlot = true;
                                    if (fromHotbar)
                                    {
                                        Game.itemsInHotbar[indexOfItem] = EnumHandler.Items.None;
                                    }
                                    else
                                    {
                                        Game.itemsInInventory[indexOfItem] = EnumHandler.Items.None;
                                    }
                                }
                            }
                        }
                    }

                    int xOfItem2 = Game.canvasWidth / 2 - 145;
                    int yOfItem2 = Game.canvasHeight / 2 - 145;
                    for (int i = 0; i < Game.itemsInInventory.Length; i++)
                    {
                        if (x >= xOfItem2 && x <= xOfItem2 + 50)
                        {
                            if (y >= yOfItem2 && y <= yOfItem2 + 50)
                            {
                                if (!itemHeldByMouse.Equals(EnumHandler.Items.None))
                                {
                                    if (Game.itemsInInventory[i].Equals(EnumHandler.Items.None))
                                    {
                                        Game.itemsInInventory[i] = itemHeldByMouse;
                                        itemHeldByMouse = EnumHandler.Items.None;
                                        foundSlot = true;
                                        if (fromHotbar)
                                        {
                                            Game.itemsInHotbar[indexOfItem] = EnumHandler.Items.None;
                                        }
                                        else
                                        {
                                            Game.itemsInInventory[indexOfItem] = EnumHandler.Items.None;
                                        }
                                    }
                                }
                            }
                        }

                        xOfItem2 += 60;
                        if (xOfItem2 >= Game.canvasWidth / 2 + 145)
                        {
                            xOfItem2 = Game.canvasWidth / 2 - 145;
                            yOfItem2 += 60;
                        }
                    }

                    if (!foundSlot)
                    {
                        itemHeldByMouse = EnumHandler.Items.None;
                    }
                }
                #endregion/Hotbar
            }
            #endregion
        }
    }
}
