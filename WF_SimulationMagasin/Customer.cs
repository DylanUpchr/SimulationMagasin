using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public TimeSpan TimeSpentWaiting { get; set; }
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
            if (this.State == CustomerStates.FindingLine || this.State == CustomerStates.GoingToLine)
            {
                this.CheckoutCounter = Shop.GetCheckoutCounterWithShortestLine();
                if (this.CheckoutCounter != null)
                {
                    this.State = CustomerStates.GoingToLine;
                }
                else
                {
                    this.State = CustomerStates.FindingLine;
                }
            }
            if (this.State == CustomerStates.Browsing || this.State == CustomerStates.FindingLine || this.State == CustomerStates.GoingToLine)
            {
                int xDiff, yDiff, movementX = this.SpeedX, movementY = this.SpeedY;
                double rateX, rateY;
                if (this.CheckoutCounter != null)
                {
                    xDiff = (this.CheckoutCounter.LineStart.X - this.X);
                    yDiff = (this.CheckoutCounter.LineStart.Y - this.Y);
                    rateX = xDiff / Math.Max(yDiff, 1);
                    rateY = yDiff / Math.Max(xDiff, 1);
                    movementX = Math.Max(Math.Min((int)(Math.Abs(this.SpeedX) * rateX), Math.Abs(this.SpeedX)), -Math.Abs(this.SpeedX));
                    movementY = Math.Max(Math.Min((int)(Math.Abs(this.SpeedY) * rateY), Math.Abs(this.SpeedY)), -Math.Abs(this.SpeedY));

                    //Check if in line
                    if ((this.X - this.CheckoutCounter.LineStart.X <= 5 && this.X - this.CheckoutCounter.LineStart.X >= -5) &&
                        (this.Y - this.CheckoutCounter.LineStart.Y <= 5 && this.Y - this.CheckoutCounter.LineStart.Y >= -5))
                    {
                        this.X = this.CheckoutCounter.LineStart.X;
                        this.Y = this.CheckoutCounter.LineStart.Y;
                        this.State = CustomerStates.InLine;
                        this.CheckoutCounter.Line.Add(this);
                        this.TimeSpentWaiting = TimeSpan.FromSeconds(0);
                    }
                }
                movementX = (int)((movementX * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
                movementY = (int)((movementY * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
                if (movementX + X >= 0 && movementX + X <= Shop.Width - Size)
                {
                    this.X += movementX;
                } else
                {
                    SpeedX = -SpeedX;
                }
                if (movementY + Y >= 0 && movementY + Y <= Shop.Height - Size)
                {
                    this.Y += movementY;
                }
                else
                {
                    SpeedY = -SpeedY;
                }
            }
            LastRefresh = Stopwatch.ElapsedMilliseconds;
        }
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color ellipseColor, textColor;
            string text = "";
            switch (this.State)
            {
                case CustomerStates.Browsing:
                    ellipseColor = Color.Black;
                    textColor = Color.White;
                    text = this.TimeUntilCheckOut.Seconds.ToString();
                    break;
                case CustomerStates.GoingToLine:
                case CustomerStates.InLine:
                    ellipseColor = Color.Red;
                    textColor = Color.Transparent;
                    break;
                case CustomerStates.FindingLine:
                    ellipseColor = Color.Red;
                    textColor = Color.Black;
                    if (this.TimeSpentWaiting.Seconds > 0)
                    {
                        text = this.TimeSpentWaiting.Seconds.ToString();
                    }
                    break;
                case CustomerStates.DoneShopping:
                    ellipseColor = Color.Transparent;
                    textColor = Color.Transparent;
                    break;
                default:
                    ellipseColor = Color.Black;
                    textColor = Color.White;
                    break;
            }
            e.Graphics.FillEllipse(new SolidBrush(ellipseColor), this.X, this.Y, this.Size, this.Size);
            e.Graphics.DrawString(
                text,
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(textColor),
                this.X + this.Size / 4,
                this.Y + this.Size / 4
                );
        }
        public void OnTick(object sender, EventArgs e)
        {
            if (this.State == CustomerStates.Browsing && this.TimeUntilCheckOut.Seconds > 0)
            {
                this.TimeUntilCheckOut = this.TimeUntilCheckOut.Subtract(TimeSpan.FromMilliseconds(t.Interval));
            }
            else if (this.State == CustomerStates.Browsing)
            {
                this.State = CustomerStates.FindingLine;
            }

            if (this.State == CustomerStates.InLine && this.TimeSpentWaiting.Seconds >= CheckoutCounter.CHECKOUT_DELAY)
            {
                t.Stop();
                this.CheckoutCounter.Line.Remove(this);
                this.CheckoutCounter.Line.ForEach(c => c.X += c.Size);
                //this.CheckoutCounter.Line.First().TimeSpentWaiting = TimeSpan.FromSeconds(0);
                this.State = CustomerStates.DoneShopping;
            }

            if (this.State == CustomerStates.InLine || this.State == CustomerStates.FindingLine)
            {
                this.TimeSpentWaiting = this.TimeSpentWaiting.Add(TimeSpan.FromMilliseconds(t.Interval));
            }
        }
    }
}
