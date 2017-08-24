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
            switch (key)
            {
                case Keys.W:
                    if (down)
                    {
                        Game.player.playerYVelocity = -Game.playerMoveSpeed;
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
                    }
                    else
                    {
                        Game.player.playerXVelocity = 0;
                    }
                    break;
            }
        }
    }
}
