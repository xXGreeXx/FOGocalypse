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
        public List<Tile> GenerateWorld(int sizeOfWorld)
        {
            List<Tile> tilesForWorld = new List<Tile>();
            EnumHandler.TileTypes type = EnumHandler.TileTypes.Grass;

            //fill with grass
            for (int x = 0; x < sizeOfWorld; x++)
            {
                for (int y = 0; y < sizeOfWorld; y++)
                {
                    tilesForWorld.Add(new Tile(x * Game.tileSize, y * Game.tileSize, type));
                }
            }

            //generate houses
            for (int i = 0; i < generator.Next(2, 4); i++)
            {
                int sizeOfHouse = generator.Next(11, 15);
                int position = (sizeOfWorld / 4) * i;
                int doorPosition = sizeOfHouse / 2 + 1;

                int xOffset = 0;
                int yOffset = 0;
                for (int houseIndex = 0; houseIndex < 4; houseIndex++)
                {
                    List<Tile> tilesReturned;

                    if (houseIndex == 0)
                    {
                        tilesReturned = generateHouse(position - xOffset, position - yOffset, sizeOfHouse, doorPosition, true);
                    }
                    else
                    {
                        tilesReturned = generateHouse(position - xOffset, position - yOffset, sizeOfHouse, doorPosition, false);
                    }

                    foreach (Tile t in tilesReturned)
                    {
                        int index = 0;
                        foreach (Tile t2 in tilesForWorld)
                        {
                            if (t.x == t2.x && t.y == t2.y)
                            {
                                tilesForWorld[index].type = t.type;
                                break;
                            }

                            index++;
                        }
                    }

                    if (xOffset == 15)
                    {
                        xOffset = 0;
                        yOffset += 15;
                    }
                    xOffset += 15;
                }
            }

            //generate fields
            int xOfField = 0;
            int yOfField = sizeOfWorld - 15;
            int sizeOfField = generator.Next(10, 15);

            foreach(Tile t in generateField(xOfField, yOfField, sizeOfField))
            {
                int index = 0;
                foreach (Tile t2 in tilesForWorld)
                {
                    if (t.x == t2.x && t.y == t2.y)
                    {
                        tilesForWorld[index].type = t.type;
                        break;
                    }

                    index++;
                }
            }

            return tilesForWorld;
        }

        //generate house
        private List<Tile> generateHouse(int x, int y, int size, int doorPosition, Boolean doorOnLeft)
        {
            //tiles
            List<Tile> tilesForHouse = new List<Tile>();

            for (int i = 0; i < size; i++)
            {
                if (i == doorPosition || i == doorPosition + 1)
                {
                    if (doorOnLeft)
                    {
                        tilesForHouse.Add(new Tile((x + 10) * Game.tileSize, (y + i) * Game.tileSize, EnumHandler.TileTypes.Wood));
                    }
                    else
                    {
                        tilesForHouse.Add(new Tile(x * Game.tileSize, (y + i) * Game.tileSize, EnumHandler.TileTypes.Wood));
                    }
                }
                else
                {
                    tilesForHouse.Add(new Tile(x * Game.tileSize, (y + i) * Game.tileSize, EnumHandler.TileTypes.Wood));
                    tilesForHouse.Add(new Tile((x + 10) * Game.tileSize, (y + i) * Game.tileSize, EnumHandler.TileTypes.Wood));
                }
            }

            for (int i = 0; i < 10; i++)
            {
                tilesForHouse.Add(new Tile((x + i) * Game.tileSize, (y) * Game.tileSize, EnumHandler.TileTypes.Wood));
                tilesForHouse.Add(new Tile((x + i) * Game.tileSize, (y + size) * Game.tileSize, EnumHandler.TileTypes.Wood));
            }

            for (int i = 1; i < 10; i++)
            {
                for (int i2 = 1; i2 < size; i2++)
                {
                    tilesForHouse.Add(new Tile((x + i) * Game.tileSize, (y + i2) * Game.tileSize, EnumHandler.TileTypes.Carpet));
                }
            }

            tilesForHouse.Add(new Tile((x + 10) * Game.tileSize, (y + size) * Game.tileSize, EnumHandler.TileTypes.Wood));

            //furniture
            Game.furnitureInWorld.Add(new Furniture((x + 1) * Game.tileSize, (y + 1) * Game.tileSize, EnumHandler.FurnitureTypes.Table, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 2) * Game.tileSize, (y + 3) * Game.tileSize, EnumHandler.FurnitureTypes.Chair, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 8) * Game.tileSize, (y + 1) * Game.tileSize, EnumHandler.FurnitureTypes.Couch, 90));
            Game.furnitureInWorld.Add(new Furniture((x + 9) * Game.tileSize, (y + size - 2) * Game.tileSize, EnumHandler.FurnitureTypes.Bed, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 8) * Game.tileSize, (y + size - 1) * Game.tileSize, EnumHandler.FurnitureTypes.SmallTable, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 1) * Game.tileSize, (y + size - 1) * Game.tileSize, EnumHandler.FurnitureTypes.Counter, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 2) * Game.tileSize, (y + size - 1) * Game.tileSize, EnumHandler.FurnitureTypes.Sink, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 3) * Game.tileSize, (y + size - 1) * Game.tileSize, EnumHandler.FurnitureTypes.Counter, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 1) * Game.tileSize, (y + size - 2) * Game.tileSize, EnumHandler.FurnitureTypes.Counter, 0));
            Game.furnitureInWorld.Add(new Furniture((x + 1) * Game.tileSize, (y + size - 3) * Game.tileSize, EnumHandler.FurnitureTypes.Oven, 90));

            //items
            int water = generator.Next(0, 4);
            int peanutButter = generator.Next(0, 3);
            int bread = generator.Next(0, 5);
            int knife = generator.Next(0, 6);
            int pistol = generator.Next(0, 15);

            if (water == 1)
            {
                Game.itemsInWorld.Add(new Item((x + 1) * Game.tileSize, (y + 1) * Game.tileSize, EnumHandler.Items.Waterbottle));
            }
            if (peanutButter == 1)
            {
                Game.itemsInWorld.Add(new Item((x + 2) * Game.tileSize, (y + 1) * Game.tileSize, EnumHandler.Items.Peanutbutter));
            }
            if (bread == 1)
            {
                Game.itemsInWorld.Add(new Item((x + 3) * Game.tileSize, (y + 1) * Game.tileSize, EnumHandler.Items.Bread));
            }
            if (knife == 1)
            {
                Game.itemsInWorld.Add(new Item((x + 1) * Game.tileSize, (y + 2) * Game.tileSize, EnumHandler.Items.Knife));
            }
            if (pistol == 1)
            {
                Item pistolItem = new Item((x + 8) * Game.tileSize, (y + size - 1) * Game.tileSize, EnumHandler.Items.Pistol);
                Item ammoItem = new Item((x + 8) * Game.tileSize, (y + size - 2) * Game.tileSize, EnumHandler.Items.PistolAmmo);

                pistolItem.ammo = generator.Next(0, 6);
                ammoItem.ammo = 12;

                Game.itemsInWorld.Add(pistolItem);
                Game.itemsInWorld.Add(ammoItem);
            }

            return tilesForHouse;
        }

        //generate forest
        private void generateForest(int x, int y, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int i2 = 0; i2 < size; i2++)
                {
                    Game.plantsInWorld.Add(new Plant((x + i) * Game.tileSize * 3 + generator.Next(10, 20), (y + i2) * Game.tileSize * 3 + generator.Next(10, 20), EnumHandler.PlantTypes.Tree, 0));
                }
            }
        }

        //generate field
        private List<Tile> generateField(int x, int y, int size)
        {
            List<Tile> tilesForField = new List<Tile>();

            Boolean skip = false;
            for (int i = 0; i < size; i++)
            {
                for (int i2 = 0; i2 < size; i2++)
                {
                    tilesForField.Add(new Tile((x + i) * Game.tileSize, (y + i2) * Game.tileSize, EnumHandler.TileTypes.TilledDirt));
                    if (skip)
                    {
                        skip = false;
                    }
                    else
                    {
                        Game.plantsInWorld.Add(new Plant((x + i) * Game.tileSize, (y + i2) * Game.tileSize, EnumHandler.PlantTypes.Bush, generator.Next(10, 15)));
                        skip = true;
                    }
                }
            }

            return tilesForField;
        }
    }
}
