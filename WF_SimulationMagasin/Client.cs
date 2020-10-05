using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    class Client : Sprite
    {
        public const int SIZE = 20;
        protected Stopwatch Stopwatch { get; set; }
        protected long LastRefresh { get; set; }
        private int SpeedMultiplier { get; set; }
        private Magasin Magasin { get; set; }
        private TimeSpan TempsAEncaissement { get; set; }
        public Client()
        {

        }
        public Client(int startX, int startY, int speedMultiplier, TimeSpan tempsAEncaissement,  Magasin magasin)
        {
            this.X = startX;
            this.Y = startY;
            this.SpeedMultiplier = speedMultiplier;
            this.TempsAEncaissement = tempsAEncaissement;
            this.Magasin = magasin;
            this.Size = SIZE;
        }
        public void Update()
        {
            X += (int)((SpeedMultiplier * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            Y += (int)((SpeedMultiplier * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            LastRefresh = Stopwatch.ElapsedMilliseconds;
        }
    }
}
