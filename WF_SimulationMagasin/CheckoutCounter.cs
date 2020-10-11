using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    enum CheckoutCounterStates
    {
        Open,
        Closed
    }
    class CheckoutCounter : Sprite
    {
        public const int SIZE = 40;
        private const int CHECKOUT_DELAY = 30;
        internal CheckoutCounterStates State;
        public Point LineStart { get { return new Point(this.X, this.Y + this.Size / 4); } }
        public int WaitTime { get { return LineLength * CHECKOUT_DELAY; } }
        public int LineLength { get; set; }
        public CheckoutCounter(int x, int y, CheckoutCounterStates state)
        {
            this.X = x;
            this.Y = y;
            this.Size = SIZE;
            this.State = state;
            this.LineLength = 0;
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color rectangleColor;
            if (this.State == CheckoutCounterStates.Closed)
            {
                rectangleColor = Color.Red;
            } else
            {
                rectangleColor = Color.Green;
            }
            e.Graphics.FillRectangle(new SolidBrush(rectangleColor), X, Y, Size, Size);
            e.Graphics.FillEllipse(new SolidBrush(Color.Blue), LineStart.X, LineStart.Y, Size / 4, Size / 4);
        }
    }
}
