using Kartya;

namespace Kartya
{
    public class Program
    {
        static void Main(string[] args)
        {
            int pakliMeret;
            int megnezendoLapSzam;
            Pakli pakli;
            bool succes=false;
            do
            {
                Console.WriteLine("Add meg a pakli méretét!");
                string pakliMerete = (Console.ReadLine());
                //succes = int.TryParse(pakliMerete, out pakliMeret);
               try
                {
                    pakliMeret = Convert.ToInt32(pakliMerete);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Számot kérek megadni!");
                    pakliMeret=0;
                }
            } while (pakliMeret==0);
            do
            {
                Console.WriteLine("Add meg, hogy mennyi lapot nézzünk meg a pakliból!");
                string megnezendoLapokSzama = (Console.ReadLine());
                succes = int.TryParse(megnezendoLapokSzama, out megnezendoLapSzam);
            } while (succes == false);
            if (pakliMeret > 0)
            {
                pakli = new Pakli(pakliMeret, "fekete");
            }
            else
            {
                pakli = new Pakli();
            }
            pakli.osszesLapKiirasa();
            pakli.blackJackLight(megnezendoLapSzam);
        }
    }
}

