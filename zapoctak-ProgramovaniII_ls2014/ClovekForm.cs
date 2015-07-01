using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zapoctak_ProgramovaniII_ls2014
{
    public partial class ClovekForm : Form
    {
        public ClovekForm()
        {
            InitializeComponent();
        }

        

        public void Show(int id, int idAtrakce, int spokojenost, int pocetPenez, int hlad)
        {
            id_label.Text = id.ToString();
            idAtrakce_label.Text = idAtrakce.ToString();
            spokojenost_label.Text = spokojenost.ToString()+" %";
            pocetPenez_label.Text = pocetPenez.ToString();
            hlad_label.Text = hlad + " %";
            this.Show();          
        }

        
        
        private void ClovekForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        
    }
}
