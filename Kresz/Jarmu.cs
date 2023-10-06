using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kresz
{
    public abstract class Jarmu
    {
        protected int _aktualisSebesseg { get; set; }
        private string _rendszam { get;set; }
        public Jarmu(int aktualisSebesseg, string rendszam)
        {
            _aktualisSebesseg = aktualisSebesseg;
            _rendszam = rendszam;
        }
        public abstract bool gyorshajtottE(int sebessegKorlat);
        public override string ToString()
        {
            return _rendszam + " - " + _aktualisSebesseg;
        }
    }
}
