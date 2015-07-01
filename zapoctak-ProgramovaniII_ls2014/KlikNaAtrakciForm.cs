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
    public partial class KlikNaAtrakciForm : Form
    {
        Atrakce atrakce;
        Hlavni_Form hlform;
        LSSAtrakce atrakceLSS;
        
        public KlikNaAtrakciForm(Atrakce atr, LSSAtrakce atrLSS, Hlavni_Form form)
        {
            InitializeComponent();
            atrakce = atr;
            atrakceLSS = atrLSS;
            hlform = form;

        }

        private void mimoProvoz_button_Click(object sender, EventArgs e)
        {
            if (atrakce.mimoProvoz) //pokud je atrakce mimo provoz nebo dobiha, tj. nevstupuji na ni nove lidi
            {
                atrakce.mimoProvoz = false;
                atrakce.zmenStav(Atrakce.Stav.cekaNaLidi);
                atrakceLSS.NastavVzdalenostVsemAtrakcim(hlform);
                mimoProvoz_button.Text = "V provozu";
                mimoProvoz_button.BackColor = Color.Lime;

            }
            else //atrakce bezi
            {
                atrakce.mimoProvoz = true;
                hlform.evidence.mapaAtrakciAChodniku.zrusPristupKAtrakci(atrakce.id); 
                atrakce.zmenStav(Atrakce.Stav.dobiha);
                mimoProvoz_button.Text = "Zákaz vstupu";
                mimoProvoz_button.BackColor = Color.Orange;
            }
        }

        private void KlikNaAtrakciForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

       

        private void vstupne_numericUpDown1_ValueChanged(object sender, EventArgs e)
        {           
            Brana atr = atrakce as Brana;
            if (atr == null) //tj. jde o jinou atrakci nez brana, nutne protoze brana ma vstupne pouze jednosmerne
            {
                atrakce.vstupne = (int)vstupne_numericUpDown1.Value;
                hlform.evidence.minimalniCena = Math.Min(atrakce.vstupne, hlform.evidence.minimalniCena);
            }
            else //jde o branu
            {
                atr.vstupneDoParku = (int)vstupne_numericUpDown1.Value; //vstupne pro vstup do parku se jmenuje vstupneDoParku, pro vystup vstupne=0              
            }
            
            
           
        }
    }
}
