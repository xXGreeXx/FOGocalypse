using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        public static List<Item> itemsInWorld { get; set; } = new List<Item>();
        public static List<Zombie> zombies { get; set; } = new List<Zombie>();
        public static List<Furniture> furnitureInWorld { get; set; } = new List<Furniture>();

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
        public static List<Particle> bloodParticles { get; set; } = new List<Particle>();

        public static int canvasWidth { get; set; }
        public static int canvasHeight { get; set; }
        public static int frameRate { get; set; } = 60;
        public static String resolution { get; set; } = "1240x1440";
        public static Boolean fogOn { get; set; } = true;
        public static String shadowQuality { get; set; } = "high";

        public static int FPS { get; set; } = 1;
        private int lastFPS = 0;
        private int physicsCycle = 0;
        public static int attackSpeedLimit { get; set; } = 0;
        Random r = new Random();

        //contrsuctor
        public Game()
        {
            InitializeComponent();

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

            timer.Interval = 1;
            timer.Start();
            timer2.Interval = 60000;
            timer2.Start();

            fpsTracker.Interval = 1000;
            fpsTracker.Start();

            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.MinimizeBox = false;
            this.MaximizeBox = false;

            Application.ApplicationExit += GameExitHandler;
        }

        //game exiting handler
        private void GameExitHandler(object sender, EventArgs e)
        {
            
        }

        //update handler
        private void timer_Tick(object sender, EventArgs e)
        {
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
                physicsCycle++;

                if (physicsCycle >= Math.Ceiling(FPS / 20F))
                {
                    physicsEngine.SimulatePhysics();
                    physicsCycle = 0;
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
    }
}
