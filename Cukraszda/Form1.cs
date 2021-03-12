using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cukraszda
{
    public partial class FoForm : Form
    {
        static List<Suti> adatok = new List<Suti>();
        static Dictionary<string, string> Sutik = new Dictionary<string, string>();
        public FoForm()
        {
            InitializeComponent();
            lbl_LegdragabbSuti.Text = "Legdrágább süteményünk";
            lbl_Legolcsobb.Text = "Legolcsóbb süteményünk";
            lblTipus.Text = "Süti tipusa:";
            lblSutiNeve.Text = "Süti neve:";
            lblSutiTipusa.Text = "Süti tipusa:";
            lblEgyseg.Text = "Egység:";
            lblAr.Text = "Ár:";
            chbDijazott.Text = "Díjazott";
        }

        private void FoForm_Load(object sender, EventArgs e)
        {
            Beolvasas();
            RandomSzamSuti();
            SutikListaban();
            Maximum();
            Minimum();
            DinyertesDB();
            Statisztika();
        }
    

        private static void Statisztika()
        {
            Dictionary<string, int> Statisztika = new Dictionary<string, int>();
            foreach (var i in adatok)
            {
                if (!Statisztika.ContainsKey(i.Tipus))
                {
                    Statisztika.Add(i.Tipus, 1);
                }
                else
                {
                    Statisztika[i.Tipus]++;
                }
            }
            StreamWriter iro = new StreamWriter("stat.csv");
            foreach (var i in Statisztika)
            {
                iro.WriteLine($"{i.Key} {i.Value}");
            }
            iro.Close();
        }

        private void RandomSzamSuti()
        {
            Random r = new Random();
            int r_Szam = r.Next(0, adatok.Count + 1);
            tbMaiAjanlat.Text = $"Mai ajánlatunk: {adatok[r_Szam].Nev}";
            tbMaiAjanlat.ReadOnly = true;
        }

        private static void Beolvasas()
        {
            StreamReader sr = new StreamReader("cuki.txt");
            while (!sr.EndOfStream)
            {
                string[] a = sr.ReadLine().Split(';');
                adatok.Add(new Suti(a[0], a[1], bool.Parse(a[2]), int.Parse(a[3]), a[4]));
            }
            sr.Close();
        }

        private static void SutikListaban()
        {
            foreach (var i in adatok)
            {
                if (!Sutik.ContainsKey(i.Nev))
                {
                    Sutik.Add(i.Nev, i.Tipus);
                }
            }
            StreamWriter sw = new StreamWriter("lista.txt");
            foreach (var i in Sutik)
            {
                sw.WriteLine($"{i.Key} {i.Value}");
            }
            sw.Close();
        }

        private void DinyertesDB()
        {
            int db = 0;
            foreach (var i in adatok)
            {
                if (i.Dij == true)
                {
                    db++;
                }
            }
            tbDijnyertesDB.Text = $"{db} féle díjnyertes édességből választhat.";
            tbDijnyertesDB.ReadOnly = true;
        }

        private void Minimum()
        {
            int max = adatok[0].Ar;
            string maxNev = "";
            string mertek = "";
            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].Ar < max)
                {
                    max = adatok[i].Ar;
                    maxNev = adatok[i].Nev;
                    mertek = adatok[i].MertekEgyseg;
                }
            }
            tbLegolcsobb.Text = $"{maxNev}";
            tb_LegolcsobbTul.Text = $"{max} Ft/ {mertek} ";
            tbLegolcsobb.ReadOnly = true;
            tb_LegolcsobbTul.ReadOnly = true;
        }

        private void Maximum()
        {
            int max = 0;
            string maxNev = "";
            string mertek = "";
            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].Ar > max)
                {
                    max = adatok[i].Ar;
                    maxNev = adatok[i].Nev;
                    mertek = adatok[i].MertekEgyseg;
                }
            }
            tbLegdragabb.Text = $"{maxNev}";
            tbLegdragabbTul.Text = $"{max} Ft/ {mertek}";
            tbLegdragabb.ReadOnly = true;
            tbLegdragabbTul.ReadOnly = true;
        }

        private void btnArajanlat_Click(object sender, EventArgs e)
        {
            
                List<int> Arak = new List<int>();
                double sum = 0;
                StreamWriter iro = new StreamWriter("arajanlat.txt");
                foreach (var i in adatok)
                {
                    if (tbTipus.Text == i.Tipus)
                    {
                        Arak.Add(i.Ar);
                    }
                    else if (tbTipus.Text.Length==0)
                    {
                        MessageBox.Show("Nem írtál be sütit!");
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Nincs ilyen tipusú sütink!");
                        break;
                    }
                }
                foreach (var a in Arak)
                {
                    sum += a;
                }
                for (int i = 0; i < adatok.Count; i++)
                {
                    if (tbTipus.Text == adatok[i].Tipus)
                    {
                        iro.WriteLine($"{adatok[i].Nev} {adatok[i].Ar} {adatok[i].MertekEgyseg}");
                    }
                }
                MessageBox.Show($"{Arak.Count} db sütit irtam az árajanlat txtbe\n Átlagár: {(double)sum / Arak.Count}");
                iro.Close();
            }

        private void btnUjSuti_Click(object sender, EventArgs e)
        {
            if (tbSutiNev.Text.Length == 0 || tbSutiTipus.Text.Length == 0 || tbEgyseg.Text.Length == 0 || tbAr.Text.Length == 0)
            {
                MessageBox.Show("Nem adtál meg minden adatott!");
            }
            bool dijazott = false;
            if (chbDijazott.Checked)
            {
                dijazott = true;
            }

            adatok.Add(new Suti(tbSutiNev.Text, tbSutiTipus.Text, dijazott, int.Parse(tbAr.Text), tbEgyseg.Text));
            MessageBox.Show("Az állomény bővítése sikeres volt");

            StreamWriter iro = new StreamWriter("cukiV2.txt");

            for (int i = 0; i < adatok.Count; i++)
            {
                iro.WriteLine($"{adatok[i].Nev} {adatok[i].Tipus} {adatok[i].Dij} {adatok[i].Ar} {adatok[i].MertekEgyseg}");
            }

            iro.Close();
        }
    }
    }
