using System;
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
            if (Game.player.playerDirection.Equals(EnumHandler.Directions.Right)) { player.RotateFlip(RotateFlipType.RotateNoneFlipX); }
            else if (Game.player.playerDirection.Equals(EnumHandler.Directions.Up)) { player.RotateFlip(RotateFlipType.Rotate90FlipNone); }
            else if (Game.player.playerDirection.Equals(EnumHandler.Directions.Down)) { player.RotateFlip(RotateFlipType.Rotate270FlipNone); }

            g.DrawImage(player, Game.player.playerX, Game.player.playerY, Game.playerSize, Game.playerSize);

            player = FOGocalypse.Properties.Resources.player;
        }
    }
}
