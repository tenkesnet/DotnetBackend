using System;

namespace AutoAlkatresz;

public class Auto
{
    public List<Alkatreszek> AlkatreszekLista=new List<Alkatreszek>();
    public Auto(string nev)
    {
        Console.WriteLine($"Létrehoztam az Auto osztály egy példányát {nev} néven");
    }
    public void AlkatreszekListaFeltoltes(Alkatreszek AlkatreszNev)     
    {
        AlkatreszekLista.Add(AlkatreszNev);    
    }
}   