using System;

namespace AutoAlkatresz;

public class FoAlkatreszek 
{
    public FoAlkatreszek(string AlkatreszNev, double AlkatreszAra)
    {
        Console.WriteLine($"A felvitt alkatrész neve: {AlkatreszNev}, {AlkatreszAra:C1}");
    }
    /*public string NevVisszaadas()
    {
        return AlkatreszNev;
    }*/
}   