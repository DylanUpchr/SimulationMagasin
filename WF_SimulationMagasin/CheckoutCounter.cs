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
        public const int MAX_WAITING_TIME = 30;
        internal CheckoutCounterStates State;
        public Point LineStart { get { return new Point(this.X - this.Size - LineLength * Customer.SIZE, this.Y + this.Size / 4); } }
        public int EstimatedWaitTime { get { return Line.Select(c => c.TimeSpentWaiting.Seconds).DefaultIfEmpty(0).Max(); } }
        public int LineLength { get { return Line.Count; } }
        public List<Customer> Line { get; set; }
        public int TimeSinceLineEmpty { get; set; }
        private Timer Timer { get; set; }
        public CheckoutCounter(int x, int y, CheckoutCounterStates state)
        {
            this.X = x;
            this.Y = y;
            this.Size = SIZE;
            this.State = state;
            this.Line = new List<Customer>();
            this.Timer = new Timer();
            this.Timer.Enabled = true;
            this.Timer.Interval = 1000;
            this.Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.State == CheckoutCounterStates.Open && LineLength == 0)
            {
                this.TimeSinceLineEmpty++;
            } else
            {
                this.TimeSinceLineEmpty = 0;
            }
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
                (this.EstimatedWaitTime > 0 ? this.EstimatedWaitTime.ToString() : "-"),
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(textColor),
                this.X + this.Size / 4,
                this.Y + this.Size / 4
                );
        }
    }
}
