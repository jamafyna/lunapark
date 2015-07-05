using System;
using System.Drawing;
using System.Windows.Forms;


namespace zapoctak_ProgramovaniII_ls2014
{
    
    public partial class Hlavni_Form : Form
    {
       
        public enum stavba { nic, chkameny, chasfalt, chpisek, chmramor, aVstup, aVystup, adetskyKol, aobcerstveni,  ahoupLod, fontana, strom };

        public int sirka=12, vyska=12; //pocatecni volba rozmeru planu - predzaskrtle
        
        public bool zbor=false;
        public bool chodnikFormOtevren = false;
        public bool atrakceFormOtevren = false;
        public Color barvaAtrakce;
        public int idAktAtrakce = 0;//id posledni postavene atrakce
        public Atrakce atrakce; //posledni postavena atrakce
        public stavba vybranoStavit;
        public Plot plot;
        public Brana brana;
        public Random random;
        public Evidence evidence;
        RozhodovaciForm rozhodovaciFormular;
        public ChodnikForm chodnikyFormular;
        public AtrakceForm atrakceFormular;
        public ClovekForm clovekFormular;
    

        public Hlavni_Form()
        {
            InitializeComponent();
            chodnikyFormular = new ChodnikForm(this);
            atrakceFormular = new AtrakceForm(this);
            clovekFormular = new ClovekForm();
            vybranoStavit = stavba.nic;
            random = new Random();                      
        }
       
        //-----metoda pro nakresleni a vytvoreni hraciho planu
        private void VytvorMapu(int sirka,int vyska)
        {
            int rozmer = Program.sizeOfSquare;

            //-----vytvoreni PictureBoxu
            Bitmap bmp = new Bitmap(sirka * rozmer, vyska * rozmer);
            pictureBox1.Image = bmp;
            pictureBox1.Size = bmp.Size;
            Graphics gr = Graphics.FromImage(bmp);
            
            //-----nakresleni mrizky-------------
            Pen pero = new Pen(Color.DarkSeaGreen, 1);
            for (int i = 0; i <= vyska; i++) 
            {
                gr.DrawLine(pero, 0, i * rozmer, sirka * rozmer, i * rozmer);
            }
            for (int i = 0; i <= sirka; i++)
            {
                gr.DrawLine(pero, i * rozmer, 0, i * rozmer, vyska * rozmer);
            }
 
            pictureBox1.Refresh();//musime provest, pokud se zmenila bitmapa a zmenu chceme videt na obrazovce

            //stavba plotu a brany
            plot = new Plot(this);
            brana = new Brana(this);
           
        }

        
        //-----kliknuti na hlavni hraci plochu, na pictureBox1
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            //zisk souradnic, rovnou upravuji na mrizkova policka
            MouseEventArgs mys = (MouseEventArgs)e;
            int x = mys.X - mys.X % Program.sizeOfSquare; 
            int y = mys.Y - mys.Y % Program.sizeOfSquare;
          
            switch (vybranoStavit)
            {
                case stavba.nic: //nic se neudela
                    
                    break;
                case stavba.chkameny:
                    {
                        if (evidence.pocetPenez >= Program.pricePStones)
                        {
                            Chodnik chodnik = new KamennyChodnik(x, y, this);
                            
                        }
                        else MessageBox.Show("Nedostatek peněz.","Upozornění",MessageBoxButtons.OK);
                    }
                    break;
                case stavba.chasfalt:
                    {
                        if (evidence.pocetPenez >= Program.pricePAsphalt)
                        {
                            Chodnik chodnik = new AsfaltChodnik(x, y, this);
                        }
                        else MessageBox.Show("Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK);
                            
                    }
                    break;
                case stavba.chpisek:
                    {
                        if (evidence.pocetPenez >= Program.pricePSand)
                        {
                            Chodnik chodnik = new PisekChodnik(x, y, this);
                        }
                        else MessageBox.Show("Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK);
                    }
                    break;
                case stavba.chmramor:
                    {
                        if (evidence.pocetPenez >= Program.pricePMarble)
                        { 
                            Chodnik chodnik = new MramorChodnik(x, y, this); 
                        }
                        else MessageBox.Show("Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK);

                    }
                    break;
                case stavba.fontana:
                    {
                        if (evidence.pocetPenez >= Program.priceFountain)
                        {
                            Atrakce fontana = new Fontana(x, y, this);
                            vybranoStavit = stavba.nic;
                        }
                        else MessageBox.Show("Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK);

                    }
                    break;

                case stavba.strom: 
                    {
                        if (evidence.pocetPenez >= Program.priceTree)
                        {
                            Policko strom = new Zelen(mys.X/10*10, mys.Y/10*10, this);
                        }
                        else MessageBox.Show("Nedostatek peněz.", "Upozornění", MessageBoxButtons.OK);
                    }
                    break;

                case stavba.adetskyKol:
                    {
                         if (overUmisteniAtrakce(x, y, 2, 2))
                            {
                                atrakce = new DetskyKolotoc(x, y, this);
                                vybranoStavit = stavba.aVstup;
                                idAktAtrakce = atrakce.id;
                            }                            
                      
                    }
                    break;
                case stavba.ahoupLod:
                    {

                        if (overUmisteniAtrakce(x, y, 5, 1))
                        {
                            atrakce = new HoupaciLod(x, y, this);
                            vybranoStavit = stavba.aVstup;
                            idAktAtrakce = atrakce.id;
                        }
                        else
                        { MessageBox.Show("Není možné atrakci umístit. Nedostatek místa.", "Varování", MessageBoxButtons.OK); }
                                  
                    }
                    break;
                case stavba.aobcerstveni:
                    {
                        atrakce = new Obcerstveni(x, y, this);
                        vybranoStavit = stavba.nic;
                        idAktAtrakce = atrakce.id;
                    }
                    break;
                case stavba.aVstup:
                    {
                        
                        if (overUmisteniVstupVystup(x, y, idAktAtrakce))
                        {
                            atrakce.vstupX = x; 
                            atrakce.vstupY = y;
                            Chodnik chodnik = new AtrakceVstupChodnik(x, y, this, atrakce);
                            
                            if (zbor) vybranoStavit = stavba.nic;
                            else vybranoStavit = stavba.aVystup;
                            
                        }
                    }
                    break;
                case stavba.aVystup:
                    {
                        if (overUmisteniVstupVystup(x, y, idAktAtrakce))
                        {
                            vybranoStavit = stavba.nic;
                            Chodnik chodnik = new AtrakceVystupChodnik(x, y, this, atrakce);
                            atrakce.vystupX = x;
                            atrakce.vystupY = y;
                        }
                       
                        
                       
                    }
                    break;
                default:
                    break;
            }
            
           
        }

        //--------overuje, zda nema byt postaven vstup nebo vystup misto jineho
        public bool povoleniStavitJineNezIO()
        {
            if (vybranoStavit == stavba.aVstup || vybranoStavit == stavba.aVystup)
            {
                MessageBox.Show("Nejdříve se musí dokončit stavba atrakce. Klikněte pro postavení vstupu k atrakci nebo výstupu. ", "Upozornění", MessageBoxButtons.OK);
                return false;
            }
            else return true;
        }
        
       //---------overuje, zda je misto na atrakci------------------------------
        private bool overUmisteniAtrakce(int x, int y, int delkax, int delkay)
        {
            bool prazdne=true;
                //overeni, zda nesaham mimo pole (pozdeji lepe osetrit jinak, napr. vyjimkou)
            if (( x + Program.sizeOfSquare * delkax > (sirka-1)*Program.sizeOfSquare) || 
                (y + Program.sizeOfSquare * delkay > (vyska-1)*Program.sizeOfSquare))
            return false;
                //v pripade, ze atrakce se vejde do mapy, overeni, zda neni nic postaveno
            for (int i = x; i < x+Program.sizeOfSquare*delkax; i+=Program.sizeOfSquare)
            {
                for (int j = y; j < y+Program.sizeOfSquare*delkay; j+=Program.sizeOfSquare)
                {
                    prazdne = prazdne && evidence.mapaAtrakciAChodniku.prazdne(i,j);
                }
            }
            if (prazdne) return true;
            else return false;
        
        }

        //--------overuje, zda uzivatel umistuje vstup/vystup k atrakci
        private bool overUmisteniVstupVystup(int x, int y, int idAtrakce)
        {
            if (evidence.mapaAtrakciAChodniku.jeAtrakce(x +Program.sizeOfSquare , y, idAtrakce)
                || evidence.mapaAtrakciAChodniku.jeAtrakce(x - Program.sizeOfSquare, y, idAtrakce)
                || evidence.mapaAtrakciAChodniku.jeAtrakce(x, y + Program.sizeOfSquare, idAtrakce)
                || evidence.mapaAtrakciAChodniku.jeAtrakce(x, y - Program.sizeOfSquare, idAtrakce)
                )               
                return true;
            else return false;
        }

        //--------spusteni hry, kliknuti na tlacitko Start
        private void Start_button1_Click(object sender, EventArgs e)
        {
            StartPanel2.Visible = false;
            pictureBox1.Visible = true;
            evidence = new Evidence(sirka, vyska);
            pocetLidiCislo_label.Text = "0";
            evidence.mapaAtrakciAChodniku = new Mapa(sirka, vyska);
            VytvorMapu(sirka, vyska);
            HraciPanel.Visible = true;       
            timerLide.Enabled = true;
            timerAtrakce.Enabled = true;
           

            
        }

        //-----pocatecni nastaveni rozmeru mapy---------------------------------

        private void radioButton20_CheckedChanged(object sender, EventArgs e) //nejspise zbytecna - predzaskrtnute
        {
            sirka = 12; //kvuli 2 okrajovym polickum
            vyska = 12; //
        }

        private void radioButton40_CheckedChanged(object sender, EventArgs e)
        {
            sirka = 22;
            vyska = 12;
        }

        private void radioButton60_CheckedChanged(object sender, EventArgs e)
        {
            sirka = 17; 
            vyska = 17;
        }

       
        //-----kliknuti na tlacitko Konec----------------------------------------
        private void Konec_button1_Click(object sender, EventArgs e)
        {
            if (chodnikFormOtevren)
            {
                chodnikFormOtevren = false;
                chodnikyFormular.Hide();
            }
            if (atrakceFormOtevren)
            {
                atrakceFormOtevren = false;
                atrakceFormular.Hide();
            }
            rozhodovaciFormular = new RozhodovaciForm(ref chodnikFormOtevren);
            DialogResult drRozhForm = rozhodovaciFormular.ShowDialog();
            timerLide.Enabled = false;
            timerAtrakce.Enabled = false;
                

            if (drRozhForm == DialogResult.Yes)
            {
                switch (rozhodovaciFormular.odkliknuto)
                {
                    case RozhodovaciForm.tlacitka.Pokracovat:
                        timerLide.Enabled = true;
                        timerAtrakce.Enabled = true;
                        break;
                    case RozhodovaciForm.tlacitka.Konec: //zde nutno ukoncit vsechny formulare
                        
                        chodnikyFormular.Dispose();
                        atrakceFormular.Dispose();
                        clovekFormular.Dispose();
                        rozhodovaciFormular.Dispose();
                        Application.Exit();
                        break;
                    case RozhodovaciForm.tlacitka.NovaHra:
                        DialogResult dotaz = MessageBox.Show("Chcete opravdu skončit rozehranou hru?", "Varování", MessageBoxButtons.YesNo);

                        if (dotaz == DialogResult.Yes)
                        {
                            inicializaceNovaHra();
                        }
                        else rozhodovaciFormular.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }

        //-----priprava na novou hru po potvrzeni skonceni aktualni-------------------------
        private void inicializaceNovaHra()
        {
            pictureBox1.Visible = false;
            HraciPanel.Visible = false;
            StartPanel2.Visible = true;
            chodnikFormOtevren = false;
            atrakceFormOtevren = false;
            zbor = false;
            zbor_button.Text = "ZBOŘ";
            pocetLidiCislo_label.Text = "0";
            pocetPenez_label.Text = "20000";
            evidence.mapaAtrakciAChodniku.destruct();
            plot.Destruct();           
            brana.Destruct();
            evidence.Destruct();
            
            
            



        }

        private void Chodnik_button_Click(object sender, EventArgs e)
        {
            if (chodnikFormOtevren)
            {
                chodnikFormOtevren = false;
                chodnikyFormular.Hide();
            }
            else
            {
                chodnikyFormular.Show();
                chodnikyFormular.Left = Left + 200;
                chodnikyFormular.Top = Top + 70;
                chodnikFormOtevren = true;
            }
        }

        private void atrakce_button_Click(object sender, EventArgs e)
        {
            if (atrakceFormOtevren)
            {
                atrakceFormOtevren = false;
                atrakceFormular.Hide();
            }
            else 
            {
                atrakceFormular.Show();
                atrakceFormular.Left = Left + 300;
                atrakceFormular.Top = Top + 70;
                atrakceFormOtevren = true;
            }
            
        }

        public string text;
        
        //timer pro chuzi lidi
        private void timerLide_Tick(object sender, EventArgs e) 
        {
            //1000 = 1s             
            evidence.lideLSS.VsemAkce();          
        }

        //timer pro naber lidi na atrakce, dobu toceni apod
        private void timerAtrakce_Tick(object sender, EventArgs e) 
        {
            //akce vsem atrakcim, vcetne brany
            evidence.atrakceLSS.VsemAkce();
            //zjisteni, zda uzivatel aktualne plati za propagaci, pocet casu venovanemu propagaci ovlivnuje pst vyroby cloveka
            if (evidence.propagace)
            {
                evidence.propagaceCislo++;
                evidence.pocetPenez --;
                pocetPenez_label.Text = evidence.pocetPenez.ToString();
            }
            else evidence.propagaceCislo--;
            //zjisteni, zda uzivatel aktualne plati za vyzkum a provedeni dusledku
            if (evidence.vyzkum)
            {
                evidence.vyzkoumano++;
                evidence.pocetPenez -= 2;
                pocetPenez_label.Text = evidence.pocetPenez.ToString();
                if (evidence.vyzkoumano == 300 && evidence.poprveZelen)//5min, do moznosti pribyde strom
                {
                    evidence.poprveZelen = false;
                    MessageBox.Show("Byla vyzkoumána výsadba stromů.","Informace.",MessageBoxButtons.OK);
                    chodnikyFormular.strom_panel.Visible = true;
                }
                else if (evidence.vyzkoumano == 600 && evidence.poprveLod)//10min, do moznosti atrakci pribyde lod
                {
                    evidence.poprveLod = false;
                    MessageBox.Show("Byla vyzkoumána atrakce Houpací loď.", "Informace.", MessageBoxButtons.OK);
                    atrakceFormular.houpacka_button.Visible = true;
                    
                }
                else if (evidence.vyzkoumano == 900 && evidence.poprveFontana)//15min, do moznosti Cesty pribyde fontana
                {
                    evidence.poprveFontana = false;
                    MessageBox.Show("Byla vyzkoumána stavba fontány.","Informace.",MessageBoxButtons.OK);
                    chodnikyFormular.fontana_panel.Visible = true;
                }
                
                
            }
        }

        //metoda osetrujici stisk tlacitka Zbor
        private void zbor_button_Click(object sender, EventArgs e)
        {
            if (zbor) 
            {
                if (vybranoStavit == stavba.aVstup || vybranoStavit == stavba.aVystup) //nelze manipulovat s tlacitkem, pokud se musi dostavit atrakce (neni postaven vstup nebo vystup)
                    MessageBox.Show("Nejprve je třeba dokončit stavbu atrakce.", "Upozornění", MessageBoxButtons.OK);
                else //vypnuti bourani
                {
                    zbor = false;
                    zbor_button.Text = "ZBOŘ";
                    vybranoStavit = stavba.nic;
                }
            }
            else
            {
                if (vybranoStavit == stavba.aVstup || vybranoStavit == stavba.aVystup)//nelze manipulovat s tlacitkem, pokud se musi dostavit atrakce (neni postaven vstup nebo vystup
                    MessageBox.Show("Nejprve je třeba dokončit stavbu atrakce.", "Upozornění", MessageBoxButtons.OK);
                else //priprava na bourani
                {
                    vybranoStavit = stavba.nic;
                    zbor = true;
                    zbor_button.Text = "BOŘÍM";
                }
            }
        }

    //pro ukonceni aplikace - at tlacitkem konec nebo krizkem
    private void Hlavni_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dotaz = MessageBox.Show("Chcete opravdu skončit aplikaci?", "Varování", MessageBoxButtons.YesNo);
            if (dotaz == DialogResult.No) e.Cancel = true;
            
        }

    private void propagace_button_Click(object sender, EventArgs e)
    {
        if (evidence.propagace)
        {
            evidence.propagace = false;
            propagace_button.Text = "PROPAGACE";
        }
        else
        {
            evidence.propagace = true;
            propagace_button.Text = "PROPAGUJI";
        }
    }

    private void vyzkum_button_Click(object sender, EventArgs e)
    {
        if (evidence.vyzkum)
        {
            evidence.vyzkum = false;
            vyzkum_button.Text = "VÝZKUM";
        }
        else
        {
            evidence.vyzkum = true;
            vyzkum_button.Text = "VYNALÉZÁM";
        }
    }

       

        
    }

   


}
