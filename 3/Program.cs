using System;

namespace Butorgyar;

public class Program
{
    public static void Main(string[] args)
    {
        AjtoTipus ajto1 = new AjtoTipus(3000);
        Butorlap NaturFarostlemez = new Butorlap(6.25, new HatlapTipus());                   //Butorlap példányok létrehozása
        Butorlap ButorFront = new Butorlap(6.25, ajto1);
        Butorlap ButorKorpusz = new Butorlap(10.5, new KorpuszTipus());
        Butor Konyhaszekreny = new Butor("konyhaszekrény");             //Butor példány létrehozása

        Konyhaszekreny.addButorlap(NaturFarostlemez);                 //Meghívjuk a butor osztály addButorlap metódusát paraméternek átadva a létrehozott bútorlap példányokat
        Konyhaszekreny.addButorlap(ButorFront);
        Konyhaszekreny.addButorlap(ButorKorpusz);
        Konyhaszekreny.addButorlap(new Butorlap(6.25, new HatlapTipus()));
        Konyhaszekreny.addButorlap(new Butorlap(50, new HatlapTipus()));
        Konyhaszekreny.addButorlap(new Butorlap(30, new ButorlapTipus("Tartóelem", 9000, 0)));
        Konyhaszekreny.addButorlap(new Butorlap(6.25, new AjtoTipus(3000)));
        if (Konyhaszekreny.valid())
        {
            Konyhaszekreny.getAr();
        }

    }
}

