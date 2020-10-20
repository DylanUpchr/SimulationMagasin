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
        const int NB_INITIAL_CUSTOMERS = NB_CHECKOUT_COUNTERS * NB_CUSTOMERS_PER_COUNTER * 2;
        const int MAX_WAIT_TIME = 3; //NB_CUSTOMERS_PER_COUNTER * CheckoutCounter.CHECKOUT_DELAY;
        const int MAX_SPEED_MULTIPLER = 150;
        const int MIN_SPEED_MULTIPLER = 75;
        const int MIN_TIME_UNTIL_CHECKOUT_SECONDS = 2;
        const int MAX_TIME_UNTIL_CHECKOUT_SECONDS = 30;
        const int WIDTH_SHOP = 800;
        const int HEIGHT_SHOP = 450;

        private List<Customer> Customers{ get; set; }
        private List<CheckoutCounter> CheckoutCounters { get; set; }
        private Random Random { get; set; }
        private Timer t;

        public Shop() : base()
        {
            t = new Timer();
            t.Interval = 1000 / 60;
            t.Enabled = true;
            t.Tick += new EventHandler(OnTick);

            DoubleBuffered = true;
            
            this.Customers = new List<Customer>();
            this.CheckoutCounters = new List<CheckoutCounter>();
            this.Random = new Random();

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
            for (int i = 0; i < NB_CHECKOUT_COUNTERS; i++)
            {
                CheckoutCounter checkoutCounter = new CheckoutCounter(WIDTH_SHOP - CheckoutCounter.SIZE, i * 50, CheckoutCounterStates.Closed);
                Paint += checkoutCounter.Paint;
                this.CheckoutCounters.Add(checkoutCounter);
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            Invalidate(true);
            Customers.ForEach(customer => customer.Update());
            AutoOpenCheckoutCounters();
        }
        private void AutoOpenCheckoutCounters()
        {
            //AutoOpen
            if (
                Customers.Any(c => c.State == CustomerStates.FindingLine) &&
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Closed) &&
                (CheckoutCounters.All(cc => cc.State == CheckoutCounterStates.Closed) ||
                Customers.Any(c => c.TimeSpentWaiting.Seconds >= MAX_WAIT_TIME))
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Closed).First().State = CheckoutCounterStates.Open;
            }
            //AutoClose
            if (
                !(Customers.Any(c => c.State == CustomerStates.FindingLine)) && 
                CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open && cc.EstimatedWaitTime == 0 && cc.LineLength == 0)
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.EstimatedWaitTime == 0 && cc.LineLength == 0).First().State = CheckoutCounterStates.Closed;
            }
        }
        public CheckoutCounter GetCheckoutCounterWithShortestLine()
        {
            if (CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength < NB_CUSTOMERS_PER_COUNTER))
            {
                return this.CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength <= NB_CUSTOMERS_PER_COUNTER).OrderBy(cc => cc.EstimatedWaitTime).First();
            } else
            {
                return null;
            }
        }
        public void RemoveCustomer(Customer customer)
        {
            this.Customers.Remove(customer);
        }
    }
}
