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
        public FoForm()
        {
            InitializeComponent();
            lbl_LegdragabbSuti.Text = "Legdrágább süteményünk";
            lbl_Legolcsobb.Text = "Legolcsóbb süteményünk";
            lblTipus.Text = "Süti tipusa:";
        }

        private void FoForm_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("cuki.txt");
            while (!sr.EndOfStream)
            {
                string[] a = sr.ReadLine().Split(';');
                adatok.Add(new Suti(a[0], a[1], bool.Parse(a[2]), int.Parse(a[3]), a[4]));
            }
            sr.Close();

            Random r = new Random();
            int r_Szam = r.Next(0, adatok.Count + 1);
            tbMaiAjanlat.Text = $"Mai ajánlatunk: {adatok[r_Szam].Nev}";
            Maximum();
            Minimum();
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
        }
    }
}
