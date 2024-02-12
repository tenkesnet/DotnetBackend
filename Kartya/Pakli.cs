using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartya
{
    internal class Pakli
    {
        string joker;
        Kartya[] kartyaPakli;
        int kartyakSzamaPakliban;
        public Pakli() 
        {
            kartyakSzamaPakliban = 52;
            joker = "fekete";
            kartyaPakli = new Kartya[kartyakSzamaPakliban];
            pakliFeltoltes();
        }
        public Pakli (int kartyaSzam, string kapottJoker)
        {
            kartyakSzamaPakliban = kartyaSzam;
            joker = kapottJoker;
            kartyaPakli = new Kartya[kartyakSzamaPakliban];
            pakliFeltoltes();
        }
        void pakliFeltoltes()
        {
            Random kartyaSzinVeletlen = new Random();
            Random kartyaSzamVeletlen = new Random();
            for (int i = 0;i<kartyakSzamaPakliban;++i)
            {
                int j = kartyaSzinVeletlen.Next(3);
                kartyaPakli[i] = new Kartya();
                switch (j) 
                {
                    case 1:
                        kartyaPakli[i].setSzin("piros");
                        break;
                    case 2:
                        kartyaPakli[i].setSzin("fekete");
                        break;
                }
                int k = kartyaSzamVeletlen.Next(2, 15);
                kartyaPakli[i].setSzam(k);
            }
        }
        public Kartya getPakliAdottIndexuKartyaja(int index)
        {
            Kartya kartya = new Kartya();
            if (index > kartyaPakli.Length)
            {
                kartya.setSzam(2);
                kartya.setSzin("fekete");
            }
            kartya=kartyaPakli[index];
            return kartya;
        }
        public int getMaxLapszam()
        {
            int lapSzam = kartyaPakli.Length;
            return lapSzam;
        }
        public void osszesLapKiirasa()
        {
            foreach (Kartya kartyaLap in kartyaPakli)
            {
                Console.WriteLine(kartyaLap);
            }
        }
        public override string ToString()
        {
            return kartyakSzamaPakliban + " lapos kártyapakli " + joker + " joker színnel.";
        }
        public int blackJackLight (int lap)
        {
            int osszeg=0;
            if (lap>kartyakSzamaPakliban)
            {
                lap = kartyakSzamaPakliban;
            }
            for (int i = kartyakSzamaPakliban-1; i >= kartyakSzamaPakliban-lap; i--)
            {
                if (kartyaPakli[i].getSzin()==joker)
                {
                    osszeg = osszeg + kartyaPakli[i].getSzam();
                }
                else osszeg = osszeg + Convert.ToInt32(Math.Round(kartyaPakli[i].getSzam()/2.0));
            }
            Console.WriteLine("Az átnézett lapok számának összege: " + osszeg);
            return osszeg;
        }
        public double blackJackLight (Kartya kartya)
        {
            double osszeg = 0;
            int i = kartyakSzamaPakliban-1;
            while ((kartyaPakli[i].getSzin()!=kartya.getSzin() || kartyaPakli[i].getSzin() != kartya.getSzin() )&& 
                kartyaPakli[i].getSzam() != kartya.getSzam()) 
            {
                if (kartyaPakli[i].getSzin() == joker)
                {
                    osszeg = osszeg + kartyaPakli[i].getSzam();
                }
                else Math.Round(osszeg = osszeg + (kartyaPakli[i].getSzam() / 2));
                i--;
            }
            Console.WriteLine("Az átnézett lapok számának összege: "+ osszeg);
            return osszeg;
        }
        public void egyszinuPakli()
        {
            for (int i = 0; i < kartyakSzamaPakliban; ++i)
            {
                if (kartyaPakli[i].getSzin()!=joker)
                {
                    kartyaPakli[i].setSzin(joker);
                }
            }
        }
    }
}
