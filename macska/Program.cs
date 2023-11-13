using Intezmeny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Macska
{
    public class Program
    {
        static void Main(string[] args)
        {
            Macska macska1=new Macska("Felix", 3.5, true);
            Macska macska2 = new Macska("Whiskas", 4.2);
            Console.WriteLine(macska1.ToString());
            Console.WriteLine(macska2.ToString());
            //IntezmenyProgram program1=new IntezmenyProgram();
            int i;
            List<int> golok = new List<int>() { 13,20,8,23,10,9};
            Console.WriteLine("Átlag: "+ golok.Average());
            List<Macska> macskak = new List<Macska>() { macska1, macska2 };
            Console.WriteLine("Átlag: " + macskak.Average(macska=>macska._suly));

            if (macska1.Eszik(0.17))
            {
                Console.WriteLine(macska1._nev +" megette az adagját.");
            }
            else
            {
                Console.WriteLine(macska1._nev + " nem volt éhes, nem evett.");
            }
            if (macska2.Eszik(0.19))
            {
                Console.WriteLine(macska2._nev + " megette az adagját.");
            }
            else
            {
                Console.WriteLine(macska2._nev + " nem volt ehes, nem evett.");
            }
            
            macska1.Futkos();
            macska2.Futkos();
            Console.WriteLine(macska1.ToString());
            Console.WriteLine(macska2.ToString());

            Program program = new Program();
            Szoveg teve= new Szoveg();
            int a = 10;
            program.osszead(a, 15, teve);
            Console.WriteLine(teve.text);
            Console.WriteLine(a);
            Console.ReadLine();
        }
        internal void osszead(int a, int b, Szoveg c)
        {
            c.text = Convert.ToString(a + b);
            a = a + b;
        }

    }
}
