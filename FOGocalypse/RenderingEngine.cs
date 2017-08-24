﻿using System;
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
            #region Game
            if (Game.state.Equals(EnumHandler.GameStates.Game))
            {
                //draw tiles
                foreach (Tile t in Game.worldTiles)
                {
                    int x = t.x - Game.player.playerX;
                    int y = t.y - Game.player.playerY;

                    if (x > 0 - Game.tileSize && x < width + Game.tileSize)
                    {
                        if (y > 0 - Game.tileSize && y < height + Game.tileSize)
                        {
                            if (t.type.Equals(EnumHandler.TileTypes.Grass)) g.FillRectangle(Brushes.Green, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Dirt)) g.FillRectangle(Brushes.Orange, x, y, Game.tileSize, Game.tileSize);
                            else if (t.type.Equals(EnumHandler.TileTypes.Wood)) g.FillRectangle(Brushes.Brown, x, y, Game.tileSize, Game.tileSize);
                        }
                    }  
                }

                //draw player
                if (Game.player.direction.Equals(EnumHandler.Directions.Right)) { player.RotateFlip(RotateFlipType.RotateNoneFlipX); }
                else if (Game.player.direction.Equals(EnumHandler.Directions.Up)) { player.RotateFlip(RotateFlipType.Rotate90FlipNone); }
                else if (Game.player.direction.Equals(EnumHandler.Directions.Down)) { player.RotateFlip(RotateFlipType.Rotate270FlipNone); }

                g.DrawImage(player, width / 2 - player.Width / 2, height / 2 - player.Height / 2, Game.tileSize, Game.tileSize);

                player = FOGocalypse.Properties.Resources.player;

            }
            #endregion
        }
    }
}
