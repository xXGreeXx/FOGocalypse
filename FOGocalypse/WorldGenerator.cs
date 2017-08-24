using System;
using System.Collections.Generic;

namespace FOGocalypse
{
    class WorldGenerator
    {
        Random generator = new Random();

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
                    int number = generator.Next(1, 10);

                    if (number == 9)
                    {
                        type = EnumHandler.TileTypes.Dirt;
                    }

                    tilesForWorld.Add(new Tile(x * sizeOfTile, y * sizeOfTile, type));
                }
            }


            return tilesForWorld;
        }
    }
}
