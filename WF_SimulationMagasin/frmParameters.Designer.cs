namespace WF_SimulationMagasin
{
    partial class frmParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudCustomersPerCounter = new System.Windows.Forms.NumericUpDown();
            this.nudNbCheckoutCounters = new System.Windows.Forms.NumericUpDown();
            this.nudNbSecondsBeforeCounterCloses = new System.Windows.Forms.NumericUpDown();
            this.nudMaxTimeUntilCheckout = new System.Windows.Forms.NumericUpDown();
            this.nudMinTimeUntilCheckout = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomersPerCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbCheckoutCounters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbSecondsBeforeCounterCloses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTimeUntilCheckout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTimeUntilCheckout)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de clients par caisse:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre de caisses:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Temps minimum en course (minutes):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Temps minimum en course (minutes):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 26);
            this.label5.TabIndex = 4;
            this.label5.Text = "Temps d\'attente avant \r\nouverture/fermture d\'une caisse: (minutes) ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Nombre de clients par heure:";
            // 
            // nudCustomersPerCounter
            // 
            this.nudCustomersPerCounter.Location = new System.Drawing.Point(237, 7);
            this.nudCustomersPerCounter.Name = "nudCustomersPerCounter";
            this.nudCustomersPerCounter.Size = new System.Drawing.Size(52, 20);
            this.nudCustomersPerCounter.TabIndex = 6;
            // 
            // nudNbCheckoutCounters
            // 
            this.nudNbCheckoutCounters.Location = new System.Drawing.Point(237, 33);
            this.nudNbCheckoutCounters.Name = "nudNbCheckoutCounters";
            this.nudNbCheckoutCounters.Size = new System.Drawing.Size(52, 20);
            this.nudNbCheckoutCounters.TabIndex = 7;
            // 
            // nudNbSecondsBeforeCounterCloses
            // 
            this.nudNbSecondsBeforeCounterCloses.Location = new System.Drawing.Point(237, 111);
            this.nudNbSecondsBeforeCounterCloses.Name = "nudNbSecondsBeforeCounterCloses";
            this.nudNbSecondsBeforeCounterCloses.Size = new System.Drawing.Size(52, 20);
            this.nudNbSecondsBeforeCounterCloses.TabIndex = 8;
            // 
            // nudMaxTimeUntilCheckout
            // 
            this.nudMaxTimeUntilCheckout.Location = new System.Drawing.Point(237, 85);
            this.nudMaxTimeUntilCheckout.Name = "nudMaxTimeUntilCheckout";
            this.nudMaxTimeUntilCheckout.Size = new System.Drawing.Size(52, 20);
            this.nudMaxTimeUntilCheckout.TabIndex = 9;
            // 
            // nudMinTimeUntilCheckout
            // 
            this.nudMinTimeUntilCheckout.Location = new System.Drawing.Point(237, 59);
            this.nudMinTimeUntilCheckout.Name = "nudMinTimeUntilCheckout";
            this.nudMinTimeUntilCheckout.Size = new System.Drawing.Size(52, 20);
            this.nudMinTimeUntilCheckout.TabIndex = 10;
            // 
            // frmParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.nudMinTimeUntilCheckout);
            this.Controls.Add(this.nudMaxTimeUntilCheckout);
            this.Controls.Add(this.nudNbSecondsBeforeCounterCloses);
            this.Controls.Add(this.nudNbCheckoutCounters);
            this.Controls.Add(this.nudCustomersPerCounter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmParameters";
            this.Text = "Paramètrage";
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomersPerCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbCheckoutCounters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNbSecondsBeforeCounterCloses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxTimeUntilCheckout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTimeUntilCheckout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudCustomersPerCounter;
        private System.Windows.Forms.NumericUpDown nudNbCheckoutCounters;
        private System.Windows.Forms.NumericUpDown nudNbSecondsBeforeCounterCloses;
        private System.Windows.Forms.NumericUpDown nudMaxTimeUntilCheckout;
        private System.Windows.Forms.NumericUpDown nudMinTimeUntilCheckout;
    }
}