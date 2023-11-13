using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kresz
{
    public class AudiS8 :Jarmu
    {
        private bool _lezerBlokkolo { get; set; }
        public AudiS8(int aktualisSebesseg, string rendszam, bool lezerBlokkolo) : base(aktualisSebesseg, rendszam)
        {
            _lezerBlokkolo = lezerBlokkolo;
        }
        public override bool gyorshajtottE(int sebessegKorlat)
        {
            if (_lezerBlokkolo == true)
            {
                return false;
            }
            else {
                if (_aktualisSebesseg > sebessegKorlat)
                {
                    return true;
                }
                else return false;
            }
        }
        public override string ToString()
        {
            return "Audi: " + base.ToString();
        }
    }
}
