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
    public partial class RozhodovaciForm : Form
    {
        public bool otevren;
        public enum tlacitka { Pokracovat, Konec, NovaHra };
        public tlacitka odkliknuto; //informace o zmacknutem tlacitku se predavaji pres odkliknuto

        public RozhodovaciForm() 
        { 
        
        }

        public RozhodovaciForm(ref bool otevren)
        {
            InitializeComponent();
        }

        private void Pokracovat_button2_Click(object sender, EventArgs e)
        {
            odkliknuto = tlacitka.Pokracovat;
        }

        private void NovaHra_button1_Click(object sender, EventArgs e)
        {
            odkliknuto = tlacitka.NovaHra;
        }

        private void UplnyKonec_button1_Click(object sender, EventArgs e)
        {
            odkliknuto = tlacitka.Konec;
        }

        
    }
}
