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
                    int number = generator.Next(1, 1800);

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
                            int doorLocation = generator.Next(2, 8);
                            int numberForWorldGenerator = generator.Next(10, 16);

                            houses.Add(new int[] { x, y });

                            #region House1
                            int index = 0;
                            foreach (Tile t in generateHouse(x, y, true, doorLocation, numberForWorldGenerator))
                            {
                                for (int index2 = 0; index2 < tilesForWorld.Count; index2++)
                                {
                                    Tile newTile = tilesForWorld[index2];

                                    if (t.x == newTile.x && t.y == newTile.y && index != index2)
                                    {
                                        tilesForWorld.RemoveAt(index2);
                                    }
                                }

                                tilesForWorld.Add(t);
                                index++;
                            }


                            generateItems(x, y, numberForWorldGenerator);
                            #endregion

                            #region House2
                            foreach (Tile t in generateHouse(x - 15, y, false, doorLocation, numberForWorldGenerator))
                            {
                                for (int index2 = 0; index2 < tilesForWorld.Count; index2++)
                                {
                                    Tile newTile = tilesForWorld[index2];

                                    if (t.x == newTile.x && t.y == newTile.y && index != index2)
                                    {
                                        tilesForWorld.RemoveAt(index2);
                                    }
                                }

                                tilesForWorld.Add(t);
                                index++;
                            }

                            generateItems(x - 15, y, numberForWorldGenerator);
                            #endregion

                            for (int yOfPath = 0; yOfPath < 20; yOfPath++)
                            {
                                tilesForWorld.Add(new Tile((x - 12) * Game.tileSize, (y - yOfPath) * Game.tileSize, EnumHandler.TileTypes.Stone));
                                tilesForWorld.Add(new Tile((x - 13) * Game.tileSize, (y - yOfPath) * Game.tileSize, EnumHandler.TileTypes.Stone));
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
        private List<Tile> generateHouse(int x, int y, Boolean doorOnLeft, int positionOfDoor, int number)
        {
            List<Tile> tilesOfHouse = new List<Tile>();
            EnumHandler.TileTypes type = EnumHandler.TileTypes.Wood;

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
                if (doorOnLeft)
                {
                    tilesOfHouse.Add(new Tile(x * Game.tileSize, (y - i) * Game.tileSize, type));

                    if (i == positionOfDoor || i == positionOfDoor + 1) continue;
                    tilesOfHouse.Add(new Tile((x - 10) * Game.tileSize, (y - i) * Game.tileSize, type));
                }
                else
                {
                    tilesOfHouse.Add(new Tile((x - 10) * Game.tileSize, (y - i) * Game.tileSize, type));

                    if (i == positionOfDoor || i == positionOfDoor + 1) continue;
                    tilesOfHouse.Add(new Tile(x * Game.tileSize, (y - i) * Game.tileSize, type));
                }
            }

            for (int xOfBlock = 9; xOfBlock > 0; xOfBlock--)
            {
                for (int yOfBlock = number - 1; yOfBlock > 0; yOfBlock--)
                {
                    Tile tileToAdd = new Tile((x - xOfBlock) * Game.tileSize, (y - yOfBlock) * Game.tileSize, EnumHandler.TileTypes.Carpet);

                    tileToAdd.roofed = true;

                    tilesOfHouse.Add(tileToAdd);
                }
            }

            return tilesOfHouse;
        }

        //generate items/furniture in house
        private void generateItems(int x, int y, int number)
        {
            int water = generator.Next(1, 3);
            int knife = generator.Next(2, 5);
            int peanutButter = generator.Next(1, 3);


            if (water == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 8) * Game.tileSize, (y - number + 1) * Game.tileSize, EnumHandler.Items.Waterbottle));
            }
            if (knife == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 2) * Game.tileSize, (y - 1) * Game.tileSize, EnumHandler.Items.Knife));
            }
            if (peanutButter == 2)
            {
                Game.itemsInWorld.Add(new Item((x - 9) * Game.tileSize, (y - number + 2) * Game.tileSize, EnumHandler.Items.Peanutbutter));
            }

            Game.itemsInWorld.Add(new Item((x - 9) * Game.tileSize, (y - number + 1) * Game.tileSize, EnumHandler.Items.Bread));

            Item pistolItem = new Item((x - 8) * Game.tileSize, (y - 1) * Game.tileSize, EnumHandler.Items.Pistol);
            pistolItem.ammo = generator.Next(0, 5);
            Game.itemsInWorld.Add(pistolItem);

            Item ammoItem = new Item((x - 3) * Game.tileSize, (y - 3) * Game.tileSize, EnumHandler.Items.PistolAmmo);
            ammoItem.ammo = 12;
            Game.itemsInWorld.Add(ammoItem);

            Game.furnitureInWorld.Add(new Furniture((x - 2) * Game.tileSize, (y - 9) * Game.tileSize, EnumHandler.FurnitureTypes.Couch, 90));
            Game.furnitureInWorld.Add(new Furniture((x - 9) * Game.tileSize, (y - number + 1) * Game.tileSize, EnumHandler.FurnitureTypes.Table, 0));
            Game.furnitureInWorld.Add(new Furniture((x - 8) * Game.tileSize, (y - number + 3) * Game.tileSize, EnumHandler.FurnitureTypes.Chair, 0));
            Game.furnitureInWorld.Add(new Furniture((x - 9) * Game.tileSize, (y - 2) * Game.tileSize, EnumHandler.FurnitureTypes.Bed, 0));
            Game.furnitureInWorld.Add(new Furniture((x - 8) * Game.tileSize, (y - 1) * Game.tileSize, EnumHandler.FurnitureTypes.SmallTable, 0));

        }
    }
}
