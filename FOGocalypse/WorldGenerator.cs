using System;
using System.Collections.Generic;

namespace FOGocalypse
{
    class WorldGenerator
    {
        //constructor
        public WorldGenerator()
        {

        }

        //generate world
        public List<Tile> GenerateWorld(int sizeOfTile, int sizeOfWorld)
        {
            List<Tile> tilesForWorld = new List<Tile>();
            Boolean grass = false;

            for (int x = 0; x < sizeOfWorld; x++)
            {
                for (int y = 0; y < sizeOfWorld; y++)
                {
                    EnumHandler.TileTypes type = EnumHandler.TileTypes.Grass;

                    tilesForWorld.Add(new Tile(x * sizeOfTile, y * sizeOfTile, type));
                }
            }


            return tilesForWorld;
        }
    }
}
