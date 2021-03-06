﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace FOGocalypse
{
    public partial class Game : Form
    {
        //define global variables
        RenderingEngine renderer = new RenderingEngine();
        KeyBoardHandler keyHandler = new KeyBoardHandler();
        MouseHandler mouseHandler = new MouseHandler();
        WorldGenerator generator = new WorldGenerator();
        Physics physicsEngine = new Physics();
        public static CombatSystem combatSystem { get; } = new CombatSystem();
        public static ParticleGenerator particleGenerator { get; } = new ParticleGenerator();

        public static List<Tile> worldTiles { get; set; } = new List<Tile>();
        public static List<Tile> allocatedTiles { get; set; } = new List<Tile>();
        public static List<Item> itemsInWorld { get; set; } = new List<Item>();
        public static List<Zombie> zombies { get; set; } = new List<Zombie>();
        public static List<Furniture> furnitureInWorld { get; set; } = new List<Furniture>();
        public static List<Plant> plantsInWorld { get; set; } = new List<Plant>();

        public static Item[] itemsInHotbar { get; set; }
        public static Item[] itemsInInventory { get; set; }

        public static Player player { get; set; }
        public static int playerMoveSpeed { get; set; } = 5;
        public static int tileSize { get; set; } = 25;
        public static int worldSize { get; set; } = 256;
        public static EnumHandler.GameStates state { get; set; } = EnumHandler.GameStates.MainMenu;
        public static int numberOfhotBarSlots { get; set; } = 5;
        public static int selectedHotbar { get; set; } = 1;

        public static Boolean inPauseMenu { get; set; } = false;
        public static Boolean inInventory { get; set; } = false;
        public static Boolean inStartScreen { get; set; } = false;
        public static Boolean inLossScreen { get; set; } = false;
        public static int playerViewDistance { get; set; } = 5;
        public static int zombieViewDistance { get; set; } = 3;
        public static int zombieHearDistance { get; set; } = 2;
        public static int zombieMoveSpeed { get; set; } = 2;
        public static int zombieSpawnChance { get; set; } = 10;
        public static int itemRarity { get; set; } = 60;
        public static int time = 800;
        public static int day = 0;
        public static int month = 0;
        public static int year = 0;
        public static EnumHandler.WeatherType weather = EnumHandler.WeatherType.Sunny;
        public static EnumHandler.SeasonType season = EnumHandler.SeasonType.Winter;
        public static List<Particle> bloodParticles { get; set; } = new List<Particle>();

        public static int canvasWidth { get; set; }
        public static int canvasHeight { get; set; }

        public static String resolution { get; set; } = "1240x1440";
        public static int frameRate { get; set; } = 60;
        public static Boolean fogOn { get; set; } = true;
        public static String shadowQuality { get; set; } = "high";
        public static Boolean rainOn { get; set; } = true;
        public static Boolean antialias { get; set; } = false;
        public static Boolean blood { get; set; } = true;
        public static int soundVolume { get; set; } = 100;
        public static int musicVolume { get; set; } = 100;

        public static int FPS { get; set; } = 1;
        private int lastFPS = 0;
        private float physicsCycle = 0;
        public static int attackSpeedLimit { get; set; } = 0;
        public static Random r { get; } = new Random();

        public static String gameSavePath { get; } = "WorldSave.txt";
        private String optionsSavePath = "GameOptions.txt";

        //contrsuctor
        public Game()
        {
            //init
            InitializeComponent();

            //game stuff
            player = new Player(worldSize / 2 * tileSize, worldSize / 2 * tileSize, EnumHandler.Directions.Left);

            itemsInHotbar = new Item[numberOfhotBarSlots];
            itemsInHotbar[0] = new Item(0, 0, EnumHandler.Items.Flashlight);
            itemsInHotbar[1] = new Item(0, 0, EnumHandler.Items.Knife);
            itemsInHotbar[2] = new Item(0, 0, EnumHandler.Items.Waterbottle);
            itemsInHotbar[3] = new Item(0, 0, EnumHandler.Items.None);
            itemsInHotbar[4] = new Item(0, 0, EnumHandler.Items.None);

            itemsInInventory = new Item[25];
            for (int i = 0; i < itemsInInventory.Length; i++)
            {
                itemsInInventory[i] = new Item(0, 0, EnumHandler.Items.None);
            }

            day = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            //component stuff
            timer.Interval = 1;
            timer.Start();
            timer2.Interval = 60000;
            timer2.Start();

            fpsTracker.Interval = 1000;
            fpsTracker.Start();

            this.KeyPreview = true;

            Application.ApplicationExit += GameExitHandler;

            //load options save
            FileStream f = File.OpenRead(optionsSavePath);
            StreamReader r = new StreamReader(f);

            resolution = r.ReadLine();
            frameRate = int.Parse(r.ReadLine());
            fogOn = Boolean.Parse(r.ReadLine());
            shadowQuality = r.ReadLine();
            rainOn = Boolean.Parse(r.ReadLine());
            antialias = Boolean.Parse(r.ReadLine());
            blood = Boolean.Parse(r.ReadLine());
            soundVolume = int.Parse(r.ReadLine());
            musicVolume = int.Parse(r.ReadLine());

            r.Close();
            f.Close();
        }

        //game exiting handler
        private void GameExitHandler(object sender, EventArgs e)
        {
            //write save options to file
            FileStream optionsStream = File.Create(optionsSavePath);
            StreamWriter optionsWriter = new StreamWriter(optionsStream);

            optionsWriter.WriteLine(resolution);
            optionsWriter.WriteLine(frameRate);
            optionsWriter.WriteLine(fogOn);
            optionsWriter.WriteLine(shadowQuality);
            optionsWriter.WriteLine(rainOn);
            optionsWriter.WriteLine(antialias);
            optionsWriter.WriteLine(blood);
            optionsWriter.WriteLine(soundVolume);
            optionsWriter.WriteLine(musicVolume);

            optionsWriter.Close();
            optionsStream.Close();

            //write world save
            FileStream f = File.Create(gameSavePath);
            StreamWriter w = new StreamWriter(f);

            w.WriteLine("BEGIN-SAVE-FILE");

            //world data
            w.WriteLine("<world-dat>");
            w.WriteLine(zombieViewDistance);
            w.WriteLine(zombieHearDistance);
            w.WriteLine(zombieMoveSpeed);
            w.WriteLine(zombieSpawnChance);
            w.WriteLine(itemRarity);
            w.WriteLine(time);
            w.WriteLine(day);
            w.WriteLine(month);
            w.WriteLine(year);
            w.WriteLine(weather);
            w.WriteLine(season);
            w.WriteLine("<end>");

            //player data
            w.WriteLine("<player>");
            w.WriteLine(player.playerX);
            w.WriteLine(player.playerY);
            w.WriteLine(player.playerWaterNeed);
            w.WriteLine(player.playerHealth);
            w.WriteLine(player.playerFoodNeed);
            w.WriteLine("<end>");

            //tile data
            w.WriteLine("<tiles>");
            foreach (Tile t in worldTiles)
            {
                w.WriteLine(t.x);
                w.WriteLine(t.y);
                w.WriteLine(t.type);
                w.WriteLine(t.roofed);
                w.WriteLine(t.fogValue);
                w.WriteLine(t.swapFog);

            }
            w.WriteLine("<end>");

            //zombie data
            w.WriteLine("<zombies>");
            foreach (Zombie z in zombies)
            {
                w.WriteLine(z.x);
                w.WriteLine(z.y);
                w.WriteLine(z.health);
                w.WriteLine(z.lookingToward.X + ", " + z.lookingToward.Y);
            }
            w.WriteLine("<end>");

            //plant data
            w.WriteLine("<plants>");
            foreach (Plant p in plantsInWorld)
            {
                w.WriteLine(p.x);
                w.WriteLine(p.y);
                w.WriteLine(p.type);
                w.WriteLine(p.berries);
            }
            w.WriteLine("<end>");

            //furniture data
            w.WriteLine("<furniture>");
            foreach (Furniture furniture in furnitureInWorld)
            {
                w.WriteLine(furniture.x);
                w.WriteLine(furniture.y);
                w.WriteLine(furniture.type);
                w.WriteLine(furniture.rotation);
                w.WriteLine(furniture.open);
            }
            w.WriteLine("<end>");

            //item data
            w.WriteLine("<items>");
            foreach (Item item in itemsInWorld)
            {
                w.WriteLine(item.x);
                w.WriteLine(item.y);
                w.WriteLine(item.type);
                w.WriteLine(item.ammo);
            }
            w.WriteLine("<end>");

            //items in hotbar
            w.WriteLine("hotbar");
            foreach (Item item in itemsInHotbar)
            {
                w.WriteLine(item.x);
                w.WriteLine(item.y);
                w.WriteLine(item.type);
                w.WriteLine(item.ammo);
            }
            w.WriteLine("<end>");

            //items in inventory
            w.WriteLine("inventory");
            foreach (Item item in itemsInInventory)
            {
                w.WriteLine(item.x);
                w.WriteLine(item.y);
                w.WriteLine(item.type);
                w.WriteLine(item.ammo);
            }
            w.WriteLine("<end>");

            w.WriteLine("END-SAVE-FILE");

            w.Close();
            f.Close();
        }

        //update handler
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 550 / frameRate;

            if (lastFPS < frameRate)
            {
                lastFPS++;

                //update canvas
                canvas.Refresh();
            }
        }

        //refresh handler
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = canvas.Width;
            int height = canvas.Height;

            canvasWidth = canvas.Width;
            canvasHeight = canvas.Height;

            renderer.DrawScreen(width, height, g);

            if (!inPauseMenu && !inLossScreen)
            {
                physicsCycle += 0.01F;

                if (physicsCycle >= (FPS / 20F) / 100)
                {
                    physicsEngine.SimulatePhysics();
                    physicsCycle = 0.0F;
                }
            }

            if (!CombatSystem.canAttack && attackSpeedLimit != -1)
            {
                StartAttackLimitTimer(attackSpeedLimit);
                attackSpeedLimit = -1;
            }
        }

        //game key down
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            keyHandler.ReadKey(e.KeyData, true);
        }

        //game key up
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            keyHandler.ReadKey(e.KeyData, false);
        }

        //mouse down handler
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseDown(e.X, e.Y, e.Button);
        }

        //mouse up handler
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseUp(e.X, e.Y, e.Button);
        }

        //mouse move handler
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseHandler.RegisterMouseMove(e.X, e.Y);
        }

        //update the date and time
        private void timer2_Tick(object sender, EventArgs e)
        {
            EnumHandler.WeatherType[] typesOfWeather = { EnumHandler.WeatherType.Rainy, EnumHandler.WeatherType.Sunny , EnumHandler.WeatherType.Cloudy };

            if (state.Equals(EnumHandler.GameStates.Game))
            {
                time += 100;

                if (time == 2500)
                {
                    time = 0;
                    if (day >= 30)
                    {
                        day = 0;

                        if (month > 12)
                        {
                            year++;
                            month = 0;
                        }

                        month++;
                    }

                    day++;

                    weather = typesOfWeather[r.Next(0, 3)];
                }


                if (month <= 3)
                {
                    season = EnumHandler.SeasonType.Spring;
                }
                else if (month <= 6)
                {
                    season = EnumHandler.SeasonType.Summer;
                }
                else if (month <= 9)
                {
                    season = EnumHandler.SeasonType.Fall;
                }
                else if (month <= 12)
                {
                    season = EnumHandler.SeasonType.Winter;
                }
            }
        }

        //fps tracker tick
        private void fpsTracker_Tick(object sender, EventArgs e)
        {
            FPS = lastFPS;
            lastFPS = 0;
        }

        //attack speed limit timer
        private void attackSpeedTimer_Tick(object sender, EventArgs e)
        {
            CombatSystem.canAttack = true;
            attackSpeedTimer.Stop();
        }

        //start attack speed limit
        public void StartAttackLimitTimer(int duration)
        {
            attackSpeedTimer.Interval = duration;
            attackSpeedTimer.Start();
        }

        //form size changed
        private void Game_SizeChanged(object sender, EventArgs e)
        {
            RenderingEngine.fogParticlesForBackground = new List<Particle>();
        }
    }
}
