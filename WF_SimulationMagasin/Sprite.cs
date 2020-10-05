using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    abstract class Sprite
    {
        protected int X { get; set; }
        protected int Y { get; set; }
        protected int Size { get; set; }

        public virtual void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Blue), X, Y, Size, Size);
        }
    }
}
