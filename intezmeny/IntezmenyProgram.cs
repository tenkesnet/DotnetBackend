using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Intezmeny
{
    internal class IntezmenyProgram
    {
        private Kurzus[] kurzusTomb = new Kurzus[5]; 
        public IntezmenyProgram()
        {
            Egyetem egyetemELTE = new Egyetem("ELTE", "Budapest");
            Egyetem egyetemDE = new Egyetem("Debreceni Egyetem", "Debrecen");
            Egyetem egyetemEKE = new Egyetem("Eszterházy Egyetem", "Eger");
            Vegzettseg vegzettsegTanar = new Vegzettseg("Tanar", egyetemEKE);
            Vegzettseg vegzettsegOrvos = new Vegzettseg("Orvos", egyetemDE);
            Vegzettseg vegzettsegBolcsesz = new Vegzettseg("Bolcsesz", egyetemELTE);
            Oktato oktatoSzabo = new Oktato("dr. Szabó József", vegzettsegOrvos);
            Oktato oktatoPintér = new Oktato("dr. Pintér Elemér", vegzettsegBolcsesz);
            Oktato oktatoKiss = new Oktato("Kiss Jenő", vegzettsegTanar);
            kurzusTomb[0] = new Kurzus("Földrajz tanári kurzus", oktatoKiss);
            kurzusTomb[1] = new Kurzus("Belgyógyász kurzus", oktatoSzabo);
            kurzusTomb[2] = new Kurzus("Biológia tanári kurzus", oktatoKiss);
            kurzusTomb[3] = new Kurzus("Bölcsész kurzus", oktatoPintér);
            kurzusTomb[4] = new Kurzus("Traumatológus kurzus", oktatoSzabo);

        }
        static void Main(string[] args)
        {      
            IntezmenyProgram intezmenyProgram = new IntezmenyProgram();
            intezmenyProgram.kiiratas();
            intezmenyProgram.adjMegVarost("Debrecen");
            Console.ReadLine();
        }
        public void kiiratas()
        {
            foreach (var elem in kurzusTomb)
            {
                Console.WriteLine(elem._kurzusNev, elem._oktato);
            }
        }
        void adjMegVarost(string varos)
        {
            HashSet<Oktato> b = new HashSet<Oktato>();
            foreach (var o in kurzusTomb)
            {
                if (o._oktato._oktatoVegzettseg._vegzettsegEgyetem._egyetemVaros == varos)
                {
                    b.Add(o._oktato);
                }
            }
            Console.WriteLine($"{varos} városban {b.Count} oktató végzett.");
        }
    }
}
