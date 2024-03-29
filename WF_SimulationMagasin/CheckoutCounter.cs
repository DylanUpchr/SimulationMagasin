﻿/* Author: DU
 * Desc: Checkout counter sprite
 * Date: 2020-10-24
 * File: CheckoutCounter.cs
 */
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
        public const int SIZE = 40; //Sprite size

        internal CheckoutCounterStates State; //Current state
        public Point LineStart { get { return new Point(this.X, this.Y - this.Size - LineLength * Customer.SIZE); } } //Coordinates of start of line, calculated to be behind the last customer in line
        public int TimeUntilNextCustomerLeaves { get { return (Line.Count > 0 ? (int)(Line.First().TimeSpentCheckingOut.TotalSeconds - Line.First().TimeForCheckOut.TotalSeconds) : 0); } }
        public int LineLength { get { return Line.Count; } } //Number of customers in line at counter
        public List<Customer> Line { get; set; } //Checkout counter line
        public int TimeSinceLineEmpty { get; set; } //Number of seconds since the line was empty 
        /// <summary>
        /// Checkout counter constructor
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        public CheckoutCounter(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Size = SIZE;
            this.Line = new List<Customer>();
            this.Timer = new Timer();
            this.Timer.Enabled = true;
            this.Timer.Interval = 1000;
            this.Timer.Tick += OnTick;
            this.State = CheckoutCounterStates.Closed;
        }
        /// <summary>
        /// Timer tick function, increments or resets TimeSinceLineEmpty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick(object sender, EventArgs e)
        {
            if (this.State == CheckoutCounterStates.Open && LineLength == 0)
            {
                this.TimeSinceLineEmpty++;
            }
            else
            {
                this.TimeSinceLineEmpty = 0;
            }
        }
        /// <summary>
        /// Draws checkout counter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Paint(object sender, PaintEventArgs e)
        {
            Color rectangleColor, textColor;
            if (this.State == CheckoutCounterStates.Closed)
            {
                rectangleColor = Color.Red;
                textColor = Color.White;
            }
            else
            {
                rectangleColor = Color.Green;
                textColor = Color.Black;
            }
            e.Graphics.DrawRectangle(new Pen(Color.Black), X, Y - ShopConfig.NB_CUSTOMERS_PER_COUNTER * SIZE, SIZE - 1, ShopConfig.NB_CUSTOMERS_PER_COUNTER * SIZE + 1);
            e.Graphics.FillRectangle(new SolidBrush(rectangleColor), X, Y, Size, Size);
            e.Graphics.DrawString(
                (this.TimeUntilNextCustomerLeaves > 0 ? this.TimeUntilNextCustomerLeaves.ToString() : "-"),
                new System.Drawing.Font("Arial", 16),
                new SolidBrush(textColor),
                this.X + this.Size / 4,
                this.Y + this.Size / 4
            );
        }
    }
}
