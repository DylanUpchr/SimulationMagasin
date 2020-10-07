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
        protected Stopwatch Stopwatch { get; set; }
        protected int X { get; set; }
        protected int Y { get; set; }
        protected int SpeedX { get; set; }
        protected int SpeedY { get; set; }
        protected long LastRefresh { get; set; }
        protected int Size { get; set; }

        public virtual void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Blue), X, Y, Size, Size);
        }
        public virtual void Update()
        {
            X += (int)((SpeedX * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            Y += (int)((SpeedY * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            LastRefresh = Stopwatch.ElapsedMilliseconds;
        }
    }
}
