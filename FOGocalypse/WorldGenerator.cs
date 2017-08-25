using System;
using System.Collections.Generic;
using System.Drawing;

namespace FOGocalypse
{
    class WorldGenerator
    {
        Random generator = new Random();
        List<int[]> houses = new List<int[]>();

        //constructor
        public WorldGenerator()
        {

        }

        //generate world
        public List<Tile> GenerateWorld(int sizeOfTile, int sizeOfWorld)
        {
            List<Tile> tilesForWorld = new List<Tile>();

            for (int x = 0; x < sizeOfWorld; x++)
            {
                for (int y = 0; y < sizeOfWorld; y++)
                {
                    EnumHandler.TileTypes type = EnumHandler.TileTypes.Grass;
                    int number = generator.Next(1, 700);

                    if (number == 10)
                    {
                        Boolean pass = true;

                        foreach (int[] house in houses)
                        {
                            Rectangle house1 = new Rectangle(x, y, 15, 15);
                            Rectangle house2 = new Rectangle(house[0], house[1], 15, 15);

                            if (house1.IntersectsWith(house2))
                            {
                                pass = false;
                                tilesForWorld.Add(new Tile(x * sizeOfTile, y * sizeOfTile, type));
                                break;
                            }
                        }

                        if (pass)
                        {
                            houses.Add(new int[] { x, y });

                            foreach (Tile t in generateHouse(x, y))
                            {
                                tilesForWorld.Add(t);
                            }
                        }
                    }
                    else
                    {
                        tilesForWorld.Add(new Tile(x * sizeOfTile, y * sizeOfTile, type));
                    }
                }
            }


            return tilesForWorld;
        }

        //generate house
        private List<Tile> generateHouse(int x, int y)
        {
            List<Tile> tilesOfHouse = new List<Tile>();
            EnumHandler.TileTypes type = EnumHandler.TileTypes.Wood;
            int number = generator.Next(10, 16);
            int door = generator.Next(1, number);
            int water = generator.Next(1, 3);
            int knife = generator.Next(2, 5);
            int peanutButter = generator.Next(1, 3);

            for (int i = 0; i < 10; i++)
            {
                tilesOfHouse.Add(new Tile((x - i) * Game.tileSize, y * Game.tileSize, type));
                tilesOfHouse.Add(new Tile((x - i) * Game.tileSize, (y - number) * Game.tileSize, type));

                if (i == 9)
                {
                    tilesOfHouse.Add(new Tile((x - i - 1) * Game.tileSize, (y - number) * Game.tileSize, type));
                }
            }
            for (int i = 0; i < number; i++)
            {
                tilesOfHouse.Add(new Tile(x * Game.tileSize, (y - i) * Game.tileSize, type));

                if (i == door || i == door + 1) continue;
                tilesOfHouse.Add(new Tile((x - 10) * Game.tileSize, (y - i) * Game.tileSize, type));
            }

            if (water == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 1) * Game.tileSize, (y - 1) * Game.tileSize, EnumHandler.Items.Waterbottle));
            }
            if (knife == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 2) * Game.tileSize, (y - 1) * Game.tileSize, EnumHandler.Items.Knife));
            }
            if (peanutButter == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 1) * Game.tileSize, (y - 2) * Game.tileSize, EnumHandler.Items.Peanutbutter));
            }

            return tilesOfHouse;
        }
    }
}
