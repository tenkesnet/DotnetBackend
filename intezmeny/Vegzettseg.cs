using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intezmeny
{
    internal class Vegzettseg
    {
        public string _vegzettsegSzak { get; set; }
        public Egyetem _vegzettsegEgyetem { get; set; }

        public Vegzettseg(string vegzettsegSzak, Egyetem vegzettsegEgyetem)
        {
            _vegzettsegSzak = vegzettsegSzak;   
            _vegzettsegEgyetem = vegzettsegEgyetem;
        }
        public override string ToString()
        {
            return _vegzettsegSzak + " " + _vegzettsegEgyetem;
        }
    }
}
