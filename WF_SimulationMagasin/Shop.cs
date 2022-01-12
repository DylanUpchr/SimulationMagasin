/* Author: DU
 * Desc: Shop component, instances clients and counters, opens and closes counters
 * Date: 2020-10-24
 * File: Shop.cs
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
    class Shop : Control
    {
        const int FPS = 120;
        public const double TIME_SPEED = 1;
        const double TIME_ADD = 1 * TIME_SPEED / FPS;
        const int OPENING_TIME = 8;
        const int CLOSING_TIME = 19;
        const int NB_CHECKOUT_COUNTERS = 13;
        const int NB_CUSTOMERS_PER_COUNTER = 5;
        const int NB_INITIAL_CUSTOMERS = NB_CHECKOUT_COUNTERS * NB_CUSTOMERS_PER_COUNTER;
        const int MAX_WAIT_TIME = 5;
        const int NB_SECONDS_BEFORE_COUNTER_CLOSES = 3;
        const int MAX_SPEED_MULTIPLER = 150 * (int)TIME_SPEED;
        const int MIN_SPEED_MULTIPLER = 75 * (int)TIME_SPEED;
        const int MIN_TIME_UNTIL_CHECKOUT_SECONDS = 2;
        const int MAX_TIME_UNTIL_CHECKOUT_SECONDS = 30;
        const int WIDTH_SHOP = 800;
        const int HEIGHT_SHOP = 450;

        private List<Customer> Customers { get; set; }
        private List<CheckoutCounter> CheckoutCounters { get; set; }
        private Random Random { get; set; }
        private Timer Timer { get; set; }
        private DateTime Time { get; set; }

        /// <summary>
        /// Shop constructor
        /// </summary>
        public Shop() : base()
        {
            Timer = new Timer();
            Timer.Interval = 1000 / FPS;
            Timer.Enabled = true;
            Timer.Tick += OnTick;

            DoubleBuffered = true;
            Paint += Shop_Paint;

            this.Customers = new List<Customer>();
            this.CheckoutCounters = new List<CheckoutCounter>();
            this.Random = new Random();
            this.Time = DateTime.Now;
            this.Time = new DateTime(1970, 1, 1, OPENING_TIME, 0, 0);
            //Instance checkout counters
            for (int i = 0; i < NB_CHECKOUT_COUNTERS; i++)
            {
                CheckoutCounter checkoutCounter = new CheckoutCounter(i * 50, HEIGHT_SHOP - CheckoutCounter.SIZE);
                Paint += checkoutCounter.Paint;
                this.CheckoutCounters.Add(checkoutCounter);
            }
        }

        private void Shop_Paint(object sender, PaintEventArgs e)
        {
            int nbCheckoutCounters, nbSecondsBeforCounterOpen, nbCustomersWOCounter, nbAvailableSpots, avgWaitSeconds;

            nbCheckoutCounters = this.CheckoutCounters.Count(cc => cc.State == CheckoutCounterStates.Open);
            if (this.Customers.Any(c => c.State == CustomerStates.FindingLine))
            {
                nbSecondsBeforCounterOpen = MAX_WAIT_TIME - (int)this.Customers.Where(c => c.State == CustomerStates.FindingLine).Max(c => c.TimeSpentWaiting).TotalSeconds;
            }
            else
            {
                nbSecondsBeforCounterOpen = 10;
            }
            if (this.Customers.Any(c => c.State == CustomerStates.FindingLine || c.State == CustomerStates.InLine))
            {
                avgWaitSeconds = (int)this.Customers.Where(c => c.State == CustomerStates.FindingLine || c.State == CustomerStates.InLine).Average(c => c.TimeSpentWaiting.TotalSeconds);
            }
            else
            {
                avgWaitSeconds = 0;
            }
            nbCustomersWOCounter = this.Customers.Count(c => c.State == CustomerStates.FindingLine);
            nbAvailableSpots = (this.CheckoutCounters.Count(cc => cc.State == CheckoutCounterStates.Open) * NB_CUSTOMERS_PER_COUNTER - this.CheckoutCounters.Sum(cc => cc.LineLength));


            DrawLabel(String.Format("Caisses {0}/{1}", nbCheckoutCounters, this.CheckoutCounters.Count), 10, 10, e);
            DrawLabel(String.Format("Temps avant ouverture: {0}s", nbSecondsBeforCounterOpen), 10, 30, e);
            DrawLabel(String.Format("Clients sans caisse: {0}/{1}", nbCustomersWOCounter, Customers.Count), 10, 50, e);
            DrawLabel(String.Format("Places disponibles: {0}", nbAvailableSpots), 10, 70, e);
            DrawLabel(String.Format("Temps moyen d'attente {0}s", avgWaitSeconds), 10, 90, e);
            DrawLabel(String.Format("Heure {0}", this.Time.ToString("H:mm")), 10, 110, e);
        }
        private void DrawLabel(string text, int x, int y, PaintEventArgs e)
        {
            Font font = new Font("Arial", 11);
            Point point = new Point(x, y);
            SizeF size = e.Graphics.MeasureString(text, font);
            Rectangle rectangle = new Rectangle(point, size.ToSize());
            e.Graphics.FillRectangle(Brushes.White, rectangle);
            e.Graphics.DrawString(text, font, Brushes.Black, point);
        }

        /// <summary>
        /// Redraw shop, update customers and open/close checkout counters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick(object sender, EventArgs e)
        {
            Customers.ForEach(customer => customer.Update());
            ManageCheckoutCounters();
            this.Time = this.Time.AddMinutes(TIME_ADD);
            SpawnCustomers();
            Invalidate(true);
        }
        /// <summary>
        /// Open or close counters automatically
        /// </summary>
        private void ManageCheckoutCounters()
        {
            //AutoOpen logic
            //If any customer finding a line
            //And any close counters
            //Either all counters closed or any customershave been waiting longer than MAX_WAIT_TIME 
            if (
                Customers.Any(c => c.State == CustomerStates.FindingLine && c.TimeSpentWaiting.TotalSeconds >= MAX_WAIT_TIME) &&
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Closed) &&
                (CheckoutCounters.All(cc => cc.State == CheckoutCounterStates.Closed) ||
                Customers.Any(c => c.TimeSpentWaiting.TotalSeconds >= MAX_WAIT_TIME))
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Closed).First().State = CheckoutCounterStates.Open;
            }
            //AutoClose logic
            //If there are not any customers finding a line
            //And any checkout counters open
            //And line empty for NB_SECONDS_BEFORE_COUNTER_CLOSES
            //And no customers going to checkout counter
            if (
                !(Customers.Any(c => c.State == CustomerStates.FindingLine)) &&
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open &&
                cc.TimeSinceLineEmpty >= NB_SECONDS_BEFORE_COUNTER_CLOSES &&
                !Customers.Any(c => c.CheckoutCounter == cc))
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.TimeSinceLineEmpty >= NB_SECONDS_BEFORE_COUNTER_CLOSES).First().State = CheckoutCounterStates.Closed;
            }
        }
        private void SpawnCustomers()
        {
            int desiredNbCustomers, nbCustomers;
            if (this.Time.Hour >= OPENING_TIME && this.Time.Hour <= CLOSING_TIME)
            {
                desiredNbCustomers = NB_INITIAL_CUSTOMERS - (Math.Abs(this.Time.Hour - 12) * 8);
            }
            else
            {
                desiredNbCustomers = 0;
            }

            nbCustomers = desiredNbCustomers - this.Customers.Count;

            for (int i = 0; i < nbCustomers; i++)
            {
                TimeSpan timeUntilCheckout = new TimeSpan(
                    this.Random.Next(MIN_TIME_UNTIL_CHECKOUT_SECONDS, MAX_TIME_UNTIL_CHECKOUT_SECONDS) * TimeSpan.TicksPerSecond
                    );
                Customer customer = new Customer(
                    this.Random.Next(0, WIDTH_SHOP - Customer.SIZE),
                    this.Random.Next(0, HEIGHT_SHOP - Customer.SIZE),
                    this.Random.Next(MIN_SPEED_MULTIPLER, MAX_SPEED_MULTIPLER),
                    timeUntilCheckout,
                    this
                );
                Paint += customer.Paint;
                this.Customers.Add(customer);
            }
        }
        /// <summary>
        /// Finds checkout counter with shortest line
        /// </summary>
        /// <returns>Checkout counter</returns>
        public CheckoutCounter GetCheckoutCounterWithShortestLine()
        {
            if (CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength < NB_CUSTOMERS_PER_COUNTER))
            {
                return this.CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength < NB_CUSTOMERS_PER_COUNTER).OrderBy(cc => cc.HighestWaitTime).First();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Remove customer that is done shopping from list
        /// </summary>
        /// <param name="customer"></param>
        public void RemoveCustomer(Customer customer)
        {
            this.Customers.Remove(customer);
        }
    }
}
