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
        private Magasin Magasin { get; set; }
        private TimeSpan TempsAEncaissement { get; set; }
        public Client()
        {

        }
        public Client(int startX, int startY, int speed, TimeSpan tempsAEncaissement,  Magasin magasin)
        {
            Stopwatch = new Stopwatch();
            this.X = startX;
            this.Y = startY;
            this.SpeedX = speed;
            this.SpeedY = speed;
            this.TempsAEncaissement = tempsAEncaissement;
            this.Magasin = magasin;
            this.Size = SIZE;
        }
        /*public void Update()
        {
            X += (int)((SpeedX * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            Y += (int)((SpeedY * (Stopwatch.ElapsedMilliseconds - LastRefresh) / 1000f));
            LastRefresh = Stopwatch.ElapsedMilliseconds;
        }*/
    }
}
