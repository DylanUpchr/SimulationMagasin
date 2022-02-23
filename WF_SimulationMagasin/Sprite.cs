/* Author: DU
 * Desc: Sprite base class
 * Date: 2020-10-24
 * File: Sprite.cs
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WF_SimulationMagasin
{
    abstract class Sprite
    {
        protected int X { get; set; } //Horizontal position
        protected int Y { get; set; } //Vertical position
        protected int SpeedX { get; set; } //Horizontal speed
        protected int SpeedY { get; set; } //Vertical speed
        protected int Size { get; set; } //Sprite size
        public Timer Timer { get; set; } //Sprite interal timer

        /// <summary>
        /// Draws sprite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), X, Y, Size, Size);
        }
    }
}
