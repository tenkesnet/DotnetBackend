using System;

namespace Butorgyar
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Butorlap b1 = new Butorlap("hatlap", 12.2);
            Butorlap b2 = new Butorlap();
            Butor konyhaszekreny = new Butor("gjgjhg");

            konyhaszekreny.addButorlap(b1);
            konyhaszekreny.addButorlap(b2);
            Console.WriteLine(konyhaszekreny.butorlapok[0].getAr());
        }
    }
}

