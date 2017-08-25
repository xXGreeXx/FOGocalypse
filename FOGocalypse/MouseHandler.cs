﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace FOGocalypse
{
    class MouseHandler
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
            mouseX = x;
            mouseY = y;
        }

        //mouse down
        public void RegisterMouseDown(int x, int y, MouseButtons button)
        {
            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                #region Items

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
