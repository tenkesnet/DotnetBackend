using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    class Alkalmazasom
    {
        static void Main(string[] args)
        {
            Dog dog1 = new Dog("Cézár", "Barna", 300, 40);
            Dog dog2 = new Dog("Roti", "Barna", 250, 50);

            int iKorokSzama = 0;
            // Console.WriteLine(ActualEletero1);
            while (dog1.Harcol(dog2))
            {
                iKorokSzama++;
                Console.WriteLine($"{iKorokSzama}. kör. Az állás: {dog1.name} {dog1.Eletero} {dog2.name} {dog2.Eletero}.");
            }
            if (dog1.Eletero <= 0 && dog2.Eletero >= 0)
            {
                Console.WriteLine(@$"A harcot a {dog2.name} nevű kutya nyerte {iKorokSzama}
                 menetben.Életereje {dog2.Eletero}. A legyőzött kutyáé {dog1.Eletero}.");
            }
            else if (dog2.Eletero <= 0 && dog1.Eletero >= 0)
            {
                Console.WriteLine(@$"A harcot a {dog1.name} nevű kutya nyerte {iKorokSzama}
                 menetben. Életereje {dog1.Eletero}. A legyőzött kutyáé {dog2.Eletero}.");
            }
            else
            {
                Console.WriteLine(@$"A harc döntetlen lett a  {iKorokSzama}
                 menetben. {dog1.name} Életereje {dog1.Eletero}. {dog2.name} Életereje {dog2.Eletero}.");
            }
        }
    }
}