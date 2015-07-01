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
    public partial class AtrakceInfoForm : Form //formular, ktery se otevre pri kliknuti na vybranou atrakci z menu Atrakce
    {       
        Hlavni_Form hlform;
        public Color barva=Color.Orange; 
    
        public AtrakceInfoForm(Hlavni_Form form)
        {
            InitializeComponent();
            hlform = form;
        }

        
        private void zmenBarvu_button_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog()==DialogResult.OK) //NEVIM, JAK ZRUSIT ZAVRENI CELEHO FORMULARE PO UKONCENI COLORDIALOGU
            {
                barva = colorDialog.Color;
                this.BackColor = barva; 
            }
           
           
        }

        
      
    }
}
