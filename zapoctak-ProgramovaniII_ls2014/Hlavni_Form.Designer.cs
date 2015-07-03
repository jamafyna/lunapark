namespace zapoctak_ProgramovaniII_ls2014
{
    partial class Hlavni_Form
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartPanel2 = new System.Windows.Forms.Panel();
            this.textBox1Rozmer = new System.Windows.Forms.TextBox();
            this.radioButton60 = new System.Windows.Forms.RadioButton();
            this.radioButton40 = new System.Windows.Forms.RadioButton();
            this.radioButton20 = new System.Windows.Forms.RadioButton();
            this.Start_button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Konec_button1 = new System.Windows.Forms.Button();
            this.HraciPanel = new System.Windows.Forms.Panel();
            this.propagace_button = new System.Windows.Forms.Button();
            this.vyzkum_button = new System.Windows.Forms.Button();
            this.pocetPenez_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pocetLidiCislo_label = new System.Windows.Forms.Label();
            this.PopisPocetLidi_label = new System.Windows.Forms.Label();
            this.zbor_button = new System.Windows.Forms.Button();
            this.atrakce_button = new System.Windows.Forms.Button();
            this.Chodnik_button = new System.Windows.Forms.Button();
            this.timerLide = new System.Windows.Forms.Timer(this.components);
            this.timerAtrakce = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.StartPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.HraciPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.StartPanel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panel1.Location = new System.Drawing.Point(-1, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 422);
            this.panel1.TabIndex = 0;
            // 
            // StartPanel2
            // 
            this.StartPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.StartPanel2.Controls.Add(this.textBox1Rozmer);
            this.StartPanel2.Controls.Add(this.radioButton60);
            this.StartPanel2.Controls.Add(this.radioButton40);
            this.StartPanel2.Controls.Add(this.radioButton20);
            this.StartPanel2.Controls.Add(this.Start_button1);
            this.StartPanel2.Location = new System.Drawing.Point(201, 112);
            this.StartPanel2.Name = "StartPanel2";
            this.StartPanel2.Size = new System.Drawing.Size(226, 310);
            this.StartPanel2.TabIndex = 6;
            // 
            // textBox1Rozmer
            // 
            this.textBox1Rozmer.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1Rozmer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1Rozmer.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1Rozmer.Location = new System.Drawing.Point(6, 18);
            this.textBox1Rozmer.Name = "textBox1Rozmer";
            this.textBox1Rozmer.Size = new System.Drawing.Size(220, 19);
            this.textBox1Rozmer.TabIndex = 5;
            this.textBox1Rozmer.Text = Labels.sizeOfMap;
            this.textBox1Rozmer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radioButton60
            // 
            this.radioButton60.AutoSize = true;
            this.radioButton60.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButton60.Location = new System.Drawing.Point(70, 141);
            this.radioButton60.Name = "radioButton60";
            this.radioButton60.Size = new System.Drawing.Size(70, 22);
            this.radioButton60.TabIndex = 4;
            this.radioButton60.Text = "15 x 15";
            this.radioButton60.UseVisualStyleBackColor = true;
            this.radioButton60.CheckedChanged += new System.EventHandler(this.radioButton60_CheckedChanged);
            // 
            // radioButton40
            // 
            this.radioButton40.AutoSize = true;
            this.radioButton40.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButton40.Location = new System.Drawing.Point(70, 96);
            this.radioButton40.Name = "radioButton40";
            this.radioButton40.Size = new System.Drawing.Size(72, 22);
            this.radioButton40.TabIndex = 3;
            this.radioButton40.Text = "10 x 20";
            this.radioButton40.UseVisualStyleBackColor = true;
            this.radioButton40.CheckedChanged += new System.EventHandler(this.radioButton40_CheckedChanged);
            // 
            // radioButton20
            // 
            this.radioButton20.AutoSize = true;
            this.radioButton20.Checked = true;
            this.radioButton20.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButton20.Location = new System.Drawing.Point(70, 53);
            this.radioButton20.Name = "radioButton20";
            this.radioButton20.Size = new System.Drawing.Size(70, 22);
            this.radioButton20.TabIndex = 2;
            this.radioButton20.TabStop = true;
            this.radioButton20.Text = "10 x 10";
            this.radioButton20.UseVisualStyleBackColor = true;
            this.radioButton20.CheckedChanged += new System.EventHandler(this.radioButton20_CheckedChanged);
            // 
            // Start_button1
            // 
            this.Start_button1.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Start_button1.Location = new System.Drawing.Point(44, 212);
            this.Start_button1.Name = "Start_button1";
            this.Start_button1.Size = new System.Drawing.Size(141, 68);
            this.Start_button1.TabIndex = 1;
            this.Start_button1.Text = "START";
            this.Start_button1.UseVisualStyleBackColor = true;
            this.Start_button1.Click += new System.EventHandler(this.Start_button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Lime;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(289, 174);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click_1);
            // 
            // Konec_button1
            // 
            this.Konec_button1.Location = new System.Drawing.Point(3, 5);
            this.Konec_button1.Name = "Konec_button1";
            this.Konec_button1.Size = new System.Drawing.Size(67, 44);
            this.Konec_button1.TabIndex = 7;
            this.Konec_button1.Text = "KONEC";
            this.Konec_button1.UseVisualStyleBackColor = true;
            this.Konec_button1.Click += new System.EventHandler(this.Konec_button1_Click);
            // 
            // HraciPanel
            // 
            this.HraciPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HraciPanel.BackColor = System.Drawing.SystemColors.Control;
            this.HraciPanel.Controls.Add(this.propagace_button);
            this.HraciPanel.Controls.Add(this.vyzkum_button);
            this.HraciPanel.Controls.Add(this.pocetPenez_label);
            this.HraciPanel.Controls.Add(this.label1);
            this.HraciPanel.Controls.Add(this.pocetLidiCislo_label);
            this.HraciPanel.Controls.Add(this.PopisPocetLidi_label);
            this.HraciPanel.Controls.Add(this.zbor_button);
            this.HraciPanel.Controls.Add(this.atrakce_button);
            this.HraciPanel.Controls.Add(this.Chodnik_button);
            this.HraciPanel.Controls.Add(this.Konec_button1);
            this.HraciPanel.Location = new System.Drawing.Point(2, 0);
            this.HraciPanel.Name = "HraciPanel";
            this.HraciPanel.Size = new System.Drawing.Size(691, 53);
            this.HraciPanel.TabIndex = 9;
            this.HraciPanel.Visible = false;
            // 
            // propagace_button
            // 
            this.propagace_button.Location = new System.Drawing.Point(379, 27);
            this.propagace_button.Name = "propagace_button";
            this.propagace_button.Size = new System.Drawing.Size(105, 23);
            this.propagace_button.TabIndex = 16;
            this.propagace_button.Text = "PROPAGACE";
            this.propagace_button.UseVisualStyleBackColor = true;
            this.propagace_button.Click += new System.EventHandler(this.propagace_button_Click);
            // 
            // vyzkum_button
            // 
            this.vyzkum_button.Location = new System.Drawing.Point(379, 5);
            this.vyzkum_button.Name = "vyzkum_button";
            this.vyzkum_button.Size = new System.Drawing.Size(105, 23);
            this.vyzkum_button.TabIndex = 15;
            this.vyzkum_button.Text = "VÝZKUM";
            this.vyzkum_button.UseVisualStyleBackColor = true;
            this.vyzkum_button.Click += new System.EventHandler(this.vyzkum_button_Click);
            // 
            // pocetPenez_label
            // 
            this.pocetPenez_label.AutoSize = true;
            this.pocetPenez_label.Location = new System.Drawing.Point(577, 30);
            this.pocetPenez_label.Name = "pocetPenez_label";
            this.pocetPenez_label.Size = new System.Drawing.Size(42, 15);
            this.pocetPenez_label.TabIndex = 14;
            this.pocetPenez_label.Text = "20000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(504, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Počet peněz:";
            // 
            // pocetLidiCislo_label
            // 
            this.pocetLidiCislo_label.AutoSize = true;
            this.pocetLidiCislo_label.Location = new System.Drawing.Point(577, 13);
            this.pocetLidiCislo_label.Name = "pocetLidiCislo_label";
            this.pocetLidiCislo_label.Size = new System.Drawing.Size(14, 15);
            this.pocetLidiCislo_label.TabIndex = 12;
            this.pocetLidiCislo_label.Text = "0";
            // 
            // PopisPocetLidi_label
            // 
            this.PopisPocetLidi_label.AutoSize = true;
            this.PopisPocetLidi_label.Location = new System.Drawing.Point(504, 13);
            this.PopisPocetLidi_label.Name = "PopisPocetLidi_label";
            this.PopisPocetLidi_label.Size = new System.Drawing.Size(59, 15);
            this.PopisPocetLidi_label.TabIndex = 11;
            this.PopisPocetLidi_label.Text = "Počet lidí:";
            // 
            // zbor_button
            // 
            this.zbor_button.Location = new System.Drawing.Point(284, 5);
            this.zbor_button.Name = "zbor_button";
            this.zbor_button.Size = new System.Drawing.Size(74, 44);
            this.zbor_button.TabIndex = 10;
            this.zbor_button.Text = "ZBOŘ";
            this.zbor_button.UseVisualStyleBackColor = true;
            this.zbor_button.Click += new System.EventHandler(this.zbor_button_Click);
            // 
            // atrakce_button
            // 
            this.atrakce_button.Location = new System.Drawing.Point(204, 5);
            this.atrakce_button.Name = "atrakce_button";
            this.atrakce_button.Size = new System.Drawing.Size(74, 44);
            this.atrakce_button.TabIndex = 9;
            this.atrakce_button.Text = "ATRAKCE";
            this.atrakce_button.UseVisualStyleBackColor = true;
            this.atrakce_button.Click += new System.EventHandler(this.atrakce_button_Click);
            // 
            // Chodnik_button
            // 
            this.Chodnik_button.Location = new System.Drawing.Point(127, 5);
            this.Chodnik_button.Name = "Chodnik_button";
            this.Chodnik_button.Size = new System.Drawing.Size(71, 44);
            this.Chodnik_button.TabIndex = 8;
            this.Chodnik_button.Text = "CESTA";
            this.Chodnik_button.UseVisualStyleBackColor = true;
            this.Chodnik_button.Click += new System.EventHandler(this.Chodnik_button_Click);
            // 
            // timerLide
            // 
            this.timerLide.Tick += new System.EventHandler(this.timerLide_Tick);
            // 
            // timerAtrakce
            // 
            this.timerAtrakce.Interval = 1000;
            this.timerAtrakce.Tick += new System.EventHandler(this.timerAtrakce_Tick);
            // 
            // Hlavni_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 479);
            this.Controls.Add(this.HraciPanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8F);
            this.Name = "Hlavni_Form";
            this.Text = "Pouť";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Hlavni_Form_FormClosing);
            this.panel1.ResumeLayout(false);
            this.StartPanel2.ResumeLayout(false);
            this.StartPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.HraciPanel.ResumeLayout(false);
            this.HraciPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_button1;
        private System.Windows.Forms.RadioButton radioButton20;
        private System.Windows.Forms.TextBox textBox1Rozmer;
        private System.Windows.Forms.RadioButton radioButton60;
        private System.Windows.Forms.RadioButton radioButton40;
        private System.Windows.Forms.Panel StartPanel2;
        private System.Windows.Forms.Button Konec_button1;
        private System.Windows.Forms.Panel HraciPanel;
        private System.Windows.Forms.Button atrakce_button;
        private System.Windows.Forms.Button Chodnik_button;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timerLide;
        private System.Windows.Forms.Timer timerAtrakce;
        public System.Windows.Forms.Button zbor_button;
        private System.Windows.Forms.Label PopisPocetLidi_label;
        public System.Windows.Forms.Label pocetLidiCislo_label;
        public System.Windows.Forms.Label pocetPenez_label;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button propagace_button;
        public System.Windows.Forms.Button vyzkum_button;




    }
}

