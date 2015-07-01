namespace zapoctak_ProgramovaniII_ls2014
{
    partial class KlikNaAtrakciForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KlikNaAtrakciForm));
            this.mimoProvoz_button = new System.Windows.Forms.Button();
            this.info_label = new System.Windows.Forms.Label();
            this.cenaVstupneho_label1 = new System.Windows.Forms.Label();
            this.vstupne_numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.vstupne_numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // mimoProvoz_button
            // 
            resources.ApplyResources(this.mimoProvoz_button, "mimoProvoz_button");
            this.mimoProvoz_button.BackColor = System.Drawing.Color.Red;
            this.mimoProvoz_button.Name = "mimoProvoz_button";
            this.mimoProvoz_button.UseVisualStyleBackColor = false;
            this.mimoProvoz_button.Click += new System.EventHandler(this.mimoProvoz_button_Click);
            // 
            // info_label
            // 
            resources.ApplyResources(this.info_label, "info_label");
            this.info_label.Name = "info_label";
            // 
            // cenaVstupneho_label1
            // 
            resources.ApplyResources(this.cenaVstupneho_label1, "cenaVstupneho_label1");
            this.cenaVstupneho_label1.Name = "cenaVstupneho_label1";
            // 
            // vstupne_numericUpDown1
            // 
            resources.ApplyResources(this.vstupne_numericUpDown1, "vstupne_numericUpDown1");
            this.vstupne_numericUpDown1.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.vstupne_numericUpDown1.Name = "vstupne_numericUpDown1";
            this.vstupne_numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.vstupne_numericUpDown1.ValueChanged += new System.EventHandler(this.vstupne_numericUpDown1_ValueChanged);
            // 
            // KlikNaAtrakciForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vstupne_numericUpDown1);
            this.Controls.Add(this.cenaVstupneho_label1);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.mimoProvoz_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "KlikNaAtrakciForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KlikNaAtrakciForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.vstupne_numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label info_label;
        public System.Windows.Forms.Button mimoProvoz_button;
        public System.Windows.Forms.Label cenaVstupneho_label1;
        public System.Windows.Forms.NumericUpDown vstupne_numericUpDown1;
    }
}