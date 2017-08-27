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
        public static Player player { get; set; }
        public static int playerMoveSpeed { get; set; } = 5;
        public static int tileSize { get; set; } = 25;
        public static int worldSize { get; set; } = 256;
        public static EnumHandler.GameStates state { get; set; } = EnumHandler.GameStates.MainMenu;
        public static int numberOfhotBarSlots { get; set; } = 5;
        public static int selectedHotbar { get; set; } = 1;
        public static EnumHandler.Items[] itemsInHotbar { get; set; }
        public static EnumHandler.Items[] itemsInInventory { get; set; }
        public static Boolean inPauseMenu { get; set; } = false;
        public static Boolean inInventory { get; set; } = false;
        public static Boolean inStartScreen { get; set; } = false;
        public static Boolean inLossScreen { get; set; } = false;
        public static int playerViewDistance { get; set; } = 5;
        public static int zombieViewDistance { get; set; } = 3;
        public static int zombieHearDistance { get; set; } = 2;
        public static int zombieMoveSpeed { get; set; } = 4;
        public static int zombieSpawnChance { get; set; } = 10;
        public static int itemRarity { get; set; } = 60;
        public static int time = 800;
        public static int day = 0;
        public static int month = 0;
        public static int year = 0;
        public static int attackEffect = -1;
        public static List<Particle> bloodParticles { get; set; } = new List<Particle>();

        public static int canvasWidth { get; set; }
        public static int canvasHeight { get; set; }
        public static int frameRate { get; set; } = 60;
        public static String resolution { get; set; } = "1240x1440";

        //contrsuctor
        public Game()
        {
            InitializeComponent();

            player = new Player(worldSize / 2 * tileSize, worldSize / 2 * tileSize, EnumHandler.Directions.Left);

            itemsInHotbar = new EnumHandler.Items[numberOfhotBarSlots];
            itemsInHotbar[0] = EnumHandler.Items.Flashlight;
            itemsInHotbar[1] = EnumHandler.Items.Knife;
            itemsInHotbar[2] = EnumHandler.Items.Waterbottle;
            itemsInHotbar[3] = EnumHandler.Items.None;
            itemsInHotbar[4] = EnumHandler.Items.None;

            itemsInInventory = new EnumHandler.Items[25];
            for (int i = 0; i < itemsInInventory.Length; i++)
            {
                itemsInInventory[i] = EnumHandler.Items.None;
            }

            day = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            timer.Interval = 1000 / 60;
            timer.Start();
            timer2.Interval = 60000;
            timer2.Start();
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
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
            //change framerate
            timer.Interval = 1000 / frameRate;

            //update canvas
            canvas.Refresh();
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
                physicsEngine.SimulatePhysics();
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
            if (state.Equals(EnumHandler.GameStates.Game))
            {
                time += 100;
            }
        }
    }
}
