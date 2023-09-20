using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intezmeny
{
    internal class Egyetem
    {
        public string _egyetemNev { get; set; }
        public string _egyetemVaros { get; set; }
        public Egyetem(string egyetemNev, string egyetemVaros) 
        {
            _egyetemNev=egyetemNev;
            _egyetemVaros=egyetemVaros; 
        }
        public override string ToString()
        {
            return _egyetemNev + " "+ _egyetemVaros;
        }
    }
}
