using System;

namespace AutoAlkatresz
{

    public class Alkatreszek
    {
        public FoAlkatreszek fajta;
        public Alkatreszek(string AlkatreszNev, double AlkatreszAra)
        {
            Console.WriteLine($"A felvitt alkatrész neve: {AlkatreszNev}, {AlkatreszAra:C1}");
        }
        public Alkatreszek(FoAlkatreszek fajta)
        {

        }
    }
}