using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukraszda
{
    class Suti
    {
        private string nev;

        public string Nev
        {
            get { return nev; }
        }

        private string tipus;

        public string Tipus
        {
            get { return tipus; }
        }

        private bool dij;

        public bool Dij
        {
            get { return dij; }
        }

        private int ar;

        public int Ar
        {
            get { return ar; }
        }

        private string mertekEgyseg;

        public string MertekEgyseg
        {
            get { return mertekEgyseg; }
        }

        public Suti(string nev, string tipus, bool dij, int ar, string mertekEgyseg)
        {
            this.nev = nev;
            this.tipus = tipus;
            this.dij = dij;
            this.ar = ar;
            this.mertekEgyseg = mertekEgyseg;
        }
    }
}
