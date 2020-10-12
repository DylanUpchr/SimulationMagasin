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
        public const int CHECKOUT_DELAY = 6;
        internal CheckoutCounterStates State;
        public Point LineStart { get { return new Point(this.X - this.Size - LineLength * Customer.SIZE, this.Y + this.Size / 4); } }
        public int WaitTime { get { return LineLength * CHECKOUT_DELAY; } }
        public int LineLength { get { return Line.Count; } }
        public List<Customer> Line { get; set; }
        private Timer Timer { get; set; }
        public CheckoutCounter(int x, int y, CheckoutCounterStates state)
        {
            this.X = x;
            this.Y = y;
            this.Size = SIZE;
            this.State = state;
            this.Line = new List<Customer>();
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color rectangleColor, textColor;
            if (this.State == CheckoutCounterStates.Closed)
            {
                rectangleColor = Color.Red;
                textColor = Color.White;
            } else
            {
                rectangleColor = Color.Green;
                textColor = Color.Black;
            }
            e.Graphics.FillRectangle(new SolidBrush(rectangleColor), X, Y, Size, Size);
            e.Graphics.DrawString(
                this.WaitTime.ToString(),
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(textColor),
                this.X + this.Size / 4,
                this.Y + this.Size / 4
                );
        }
    }
}
