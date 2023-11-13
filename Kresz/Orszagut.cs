using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kresz
{ 
    internal class Orszagut
    {
        public static List<Jarmu> jarmuLista = new List<Jarmu>();
        static void Main(string[] args)
        {
            jarmuvekJonnek(@"D:\Teszt\robogo.txt");
            kiketMertunkBe(60);

        }
        static void jarmuvekJonnek(string fajlUtvonal)
        {
            StreamReader beolvas=new StreamReader(fajlUtvonal);
            while (!beolvas.EndOfStream)
            {
                string sor=beolvas.ReadLine();
                string[] tomb = sor.Split(';');
                if (tomb.Length == 4)
                {
                    if (tomb[0] == "robogo")
                    {
                        Robogo jarmu = new Robogo(int.Parse(tomb[2]), tomb[1], int.Parse(tomb[3]));
                        jarmuLista.Add(jarmu);
                    }
                    else
                    {
                        AudiS8 jarmu = new AudiS8(int.Parse(tomb[2]), tomb[1], bool.Parse(tomb[3]));
                        jarmuLista.Add(jarmu);
                    }
                }
            } 
            beolvas.Close();
        }
        static void kiketMertunkBe(int vegsebesseg)
        {
            StreamWriter kiir = new StreamWriter(@"d:\Teszt\buntetes.txt");
            foreach (Jarmu jarmu in jarmuLista)
            { 
                string stringObj=jarmu.ToString();
                if (jarmu != null && jarmu is Robogo)
                {
                    stringObj=stringObj+ ((jarmu as Robogo).haladhatItt(vegsebesseg) ? " Haladhat itt" : " Nem haladhat itt.");
                }
                else
                {
                    stringObj = stringObj + ((jarmu as AudiS8).gyorshajtottE(vegsebesseg) ? " Gyorshajtott " : " Nem gyorshajtott");
                }
                kiir.WriteLine(stringObj);
            }
            kiir.Close();
        }
    }
}

