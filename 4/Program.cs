using System;

namespace AutoAlkatresz;

public class Program
{
    public static void Main(string[] args)
    {
        Auto Mercedes=new Auto("Mercedes");
        Mercedes.AlkatreszekListaFeltoltes(new Alkatreszek("Kormány",321000));
        Mercedes.AlkatreszekListaFeltoltes(new Alkatreszek("Ülések", 428000));
        Mercedes.AlkatreszekListaFeltoltes(new Alkatreszek("Üvegek",290000));
        Mercedes.AlkatreszekListaFeltoltes(new Alkatreszek(new FoAlkatreszek("Alváz", 3420000)));
    }
}