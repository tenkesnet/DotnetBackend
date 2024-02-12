using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartya
{
    public class Kartya
    {
        private int szamErtek;
        private string szin;
        public Kartya(int szamErtek, string szin)
        {
            this.szin = szin;
            if (szamErtek < 2 || szamErtek > 14) {
                this.szamErtek = 2;
            }
            else this.szamErtek = szamErtek;
        }
        public Kartya()
        {
            this.szamErtek = 0;
            this.szin = "piros";
        }
        public int getSzam()
        {
            return this.szamErtek;
        }
        public void setSzam(int szam)
        {
            if (szam >= 2 && szam <= 14)
            {
                this.szamErtek = szam;
            }
        }
        public string getSzin()
        {
            return this.szin;
        }
        public void setSzin(string szin)
        {
            this.szin=szin;
        }
        public string kartyaSzovegesForma(int szamErtek, string szin)
        {
            string kartyaSzam=Convert.ToString(szamErtek);
            string kartyaSzinSzam = szin + " " + kartyaSzam;
            return kartyaSzinSzam;
        }
        public override string ToString()
        {
            return getSzin() + " " + getSzam();
        }
    }
}
