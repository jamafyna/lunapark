namespace zapoctak_ProgramovaniII_ls2014
{
    partial class ClovekForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClovekForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.id_label = new System.Windows.Forms.Label();
            this.idAtrakce_label = new System.Windows.Forms.Label();
            this.spokojenost_label = new System.Windows.Forms.Label();
            this.pocetPenez_label = new System.Windows.Forms.Label();
            this.hlad_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // id_label
            // 
            resources.ApplyResources(this.id_label, "id_label");
            this.id_label.Name = "id_label";
            //this.id_label.Click += new System.EventHandler(this.id_label_Click);
            // 
            // idAtrakce_label
            // 
            resources.ApplyResources(this.idAtrakce_label, "idAtrakce_label");
            this.idAtrakce_label.Name = "idAtrakce_label";
            // 
            // spokojenost_label
            // 
            resources.ApplyResources(this.spokojenost_label, "spokojenost_label");
            this.spokojenost_label.Name = "spokojenost_label";
            // 
            // pocetPenez_label
            // 
            resources.ApplyResources(this.pocetPenez_label, "pocetPenez_label");
            this.pocetPenez_label.Name = "pocetPenez_label";
            // 
            // hlad_label
            // 
            resources.ApplyResources(this.hlad_label, "hlad_label");
            this.hlad_label.Name = "hlad_label";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // ClovekForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hlad_label);
            this.Controls.Add(this.pocetPenez_label);
            this.Controls.Add(this.spokojenost_label);
            this.Controls.Add(this.idAtrakce_label);
            this.Controls.Add(this.id_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ClovekForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClovekForm_FormClosing);
            //this.Load += new System.EventHandler(this.ClovekForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label id_label;
        private System.Windows.Forms.Label idAtrakce_label;
        private System.Windows.Forms.Label spokojenost_label;
        private System.Windows.Forms.Label pocetPenez_label;
        private System.Windows.Forms.Label hlad_label;
        private System.Windows.Forms.Label label6;
    }
}