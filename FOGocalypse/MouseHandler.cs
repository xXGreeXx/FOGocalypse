using System;
using System.Drawing;
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

        //constructor
        public MouseHandler()
        {

        }

        //mouse moved
        public void RegisterMouseMove(int x, int y)
        {
            if (!Game.inPauseMenu)
            {
                mouseX = x;
                mouseY = y;
            }
        }

        //mouse down
        public void RegisterMouseDown(int x, int y, MouseButtons button)
        {
            #region MainMenu
            if (Game.state.Equals(EnumHandler.GameStates.MainMenu))
            {
                Graphics g = Graphics.FromImage(FOGocalypse.Properties.Resources.player);
                Font f = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                int baseOfText = 200;

                if (mouseX >= Game.canvasWidth / 2 - g.MeasureString("Play", f).Width / 2 && mouseX <= Game.canvasWidth / 2 + g.MeasureString("Play", f).Width / 2)
                {
                    if (mouseY >= baseOfText && MouseHandler.mouseY <= baseOfText + g.MeasureString("Play", f).Height)
                    {
                        Game.state = EnumHandler.GameStates.Game;
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

            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                #region Items
                if (!Game.inInventory && !Game.inPauseMenu)
                {
                    EnumHandler.Items selectedItem = Game.itemsInHotbar[Game.selectedHotbar - 1];

                    switch (selectedItem)
                    {
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
                                Game.itemsInInventory[i] = EnumHandler.Items.None;
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
                                Game.itemsInHotbar[i] = itemHeldByMouse;
                                itemHeldByMouse = EnumHandler.Items.None;
                                foundSlot = true;
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
                                    Game.itemsInInventory[i] = itemHeldByMouse;
                                    Game.itemsInHotbar[indexOfItem] = EnumHandler.Items.None;
                                    itemHeldByMouse = EnumHandler.Items.None;
                                    foundSlot = true;
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
