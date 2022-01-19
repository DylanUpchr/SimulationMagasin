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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shop
            // 
            this.shop.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.shop.Location = new System.Drawing.Point(1, 0);
            this.shop.Name = "shop";
            this.shop.Size = new System.Drawing.Size(930, 600);
            this.shop.TabIndex = 0;
            this.shop.Text = "shop";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(732, 485);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(732, 541);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 50);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // frmSimulationMagasin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 600);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSimulationMagasin";
            this.Text = "Simulation Magasin";
            this.ResumeLayout(false);

        }

        #endregion

        private Shop shop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

