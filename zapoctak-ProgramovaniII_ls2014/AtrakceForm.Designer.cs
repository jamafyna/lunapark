namespace zapoctak_ProgramovaniII_ls2014
{
    partial class AtrakceForm
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
            this.detskyKol_button = new System.Windows.Forms.Button();
            this.obcerstveni_button = new System.Windows.Forms.Button();
            this.houpacka_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // detskyKol_button
            // 
            this.detskyKol_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.detskyKol_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.detskyKol_button.Location = new System.Drawing.Point(14, 22);
            this.detskyKol_button.Name = "detskyKol_button";
            this.detskyKol_button.Size = new System.Drawing.Size(89, 33);
            this.detskyKol_button.TabIndex = 4;
            this.detskyKol_button.Text = "dětský kolotoč";
            this.detskyKol_button.UseVisualStyleBackColor = true;
            this.detskyKol_button.Click += new System.EventHandler(this.detskyKol_button_Click);
            // 
            // obcerstveni_button
            // 
            this.obcerstveni_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.obcerstveni_button.Location = new System.Drawing.Point(109, 22);
            this.obcerstveni_button.Name = "obcerstveni_button";
            this.obcerstveni_button.Size = new System.Drawing.Size(89, 33);
            this.obcerstveni_button.TabIndex = 6;
            this.obcerstveni_button.Text = "občerstvení";
            this.obcerstveni_button.UseVisualStyleBackColor = true;
            this.obcerstveni_button.Click += new System.EventHandler(this.obcerstveni_button_Click);
            // 
            // houpacka_button
            // 
            this.houpacka_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.houpacka_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.houpacka_button.Location = new System.Drawing.Point(204, 22);
            this.houpacka_button.Name = "houpacka_button";
            this.houpacka_button.Size = new System.Drawing.Size(89, 33);
            this.houpacka_button.TabIndex = 7;
            this.houpacka_button.Text = "houpací loď";
            this.houpacka_button.UseVisualStyleBackColor = true;
            this.houpacka_button.Visible = false;
            this.houpacka_button.Click += new System.EventHandler(this.houpacka_button_Click);
            // 
            // AtrakceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(331, 68);
            this.Controls.Add(this.houpacka_button);
            this.Controls.Add(this.obcerstveni_button);
            this.Controls.Add(this.detskyKol_button);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AtrakceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Atrakce";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AtrakceForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button detskyKol_button;
        private System.Windows.Forms.Button obcerstveni_button;
        public System.Windows.Forms.Button houpacka_button;
    }
}