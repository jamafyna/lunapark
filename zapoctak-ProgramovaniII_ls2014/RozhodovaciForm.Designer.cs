namespace zapoctak_ProgramovaniII_ls2014
{
    partial class RozhodovaciForm
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
            this.NovaHra_button1 = new System.Windows.Forms.Button();
            this.UplnyKonec_button1 = new System.Windows.Forms.Button();
            this.Pokracovat_button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NovaHra_button1
            // 
            this.NovaHra_button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.NovaHra_button1.Location = new System.Drawing.Point(18, 63);
            this.NovaHra_button1.Name = "NovaHra_button1";
            this.NovaHra_button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.NovaHra_button1.Size = new System.Drawing.Size(255, 43);
            this.NovaHra_button1.TabIndex = 8;
            this.NovaHra_button1.Text = "NOVÁ HRA";
            this.NovaHra_button1.UseVisualStyleBackColor = true;
            this.NovaHra_button1.Click += new System.EventHandler(this.NovaHra_button1_Click);
            // 
            // UplnyKonec_button1
            // 
            this.UplnyKonec_button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.UplnyKonec_button1.Location = new System.Drawing.Point(18, 113);
            this.UplnyKonec_button1.Name = "UplnyKonec_button1";
            this.UplnyKonec_button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.UplnyKonec_button1.Size = new System.Drawing.Size(255, 43);
            this.UplnyKonec_button1.TabIndex = 9;
            this.UplnyKonec_button1.Text = "KONEC";
            this.UplnyKonec_button1.UseVisualStyleBackColor = true;
            this.UplnyKonec_button1.Click += new System.EventHandler(this.UplnyKonec_button1_Click);
            // 
            // Pokracovat_button2
            // 
            this.Pokracovat_button2.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Pokracovat_button2.Location = new System.Drawing.Point(18, 14);
            this.Pokracovat_button2.Name = "Pokracovat_button2";
            this.Pokracovat_button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Pokracovat_button2.Size = new System.Drawing.Size(255, 43);
            this.Pokracovat_button2.TabIndex = 10;
            this.Pokracovat_button2.Text = "POKRAČOVAT";
            this.Pokracovat_button2.UseVisualStyleBackColor = true;
            this.Pokracovat_button2.Click += new System.EventHandler(this.Pokracovat_button2_Click);
            // 
            // RozhodovaciForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 302);
            this.Controls.Add(this.UplnyKonec_button1);
            this.Controls.Add(this.Pokracovat_button2);
            this.Controls.Add(this.NovaHra_button1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "RozhodovaciForm";
            this.Text = "Kam dál";
            //this.Load += new System.EventHandler(this.RozhodovaciForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NovaHra_button1;
        private System.Windows.Forms.Button UplnyKonec_button1;
        private System.Windows.Forms.Button Pokracovat_button2;

    }
}