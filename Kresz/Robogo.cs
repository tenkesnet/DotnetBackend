using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kresz
{
    public class Robogo : Jarmu, IKisGepjarmu
    {
        private int _maxSebesseg { get; set; }
        public Robogo (int aktualisSebesseg, string rendszam, int maxSebesseg)
        {
            _maxSebesseg = maxSebesseg;
            _aktualisSebesseg=aktualisSebesseg;
        }
        public override bool gyorshajtottE(int sebessegKorlat)
        {
            if (_aktualisSebesseg >sebessegKorlat)
            {
                return true;
            }
            else return false;
        }
        public bool haladhatItt(int sebesseg)
        {
            if (_maxSebesseg>sebesseg)
            {
                return false;
            }
            else return true;
        }
        public override string ToString()
        {
            return "Robogó: " + this.ToString;
        }
    }
}
