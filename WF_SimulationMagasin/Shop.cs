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
        const int FPS = 100;
        public const double TIME_SPEED = 1;
        const double TIME_ADD = 1 * TIME_SPEED / FPS;
        const int OPENING_TIME = 7;
        const int MAX_WAIT_TIME = 5;
        const int MAX_SPEED_MULTIPLER = 150 * (int)TIME_SPEED;
        const int MIN_SPEED_MULTIPLER = 75 * (int)TIME_SPEED;
        const int WIDTH_SHOP = 930;
        const int HEIGHT_SHOP = 600;
        public const int X_START_MOVABLE_AREA = 0;
        public const int Y_START_MOVABLE_AREA = 24;
        public const int WIDTH_MOVABLE_AREA = WIDTH_SHOP;
        public const int HEIGHT_MOVABLE_AREA = HEIGHT_SHOP - CheckoutCounter.SIZE * (ShopConfig.NB_CUSTOMERS_PER_COUNTER + 1);

        private List<Customer> Customers { get; set; }
        private List<CheckoutCounter> CheckoutCounters { get; set; }
        private Random Random { get; set; }
        private Timer Timer { get; set; }
        private DateTime Time { get; set; }
        public ShopConfig ShopConfig { get; private set; }

        private Button btnAddClients;
        private Button btnAddHour;
        private MenuStrip menu;
        private ToolStripMenuItem tsmiAbout;
        private ToolStripMenuItem tsmiParams;

        /// <summary>
        /// Shop constructor
        /// </summary>
        public Shop() : base()
        {
            this.ShopConfig = new ShopConfig();
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
            for (int i = 0; i < ShopConfig.NbCheckoutCounters; i++)
            {
                CheckoutCounter checkoutCounter = new CheckoutCounter(i * 50, HEIGHT_SHOP - CheckoutCounter.SIZE);
                Paint += checkoutCounter.Paint;
                this.CheckoutCounters.Add(checkoutCounter);
            }

            this.btnAddClients = new Button();
            this.btnAddHour = new Button();
            this.menu = new MenuStrip();
            this.tsmiAbout = new ToolStripMenuItem();
            this.tsmiParams = new ToolStripMenuItem();

            this.Controls.Add(this.btnAddHour);
            this.Controls.Add(this.btnAddClients);
            this.Controls.Add(this.menu);
            this.menu.Items.Add(this.tsmiAbout);
            this.menu.Items.Add(this.tsmiParams);

            // 
            // btnAddClients
            // 
            this.btnAddClients.Location = new System.Drawing.Point(732, 485);
            this.btnAddClients.Name = "btnAddClients";
            this.btnAddClients.Size = new System.Drawing.Size(187, 50);
            this.btnAddClients.TabIndex = 1;
            this.btnAddClients.Text = "Ajouter 5 clients";
            this.btnAddClients.UseVisualStyleBackColor = true;
            this.btnAddClients.Click += Add5Customers;
            // 
            // btnAddHour
            // 
            this.btnAddHour.Location = new System.Drawing.Point(732, 541);
            this.btnAddHour.Name = "btnAddHour";
            this.btnAddHour.Size = new System.Drawing.Size(187, 50);
            this.btnAddHour.TabIndex = 2;
            this.btnAddHour.Text = "Ajouter 1H";
            this.btnAddHour.UseVisualStyleBackColor = true;
            this.btnAddHour.Click += AddHour;
            //
            // tsmiAbout
            //
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += TsmiAbout_Click;
            //
            // tsmiParams
            //
            this.tsmiParams.Text = "Paramètres";
            this.tsmiParams.Click += TsmiParams_Click;
        }

        private void TsmiParams_Click(object sender, EventArgs e)
        {
            frmParameters frmParameters = new frmParameters(this.ShopConfig);
            frmParameters.ShowDialog();
        }

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
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
            nbAvailableSpots = (this.CheckoutCounters.Count(cc => cc.State == CheckoutCounterStates.Open) * ShopConfig.NbCustomersPerCounter - this.CheckoutCounters.Sum(cc => cc.LineLength));


            DrawLabel(String.Format("Caisses {0}/{1}", nbCheckoutCounters, this.CheckoutCounters.Count), this.Size.Width - 200, this.Height - 250, e);
            DrawLabel(String.Format("Temps avant ouverture: {0}s", nbSecondsBeforCounterOpen), this.Size.Width - 200, this.Height - 230, e);
            DrawLabel(String.Format("Clients sans caisse: {0}/{1}", nbCustomersWOCounter, Customers.Count), this.Size.Width - 200, this.Height - 210, e);
            DrawLabel(String.Format("Places disponibles: {0}", nbAvailableSpots), this.Size.Width - 200, this.Height - 190, e);
            DrawLabel(String.Format("Temps moyen d'attente {0}s", avgWaitSeconds), this.Size.Width - 200, this.Height - 170, e);
            DrawLabel(String.Format("Heure {0}", this.Time.ToString("HH:mm")), this.Size.Width - 200, this.Height - 150, e);
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
                cc.TimeSinceLineEmpty >= ShopConfig.NbSecondsBeforeCounterCloses &&
                !Customers.Any(c => c.CheckoutCounter == cc))
                )
            {
                CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.TimeSinceLineEmpty >= ShopConfig.NbSecondsBeforeCounterCloses).First().State = CheckoutCounterStates.Closed;
            }
        }
        private void SpawnCustomers()
        {
            int desiredNbCustomers, nbCustomers;

            desiredNbCustomers = ShopConfig.NbCustomersPerHour[this.Time.Hour];

            nbCustomers = desiredNbCustomers - this.Customers.Count;

            for (int i = 0; i < nbCustomers; i++)
            {
                AddCustomer();
            }
        }
        /// <summary>
        /// Finds checkout counter with shortest line
        /// </summary>
        /// <returns>Checkout counter</returns>
        public CheckoutCounter GetCheckoutCounterWithShortestLine()
        {
            if (CheckoutCounters.Any(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength < ShopConfig.NbCustomersPerCounter))
            {
                return this.CheckoutCounters.Where(cc => cc.State == CheckoutCounterStates.Open && cc.LineLength < ShopConfig.NbCustomersPerCounter).OrderBy(cc => cc.HighestWaitTime).First();
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
        private void AddHour(object sender, EventArgs e)
        {
            this.Time = this.Time.Add(new TimeSpan(0, 1, 0, 0));
        }
        private void Add5Customers(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                AddCustomer();
            }
        }
        private void AddCustomer()
        {
            TimeSpan timeUntilCheckout = new TimeSpan(
                this.Random.Next(ShopConfig.MinTimeUntilCheckoutSeconds, ShopConfig.MaxTimeUntilCheckoutSeconds) * TimeSpan.TicksPerSecond
                );
            Customer customer = new Customer(
                this.Random.Next(X_START_MOVABLE_AREA, WIDTH_MOVABLE_AREA - Customer.SIZE),
                this.Random.Next(Y_START_MOVABLE_AREA, HEIGHT_MOVABLE_AREA - Customer.SIZE),
                this.Random.Next(MIN_SPEED_MULTIPLER, MAX_SPEED_MULTIPLER),
                timeUntilCheckout,
                this
            );
            Paint += customer.Paint;
            this.Customers.Add(customer);
        }
    }
}
