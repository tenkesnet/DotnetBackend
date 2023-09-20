using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intezmeny
{
    internal class Oktato
    {
        public string _oktatoNev { get; set; }
        public Vegzettseg _oktatoVegzettseg { get; set; }
        public Oktato(string oktatoNev, Vegzettseg oktatoVegzettseg) 
        {
            _oktatoNev = oktatoNev;
            _oktatoVegzettseg = oktatoVegzettseg;
        }
        public override string ToString()
        {
            return _oktatoNev + " " + _oktatoVegzettseg;
        }

    }
}
