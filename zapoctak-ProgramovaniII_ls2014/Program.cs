using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace zapoctak_ProgramovaniII_ls2014
{    
    public enum Direction {N,S,W,E,no};//smer
     
    static class Program
    {
        
        public const int pricePStones = 190;
        public const int pricePAsphalt = 200;
        public const int pricePSand = 195;
        public const int pricePMarble = 250; //mramor
        public const int priceCarousel = 7500;//kolotoč
        public const int priceShip = 15000;//loď
        public const int priceRestaurant = 5000;//občerstvení
        public const int priceFountain = 750;//fontána
        public const int priceTree = 150;
        public const int sizeOfSquare = 50;//rozměr políčka
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Hlavni_Form());

        }
        //funkce rozhodujici o vyrobe cloveka
        public static bool pstniFceVyroby(Random rnd, int attractionCount, int celkovaLakavost, int pocetLidi, int vstupne, int propagace)
        {
            if (attractionCount == 1) return false; //pokud neni v parku zadna atrakce - 1 nebot se pocita i brana do poctu atrakci
            int cislo = rnd.Next(1, 1001);//na procenta
            int a;
            if (attractionCount <= 10) a = attractionCount * (90 - (pocetLidi / 10) * 10);
            else a = (90 - (pocetLidi / 10) * 10); //ZDE CHYBA, BYLO PROHOZEN ROZDIL!!!
            int b = Math.Min( celkovaLakavost, 700); //minimalne 100
            int c = Math.Max(Math.Min(propagace, 100), 0); //vstupne je omezeno hranici 100, tj. maximalne
           
            if ((cislo + a + b - (vstupne ^ 2)/50) + c > 1000) return true; 
            else return false;
           
        }
        
    }

    static class UrceniSmeruCesty ///spocte vzdalenost vsech chodniku k dane atrakci
       
    {
        
        //spocte vzdalenost od vstupu dane atrakce ke vsem chodnikum, vlna, a vsem chodnikum nastavi smer nejkratsi cesty k atrakci
        public static void spoctiVzdalenostOdAtrakce(int idAtrakce, int xAtrakce, int yAtrakce, int vyska, int sirka, IntSmer[,] JsouChodniky, Mapa mapaChodniku)
        { 
            int sxA = xAtrakce / Program.sizeOfSquare; //indexy pocatecniho policka-vstupu atrakce
            int syA = yAtrakce / Program.sizeOfSquare;
            Fronta<PrvekFronty> fronta = new Fronta<PrvekFronty>(sirka*vyska+5);
            PocatecniVlozeni(sxA, syA, fronta, JsouChodniky); //kvuli brane - ta muze mit sousedni policko mimo mapu
            while (!fronta.Prazdna())
            {
                PrvekFronty prvek = fronta.VratPrvek();
                //prohlednuti sousedu a prip. pridani do fronty
                //okrajova policka nevadi, nebot na okrajich je plot, tj -1
                //Smer je zrcadlove, z pohledu cloveka je to totiz obracene, tj. z pohledu cloveka takto spravne
                if (NeighbourAction(JsouChodniky, prvek.sx, prvek.sy + 1, prvek.pocetKroku, fronta))
                    JsouChodniky[prvek.sx, prvek.sy + 1].smer = Direction.N; 
                if (NeighbourAction(JsouChodniky, prvek.sx, prvek.sy - 1, prvek.pocetKroku, fronta))
                    JsouChodniky[prvek.sx, prvek.sy - 1].smer = Direction.S;
                if (NeighbourAction(JsouChodniky, prvek.sx + 1, prvek.sy, prvek.pocetKroku, fronta))
                    JsouChodniky[prvek.sx + 1, prvek.sy].smer = Direction.W;
                if ((prvek.sx - 1 >= 0) && NeighbourAction(JsouChodniky, prvek.sx - 1, prvek.sy, prvek.pocetKroku, fronta))
                    JsouChodniky[prvek.sx - 1, prvek.sy].smer = Direction.E;
            }
            ZaznamenejDoMapy(vyska, sirka, idAtrakce, JsouChodniky, mapaChodniku);           
            NastavHodnotyZpet(vyska, sirka, JsouChodniky, xAtrakce, yAtrakce);
        }
                   
            //vlozi do fronty sousedy startovniho policka (vstup atrakce), v pripade vseho jineho nez brany jsou az 4, v pripade brany prave 1
        private static void PocatecniVlozeni(int sxA, int syA, Fronta<PrvekFronty> fronta, IntSmer[,] JsouChodniky)
        {
            JsouChodniky[sxA, syA].cislo = 0; 

            if ((sxA - 1 >= 0) && (NeighbourAction(JsouChodniky, sxA - 1, syA, 1, fronta))) //&& funguje tak, ze se nevyhodnocuje druha cast podminky, pokud je prvni nesplnena, tj. nevadi takovyto zapis
            {
                JsouChodniky[sxA - 1, syA].smer = Direction.E;
                
            }
            if (NeighbourAction(JsouChodniky, sxA, syA - 1, 1, fronta))
            {
                JsouChodniky[sxA, syA - 1].smer = Direction.S;
                
            }
            if (NeighbourAction(JsouChodniky, sxA, syA + 1, 1, fronta))
            {
                JsouChodniky[sxA, syA + 1].smer = Direction.N;
                
            }
            if (NeighbourAction(JsouChodniky, sxA + 1, syA, 1, fronta))
            {
                JsouChodniky[sxA + 1, syA].smer = Direction.W;
                
            }
        }

        

        private static bool NeighbourAction(IntSmer[,] JsouChodniky, int i, int j, int pocetKroku, Fronta<PrvekFronty> fronta)
        {
            if (JsouChodniky[i, j].cislo == int.MaxValue) //tzn. je zde chodnik (kdyby nebyl, je -1) a nebyl jeste projit
            {
                PrvekFronty objekt = new PrvekFronty(i, j, pocetKroku + 1);
                fronta.Vloz(objekt);
                JsouChodniky[i, j].cislo = pocetKroku;
                return true;
            }
            else return false;
        }
        //prochazi pole a zaznamenat do mapy chodniku a atrakci
        private static void ZaznamenejDoMapy(int vyska, int sirka, int id, IntSmer[,] JsouChodniky, Mapa MapaChodniku)
        {
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < vyska; j++)
                {
                    MapaChodniku.ulozSmer(i, j, id, JsouChodniky[i, j].smer);
                }
            }
        }
        //upravuje zpet pole
        private static void NastavHodnotyZpet(int vyska, int sirka, IntSmer[,] JsouChodniky, int xAtrakce, int yAtrakce)
        {
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < vyska; j++)
                {
                    if (JsouChodniky[i, j].cislo >= 0)//resp. je !=-1
                    {
                        JsouChodniky[i, j].cislo = int.MaxValue;
                        JsouChodniky[i, j].smer = Direction.no; //dulezite, pokud bych nenastavila, tak napr. pokud k dalsi atrakci nevede cesta, zustal by nastaven smer z predchozi - proto chodili jen na nejnovejsi
                    }
                }
            }
         }
       
    }


    
    //----------trida pro policko na mape--------------------------------------
    
    abstract public class Policko 
    {
        protected Hlavni_Form hlform;
        public PictureBox pbox;
        

        public Policko() { }
        
        public Policko(Hlavni_Form form)
        {
            hlform = form;
        }

        public Policko(int x, int y, Hlavni_Form form)
        {
            hlform = form;
            Umisti(x,y);
            pbox.Click += new EventHandler(Click);
            form.evidence.mapaAtrakciAChodniku.pridej(x,y,this);
            pbox.Parent = hlform.pictureBox1;            
        }

        
        public void Umisti(int x, int y)
        {
            pbox = new PictureBox();
            pbox.Left=x+1;
            pbox.Top=y+1;
            pbox.Width=Program.sizeOfSquare-1;
            pbox.Height = Program.sizeOfSquare-1;
            pbox.Visible = true;
            pbox.Parent = hlform.pictureBox1;
           
            
        }
        protected virtual void Click(object sender, EventArgs e)
        {
            if (hlform.zbor)
            {
                
                Zbor(e); //Destruct(pbox.Left, pbox.Top);
                VratCenu();
            }

            else HlaskaPoKliknuti();
            
        }
        public virtual void HlaskaPoKliknuti()
        { }
        public virtual void chUlozSmer(int id, Direction smer) //kvuli chodnikum a moznosti volat z mapyPolicek
        {
        }
        protected virtual void VratCenu()
        { }
        
        public virtual void Zbor(EventArgs e)
        {
           
            VratCenu();
            this.Destruct(pbox.Left, pbox.Top); 
        }

        public virtual void Destruct(int x, int y)
        {
                       
        }
       
    }
    
    abstract class Chodnik : Policko
   { 
        
        public Direction[] smerovka;
        public Chodnik(int x, int y, Hlavni_Form form) 
            : base (x, y, form)
        {
            form.pictureBox1.Controls.SetChildIndex(pbox,10); //kvuli poradi v pozadi, 10 nejdale        
            
            smerovka = new Direction[form.evidence.maxPocetAtrakci + 1];//0 je brana, ostatni cisla zbytek
            for (int i = 0; i < form.evidence.maxPocetAtrakci+1; i++)
            {
                smerovka[i] = Direction.no;
            }
            form.evidence.PridejChodnikDoIntMapy(x, y); // v mape chodniku nastavi MAXINT, tzn. "na tomto miste je chodnik"           
            form.evidence.atrakceLSS.NastavVzdalenostVsemAtrakcim(form); 
            
        }

        public override void chUlozSmer(int id, Direction smer)
        {
            smerovka[id] = smer;
        }

        public Direction chVratSmer(int id)
        {
            return smerovka[id];
        }

        public override void Destruct(int x, int y)
        {
            pbox.Dispose();
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x,y); // nastavi null na toto misto v mape - odebiram policko z Pole, kde jsou ulozena vsechna policka
            hlform.evidence.SmazChodnikZMapyChodnikuBezAktualizaceSmeru(x, y); //v intMapeChodniku nastavi -1, tj. chodnik zde neni
            
            LSSAtrakce pom = hlform.evidence.atrakceLSS as LSSAtrakce; //zaktualizuje smery u chodniku
            pom.NastavVzdalenostVsemAtrakcim(hlform);
            
        }
        public void DestructBezAktualizaceSmeru(int x, int y)
        {
            pbox.Dispose();
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x, y); // odebiram policko z Pole, kde jsou ulozena vsechna policka
            hlform.evidence.SmazChodnikZMapyChodnikuBezAktualizaceSmeru(x, y); //v intMapeChodniku nastavi -1, tj. chodnik zde neni       
        }
        

        
   }

    class KamennyChodnik : Chodnik
    {
        public KamennyChodnik(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            pbox.BackgroundImage = Properties.Resources.chodnik_sterk_spravny;
            hlform.evidence.pocetPenez -= Program.pricePStones;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }

        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.pricePStones); //vraci se 70 % z kupni ceny
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
       
    }

    class AsfaltChodnik : Chodnik
    {
        public AsfaltChodnik(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            pbox.BackgroundImage = Properties.Resources.chodnik_asfalt;
            hlform.evidence.pocetPenez -= Program.pricePAsphalt;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
       
        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.pricePAsphalt);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
        
    }

    class PisekChodnik : Chodnik
    {
        public PisekChodnik(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            pbox.BackgroundImage = Properties.Resources.chodnik_pisek;

            hlform.evidence.pocetPenez -= Program.pricePSand;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.pricePSand);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
       
    }
    
    class MramorChodnik : Chodnik
    {
        public MramorChodnik(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            pbox.BackgroundImage = Properties.Resources.chodnik_mramor;
            
            hlform.evidence.pocetPenez -= Program.pricePMarble;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
            hlform.evidence.lakavost += 2;
        }
        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.pricePMarble);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
        
    }

    class AtrakceVstupChodnik : Chodnik 
    {      
        Atrakce atrakceKNizPatri;
        public AtrakceVstupChodnik (int x, int y, Hlavni_Form form, Atrakce atr)
            : base(x, y, form)
        {
            pbox.BackColor = Color.Blue;
             form.evidence.PridejChodnikDoIntMapy(x, y); //NEMELO BY ZDE BYT, JE VE ZDEDENEM KONSTRUKTORU,ALE PROC NEFUNGUJE??? 
            atrakceKNizPatri = atr;
        }
        protected override void Click(object sender, EventArgs e)
        {
            if (hlform.zbor)
            {
                if (atrakceKNizPatri.MimoProvoz())
                {
                    if (hlform.vybranoStavit == Hlavni_Form.stavba.aVystup)
                        MessageBox.Show("Nejprve je třeba dokončit stavbu atrakce.", "Upozornění", MessageBoxButtons.OK);
                    else
                    {
                        
                        Zbor(e);//Destruct(pbox.Left, pbox.Top);
                        hlform.vybranoStavit = Hlavni_Form.stavba.aVstup; //v tomto se lisi od funkce predka
                        
                        hlform.atrakce = atrakceKNizPatri;
                        hlform.idAktAtrakce = atrakceKNizPatri.id;
                    }
                }
                else MessageBox.Show("Nelze stavit, pokud není atrakce odstavena z provozu.","Upozornění",MessageBoxButtons.OK);
            }
            else
            {
                HlaskaPoKliknuti();
            }
        }
        public override void HlaskaPoKliknuti()
        {
            MessageBox.Show("Počet lidí ve frontě: " + atrakceKNizPatri.PocetLidiVeFronte(), "Informace", MessageBoxButtons.OK);
        }
    }

    class AtrakceVystupChodnik : Chodnik
    {
        Atrakce atrakceKNizPatri;
        public AtrakceVystupChodnik (int x, int y, Hlavni_Form form, Atrakce atr)
            : base(x, y, form)
        {
            pbox.BackColor = Color.Red;          
            atrakceKNizPatri = atr;
            if (!hlform.zbor)
            {
                atrakceKNizPatri.HlaskaPoKliknuti();
            }
            atrakceKNizPatri.zacatek = false;
        }
        protected override void Click(object sender, EventArgs e)
        {
            if (hlform.zbor)
            {
                if (atrakceKNizPatri.MimoProvoz())
                {
                    if (hlform.vybranoStavit == Hlavni_Form.stavba.aVstup)
                        MessageBox.Show("Nejprve je třeba dokončit stavbu atrakce.", "Upozornění", MessageBoxButtons.OK);
                    else
                    {
                        //Destruct(pbox.Left, pbox.Top);
                        Zbor(e);
                        hlform.vybranoStavit = Hlavni_Form.stavba.aVystup; //v tomto se lisi od funkce predka
                        hlform.atrakce = atrakceKNizPatri;
                        hlform.idAktAtrakce = atrakceKNizPatri.id;
                    }
                }
                else MessageBox.Show("Nelze stavit, pokud není atrakce odstavena z provozu.", "Upozornění", MessageBoxButtons.OK);
            }
            else HlaskaPoKliknuti();
        }
        
    } 
   
    public class Plot : Policko 
    {
        public PictureBox plotS;
        public PictureBox plotJ;
        public  PictureBox plotV;
        public  PictureBox plotZ;
        
        public Plot(Hlavni_Form form): base(form)
        {
            int rozmer = Program.sizeOfSquare;
            
            plotS = new PictureBox();
            plotJ = new PictureBox();
            plotV = new PictureBox();
            plotZ = new PictureBox();
                     
            plotS.Height = rozmer;
            plotS.Width = hlform.sirka * rozmer; 
            plotS.Top = hlform.pictureBox1.Top;
            plotS.Left = hlform.pictureBox1.Left;
            plotS.Parent = hlform.pictureBox1;
            plotS.BackgroundImage = Properties.Resources.kere; //implicitne nastaveno: BackgroundImageLayout=Tile, tzn.maly obrazek zopakuje

            
            plotJ.Height = rozmer;
            plotJ.Width = hlform.sirka * rozmer;
            plotJ.Top = hlform.pictureBox1.Bottom - rozmer + 1;
            plotJ.Left = hlform.pictureBox1.Left;
            plotJ.Parent = hlform.pictureBox1;
            plotJ.BackgroundImage = Properties.Resources.kere;

            plotZ.Height = (hlform.vyska - 2) * rozmer + 1;
            plotZ.Width = rozmer;
            plotZ.Top = hlform.pictureBox1.Top + rozmer;
            plotZ.Left = hlform.pictureBox1.Left;
            plotZ.Parent = hlform.pictureBox1;
            plotZ.BackgroundImage = Properties.Resources.kere;

            plotV.Height = (hlform.vyska - 2) * rozmer + 1;
            plotV.Width = rozmer;
            plotV.Top = hlform.pictureBox1.Top + rozmer;
            plotV.Left = hlform.pictureBox1.Right - rozmer + 1;
            plotV.Parent = hlform.pictureBox1;
            plotV.BackgroundImage = Properties.Resources.kere;        
        }

        protected override void Click(object sender, EventArgs e)
        {
            //nic nema delat
        }

        public void Destruct() //pouziva se jen na konci, tj. neni nutne odstranovat z mapy, ta se ve stejne chvili bude nullovat
        {
            plotS.Dispose();
            plotJ.Dispose();
            plotV.Dispose();
            plotZ.Dispose();    
        }
    
    }

    public class Zelen: Policko
    {
        public Zelen(int x, int y, Hlavni_Form form) 
        {
            hlform = form;
            Umisti(x,y);
            pbox.Click += new EventHandler(Click);
            pbox.Parent = hlform.pictureBox1;
            form.pictureBox1.Controls.SetChildIndex(pbox, 0);
           
            form.evidence.pocetPenez -= Program.priceTree;
            form.pocetPenez_label.Text = form.evidence.pocetPenez.ToString();
           
            hlform.evidence.zelen++;
            if (hlform.evidence.poprveZelen && hlform.evidence.zelen == hlform.sirka * hlform.vyska / 3)
            {
                hlform.evidence.poprveZelen = false;
                MessageBox.Show("Gratulujeme! Obdrželi jste ocenění Zelené přírody. Získáváte 10000. ", "Odměna", MessageBoxButtons.OK);
                form.evidence.pocetPenez += 10000;
                form.pocetPenez_label.Text = form.evidence.pocetPenez.ToString();
                form.chodnikyFormular.fontana_panel.Visible = true;
            }
            
            hlform.evidence.ostatniLSS.VlozNaKonec(this);
        }
        public new void Umisti(int x, int y)
        {
            pbox = new PictureBox();
            pbox.BackgroundImage = Properties.Resources.strom;
            pbox.BackgroundImageLayout = ImageLayout.Stretch;
            pbox.Width = 15;
            pbox.Height = 25;
            pbox.Left = x;
            pbox.Top = y - pbox.Height + 9; //+9 kvuli tomu, ze se souradnice predavaji na celych desitkach, tak aby se strom objevil, kam uzivatel klikne
            pbox.Visible = true;
        }

        public override void Destruct(int x, int y)
        {
            hlform.evidence.zelen--;
            pbox.Dispose();
        }

    } 
    public abstract class Atrakce : Policko
    {
        //public abstract int cenaKolotoce {public get; protected set;} 
        //todo: neco ve stylu vyse chci
        public enum Stav { cekaNaLidi, bezi, mimoProvoz, dobiha}
        public int id, vstupne, puvodniVstupne, lakavost;
        protected int kapacita, dobaToceni, maxDobaCekani, konecPole;
        private int casBehu, casCekani;
        private bool dobiha, bezi;
        public bool zacatek;
        public int vstupX, vstupY, vystupX, vystupY;
        public bool mimoProvoz; //slouzi k tomu, zda je atrakce viditelna pro lidi, nebo ne
        protected Stav stav;
        public Atrakce() { }
        protected LSSClovek LSSfrontyLidi;
        protected Clovek[] LideNaAtrakci;
        public KlikNaAtrakciForm klikForm;
        
        public Atrakce(int x, int y, Hlavni_Form form) : base(x, y, form)
        {
            pbox.BackColor = hlform.barvaAtrakce;
            form.pictureBox1.Controls.SetChildIndex(pbox,1);
            id = form.evidence.vratVolneID();//id musi byt nastaveno pred vlozenim do LSS
            form.evidence.atrakceLSS.VlozNaKonec(this);
            LSSfrontyLidi = new LSSClovek(0);//seznam s hlavou-ta ma id=0;
            //zde zvysuji pocet atrakci, ackoli neni cela postavena
            hlform.evidence.pocetAtrakci++;
            
            konecPole = 0;
            casBehu = 0;
            casCekani = 0;
            mimoProvoz = true;
            klikForm = new KlikNaAtrakciForm(this, (LSSAtrakce)form.evidence.atrakceLSS, form);
            dobiha = false;
            bezi = false;
            zacatek = true;
            stav = Stav.mimoProvoz;
           
            
        }
        public int PocetLidiVeFronte()
        {
            return LSSfrontyLidi.PocetUzlu(); //mozna lepsi pocet uzlu u LSSfrontyLidi public, ale aspon ho nikde nezmenim
        }
        public bool MimoProvoz()
        {
            if (stav == Stav.mimoProvoz)
                return true;
            else return false;
        }
        protected void PridejDoMapy(int x, int y, int delkax, int delkay)
        {
            for (int i = x; i < x + Program.sizeOfSquare * delkax; i += Program.sizeOfSquare)
            {
                for (int j = y; j < y + Program.sizeOfSquare * delkay; j += Program.sizeOfSquare)
                {
                    hlform.evidence.mapaAtrakciAChodniku.pridej(i, j, this);
                }
            }
        }
        public virtual void Akce()
        {
            int pom;
            switch (stav)
            {
                case Stav.cekaNaLidi:
                    {
                        pom = PocetLidiVeFronte();
                        if (pom >= 0.8 * kapacita || (casCekani >= maxDobaCekani && pom > 0))
                        {
                            casCekani = 0;
                            casBehu = 0; //aktualni doba toceni atrakce
                            pom = Math.Min(pom, kapacita); 
                            nalozLidi(pom); //vlozi lidi z fronty do atrakce, zmeni jim stav na VAtrakci a zneviditelni je
                            stav = Stav.bezi;
                            bezi = true;
                        }
                        else casCekani++;
                        hlform.evidence.pocetPenez -= 7; //provozni cena
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    break;
                case Stav.bezi:
                    {
                        if (casBehu >= dobaToceni)
                        {
                            vylozLidi();
                            if (dobiha) stav = Stav.dobiha;
                            else stav = Stav.cekaNaLidi;
                            bezi = false;
                            casBehu = 0;
                        }
                        else casBehu++;
                        hlform.evidence.pocetPenez -= 15; //provozni cena
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    break;
                case Stav.dobiha:
                    {
                        
                        if (PocetLidiVeFronte() == 0 && (!bezi)) //!bezi-aby se nezmenilo, kdyz uzivatel klikne uprostred behu
                        {
                            stav = Stav.mimoProvoz;
                            klikForm.mimoProvoz_button.Text = "Mimo provoz";
                            klikForm.mimoProvoz_button.BackColor = Color.Red;
                            dobiha = false;
                        }
                        else
                        {
                            dobiha = true;
                            if (bezi) stav = Stav.bezi;
                            else stav = Stav.cekaNaLidi;
                        }
                        hlform.evidence.pocetPenez -= 7; //provozni cena
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    break;
                case Stav.mimoProvoz: //nic se nedela
                    break;
                default:
                    break;
            }
        
        
        
        
        }

        public void zmenStav(Stav st)
        {
            this.stav = st;
        }
        public void vylozLidi()
        {
            Clovek clovek;
            for (int i = 0; i < konecPole; i++)
            {
                clovek = LideNaAtrakci[i];
                clovek.ZmenStav(Clovek.Stav.vybiraAtrakci);
                clovek.Premisti(vystupX,vystupY);
                clovek.Zviditelni();
            }
            konecPole = 0;
        }
        public void pridejDoFronty( Clovek clovek)
        {
            LSSfrontyLidi.VlozNaKonec(clovek);
        }
        public void smazZFrontyID(int idCloveka)
        {
            LSSfrontyLidi.NajdiASmaz(idCloveka);
        }

        public void nalozLidi(int n)
        {
            //Atrakce atrakce = hlform.evidence.atrakceLSS.VratClenSId(idAtrakce);
            Clovek clovek;
            for (int i = 1; i <= n; i++)
            {
                //clovek=LSSfrontyLidi.VratNtyPrvekASmazHo(1);//1 nebot kdyz prvek smazu, dalsi bude zase prvni
                clovek = LSSfrontyLidi.VratPrvniPrvekASmazHo();
                if (clovek != null) //melo by jit jen o zbytecne ujisteni
                {
                    clovek.ZmenStav(Clovek.Stav.naAtrakci); //jeste treba tohoto cloveka strcit do nejakeho pole atrakce, aby nekde byl behem jizdy, a zaroven jeho vzhled.visible=false;
                    clovek.Zneviditelni();
                    clovek.pocetPenez -= this.vstupne;
                    hlform.evidence.pocetPenez += this.vstupne;
                    hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    LideNaAtrakci[konecPole] = clovek;
                    konecPole++;
                }
            }
        }

      
        protected override void Click(object sender, EventArgs e)
        {
            if (!zacatek)
            {
                if (hlform.zbor)
                {
                    if (stav == Stav.mimoProvoz)
                    {
                        Zbor(e);
                        hlform.vybranoStavit = Hlavni_Form.stavba.nic; //v tomto se lisi od funkce predka; v pripade, ze se nejprve rusil vstup a pak atrakce
                    }
                    else MessageBox.Show("Nelze zbořit, dokud atrakce běží.", "Upozornění", MessageBoxButtons.OK);

                }
                else
                {
                    HlaskaPoKliknuti();
                }
            }
        }
        
        public override void Destruct(int x, int y)
        {
            hlform.vybranoStavit = Hlavni_Form.stavba.nic;//kvuli rucnimu boreni, aby nezustala stavba vstupu nebo vystupu
            hlform.evidence.atrakceLSS.NajdiASmaz(this.id);
            hlform.evidence.pocetAtrakci--; //snizuji pocetAtrakci zde, jinak se snizuje v destrukt vystupu, ale ten se nevola
            hlform.evidence.uvolniID(this.id);
            pbox.Dispose();
            //do smerovek k tomuto id nastavit smer.zadny
            hlform.evidence.mapaAtrakciAChodniku.zrusPristupKAtrakci(this.id);
            // odebere z mapy vstup a vystup a zavola na ne DestructBezAktualizaceSmeru
            hlform.evidence.mapaAtrakciAChodniku.odeber(this.vstupX, this.vstupY); 
            hlform.evidence.mapaAtrakciAChodniku.odeber(this.vystupX, this.vystupY);
            LSSAtrakce pom = hlform.evidence.atrakceLSS as LSSAtrakce; //zaktualizuje smery u chodniku
            pom.NastavVzdalenostVsemAtrakcim(hlform);
                        
        }

        
    }

    class DetskyKolotoc : Atrakce
    {
        public DetskyKolotoc(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            //detsky kolotoc zabira 1 velke policko velikosti 2x2
            pbox.BackgroundImage = Properties.Resources.kolotoc;
            pbox.Width = 2 * Program.sizeOfSquare - 1;//aby zustaly okrajove cary, proto o 1 mensi
            pbox.Height = 2 * Program.sizeOfSquare - 1;
            PridejDoMapy(x, y, 2, 2);
            kapacita = 5;
            dobaToceni = 4; //timer ma interval 1000=1s
            maxDobaCekani = 15;
            LideNaAtrakci=new Clovek[kapacita];
            vstupne = 100;
            puvodniVstupne = 100;
            lakavost = 25;
            hlform.evidence.lakavost += lakavost;
            
            hlform.evidence.minimalniCena = Math.Min(this.vstupne, hlform.evidence.minimalniCena);
            
           
           
        }

        public override void HlaskaPoKliknuti()
        {
            klikForm.Text = "Dětský kolotoč";
            klikForm.info_label.Text = "kapacita: "+this.kapacita;//sem se pak ukladaji veskere informace
            klikForm.cenaVstupneho_label1.Text = "vstupné: ";
            klikForm.vstupne_numericUpDown1.Value = this.vstupne;
            klikForm.ShowDialog();                   
         }
        protected override void VratCenu()
        {
           
           hlform.evidence.pocetPenez += (int)(0.7 * Program.priceCarousel);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
        public override void Destruct(int x, int y) 
        {
            
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x,y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + Program.sizeOfSquare, y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x, y + Program.sizeOfSquare);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + Program.sizeOfSquare, y + Program.sizeOfSquare);
            base.Destruct(x, y);
            
            
        }
        
    }
    class HoupaciLod : Atrakce
    {     
        public HoupaciLod(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            //houpaci lod zabira 1*5 policek (kdybych chtela volit smer, nutno zmenit i destruct a v hlavnim formu picturebox.click)
            pbox.Width = 5 * Program.sizeOfSquare - 1;//aby zustaly okrajove cary, proto o 1 mensi
            pbox.Height = Program.sizeOfSquare - 1;
            pbox.BackgroundImage = Properties.Resources.lod2;
            PridejDoMapy(x, y, 5, 1);
            kapacita = 7;
            dobaToceni = 6; //timer ma interval 1000=1s
            maxDobaCekani = 15;
            LideNaAtrakci=new Clovek[kapacita];
            vstupne = 150;
            puvodniVstupne = 150;
            lakavost = 70;
            hlform.evidence.lakavost += lakavost;
            hlform.evidence.pocetPenez -= Program.priceShip;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
            hlform.evidence.minimalniCena = Math.Min(this.vstupne, hlform.evidence.minimalniCena);
            
           
        }

        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.priceShip);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }
        public override void HlaskaPoKliknuti()
        {
            klikForm.Text = "Houpací loď";
            klikForm.info_label.Text = "kapacita: "+this.kapacita;//sem pak ulozit veskere informace
            klikForm.cenaVstupneho_label1.Text = "vstupné: ";
            klikForm.vstupne_numericUpDown1.Value = this.vstupne;
            klikForm.ShowDialog();                      
        }

        
        public override void Destruct(int x, int y) 
        {
            
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x,y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + Program.sizeOfSquare, y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + 2 * Program.sizeOfSquare, y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + 3 * Program.sizeOfSquare, y);
            hlform.evidence.mapaAtrakciAChodniku.vynulluj(x + 4 * Program.sizeOfSquare, y);
            base.Destruct(x, y);
            
            
        }
    
    
    }
    class Obcerstveni : Atrakce
    {
        bool dobiha = false; 
        Chodnik pomChodnik;

        public Obcerstveni(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            //obcerstveni zabira 1 policko
            pbox.Width = Program.sizeOfSquare - 1;//aby zustaly okrajove cary, proto o 1 mensi
            pbox.Height = Program.sizeOfSquare - 1;
            pbox.BackgroundImage = Properties.Resources.obcerstveni;
            PridejDoMapy(x, y, 1, 1);
            kapacita = 1;
            dobaToceni = 0; //timer ma interval 1000=1s
            vstupX = x;
            vystupX = x;
            vstupY = y;
            vystupY = y;
            vstupne = 75;
            puvodniVstupne = 75;
            lakavost = 5;
            hlform.evidence.lakavost += lakavost;
            zacatek = false;//ukladam false, nebot zde se nestavi vstup/vystup
            klikForm.Text = "Občerstvení";
            klikForm.cenaVstupneho_label1.Text = "cena jídla: ";
            klikForm.vstupne_numericUpDown1.Value = this.vstupne;
            klikForm.info_label.Text = "";//sem pak ulozit veskere informace
            klikForm.ShowDialog();
            hlform.evidence.pocetPenez -= Program.priceRestaurant;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
            hlform.evidence.minimalniCena = Math.Min(this.vstupne, hlform.evidence.minimalniCena);
            hlform.evidence.obcerstveniLSS.VlozNaKonec(this);//vklada navic do seznamu obcerstveni
            //umele vyrobeny chodnik, aby lide mohli chodit do obcerstveni a ven, v mape chodniku a atrakci prepisuje atrakci -  nicemu nevadi
            pomChodnik = new KamennyChodnik(x, y, hlform); 
        }

        public override void HlaskaPoKliknuti()
        {
            klikForm.Text = "Občerstvení";
            klikForm.cenaVstupneho_label1.Text = "cena jídla: ";
            klikForm.vstupne_numericUpDown1.Value = this.vstupne;
            klikForm.ShowDialog();
        }

        public override void Akce()
        {
            Clovek clovek; 
            
            switch (stav)
            {
               case Stav.cekaNaLidi: //stav bezi vynechavam, a nechavam cekaNaLidi, protoze ten se aktivuje tlacitkem na atrakci
                    {
                        if (PocetLidiVeFronte() > 0) 
                            {
                                //obsluhuje 1 cloveka 
                                clovek = LSSfrontyLidi.VratPrvniPrvekASmazHo();
                                clovek.pocetPenez -= this.vstupne;
                                clovek.hlad = 0;
                                hlform.evidence.pocetPenez += this.vstupne;
                                hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                                clovek.ZmenStav(Clovek.Stav.vybiraAtrakci);
                                if (dobiha) stav = Stav.dobiha;
                            }
                        hlform.evidence.pocetPenez -= 8; //provozni cena
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    break;
                case Stav.dobiha:
                    {
                        if (PocetLidiVeFronte() == 0) //&& !bezi-neni potreba, kdyz je doba behu 0
                        {
                            stav = Stav.mimoProvoz;
                            klikForm.mimoProvoz_button.Text = "Mimo provoz";
                            klikForm.mimoProvoz_button.BackColor = Color.Red;
                            dobiha = false;
                        }
                        else
                        {
                            stav = Stav.cekaNaLidi;
                            dobiha = true;//pro jistotu
                        }
                        hlform.evidence.pocetPenez -= 5; //provozni cena
                        hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                    }
                    break;
                case Stav.mimoProvoz: //nic se nedela
                    break;
                default:
                    break;
            }
        
        
        
        }
        
        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.priceRestaurant);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }

        public override void Destruct(int x, int y)
        {
            pomChodnik.DestructBezAktualizaceSmeru(vstupX,vstupY); //rusi se chodnik pod atrakci
            hlform.evidence.obcerstveniLSS.NajdiASmaz(this.id);
            base.Destruct(x, y);//rusim atrakci a aktualizuji smery
            
        }
    }

    class Fontana : Atrakce // v nabidce spolu s chodniky
    {
        bool dobiha = false; 
        Chodnik pomChodnik;
        

        public Fontana(int x, int y, Hlavni_Form form)
            : base(x, y, form)
        {
            //fontana zabira 1 policko
            pbox.Width = Program.sizeOfSquare - 1;//aby zustaly okrajove cary, proto o 1 mensi
            pbox.Height = Program.sizeOfSquare - 1;
            pbox.BackgroundImage=Properties.Resources.fontanaMalovani;
            mimoProvoz = false;
            
            PridejDoMapy(x, y, 1, 1);
            kapacita = int.MaxValue;
            dobaToceni = 0; //timer ma interval 1000=1s
           
            vstupX = x;
            vystupX = x;
            vstupY = y;
            vystupY = y;
            vstupne = 0;
            lakavost = 10;
            hlform.evidence.lakavost += lakavost;
           
            zacatek = false;//ukladam false, nebot zde se nestavi vstup/vystup
            
            hlform.evidence.pocetPenez -= Program.priceFountain;
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
            form.pictureBox1.Controls.SetChildIndex(pbox, 3);

            pomChodnik = new KamennyChodnik(x, y, hlform); //pomocny chodnik jako u obcerstveni
            hlform.evidence.ostatniLSS.VlozNaKonec(this);
        }

        protected override void Click(object sender, EventArgs e)
        {
            if (!zacatek)
            {
                if (hlform.zbor)
                {
                    Zbor(e);
                    hlform.vybranoStavit = Hlavni_Form.stavba.nic; 
                }
                //else HlaskaPoKliknuti() neni treba, protoze zadnou hlasku nerika
            }
        }

        public override void Akce()
        {
            Clovek clovek;

            if (PocetLidiVeFronte() > 0)
            {
                //obsluhuje 1 cloveka 
                clovek = LSSfrontyLidi.VratPrvniPrvekASmazHo();
                clovek.hlad = 0;
                clovek.spokojenost = 100;
                clovek.ZmenStav(Clovek.Stav.vybiraAtrakci);
                if (dobiha) stav = Stav.dobiha;
            }
        }

        protected override void VratCenu()
        {
            hlform.evidence.pocetPenez += (int)(0.7 * Program.priceFountain);
            hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
        }

        public override void Destruct(int x, int y)
        {
            pomChodnik.DestructBezAktualizaceSmeru(vstupX, vstupY); //rusi se chodnik pod atrakci
            //hlform.evidence.mapaAtrakciAChodniku.vynulluj(x, y); // 
            base.Destruct(x, y);//rusim atrakci a aktualizuji smery

        }
    }

    //------brana - vstup do parku --------------------------------------------------------------------------
    public class Brana : Atrakce
    {
        PictureBox brana;
        public int x, y;
        public int vstupneDoParku;

        
       
        public Brana(Hlavni_Form form)
        {
            id=0;
            vstupne = 0; // tj. kolik lide zaplati za atrakci, zde brano jako vystup, tj. 0
            vstupneDoParku = 50;
            brana = new PictureBox();
            brana.Click += new EventHandler(Click); //neprebira konstruktor policka, proto zde
            brana.Width = Program.sizeOfSquare;
            brana.Height = 3 * Program.sizeOfSquare;
            x = form.pictureBox1.Left;
            y = form.random.Next(1, form.vyska - 4) * Program.sizeOfSquare;
            brana.Top = y;
            brana.Left = x;
            brana.Parent = form.plot.plotZ;//misto toho by slo: parent=pictureBox1;form.pictureBox1.Controls.SetChildIndex(pbox,1);
            brana.BackColor = Color.Black;
            brana.BackgroundImage = Properties.Resources.brana;
            form.evidence.atrakceLSS.VlozNaKonec(this);
            hlform = form;
            vstupX = x;
            vstupY = y + 2*Program.sizeOfSquare; 
            vystupX = x;
            vystupY = y + 2*Program.sizeOfSquare;
            hlform.evidence.pocetAtrakci++; //je nutno zvysit, nebot neprebira konstruktor atrakce
            LSSfrontyLidi = new LSSClovek(0); //seznam s hlavou s id 0
            klikForm = new KlikNaAtrakciForm(this, (LSSAtrakce)form.evidence.atrakceLSS, form);
        }

        public override void Akce()
        {
            LSSfrontyLidi.SmazVseKromeHlavy(); //maze vsechny lidi, ktere k ni dosli, tj. opousti park
            
            Clovek clovek; //rozhoduje, zda se vyrobi novy clovek
            if (Program.pstniFceVyroby(hlform.random, hlform.evidence.pocetAtrakci, hlform.evidence.lakavost, hlform.evidence.aktualniPocetLidi,vstupneDoParku, hlform.evidence.propagaceCislo) //misto 100 bude spokojenost
                && hlform.evidence.mapaAtrakciAChodniku.jeChodnik(vstupX + Program.sizeOfSquare, vstupY)
                )
            {
                clovek = new Clovek(hlform);
                hlform.evidence.pocetPenez += vstupneDoParku;
                hlform.pocetPenez_label.Text = hlform.evidence.pocetPenez.ToString();
                
            }
                
        }

        protected override void Click(object sender, EventArgs e)
        {
            HlaskaPoKliknuti(); //nelze zborit, proto jen hlaska
        }
        public override void HlaskaPoKliknuti()
        {
            
            klikForm.Text = "Zábavní park";
            klikForm.info_label.Text = "Počet lidí v parku: "+hlform.evidence.aktualniPocetLidi+"\nCelkový počet návštěvníků: "+hlform.evidence.pocetVsechLidi;//sem pak ulozit veskere informace
            klikForm.cenaVstupneho_label1.Text = "vstupné do parku: ";
            klikForm.vstupne_numericUpDown1.Value = this.vstupneDoParku;
            klikForm.mimoProvoz_button.Visible = false;
            klikForm.ShowDialog();
        }
        public void Destruct()
        {
            brana.Dispose(); 
        }
        
        
    }

    //------trida pro evidenci atrakci a zejm. chodniku v mape, potrebna pro chuzi lidi------------------------
    public class Mapa 
    {
        private Policko[,] pole;
        private int sirka, vyska;

        public Mapa(int sir, int vys)
        {
            pole = new Policko[sir, vys];
            sirka = sir;
            vyska = vys;
        }

        public void pridej(int x, int y, Policko policko)
        {
            pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] = policko;
        }

        public void odeber(int x, int y)
        {
            Policko policko;
            policko = pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare];
            
           
            if (policko != null) 
            { 
                Chodnik chodnik = policko as Chodnik;
                if (chodnik != null) chodnik.DestructBezAktualizaceSmeru(x,y);
                else policko.Destruct(x, y); //delim do dvou, protoze soucasti chodniku je aktualizace smeru, kterou zde nechci
            }
               
        }

        public bool prazdne(int x, int y) //overuje, zda je na danych souradnicich prazdno=nic nepostaveno
        {
            if (pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] == null) return true;
            else return false;
        }

        public void vynulluj(int x, int y) //vlozi null do Pole, ktere patri policku o souradnicich x, y
        {
            pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] = null;
        }

        public bool jeChodnik(int x, int y)
        {
            if (pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] is Chodnik) //zda prislusi do tridy Chodnik
                return true;
            else return false;
            }

        public bool jeAtrakce(int x, int y, int id)
        {
            Atrakce pom=pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] as Atrakce;
            if (pom != null && pom.id == id) return true;
            else return false;       
        }
        public Direction vratSmer(int x, int y, int idAtrakce) //vraci smer k atrakci s cislem idAtrakce, ktery je ulozen v chodniku
        {
            Chodnik pom=pole[x / Program.sizeOfSquare, y / Program.sizeOfSquare] as Chodnik; //as je pro bezpecne pretypovani
            if (pom != null)
                return pom.chVratSmer(idAtrakce);
            else return Direction.no; 
        }

        public void ulozSmer(int i, int j, int id, Direction smer)
        { 
            Chodnik pom=pole[i,j] as Chodnik;
            if (pom != null) pom.chUlozSmer(id, smer); 
            
        }

        public void zrusPristupKAtrakci(int idAtrakce) //vsem chodnikum nastavi smer.zadny k dane atrakci - napr. po smazani atrakce nebo vypnuti
        {
            Chodnik pom;
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < vyska; j++)
                {
                   pom = pole[i, j] as Chodnik;
                   if (pom != null) pom.chUlozSmer(idAtrakce, Direction.no);
                }
            }
        
        }

        public void destruct()
        {
            int rozmer = Program.sizeOfSquare;
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < vyska; j++)
                {
                    if (pole[i, j] != null) pole[i, j].Destruct(i * rozmer, j * rozmer);
                }
            }
           
            
       }
        
       
    }
   
    
    //------pomocna trida pro pocitani vzdalenosti a smeru------------------------------------------
    public class IntSmer
    {
        public int cislo;
        public Direction smer;
        public IntSmer()
        {
            cislo = -1;
            smer = Direction.no;
        }

    }
    
  
    //------trida pro cloveka-----------------------------------------------------------------------
    public class Clovek
    {       
        int trpelivost; // cas cekani ve fronte pri kterem se nesnizuje spokojenost
        public int pocetPenez, spokojenost, hlad;
        int kamJde, casCekani, pocetKroku, pocatecniChuze,  pocatecniSmer;
        float koefDrahoty;
        bool zacatek;
        public int x, y;
        public int id;
        
        public enum Stav { jde, naKrizovatce, veFronte, naAtrakci, vybiraAtrakci, konec, pocatecniChuze };
        PictureBox vzhled;
        Stav stav;
        Direction smer;
        Hlavni_Form hlform;
        
        public Clovek(Hlavni_Form form)
        {
            
            hlform = form;
            VytvorCloveka();
            stav=Stav.pocatecniChuze;
            casCekani = 0;
            pocetKroku = 0;
            pocatecniChuze = 2 * Program.sizeOfSquare; //2 mi prijde vhodne zvolena konstanta
            pocatecniSmer = 1;
            zacatek = true;
            
            form.evidence.pocetVsechLidi++;
            if (form.evidence.pocetVsechLidi == 100 && form.evidence.poprve)
            {
                form.evidence.poprve = false;
                MessageBox.Show("Gratulujeme! Park navštívilo již 100 lidí. Získáváte 5000 navíc.","Odměna",MessageBoxButtons.OK);
                form.evidence.pocetPenez += 5000;
                form.pocetPenez_label.Text = form.evidence.pocetPenez.ToString();
                
            }
            if (form.evidence.pocetVsechLidi == 250 && form.evidence.poprve2)
            {
                form.evidence.poprve2 = false;
                MessageBox.Show("Gratulujeme! Park navštívilo již 250 lidí. Získáváte odměnu 10000.", "Odměna", MessageBoxButtons.OK);
                form.evidence.pocetPenez += 10000;
                form.pocetPenez_label.Text = form.evidence.pocetPenez.ToString();
               
            }
            form.evidence.aktualniPocetLidi++;            
            form.pocetLidiCislo_label.Text = form.evidence.aktualniPocetLidi.ToString();
            form.evidence.lideLSS.VlozNaKonec(this);

            vzhled.Click += new EventHandler(Click);//prirazuji pri kliknuti na pictureBox vzhled funkci Click
                       
        }
        
        private void VytvorCloveka()
        {
            
            id = (hlform.evidence.pocetVsechLidi + 1) % 50000;//50000 konstanta, ktera urcite staci, aby dva lide nemeli stejne id
            trpelivost = hlform.random.Next(100, 200); //timer ma interval 100=0,1S, tj nyni je trpelivost 10s-20s
            pocetPenez = hlform.random.Next(500,3000); 
            spokojenost = 100;
            hlad = 0;
            koefDrahoty = (hlform.random.Next(7, 18) + hlform.random.Next(3, 9)) / (float)10; //melo by nejcasteji davat kolem 16, pred ni rust krivka rychleji a pomaleji klesat za ni
            vzhled = new PictureBox();
            //obrazek hlavy
            vzhled.BackgroundImage = Properties.Resources.clovek_maly;
            //urceni barvy obleceni pomoci RGB
            vzhled.BackColor = Color.FromArgb(hlform.random.Next(255), hlform.random.Next(255), hlform.random.Next(255));
            //obrezek zustane takovy, jaky byl, tj. neroztahuje se, neopakuje apod.
            vzhled.BackgroundImageLayout = ImageLayout.None;
            vzhled.Width = 7;
            vzhled.Height = Program.sizeOfSquare / 2;
            //uchyceni leveho dolniho rohu
            vzhled.Anchor = AnchorStyles.Bottom;
            vzhled.Anchor = AnchorStyles.Left;           
            vzhled.Top = hlform.brana.vstupY + 2;
            vzhled.Left = hlform.brana.vstupX + Program.sizeOfSquare + 1; 
            x = vzhled.Left+vzhled.Width/2;
            y = vzhled.Bottom;        
            vzhled.Parent = hlform.pictureBox1;
            hlform.pictureBox1.Controls.SetChildIndex(vzhled, 2); //poradi v popredi, 0 nejblize, cim vysssi, tim vice v pozadi                 
        }
        public void Click(object sender, EventArgs e)
        {
            hlform.clovekFormular.Show(this.id, this.kamJde, this.spokojenost, this.pocetPenez, this.hlad/20);
        }
        public void Zneviditelni()
        {
            this.vzhled.Visible = false;
        }

        public void Zviditelni()
        {
            this.vzhled.Visible = true;
        }
        public void Premisti(int xovaSour, int yovaSour)
        {
            //spoleham na to, ze dostavam souradnice leveho horniho rohu, tj. bodu mrizky
            vzhled.Top = yovaSour + hlform.random.Next(Program.sizeOfSquare / 3, Program.sizeOfSquare / 2); 
            vzhled.Left = xovaSour + hlform.random.Next(Program.sizeOfSquare / 3, Program.sizeOfSquare / 2);            
            x = vzhled.Left + vzhled.Width / 2;
            y = vzhled.Bottom;
        }
        public void Akce() //interval 100 - tj probehne 10x za sekundu
        {          
            
            hlad=Math.Min(hlad+1,2000); //2000 je 100 %
            switch (stav)
            {
                case Stav.pocatecniChuze:
                    {
                        if (pocatecniChuze > 0)
                        {
                            JdiNahodne();
                            pocatecniChuze--;
                        }
                        else
                        {
                            zacatek = false;
                            stav = Stav.vybiraAtrakci;
                        }
                    }
                    break;
                case Stav.jde:
                    {
                        //UdelejKrok(); //lepe napsat rovnou sem nez do funkce - rychlejsi
                        
                        if (pocetKroku < Program.sizeOfSquare)
                        {
                            switch (smer)
                            {
                                case Direction.N: { y--; vzhled.Top--; }
                                    break;
                                case Direction.S: { y++; vzhled.Top++; }
                                    break;
                                case Direction.W: { x--; vzhled.Left--; }
                                    break;
                                case Direction.E: { x++; vzhled.Left++; }
                                    break;
                                case Direction.no: stav = Stav.vybiraAtrakci;
                                    break;
                                default:
                                    break;
                            }
                            pocetKroku++;
                        }
                        else
                        {
                            stav = Stav.naKrizovatce;
                            pocetKroku = 0;
                        }

                    }
                    break;
                case Stav.naKrizovatce:
                    {
                        //testovani, zda uz nedosel do cile, pokud ano, ceka frontu, jinak se zepta na cestu
                        int xsou, ysou;
                        hlform.evidence.atrakceLSS.VratSouradniceVstupu(kamJde, out xsou, out ysou); 
                        if ((x / Program.sizeOfSquare) == (xsou / Program.sizeOfSquare) &&
                           (y / Program.sizeOfSquare) == (ysou / Program.sizeOfSquare))
                        {
                            Atrakce atrakceUNizJe = hlform.evidence.atrakceLSS.VratClenSId(kamJde);
                            int vstupneAtr=atrakceUNizJe.vstupne;
                            
                            if (vstupneAtr > koefDrahoty * atrakceUNizJe.puvodniVstupne || vstupneAtr > this.pocetPenez) //tj. clovek na ni nema penize
                            {
                                stav = Stav.vybiraAtrakci;
                                spokojenost = Math.Max(spokojenost-10,0);
                                
                            }
                            else
                            {
                                stav = Stav.veFronte;
                                casCekani = 0;
                                atrakceUNizJe.pridejDoFronty(this);
                                pocetKroku = 0; 
                            }
                        }

                        else
                        {
                            smer = ZeptejSeNaCestu(kamJde);
                            stav = Stav.jde;
                        }
                    }
                    break;
                case Stav.veFronte:
                    {
                        if (casCekani < trpelivost)
                        {
                            casCekani++;
                        }
                        else //snizuje spokojenost, protoze ho prestalo bavit cekani
                        {
                            spokojenost = Math.Max(spokojenost - 2, 0);                                                   
                        }
                    }
                    break;
                case Stav.naAtrakci:
                    {
                        spokojenost = Math.Min(spokojenost + 1, 100);
                    }
                    break;
                case Stav.vybiraAtrakci:
                    {
                        casCekani = 0;
                        kamJde=VyberAtrakci();
                        stav = Stav.naKrizovatce;
                    }
                    break;
                
                default:
                    break;
            }
        }

        private void JdiNahodne() //metoda pro pocatecni chuzi
        {
            switch (pocatecniSmer)
            {
                case 1: 
                    if (hlform.evidence.mapaAtrakciAChodniku.jeChodnik(x + 1, y)) { vzhled.Left++; x++; }
                    else pocatecniSmer = hlform.random.Next(1, 5);
                    break;
                case 2: if (hlform.evidence.mapaAtrakciAChodniku.jeChodnik(x, y - 1)) { vzhled.Top--; y--; }
                    else pocatecniSmer = hlform.random.Next(1, 5);
                    break;   
                case 3: if (hlform.evidence.mapaAtrakciAChodniku.jeChodnik(x, y + 1)) { vzhled.Top++; y++; }
                    else pocatecniSmer = hlform.random.Next(1, 5);
                    break;
                case 4: if (hlform.evidence.mapaAtrakciAChodniku.jeChodnik(x - 1, y)) { vzhled.Left--; x--; }
                    else pocatecniSmer = hlform.random.Next(1, 5);
                    break;
                             
            }
            if (hlform.random.Next(1,12) % 7 == 0) pocatecniSmer = hlform.random.Next(1, 4);
        }
             
        private Direction ZeptejSeNaCestu(int idAtrakce)
        {
            return hlform.evidence.mapaAtrakciAChodniku.vratSmer(x, y, idAtrakce);            
        }
        private int VyberAtrakci() 
        {
            //nema penize na zadnou atrakci -> opousti park
            if (this.pocetPenez < hlform.evidence.minimalniCena) return 0;
            //kdyz je hlad 90%, vybira obcerstveni
            if (this.spokojenost == 0) return 0; 
            if (this.hlad > 20 * 90) //2000=100 %, tj. 2000*0.9
            {
                int pom2 = hlform.random.Next(1,hlform.evidence.obcerstveniLSS.PocetUzlu()+1);
                return hlform.evidence.obcerstveniLSS.VratIdNtehoClenu(pom2);
            }
            //jinak vybira z normalnich atrakci
            
                int vyberTypAtrakce = hlform.random.Next(101); //nah. cislo 0-100
                if (vyberTypAtrakce > this.spokojenost) return 0; //tj. spokojenost je slaba a tj. vetsi pst opustit park 
                else if (vyberTypAtrakce < this.hlad / 20) //tj. roste hlad, vetsi pst jit do obcerstveni | 2000=100%, tj./20=pocet procent
                {
                    int pom2 = hlform.random.Next(hlform.evidence.obcerstveniLSS.PocetUzlu());
                    return hlform.evidence.obcerstveniLSS.VratIdNtehoClenu(pom2);
                }
                else
                {

                    int pom = hlform.random.Next(hlform.evidence.pocetAtrakci);//cisla od 0 do poc.atrakci-1 - spravne, nebot v PoctuAtrakci je i brana a ta ma cislo 0
                    
                    if (pom == 0)
                    {
                        //pokud jde o PocatecniChuzi, tj. nedavny vstup do parku, neni dovoleno volit 0, tj. utect, ale musi byt postavena alespon jedna atrakce mimo branu
                        
                        if (zacatek && hlform.evidence.pocetAtrakci > 1)
                        {
                            pom = hlform.random.Next(1, hlform.evidence.pocetAtrakci);
                            return hlform.evidence.atrakceLSS.VratIdNtehoClenu(pom);
                        }
                        else return 0;
                    }

                    else return hlform.evidence.atrakceLSS.VratIdNtehoClenu(pom);
                }

            
           
        }
        public void ZmenStav(Stav zmena)
        {
            stav = zmena;
        }

        public void Destruct()
        {
            vzhled.Dispose();
            hlform.evidence.lideLSS.NajdiASmaz(this.id);//maze cloveka z LSS
            hlform.evidence.aktualniPocetLidi--;
            hlform.pocetLidiCislo_label.Text = hlform.evidence.aktualniPocetLidi.ToString();
        
        }

        public void SmazPbox()
        {
            vzhled.Dispose();
        }
    
    
    }
  
    //------trida, vytvari se jedina instance, eviduje menne prvky hry------------------------------
    public class Evidence
    {
        public Mapa mapaAtrakciAChodniku;
        private bool[] volnaID;
        public bool poprve, poprve2, poprveZelen, poprveFontana, poprveLod; //slouzi k odmenam 
        public bool vyzkum, propagace;
        public int pocetPenez, pocetAtrakci, aktualniPocetLidi, maxPocetLidi, lakavost, propagaceCislo;
        public int minimalniCena; //aktualne nejnizsi cena vstupu na alespon 1 atrakci
        public int zelen;
        public int vyzkoumano; //kolik bylo zatim vyzkoumano
        public int maxPocetAtrakci, pocetVsechLidi;
        public IntSmer[,] intChodniky; //public musi byt, abych ji zvladla predat potrebnym metodam
        public LSSAtrakce atrakceLSS; // LSS vsech atrakci v parku
        public LSSClovek lideLSS; //LSS vsech lidi v parku
        public LSSAtrakce obcerstveniLSS; //LSS vsech obcerstveni v parku - jsou ulozena i v LSSAtrakce
        public LSSeznam<Policko> ostatniLSS; //pro moznost smazani pri nove hre

        public Evidence(int sirka,int vyska)
        {
            zelen = 0; vyzkoumano = 0;
            vyzkum = false;
            propagace = false;
            poprve = true;
            poprve2 = true;
            poprveZelen = true; poprveFontana = true; poprveLod = true;
            pocetAtrakci = 0;//do poct atrakci se pocita i brana, hodnota se zvysuje stejne jako u atrakci v konstruktoru predka
            aktualniPocetLidi = 0;
            pocetVsechLidi = 0;
            lakavost = 100;
            pocetPenez = 20000;
            minimalniCena = int.MaxValue;
            
            mapaAtrakciAChodniku = new Mapa(sirka,vyska);
            intChodniky=new IntSmer[sirka,vyska];
            NastavChodniky(sirka,vyska);
            atrakceLSS = new LSSAtrakce();
            lideLSS = new LSSClovek();
            obcerstveniLSS = new LSSAtrakce(0);//seznam s hlavou s id 0
            ostatniLSS = new LSSeznam<Policko>();
            
            maxPocetLidi = sirka * vyska * 3;
            maxPocetAtrakci=sirka*vyska / 10;
            volnaID=new bool[maxPocetAtrakci+2];//aby se pridelovaly id od 1, a na konci zarazka
            for (int i=1; i<= maxPocetAtrakci+1; i++)
			{
			    volnaID[i]=true;
			}
            volnaID[0]=false;
        }

        
        private void NastavChodniky(int sirka,int vyska) //kvuli inicializaci pole, jinak bych nemohla pristupovat k jednotlivym polozkam - nebyly by tam
        {
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < vyska; j++)
                {
                    IntSmer objekt= new IntSmer(); //soucasti je nastaveni cisla=-1 a smeru=zadny v konstruktoru
                    intChodniky[i,j] = objekt;
                }
            }
        }

        public void PridejChodnikDoIntMapy(int x, int y) 
        {
            intChodniky[x / Program.sizeOfSquare, y / Program.sizeOfSquare].cislo = int.MaxValue; 
        }

        public void SmazChodnikZMapyChodnikuBezAktualizaceSmeru(int x, int y)
        {
            intChodniky[x / Program.sizeOfSquare, y / Program.sizeOfSquare].cislo = -1;                    
        }

        public int vratVolneID() // pro atrakce
        {
            int i=1;
            while(!volnaID[i])
            {
                i++;
            }
            if(i<=maxPocetAtrakci)
            {   
                volnaID[i]=false;
                return i;
            }
            else return -1;
        }

        public void uvolniID(int id)
        {
            volnaID[id]=true;       
        }

        public void Destruct()
        {
            mapaAtrakciAChodniku.destruct(); //znici obrazky chodniku a atrakci
            obcerstveniLSS.DestructPBox();
            ostatniLSS.DestructPBox();
            lideLSS.Destruct();
            atrakceLSS.Destruct();
            //??mam nejak uvolnovat LSS? 
            
        
        }
    
    }


    //------pomocna trida - uzel v LSS---------------------------------------------------------  
    public class Uzel<TypHodnoty>
    {
        public TypHodnoty objekt; //uložená informace
        public int id;//u atrakce a cloveka jde o id atrakce a cloveka, jinde ne
        public Uzel<TypHodnoty> dalsi; //následník
        public Uzel(TypHodnoty inf) //konstruktor
        { 
            this.objekt = inf; 
            dalsi = null; 
        }
        public Uzel(int id) //konstruktor
        {
            
            this.id = id;
            dalsi = null;
        }
    }

    //------abstraktni trida pro vytvoreni LSS
    public class LSSeznam<TypHodnoty> 
    {
        protected Uzel<TypHodnoty> zacatek; //první prvek seznamu
        protected Uzel<TypHodnoty> konec; //poslední prvek seznamu 
        protected int pocetUzlu;

        public LSSeznam()
        {
            zacatek = null;
            konec = null;
            pocetUzlu = 0;
        }
        public LSSeznam(int idHlavy)
        {
            Uzel<TypHodnoty> hlava = new Uzel<TypHodnoty>(idHlavy);//vytvari clen s id, bez objekt
            zacatek = hlava;
            konec = hlava;
            pocetUzlu = 0;
        }

        public int PocetUzlu()
        {
            return pocetUzlu;
        }
        virtual public int VratIdNtehoClenu(int n) 
        {
            
            Uzel<TypHodnoty> uzel = zacatek;
            if (uzel != null)
            {
                for (int i = 1; i < n; i++) //pozor na to, zda bude seznam s hlavou nebo ne
                {
                    uzel = uzel.dalsi;
                }
                return uzel.id; // u atrakci a lidi shodne s id atrakce a cloveka
            }
            else return 0;
        }

        virtual public void VlozNaKonec(TypHodnoty co)
        {
            Uzel<TypHodnoty> uzel = new Uzel<TypHodnoty>(co);
            if (zacatek == null)
            {
                zacatek = uzel;
                konec = uzel;
                
            }
            else
            {
                konec.dalsi = uzel;
                konec = uzel;
            }
            pocetUzlu++;
        }

        public TypHodnoty VratNtyPrvekASmazHo(int n)//pro frontu lidi v atrakci, delano pro seznam s hlavou
        {
            if (n > pocetUzlu) return zacatek.objekt; 
             
            else
              {
                Uzel<TypHodnoty> uk = zacatek.dalsi;
                Uzel<TypHodnoty> kun = zacatek;

                for (int i = 1; i < n; i++) 
                {
                    kun = uk;
                    uk = uk.dalsi;
                }
                if (uk.dalsi == null) konec = kun;
                kun.dalsi=uk.dalsi; //"mazu" dany uzel z fronty
                pocetUzlu--;
                return uk.objekt; 
              }
            
        }
        public TypHodnoty VratPrvniPrvekASmazHo() //delano pro seznam s hlavou
        {
            if (zacatek.dalsi != null)
            {
                Uzel<TypHodnoty> prvek = zacatek.dalsi;
                zacatek = zacatek.dalsi;
                if (zacatek.dalsi == null) konec = zacatek;
                pocetUzlu--;
                return prvek.objekt;
            }
            else return zacatek.objekt;
        }
        
        public void NajdiASmaz(int id)//tzn. je alespon jeden prvek a id v nem je
        {
            Uzel<TypHodnoty> uk = zacatek.dalsi;
            Uzel<TypHodnoty> kun = zacatek;
            if (id == zacatek.id) zacatek = uk;
            else
            {
                while (uk!=konec && uk.id != id )
                {
                    kun = uk;
                    uk = uk.dalsi;
                }
               
                kun.dalsi = uk.dalsi;
                if (uk.dalsi == null) konec = kun;
                

            }
            pocetUzlu--;
        }

        public TypHodnoty VratClenSId(int id)//predpoklada, ze vzdy je
        {
            bool nekonci = true;
            Uzel<TypHodnoty> uk = zacatek;
            while (uk != null && nekonci)
            {
                if (uk.id == id)
                {
                    nekonci = false;
                    return uk.objekt;
                }
                else uk = uk.dalsi;
            }
            return zacatek.objekt; //kvuli prekladaci - stezuje si a nevi, ze id se vzdy najde
        }
        virtual public void Destruct()
        {
            zacatek = null;
            konec = null;
        }
        public void DestructPBox()
        {
            Uzel<TypHodnoty> uk = zacatek;
            Policko obj;
            while (uk != null)
            {
                obj = uk.objekt as Policko;
                if (obj != null) obj.pbox.Dispose();
                uk = uk.dalsi;
            }
            zacatek = null;
            konec = null;
        }
        virtual public void VsemAkce() {} //puvodne abstraktni, ale kvuli ostatniLSS se vztvari instance i teto tridy, tj. metoda virtualni
        virtual public void VratSouradniceVstupu(int id, out int xsouradnice, out int ysouradnice) //jen kvuli tomu, ze se mi nedari mit fci jen v potomkovi
        { xsouradnice = 0; ysouradnice = 0; }
        virtual public void SmazVseKromeHlavy()
        {
            konec = zacatek;
            zacatek.dalsi=null; 
            pocetUzlu = 0;
        }
        
        
    }

        public class LSSClovek : LSSeznam<Clovek>
        {
            
            protected Uzel<Clovek> uk;
            public LSSClovek()
                : base()
            {
                     
            }
            
             public LSSClovek(int idHlavy) //pro seznam s hlavou
                 : base(idHlavy)
             {
                
             }

            public override void VlozNaKonec(Clovek co)
            {
                Uzel<Clovek> uzel = new Uzel<Clovek>(co);
                pocetUzlu++;
                uzel.id = co.id;
                
                if (zacatek == null)
                {
                    zacatek = uzel;
                    konec = uzel;

                }
                else
                {
                    konec.dalsi = uzel;
                    konec = uzel;
                }
            }
            public override void SmazVseKromeHlavy()
            {
              
               uk = zacatek.dalsi; //ukladam prvni prvek seznamu, nebot je s hlavou
               while (uk != null)
               {
                   uk.objekt.Destruct();
                   uk = uk.dalsi;               
               }
                zacatek.dalsi = null;
                konec = zacatek;
               pocetUzlu = 0;
            }
            
            public override void Destruct()
            {
                uk = zacatek;
                while (uk != null)
                {
                    uk.objekt.SmazPbox();
                    uk = uk.dalsi;
                }
                zacatek = null;
                konec = zacatek;
                pocetUzlu=0;
            }

            public override void VsemAkce()
            {
                uk = zacatek;
                while (uk != null)
                {
                    uk.objekt.Akce();
                    uk = uk.dalsi;
                    
                }
            }
        }

        public class LSSAtrakce : LSSeznam<Atrakce>
        {
            
            public int x, y;          
            public LSSAtrakce():base()               
            {
               
            }

            public LSSAtrakce(int cislo) //seznam s hlavou s id cislo
                : base()
            {
                Uzel<Atrakce> uzel = new Uzel<Atrakce>(cislo);
                zacatek = uzel;
                konec = uzel;               
            }
            public override void VlozNaKonec(Atrakce co)
            {
                pocetUzlu++;
                Uzel<Atrakce> uzel = new Uzel<Atrakce>(co);
                if (zacatek == null)
                {
                    uzel.id = co.id;
                    zacatek = uzel;
                    konec = uzel;
                    

                }
                else
                {
                    uzel.id = co.id;
                    konec.dalsi = uzel;
                    konec = uzel;
                }
            }


            public override int VratIdNtehoClenu(int n) //mysli se n te atrakce, bez brany
            {
                Uzel<Atrakce> uzel = zacatek.dalsi; //zacatek nikdy neni null, protoze je tam brana
                if (n > pocetUzlu || uzel==null) return 0;
                else //tj. if (uzel != null)
                {
                    for (int i = 1; i < n; i++) //pozor na to, zda bude seznam s hlavou nebo ne
                    {
                       uzel = uzel.dalsi;
                    }
                    return uzel.objekt.id; 
                }
               
            }
                
            public override void VsemAkce() //pro atrakce - doba behu apod., pro branu: vyrabi lidi
            {
                Uzel<Atrakce> uk3 = zacatek; 
                while (uk3 != null)
                {
                    uk3.objekt.Akce();
                    uk3 = uk3.dalsi;
                }
            }

            public override void VratSouradniceVstupu(int id, out int xsouradnice, out int ysouradnice)
            {
                Atrakce atr = VratClenSId(id);
                xsouradnice = atr.vstupX;
                ysouradnice = atr.vstupY;

            }


            public void NastavVzdalenostVsemAtrakcim(Hlavni_Form form)
            {
                Uzel<Atrakce> uk2 = zacatek;
                while (uk2 != null)
                {
                    Atrakce pom = uk2.objekt as Atrakce; //tato podminka je kvuli pristupu na pom.vstupX apod.
                    if (pom != null && (!pom.mimoProvoz))
                    {
                       
                        UrceniSmeruCesty.spoctiVzdalenostOdAtrakce(pom.id, pom.vstupX, pom.vstupY, form.vyska, form.sirka, form.evidence.intChodniky, form.evidence.mapaAtrakciAChodniku);
                    }
                    uk2 = uk2.dalsi; 
                }
            }

            public void AktualizujPoZboreniAtrakce(int idAtrakce, Hlavni_Form form)
            { 
                this.NastavVzdalenostVsemAtrakcim(form); 
                form.evidence.mapaAtrakciAChodniku.zrusPristupKAtrakci(idAtrakce);            
            }


        }  
        
   
    public class PrvekFronty
    {
        public int sx;
        public int sy;
        public int pocetKroku;
        public PrvekFronty(int x, int y, int kroky)
        {
            pocetKroku = kroky;
            sx = x;
            sy = y;
        }
    
    }

    public class Fronta<TypHodnoty>
    {
        public TypHodnoty[] fronta;
        public int zacatek;
        public int konec;
        private int delka;

        public Fronta(int delka)
        {
            this.fronta = new TypHodnoty[delka];
            this.zacatek = 0;
            this.konec = 0;
            this.delka = delka;
        }
        public void Vloz(TypHodnoty objekt) //pridava na konec
        {
            
            fronta[konec%delka] = objekt;
            konec++;
        }
        public TypHodnoty VratPrvek()
        {
            zacatek++;
            return fronta[(zacatek-1)%delka];
        }
        public bool Prazdna()
        {
            if (zacatek != konec) return false;
            else return true;
        }
    }  

   

}
