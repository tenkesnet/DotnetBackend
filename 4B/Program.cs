using System;

namespace AutoAlkatresz;

public class Program
{
    public static void Main(string[] args)
    {
        Auto Mercedes = new Auto("Mercedes");
        Mercedes.addAlkatresz(new Alkatresz("Kormány", 321000, AlkatreszType.Other));
        Mercedes.addAlkatresz(new Alkatresz("Ülések", 428000, AlkatreszType.Other));
        Mercedes.addAlkatresz(new Alkatresz("Üvegek", 290000, AlkatreszType.Other));
        Mercedes.addAlkatresz(new Foalkatresz("Karosszéria", 2500000, AlkatreszType.Karosszeria));
        Mercedes.addAlkatresz(new Foalkatresz("Ajtó", 410000, AlkatreszType.Ajto));
        Mercedes.addAlkatresz(new Foalkatresz("Ajtó", 410000, AlkatreszType.Ajto));
        Mercedes.addAlkatresz(new Foalkatresz("Ajtó", 410000, AlkatreszType.Ajto));
        Mercedes.addAlkatresz(new Foalkatresz("Ajtó", 410000, AlkatreszType.Ajto));
        Mercedes.addAlkatresz(new Foalkatresz("Motor", 1500000, AlkatreszType.Motor));
        Mercedes.addAlkatresz(new Foalkatresz("Alváz", 3420000, AlkatreszType.Alvaz));
        if (Mercedes.AutoKesz() == true)
        {
            Console.Write("Az autónak minden főalkatrésze megvan. Ára:");
            Mercedes.AutoarSzamolasa();
        }
        else
        {
            Console.WriteLine("Az autónak még nincs meg minden főalkatrésze, ezért összár nem számolható.");
        }

    }
}