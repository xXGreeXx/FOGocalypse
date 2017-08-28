using System;
using System.Windows.Forms;
using System.Drawing;

namespace FOGocalypse
{
    public class KeyBoardHandler
    {
        Bitmap player = FOGocalypse.Properties.Resources.player;
        public static String lastKeyPressed { get; set; } = "";

        //constructor
        public KeyBoardHandler()
        {

        }

        //read key input
        public void ReadKey(Keys key, Boolean down)
        {
            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game) && !Game.inLossScreen)
            {
                if (down && (key.Equals(Keys.W) || key.Equals(Keys.S) || key.Equals(Keys.A) || key.Equals(Keys.D)))
                {
                    lastKeyPressed = key.ToString();
                }
                else if (!down && key.ToString().Equals(lastKeyPressed))
                {
                    lastKeyPressed = "";
                }

                switch (key)
                {
                    case Keys.W:
                        if (down)
                        {
                            Game.player.playerYVelocity = -Game.playerMoveSpeed;
                            Game.player.direction = EnumHandler.Directions.Up;
                        }
                        else
                        {
                            Game.player.playerYVelocity = 0;
                        }
                        break;
                    case Keys.S:
                        if (down)
                        {
                            Game.player.playerYVelocity = Game.playerMoveSpeed;
                            Game.player.direction = EnumHandler.Directions.Down;
                        }
                        else
                        {
                            Game.player.playerYVelocity = 0;
                        }
                        break;
                    case Keys.A:
                        if (down)
                        {
                            Game.player.playerXVelocity = -Game.playerMoveSpeed;
                            Game.player.direction = EnumHandler.Directions.Left;
                        }
                        else
                        {
                            Game.player.playerXVelocity = 0;
                        }
                        break;
                    case Keys.D:
                        if (down)
                        {
                            Game.player.playerXVelocity = Game.playerMoveSpeed;
                            Game.player.direction = EnumHandler.Directions.Right;
                        }
                        else
                        {
                            Game.player.playerXVelocity = 0;
                        }
                        break;

                    case Keys.D1:
                        if (down)
                        {
                            Game.selectedHotbar = 1;
                        }
                        break;
                    case Keys.D2:
                        if (down)
                        {
                            Game.selectedHotbar = 2;
                        }
                        break;
                    case Keys.D3:
                        if (down)
                        {
                            Game.selectedHotbar = 3;
                        }
                        break;
                    case Keys.D4:
                        if (down)
                        {
                            Game.selectedHotbar = 4;
                        }
                        break;
                    case Keys.D5:
                        if (down)
                        {
                            Game.selectedHotbar = 5;
                        }
                        break;

                    case Keys.F:
                        if (down)
                        {
                            for (int index = 0; index < Game.itemsInWorld.Count; index++)
                            {
                                Item i = Game.itemsInWorld[index];

                                int newX = i.x - Game.player.playerX;
                                int newY = i.y - Game.player.playerY;

                                if (newX >= Game.canvasWidth / 2 - player.Width / 2 - 80 && newX <= Game.canvasWidth / 2 - player.Width / 2 + 80)
                                {
                                    if (newY >= Game.canvasHeight / 2 - player.Height / 2 - 80 && newY <= Game.canvasHeight / 2 - player.Height / 2 + 80)
                                    {
                                        int indexOfHotbarSlot = 0;
                                        foreach (Item itemInHotbar in Game.itemsInHotbar)
                                        {
                                            if (itemInHotbar.type.Equals(EnumHandler.Items.None))
                                            {
                                                Game.itemsInHotbar[indexOfHotbarSlot] = i;
                                                Game.itemsInWorld.RemoveAt(index);
                                                return;
                                            }

                                            indexOfHotbarSlot++;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case Keys.Q:
                        Item item = Game.itemsInHotbar[Game.selectedHotbar - 1];
                        if (!item.type.Equals(EnumHandler.Items.None))
                        {
                            item.x = Game.player.playerX + Game.canvasWidth / 2;
                            item.y = Game.player.playerY + Game.canvasHeight / 2;

                            Game.itemsInWorld.Add(item);
                            Game.itemsInHotbar[Game.selectedHotbar - 1] = new Item(0, 0, EnumHandler.Items.None);
                        }
                        break;

                    case Keys.E:
                        if (down)
                        {
                            if (Game.inInventory)
                            {
                                Game.inInventory = false;
                            }
                            else
                            {
                                Game.inInventory = true;
                            }
                        }
                        break;

                    case Keys.Escape:
                        if (down)
                        {
                            if (Game.inPauseMenu)
                            {
                                Game.inPauseMenu = false;
                            }
                            else
                            {
                                Game.inPauseMenu = true;
                            }
                        }
                        break;
                }
            }
            #endregion

            #region OptionsMenu
            if (Game.state.Equals(EnumHandler.GameStates.OptionsMenu))
            {
                switch (key)
                {
                    case Keys.Escape:
                        Game.state = EnumHandler.GameStates.MainMenu;
                        break;
                }
            }
            #endregion

            #region GameSettingsMenu
            if (Game.state.Equals(EnumHandler.GameStates.GameSettingsMenu))
            {
                switch (key)
                {
                    case Keys.Escape:
                        Game.state = EnumHandler.GameStates.MainMenu;
                        break;
                }
            }
            #endregion
        }
    }
}
