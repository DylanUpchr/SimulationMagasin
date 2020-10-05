using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    class Caisse : Sprite
    {
        public const int SIZE = 20;
        public Caisse(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Size = SIZE;
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), X, Y, Size, Size);
        }
    }
}
