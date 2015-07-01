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
    public partial class ChodnikForm : Form
    {
        private Hlavni_Form hlform;
        public ChodnikForm()
        {
            InitializeComponent();
        }
        public ChodnikForm(Hlavni_Form form)
        {
            InitializeComponent();
            cenaKameny_label3.Text = Program.pricePStones.ToString();
            hlform = form; // ukladam odkaz na hlavni form, abych s nim mohla pracovat i v jinych procedurach
        }

        private void ChodnikForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hlform.chodnikFormOtevren = false;
            if(
               hlform.vybranoStavit == Hlavni_Form.stavba.chasfalt ||
               hlform.vybranoStavit == Hlavni_Form.stavba.chkameny ||
               hlform.vybranoStavit == Hlavni_Form.stavba.chmramor||
               hlform.vybranoStavit == Hlavni_Form.stavba.chpisek 
               )
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.nic;
            }
            e.Cancel = true;
            this.Hide(); //pouze skryvam, jelikoz se pouziva casto
            
        }

        private void kameny_button_Click(object sender, EventArgs e)
        {
            if (hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.chkameny;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
           
           
        }

        private void asfalt_button_Click(object sender, EventArgs e)
        {
            if (hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.chasfalt;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
        }

        private void pisek_button_Click(object sender, EventArgs e)
        {
            if (hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.chpisek;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
        }

        private void mramor_button_Click(object sender, EventArgs e)
        {
            if(hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.chmramor;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
        }

        private void fontana_button_Click(object sender, EventArgs e)
        {
            if(hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.fontana;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
        }

        private void strom_button_Click(object sender, EventArgs e)
        {
            if (hlform.povoleniStavitJineNezIO())
            {
                hlform.vybranoStavit = Hlavni_Form.stavba.strom;
                hlform.zbor = false;
                hlform.zbor_button.Text = "ZBOŘ";
            }
        }

        
    }
}
