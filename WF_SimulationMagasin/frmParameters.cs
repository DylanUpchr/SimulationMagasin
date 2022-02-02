using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    public partial class frmParameters : Form
    {
        public frmParameters(ShopConfig shopConfig)
        {
            InitializeComponent();
            this.nudCustomersPerCounter.Value = shopConfig.NbCustomersPerCounter;
        }
    }
}
