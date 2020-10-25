namespace WF_SimulationMagasin
{
    partial class frmSimulationMagasin
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.shop = new WF_SimulationMagasin.Shop();
            this.SuspendLayout();
            // 
            // shop
            // 
            this.shop.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.shop.Location = new System.Drawing.Point(1, 0);
            this.shop.Name = "shop";
            this.shop.Size = new System.Drawing.Size(800, 450);
            this.shop.TabIndex = 0;
            this.shop.Text = "shop";
            // 
            // frmSimulationMagasin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.shop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSimulationMagasin";
            this.Text = "Simulation Magasin";
            this.ResumeLayout(false);

        }

        #endregion

        private Shop shop;
    }
}

