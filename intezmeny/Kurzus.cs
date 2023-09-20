using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intezmeny
{
    internal class Kurzus
    {
        public string _kurzusNev { get; set; }
        public Oktato _oktato { get; set; }

        public Kurzus(string kurzusNev, Oktato oktato)
        {
            _kurzusNev = kurzusNev;
            _oktato = oktato;
        }
        public override string ToString()
        {
            return _kurzusNev + " " + _oktato;
        }
    }
}
