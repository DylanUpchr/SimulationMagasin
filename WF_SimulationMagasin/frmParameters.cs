using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_SimulationMagasin
{
    public partial class frmParameters : Form
    {
        public ShopConfig Config { get; set; }
        public frmParameters(ShopConfig shopConfig)
        {
            InitializeComponent();
            this.Config = shopConfig;
            LoadSettings(this.Config);
        }
        /// <summary>
        /// Load settings into interface from shopConfig
        /// </summary>
        /// <param name="shopConfig">Shop configuration</param>
        private void LoadSettings(ShopConfig shopConfig)
        {
            this.nudCustomersPerCounter.Value = shopConfig.NbCustomersPerCounter;
            this.nudNbCheckoutCounters.Value = shopConfig.NbCheckoutCounters;
            this.nudMinTimeUntilCheckout.Value = shopConfig.MinTimeUntilCheckoutSeconds;
            this.nudMaxTimeUntilCheckout.Value = shopConfig.MaxTimeUntilCheckoutSeconds;
            this.nudNbSecondsBeforeCounterCloses.Value = shopConfig.NbSecondsBeforeCounterCloses;
            this.tbxNbCustomersPerHour.Text = IntArrayToString(shopConfig.NbCustomersPerHour);
        }
        /// <summary>
        /// Save entered data to shopConfig
        /// </summary>
        /// <param name="shopConfig">Shop configuration</param>
        private void SaveSettings(ShopConfig shopConfig)
        {
            shopConfig.NbCustomersPerCounter = (int)this.nudCustomersPerCounter.Value;
            shopConfig.NbCheckoutCounters = (int)this.nudNbCheckoutCounters.Value;
            shopConfig.MinTimeUntilCheckoutSeconds = (int)this.nudMinTimeUntilCheckout.Value;
            shopConfig.MaxTimeUntilCheckoutSeconds = (int)this.nudMaxTimeUntilCheckout.Value;
            shopConfig.NbSecondsBeforeCounterCloses = (int)this.nudNbSecondsBeforeCounterCloses.Value;
            int[] newValue = StringToIntArray(this.tbxNbCustomersPerHour.Text);
            if (newValue != null)
            {
                shopConfig.NbCustomersPerHour = newValue;
            }
        }
        /// <summary>
        /// Transform int array to string
        /// </summary>
        /// <param name="array">Int array</param>
        /// <returns>int array in string format</returns>
        private string IntArrayToString(int[] array)
        {
            string result = "[";

            foreach (int i in array)
            {
            }
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString();
                if (i != array.Length - 1)
                {
                    result += ", ";
                }
            }

            result += "]";

            return result;
        }
        /// <summary>
        /// Transform string to int array
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>int array</returns>
        private int[] StringToIntArray(string input)
        {
            Regex r2 = new Regex("[\\d]+");
            var matches = r2.Matches(input);
            int[] result = new int[24];

            for (int i = 0; i < matches.Count; i++)
            {
                Int32.TryParse(matches[i].Value, out result[i]);
            }

            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings(this.Config);
            this.Close();
        }
        /// <summary>
        /// Validate form of user entered data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateInput(object sender, EventArgs e)
        {
            Regex r1 = new Regex(@"\[(\d+\,\s){23}\d\]");
            if (r1.IsMatch(this.tbxNbCustomersPerHour.Text))
            {
                this.btnSave.Enabled = true;
                this.lblInvalid.Visible = false;
            }
            else
            {
                this.btnSave.Enabled = false;
                this.lblInvalid.Visible = true;
            }
        }
    }
}
