using System;

namespace AutoAlkatresz;
public class Alkatresz
{
    public double ar;
    public string nev;
    public AlkatreszType tipus { get; set; }
    public Alkatresz(string AlkatreszNev, double AlkatreszAra, AlkatreszType alkatreszType)
    {

        Console.WriteLine($"Alkatrész neve: {AlkatreszNev}, {AlkatreszAra:C1}");
        ar = AlkatreszAra;
        nev = AlkatreszNev;
        tipus = alkatreszType;
    }

    public double getAlkatreszAra()
    {
        return ar;
    }
    public string getAlkatreszNev()
    {
        return nev;
    }
}