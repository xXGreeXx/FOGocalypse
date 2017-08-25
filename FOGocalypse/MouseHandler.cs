using System;
using System.Drawing;
using System.Windows.Forms;

namespace FOGocalypse
{
    class MouseHandler
    {
        //define global variabels
        public static int mouseX { get; set; }
        public static int mouseY { get; set; }
        public static EnumHandler.Items itemHeldByMouse { get; set; }

        //constructor
        public MouseHandler()
        {

        }

        //mouse moved
        public void RegisterMouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y;
        }

        //mouse down
        public void RegisterMouseDown(int x, int y, MouseButtons button)
        {
            #region Game
            #endregion
        }

        //mouse up
        public void RegisterMouseUp(int x, int y, MouseButtons button)
        {

        }
    }
}
