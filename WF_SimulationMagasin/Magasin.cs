using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    class Magasin : Control
    {
        const int NB_CLIENTS_DEPART = 50;
        const int NB_CAISSES = 5;
        const int MAX_SPEED_MULTIPLER = 5;
        const int MIN_TEMPS_A_ENCAISSEMENT_SECONDES = 10;
        const int MAX_TEMPS_A_ENCAISSEMENT_SECONDES = 60;
        const int WIDTH_MAGASIN = 800;
        const int HEIGHT_MAGASIN = 450;

        private List<Client> ClientsEnMouvement { get; set; }
        private List<Client> ClientsEnFilAttente { get; set; }
        private List<Client> ClientsCherchantFilAttente { get; set; }
        private List<Caisse> Caisses { get; set; }
        private Random Random { get; set; }

        public Magasin()
        {
            this.ClientsEnMouvement = new List<Client>();
            this.ClientsEnFilAttente = new List<Client>();
            this.ClientsCherchantFilAttente = new List<Client>();
            this.Caisses = new List<Caisse>();
            this.Random = new Random();

            for (int i = 0; i < NB_CLIENTS_DEPART; i++)
            {
                TimeSpan tempsAEncaissement = new TimeSpan(
                    this.Random.Next(MIN_TEMPS_A_ENCAISSEMENT_SECONDES, MAX_TEMPS_A_ENCAISSEMENT_SECONDES) * TimeSpan.TicksPerSecond
                    );
                Client client = new Client(
                    this.Random.Next(0, WIDTH_MAGASIN - Client.SIZE),
                    this.Random.Next(0, HEIGHT_MAGASIN - Client.SIZE),
                    this.Random.Next(MAX_SPEED_MULTIPLER),
                    tempsAEncaissement,
                    this
                );
                Paint += client.Paint;
                this.ClientsEnMouvement.Add(client);
            }
            for (int i = 0; i < NB_CAISSES; i++)
            {
                Caisse caisse = new Caisse(WIDTH_MAGASIN - Caisse.SIZE, i * 50);
                Paint += caisse.Paint;
                this.Caisses.Add(caisse);
            }
        }
    }
}
