using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macska
{
    public class Macska
    {
        public string _nev;
        public double _suly;
        public bool _ehes;
        public Macska(string nev, double suly, bool ehes) 
        {
            _suly = suly;
            _nev = nev;
            _ehes = ehes;
        }

        public Macska(string nev, double suly)
        {
            _nev= nev;
            _suly = suly;
            _ehes = true;
        }

        public bool Eszik(double etelMennyisege)
        {
            if (_ehes)
            {
                _suly = _suly+etelMennyisege;
                _ehes=false;
                return true;
            }
            else { return false; }
        }

        public void Futkos ()
        {
            _suly = _suly - 0.1;
            _ehes = true;
        }

        public override string ToString()
        {
            return _nev + "; " + _suly + "; "+ _ehes;
        }
    }
}
