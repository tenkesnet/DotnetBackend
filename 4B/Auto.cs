using System;

namespace AutoAlkatresz;

public class Auto
{
    public List<Alkatresz> AlkatreszekLista = new List<Alkatresz>();
    public string nev { get; set; }
    public Auto(string nev)
    {
        Console.WriteLine($"Létrehoztam az Auto osztály egy példányát {nev} néven");
        this.nev = nev;
    }
    public void addAlkatresz(Alkatresz alkatresz)
    {
        AlkatreszekLista.Add(alkatresz);
    }
    public double AutoarSzamolasa()
    {
        double Autoar = 0;
        foreach (Alkatresz item in AlkatreszekLista)
        {
            Autoar += item.getAlkatreszAra();
        }
        Console.WriteLine($"{Autoar:C1}");
        return Autoar;
    }
    public bool AutoKesz()
    {
        int Ajtoszam = 0;
        int Alvazszam = 0;
        int Karosszeriaszam = 0;
        int Motorszam = 0;
        foreach (Alkatresz item in AlkatreszekLista)
        {
            switch (item.tipus)
            {
                case AlkatreszType.Ajto:
                    Ajtoszam++;
                    break;
                case AlkatreszType.Alvaz:
                    Alvazszam++;
                    break;
                case AlkatreszType.Karosszeria:
                    Karosszeriaszam++;
                    break;
                case AlkatreszType.Motor:
                    Motorszam++;
                    break;
                default:
                    break;
            }
        }
        return Ajtoszam == 4 && Alvazszam == 1 && Karosszeriaszam == 1 && Motorszam == 1;
    }
}