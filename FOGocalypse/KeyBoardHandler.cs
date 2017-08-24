using System;
using System.Windows.Forms;
namespace FOGocalypse
{
    public class KeyBoardHandler
    {
        //constructor
        public KeyBoardHandler()
        {

        }

        //read key input
        public void ReadKey(Keys key, Boolean down)
        {
            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
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
                }
            }
            #endregion
        }
    }
}
