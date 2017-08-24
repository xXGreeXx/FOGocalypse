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
        WorldGenerator generator = new WorldGenerator();
        Physics physicsEngine = new Physics();
        public static List<Tile> worldTiles { get; set; } = new List<Tile>();
        public static Player player { get; set; }
        public static int playerMoveSpeed { get; set; } = 5;
        public static int tileSize { get; set; } = 25;
        public static int worldSize { get; set; } = 256;
        public static EnumHandler.GameStates state { get; set; } = EnumHandler.GameStates.Game;
        public static int numberOfhotBarSlots { get; set; } = 5;
        public static int selectedHotbar { get; set; } = 1;

        //contrsuctor
        public Game()
        {
            InitializeComponent();

            player = new Player(worldSize / 2 * tileSize, worldSize / 2 * tileSize, EnumHandler.Directions.Left);
            worldTiles = generator.GenerateWorld(tileSize, worldSize);

            timer.Interval = 1000 / 60;
            timer.Start();
            this.KeyPreview = true;
        }

        //update handler
        private void timer_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        //refresh handler
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = canvas.Width;
            int height = canvas.Height;

            renderer.DrawScreen(width, height, g);
            physicsEngine.SimulatePhysics();
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
    }
}
