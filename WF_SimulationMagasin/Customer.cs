/* Author: DU
 * Desc: Customer sprite, controls customer movement
 * Date: 2020-10-24
 * File: Customer.cs
 */
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
        public const int SIZE = 40; //Size default
        private Shop Shop { get; set; } //Parent shop
        public CheckoutCounter CheckoutCounter { get; set; } //Checkout counter that the customer is at/wants to go to
        public TimeSpan TimeUntilCheckOut { get; set; } //Time until customer starts looking for a checkout counter
        public TimeSpan TimeSpentWaiting { get; set; } //How much time the customer has been waiting for a line
        public TimeSpan TimeSpentCheckingOut { get; set; } //How much time the customer has been at the checkout counter
        public TimeSpan TimeForCheckOut { get; set; } //How much time the customer will be at the checkout counter
        internal CustomerStates State { get; set; } //Current state
        /// <summary>
        /// Customer constructor
        /// </summary>
        /// <param name="startX">Initial horizontal position</param>
        /// <param name="startY">Initial vertical position</param>
        /// <param name="speed">Initial speed</param>
        /// <param name="timeUntilCheckout">Numbger of seconds before checkout</param>
        /// <param name="shop">Parent shop</param>
        public Customer(int startX, int startY, int speed, TimeSpan timeUntilCheckout, Shop shop)
        {
            this.Timer = new Timer();
            this.Timer.Interval = 1000;
            this.Timer.Tick += new EventHandler(OnTick);
            this.Timer.Enabled = true;
            this.X = startX;
            this.Y = startY;
            this.SpeedX = speed;
            this.SpeedY = speed;
            this.TimeUntilCheckOut = timeUntilCheckout;
            this.TimeForCheckOut = TimeSpan.FromSeconds(timeUntilCheckout.TotalSeconds / 5);
            this.Shop = shop;
            this.Size = SIZE;
            this.State = CustomerStates.Browsing;

        }
        /// <summary>
        /// Move sprite based on speed and time since last movement and change customer states
        /// </summary>
        public void Update()
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
            //Calculate horizontal and vertical speed to go straight to checkout line start
            if (this.State == CustomerStates.Browsing || this.State == CustomerStates.FindingLine || this.State == CustomerStates.GoingToLine)
            {
                int xDiff, yDiff, movementX = this.SpeedX, movementY = this.SpeedY;
                double rateX, rateY;
                if (this.CheckoutCounter != null)
                {
                    //Difference between customer position and LineStart
                    xDiff = (this.CheckoutCounter.LineStart.X - this.X);
                    yDiff = (this.CheckoutCounter.LineStart.Y - this.Y);
                    //Ratio of xDiff and yDiff to get rate at which customer moves towards the point
                    //Both rates control the angle of the customers movement
                    rateX = xDiff / Math.Max(yDiff, 1);
                    rateY = yDiff / Math.Max(xDiff, 1);
                    //Calculate how fast to move in each axis
                    movementX = Math.Max(Math.Min((int)(Math.Abs(this.SpeedX) * rateX), Math.Abs(this.SpeedX)), -Math.Abs(this.SpeedX));
                    movementY = Math.Max(Math.Min((int)(Math.Abs(this.SpeedY) * rateY), Math.Abs(this.SpeedY)), -Math.Abs(this.SpeedY));

                    //Check if in line
                    if ((this.X == this.CheckoutCounter.LineStart.X) &&
                        (this.Y == this.CheckoutCounter.LineStart.Y))
                    {
                        this.State = CustomerStates.InLine;
                        this.CheckoutCounter.Line.Add(this);
                        this.TimeSpentWaiting = TimeSpan.FromSeconds(0);
                    }
                }
                //Calculate pixels to move
                movementX = (int)((movementX * (Shop.Stopwatch.ElapsedMilliseconds - Shop.LastRefresh) / 1000f));
                movementY = (int)((movementY * (Shop.Stopwatch.ElapsedMilliseconds - Shop.LastRefresh) / 1000f));
                //If the desired movement isn't out of bounds move, otherwise invert speed
                if (movementX + X >= Shop.X_START_MOVABLE_AREA && movementX + X <= (State == CustomerStates.Browsing ? Shop.WIDTH_MOVABLE_AREA : Shop.Width) - Size)
                {
                    this.X += movementX;
                } else
                {
                    SpeedX = -SpeedX;
                }
                if (movementY + Y >= Shop.Y_START_MOVABLE_AREA && movementY + Y <= (State == CustomerStates.Browsing ? Shop.HEIGHT_MOVABLE_AREA : Shop.Height) - Size)
                {
                    this.Y += movementY;
                }
                else
                {
                    SpeedY = -SpeedY;
                }
            }
        }
        /// <summary>
        /// Draw client based on state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color ellipseColor, textColor;
            string text = "";
            switch (this.State)
            {
                case CustomerStates.Browsing:
                    double intensity = this.TimeUntilCheckOut.TotalSeconds / Shop.ShopConfig.MaxTimeUntilCheckoutSeconds;
                    int color = (int)(255 * intensity);
                    ellipseColor = Color.FromArgb(color, color, color);
                    textColor = Color.Red;
                    text = this.TimeUntilCheckOut.TotalSeconds.ToString();
                    break;
                case CustomerStates.GoingToLine:
                case CustomerStates.InLine:
                    ellipseColor = Color.Red;
                    textColor = Color.Transparent;
                    break;
                case CustomerStates.FindingLine:
                    ellipseColor = Color.Red;
                    textColor = Color.Black;
                    if (this.TimeSpentWaiting.TotalSeconds > 0)
                    {
                        text = this.TimeSpentWaiting.TotalSeconds.ToString();
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
        /// <summary>
        /// Handle time based state changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTick(object sender, EventArgs e)
        {
            if (this.State == CustomerStates.Browsing && this.TimeUntilCheckOut.TotalSeconds > 0)
            {
                this.TimeUntilCheckOut = this.TimeUntilCheckOut.Subtract(TimeSpan.FromMilliseconds(Timer.Interval));
            }
            else if (this.State == CustomerStates.Browsing)
            {
                this.State = CustomerStates.FindingLine;
            }

            if (this.State == CustomerStates.InLine && this.TimeSpentCheckingOut.TotalSeconds >= this.TimeForCheckOut.TotalSeconds)
            {
                Timer.Stop();
                this.CheckoutCounter.Line.Remove(this);
                this.Shop.RemoveCustomer(this);
                this.CheckoutCounter.Line.ForEach(c => c.Y += c.Size);
                this.State = CustomerStates.DoneShopping;
            }

            if (this.State == CustomerStates.InLine || this.State == CustomerStates.FindingLine)
            {
                this.TimeSpentWaiting = this.TimeSpentWaiting.Add(TimeSpan.FromMilliseconds(Timer.Interval));
            }

            if (this.State == CustomerStates.InLine)
            {
                if (this.CheckoutCounter.Line.First() == this)
                {
                    this.TimeSpentCheckingOut = this.TimeSpentCheckingOut.Add(TimeSpan.FromMilliseconds(Timer.Interval));
                }
            }
        }
    }
}
