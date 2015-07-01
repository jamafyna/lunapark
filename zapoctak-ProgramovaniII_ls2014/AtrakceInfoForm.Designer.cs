namespace zapoctak_ProgramovaniII_ls2014
{
    partial class AtrakceInfoForm
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
            this.atrKoupit_button = new System.Windows.Forms.Button();
            this.nazevAtrakce_label = new System.Windows.Forms.Label();
            this.atrInfo_label = new System.Windows.Forms.Label();
            this.zpet_button = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.zmenBarvu_button = new System.Windows.Forms.Button();
            this.atrNahled_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.atrNahled_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // atrKoupit_button
            // 
            this.atrKoupit_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.atrKoupit_button.Location = new System.Drawing.Point(13, 216);
            this.atrKoupit_button.Name = "atrKoupit_button";
            this.atrKoupit_button.Size = new System.Drawing.Size(140, 38);
            this.atrKoupit_button.TabIndex = 1;
            this.atrKoupit_button.Text = "koupit";
            this.atrKoupit_button.UseVisualStyleBackColor = true;
           // this.atrKoupit_button.Click += new System.EventHandler(this.atrKoupit_button_Click);
            // 
            // nazevAtrakce_label
            // 
            this.nazevAtrakce_label.AutoSize = true;
            this.nazevAtrakce_label.Location = new System.Drawing.Point(12, 17);
            this.nazevAtrakce_label.Name = "nazevAtrakce_label";
            this.nazevAtrakce_label.Size = new System.Drawing.Size(108, 15);
            this.nazevAtrakce_label.TabIndex = 2;
            this.nazevAtrakce_label.Text = "DĚTSKÝ KOLOTOČ";
            // 
            // atrInfo_label
            // 
            this.atrInfo_label.AutoSize = true;
            this.atrInfo_label.Location = new System.Drawing.Point(10, 45);
            this.atrInfo_label.Name = "atrInfo_label";
            this.atrInfo_label.Size = new System.Drawing.Size(123, 45);
            this.atrInfo_label.TabIndex = 3;
            this.atrInfo_label.Text = "velikost: 2 x 2\r\npořizovací cena: 7500\r\nkapacita: 5";
           // this.atrInfo_label.Click += new System.EventHandler(this.atrInfo_label_Click);
            // 
            // zpet_button
            // 
            this.zpet_button.DialogResult = System.Windows.Forms.DialogResult.No;
            this.zpet_button.Location = new System.Drawing.Point(159, 216);
            this.zpet_button.Name = "zpet_button";
            this.zpet_button.Size = new System.Drawing.Size(140, 38);
            this.zpet_button.TabIndex = 4;
            this.zpet_button.Text = "zpět";
            this.zpet_button.UseVisualStyleBackColor = true;
            //this.zpet_button.Click += new System.EventHandler(this.zpet_button_Click);
            // 
            // zmenBarvu_button
            // 
            this.zmenBarvu_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.zmenBarvu_button.Location = new System.Drawing.Point(159, 178);
            this.zmenBarvu_button.Name = "zmenBarvu_button";
            this.zmenBarvu_button.Size = new System.Drawing.Size(140, 26);
            this.zmenBarvu_button.TabIndex = 5;
            this.zmenBarvu_button.Text = "změň barvu";
            this.zmenBarvu_button.UseVisualStyleBackColor = true;
            this.zmenBarvu_button.Click += new System.EventHandler(this.zmenBarvu_button_Click);
            // 
            // atrNahled_pictureBox
            // 
            this.atrNahled_pictureBox.BackColor = System.Drawing.Color.DarkOrange;
            this.atrNahled_pictureBox.BackgroundImage = global::zapoctak_ProgramovaniII_ls2014.Properties.Resources.kolotoc;
            this.atrNahled_pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.atrNahled_pictureBox.Location = new System.Drawing.Point(13, 103);
            this.atrNahled_pictureBox.Name = "atrNahled_pictureBox";
            this.atrNahled_pictureBox.Size = new System.Drawing.Size(100, 100);
            this.atrNahled_pictureBox.TabIndex = 0;
            this.atrNahled_pictureBox.TabStop = false;
            // 
            // AtrakceInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(331, 264);
            this.Controls.Add(this.zmenBarvu_button);
            this.Controls.Add(this.zpet_button);
            this.Controls.Add(this.atrInfo_label);
            this.Controls.Add(this.nazevAtrakce_label);
            this.Controls.Add(this.atrKoupit_button);
            this.Controls.Add(this.atrNahled_pictureBox);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "AtrakceInfoForm";
            this.Text = "Informace";
            //this.Load += new System.EventHandler(this.AtrakceInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.atrNahled_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button atrKoupit_button;
        private System.Windows.Forms.Button zpet_button;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button zmenBarvu_button;
        public System.Windows.Forms.PictureBox atrNahled_pictureBox;
        public System.Windows.Forms.Label nazevAtrakce_label;
        public System.Windows.Forms.Label atrInfo_label;

    }
}