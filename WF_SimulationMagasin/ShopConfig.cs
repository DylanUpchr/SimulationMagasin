using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_SimulationMagasin
{
    public class ShopConfig
    {
        public const int NB_CUSTOMERS_PER_COUNTER = 5;
        const int NB_CHECKOUT_COUNTERS = 13;
        const int MIN_TIME_UNTIL_CHECKOUT_SECONDS = 5;
        const int MAX_TIME_UNTIL_CHECKOUT_SECONDS = 30;
        const int NB_SECONDS_BEFORE_COUNTER_CLOSES = 3;
        readonly int[] NB_CUSTOMERS_PER_HOUR = { 0, 0, 0, 0, 0, 0, 0, 30, 30, 40, 50, 60, 100, 80, 50, 30, 80, 100, 50, 50, 80, 0, 0, 0 };

        public int NbCustomersPerCounter { get; set; }
        public int NbCheckoutCounters { get; set; }
        public int MinTimeUntilCheckoutSeconds { get; set; }
        public int MaxTimeUntilCheckoutSeconds { get; set; }
        public int NbSecondsBeforeCounterCloses { get; set; }
        public int[] NbCustomersPerHour { get; set; }

        public ShopConfig()
        {
            this.NbCustomersPerCounter = NB_CUSTOMERS_PER_COUNTER;
            this.NbCheckoutCounters = NB_CHECKOUT_COUNTERS;
            this.MinTimeUntilCheckoutSeconds = MIN_TIME_UNTIL_CHECKOUT_SECONDS;
            this.MaxTimeUntilCheckoutSeconds = MAX_TIME_UNTIL_CHECKOUT_SECONDS;
            this.NbSecondsBeforeCounterCloses = NB_SECONDS_BEFORE_COUNTER_CLOSES;
            this.NbCustomersPerHour = NB_CUSTOMERS_PER_HOUR;
        }
    }
}
