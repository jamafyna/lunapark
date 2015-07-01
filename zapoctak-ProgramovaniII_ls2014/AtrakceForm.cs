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
    public partial class AtrakceForm : Form
    {
        Hlavni_Form hlform;
        public AtrakceForm(Hlavni_Form form)
        {
            InitializeComponent();
            hlform = form;
        }

                
        private void detskyKol_button_Click(object sender, EventArgs e)
        {
            AtrakceInfoForm infoFormular = new AtrakceInfoForm(hlform);
            DialogResult infoForm = infoFormular.ShowDialog();
            if (infoForm == DialogResult.Yes) //yes = koupit, no=zpět
            {
               
                if (hlform.evidence.pocetPenez >= Program.priceCarousel)
                {
                    

                    if (hlform.povoleniStavitJineNezIO())
                    {
                        hlform.vybranoStavit = Hlavni_Form.stavba.adetskyKol;
                        hlform.barvaAtrakce = infoFormular.barva;
                        hlform.zbor = false;
                        hlform.zbor_button.Text = "ZBOŘ";
                        hlform.evidence.pocetPenez -= Program.priceCarousel; ;
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    
                }
                else MessageBox.Show("Není možné atrakci koupit. Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK); 
            }
        }

        private void obcerstveni_button_Click(object sender, EventArgs e)
        {
            AtrakceInfoForm infoFormular = new AtrakceInfoForm(hlform);
            infoFormular.nazevAtrakce_label.Text = "OBČERSTVENÍ";
            infoFormular.atrInfo_label.Text = "velikost: 1 x 1 \npořizovací cena: " + Program.priceRestaurant; // \n ... novy radek
            infoFormular.atrNahled_pictureBox.BackgroundImage = Properties.Resources.obcerstveni;
            infoFormular.atrNahled_pictureBox.Height = 50;
            infoFormular.atrNahled_pictureBox.Width = 50;
            DialogResult infoForm = infoFormular.ShowDialog();
            if (infoForm == DialogResult.Yes) //yes = koupit, no=zpět
            {
                if (hlform.evidence.pocetPenez >= Program.priceRestaurant)
                {

                    if (hlform.povoleniStavitJineNezIO())
                    {
                        hlform.vybranoStavit = Hlavni_Form.stavba.aobcerstveni;
                        hlform.barvaAtrakce = infoFormular.barva;
                        hlform.zbor = false;
                        hlform.zbor_button.Text = "ZBOŘ";
                    }
                }
                else MessageBox.Show("Není možné atrakci koupit. Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK); 
            }
        } 

        private void houpacka_button_Click(object sender, EventArgs e)
        {
            AtrakceInfoForm infoFormular = new AtrakceInfoForm(hlform);
            infoFormular.nazevAtrakce_label.Text = "HOUPACÍ LOĎ";
            infoFormular.atrNahled_pictureBox.BackgroundImage = Properties.Resources.lod2;
            infoFormular.atrNahled_pictureBox.Height = 50;
            infoFormular.atrNahled_pictureBox.Width = 250;
            infoFormular.atrInfo_label.Text = "velikost: 1x5 \npořizovací cena: "+Program.priceShip+"\nkapacita: 8 "; // \n ... novy radek
           
            DialogResult infoForm = infoFormular.ShowDialog();
            if (infoForm == DialogResult.Yes) //yes = koupit, no=zpět
            {
               
                if (hlform.evidence.pocetPenez >= Program.priceShip)
                {

                    if (hlform.povoleniStavitJineNezIO())
                    {
                        hlform.vybranoStavit = Hlavni_Form.stavba.ahoupLod;
                        hlform.barvaAtrakce = infoFormular.barva;
                        hlform.zbor = false;
                        hlform.zbor_button.Text = "ZBOŘ";
                    }
                }
                else MessageBox.Show("Není možné atrakci koupit. Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK); 
            }
        }

        private void AtrakceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            hlform.atrakceFormOtevren = false;
            e.Cancel = true;
            this.Hide();
        }

        
    }
}
