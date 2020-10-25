/* Author: DU
 * Desc: Shop component, instances clients and counters, opens and closes counters
 * Date: 2020-10-24
 * File: Shop.cs
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    class Shop : Control
    {
        const int NB_CHECKOUT_COUNTERS = 7;
        const int NB_CUSTOMERS_PER_COUNTER = 5;
        const int NB_INITIAL_CUSTOMERS = NB_CHECKOUT_COUNTERS * NB_CUSTOMERS_PER_COUNTER;
        const int MAX_WAIT_TIME = 3;
        const int NB_SECONDS_BEFORE_COUNTER_CLOSES = 3;
        const int MAX_SPEED_MULTIPLER = 150;
        const int MIN_SPEED_MULTIPLER = 75;
        const int MIN_TIME_UNTIL_CHECKOUT_SECONDS = 2;
        const int MAX_TIME_UNTIL_CHECKOUT_SECONDS = 30;
        const int WIDTH_SHOP = 800;
        const int HEIGHT_SHOP = 450;

        private List<Customer> Customers { get; set; }
        private List<CheckoutCounter> CheckoutCounters { get; set; }
        private Random Random { get; set; }
        private Timer Timer { get; set; }

        /// <summary>
        /// Shop constructor
        /// </summary>
        public Shop() : base()
        {
            Timer = new Timer();
            Timer.Interval = 1000 / 60;
            Timer.Enabled = true;
            Timer.Tick += OnTick;

            DoubleBuffered = true;

            this.Customers = new List<Customer>();
            this.CheckoutCounters = new List<CheckoutCounter>();
            this.Random = new Random();

            //Instance customers
            for (int i = 0; i < NB_INITIAL_CUSTOMERS; i++)
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
            //Instance checkout counters
            for (int i = 0; i < NB_CHECKOUT_COUNTERS; i++)
            {
                CheckoutCounter checkoutCounter = new CheckoutCounter(WIDTH_SHOP - CheckoutCounter.SIZE, i * 50);
                Paint += checkoutCounter.Paint;
                this.CheckoutCounters.Add(checkoutCounter);
            }
        }
        /// <summary>
        /// Redraw shop, update customers and open/close checkout counters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick(object sender, EventArgs e)
        {
            Invalidate(true);
            Customers.ForEach(customer => customer.Update());
            ManageCheckoutCounters();
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
                Customers.Any(c => c.State == CustomerStates.FindingLine) &&
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Closed) &&
                (CheckoutCounters.All(cc => cc.State == CheckoutCounterStates.Closed) ||
                Customers.Any(c => c.TimeSpentWaiting.Seconds >= MAX_WAIT_TIME))
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Closed).First().State = CheckoutCounterStates.Open;
            }
            //AutoClose logic
            //If there are not any customers finding a line
            //And any checkout counters open
            //And line empty for NB_SECONDS_BEFORE_COUNTER_CLOSES
            if (
                !(Customers.Any(c => c.State == CustomerStates.FindingLine)) &&
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open && 
                cc.TimeSinceLineEmpty >= NB_SECONDS_BEFORE_COUNTER_CLOSES)
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.TimeSinceLineEmpty >= NB_SECONDS_BEFORE_COUNTER_CLOSES).First().State = CheckoutCounterStates.Closed;
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
