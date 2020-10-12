using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    enum CustomerStates
    {
        Browsing,
        FindingLine,
        GoingToLine,
        InLine,
        DoneShopping
    }
    class Customer : Sprite
    {
        public const int SIZE = 40;
        private Shop Shop { get; set; }
        public CheckoutCounter CheckoutCounter { get; set; }
        public TimeSpan TimeUntilCheckOut { get; set; }
        internal CustomerStates State { get; set; }

        private Timer t;
        public Customer(int startX, int startY, int speed, TimeSpan timeUntilCheckout, Shop shop)
        {
            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(OnTick);
            t.Enabled = true;
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            this.X = startX;
            this.Y = startY;
            this.SpeedX = speed;
            this.SpeedY = speed;
            this.TimeUntilCheckOut = timeUntilCheckout;
            this.Shop = shop;
            this.Size = SIZE;
            this.State = CustomerStates.Browsing;

        }
        public override void Update()
        {
            if (X <= 0 + Size || X >= +Shop.Width - Size)
            {
                SpeedX = -SpeedX;
            }
            if (Y <= 0 + Size || Y >= Shop.Height - Size)
            {
                SpeedY = -SpeedY;
            }
            if (this.State == CustomerStates.FindingLine)
            {
                this.CheckoutCounter = Shop.GetCheckoutCounterWithShortestLine();
                if (this.CheckoutCounter != null)
                {
                    this.State = CustomerStates.GoingToLine;
                }
            }
            if (this.State == CustomerStates.Browsing || this.State == CustomerStates.FindingLine)
            {
                X += (int)((SpeedX * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
                Y += (int)((SpeedY * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            }
            else if (this.State == CustomerStates.GoingToLine)
            {
                //Teleport first, put movement later
                this.X = this.CheckoutCounter.LineStart.X;
                this.Y = this.CheckoutCounter.LineStart.Y;
                this.State = CustomerStates.InLine;
                this.CheckoutCounter.Line.Add(this);
            }
            LastRefresh = Stopwatch.ElapsedMilliseconds;
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color ellipseColor, textColor;
            if (this.State == CustomerStates.Browsing)
            {
                ellipseColor = Color.Black;
                textColor = Color.White;
            }
            else
            {
                ellipseColor = Color.Red;
                textColor = Color.Black;
            }
            e.Graphics.FillEllipse(new SolidBrush(ellipseColor), this.X, this.Y, this.Size, this.Size);
            e.Graphics.DrawString(
                this.TimeUntilCheckOut.Seconds.ToString(),
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(textColor),
                this.X + this.Size / 4,
                this.Y + this.Size / 4
                );
        }
        public void OnTick(object sender, EventArgs e)
        {
            if (this.TimeUntilCheckOut.Seconds > 0)
            {
                this.TimeUntilCheckOut = this.TimeUntilCheckOut.Subtract(TimeSpan.FromMilliseconds(t.Interval));
            }
            else if (this.State == CustomerStates.Browsing)
            {
                t.Stop();
                this.State = CustomerStates.FindingLine;
            }
        }
    }
}
