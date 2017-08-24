using System.Drawing;

namespace FOGocalypse
{
    public class RenderingEngine
    {
        //define global variables
        Bitmap player = FOGocalypse.Properties.Resources.player;

        //constructor
        public RenderingEngine()
        {

        }

        //draw screen
        public void DrawScreen(int width, int height, Graphics g)
        {
            //draw player
            g.DrawImage(player, Game.player.playerX, Game.player.playerY, 50, 50);
        }
    }
}
