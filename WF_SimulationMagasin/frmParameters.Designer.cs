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
            this.tbxNbCustomersPerHour = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInvalid = new System.Windows.Forms.Label();
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
            this.nudCustomersPerCounter.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCustomersPerCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCustomersPerCounter.Name = "nudCustomersPerCounter";
            this.nudCustomersPerCounter.Size = new System.Drawing.Size(52, 20);
            this.nudCustomersPerCounter.TabIndex = 6;
            this.nudCustomersPerCounter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudNbCheckoutCounters
            // 
            this.nudNbCheckoutCounters.Location = new System.Drawing.Point(237, 33);
            this.nudNbCheckoutCounters.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.nudNbCheckoutCounters.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNbCheckoutCounters.Name = "nudNbCheckoutCounters";
            this.nudNbCheckoutCounters.Size = new System.Drawing.Size(52, 20);
            this.nudNbCheckoutCounters.TabIndex = 7;
            this.nudNbCheckoutCounters.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudNbSecondsBeforeCounterCloses
            // 
            this.nudNbSecondsBeforeCounterCloses.Location = new System.Drawing.Point(237, 111);
            this.nudNbSecondsBeforeCounterCloses.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudNbSecondsBeforeCounterCloses.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNbSecondsBeforeCounterCloses.Name = "nudNbSecondsBeforeCounterCloses";
            this.nudNbSecondsBeforeCounterCloses.Size = new System.Drawing.Size(52, 20);
            this.nudNbSecondsBeforeCounterCloses.TabIndex = 8;
            this.nudNbSecondsBeforeCounterCloses.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMaxTimeUntilCheckout
            // 
            this.nudMaxTimeUntilCheckout.Location = new System.Drawing.Point(237, 85);
            this.nudMaxTimeUntilCheckout.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudMaxTimeUntilCheckout.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudMaxTimeUntilCheckout.Name = "nudMaxTimeUntilCheckout";
            this.nudMaxTimeUntilCheckout.Size = new System.Drawing.Size(52, 20);
            this.nudMaxTimeUntilCheckout.TabIndex = 9;
            this.nudMaxTimeUntilCheckout.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudMinTimeUntilCheckout
            // 
            this.nudMinTimeUntilCheckout.Location = new System.Drawing.Point(237, 59);
            this.nudMinTimeUntilCheckout.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMinTimeUntilCheckout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMinTimeUntilCheckout.Name = "nudMinTimeUntilCheckout";
            this.nudMinTimeUntilCheckout.Size = new System.Drawing.Size(52, 20);
            this.nudMinTimeUntilCheckout.TabIndex = 10;
            this.nudMinTimeUntilCheckout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tbxNbCustomersPerHour
            // 
            this.tbxNbCustomersPerHour.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbxNbCustomersPerHour.Location = new System.Drawing.Point(18, 168);
            this.tbxNbCustomersPerHour.Name = "tbxNbCustomersPerHour";
            this.tbxNbCustomersPerHour.Size = new System.Drawing.Size(450, 20);
            this.tbxNbCustomersPerHour.TabIndex = 11;
            this.tbxNbCustomersPerHour.TextChanged += new System.EventHandler(this.ValidateInput);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(260, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Sauvegarder et quitter";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(394, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblInvalid
            // 
            this.lblInvalid.AutoSize = true;
            this.lblInvalid.ForeColor = System.Drawing.Color.Red;
            this.lblInvalid.Location = new System.Drawing.Point(15, 191);
            this.lblInvalid.Name = "lblInvalid";
            this.lblInvalid.Size = new System.Drawing.Size(160, 26);
            this.lblInvalid.TabIndex = 14;
            this.lblInvalid.Text = "La saisie n\'est pas valide. \r\nFormat: [0, 0, 0 ... 0] (24 valeurs)";
            this.lblInvalid.Visible = false;
            // 
            // frmParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 228);
            this.Controls.Add(this.lblInvalid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxNbCustomersPerHour);
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
        private System.Windows.Forms.TextBox tbxNbCustomersPerHour;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInvalid;
    }
}