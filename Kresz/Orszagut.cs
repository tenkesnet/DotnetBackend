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
            jarmuvekJonnek("D:\\Teszt\robogo");
            kiketMertünkBe();

        }
        static void jarmuvekJonnek(string fajlUtvonal)
        {
            StreamReader beolvas=new StreamReader(fajlUtvonal);
            do
            {
                string sor=beolvas.ReadLine();
                string[] tomb = sor.Split(';');
                if (tomb[0]=="robogo")
                {
                    Robogo jarmu=new Robogo(int.Parse(tomb[2]), tomb[1], int.Parse(tomb[3]));
                    jarmuLista.Add(jarmu);
                }
                else
                {
                    AudiS8 jarmu = new AudiS8(int.Parse(tomb[2]), tomb[1], bool.Parse(tomb[3]));
                    jarmuLista.Add(jarmu);
                }
            } while (!beolvas.EndOfStream);
            beolvas.Close();
        }
        static void kiketMertünkBe()
        {
            StreamWriter kiir = new StreamWriter("d:\\Teszt\buntetes.txt");
            foreach (Jarmu jarmu in jarmuLista)
            {
                string stringObj;
                stringObj = jarmu.GetType().Name;
                if (jarmu.GetType() == typeof(Robogo))
                {
                    stringObj=stringObj+typeof(Robogo).;
                }
                else
                {
                    
                }
                kiir.WriteLine(stringObj);
                kiir.Close();
            }
        }
    }
}

