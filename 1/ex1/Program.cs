using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog1 = new Dog("Cézár", "Barna", 300, 40);
            Dog dog2 = new Dog("Roti", "Barna", 250, 50);
            int iKorokSzama=0;
            int ActualEletero1=dog1.Eletero;
            int ActualEletero2=dog2.Eletero;
            Console.WriteLine(ActualEletero1);
            while (dog1.Harcol(ActualEletero1) && dog2.Harcol(ActualEletero2))
            {
                iKorokSzama++;
                ActualEletero1=dog1.Sebzes(ActualEletero1,dog2.Harciero);
                ActualEletero2=dog2.Sebzes(ActualEletero2,dog1.Harciero);
                dog1.Harcol(ActualEletero1);
                Console.WriteLine($"{iKorokSzama}. kör. Az állás: {dog1.name} {ActualEletero1} {dog2.name} {ActualEletero2}.");
            }
            if (dog1.Harcol(ActualEletero1)==false && dog2.Harcol(ActualEletero2)==true)
                Console.WriteLine($"A harcot a {dog2.name} nevű kutya nyerte {iKorokSzama} menetben. Életereje {ActualEletero2}. A legyőzött kutyáé {ActualEletero1}.");
            if (dog2.Harcol(ActualEletero2)==false && dog1.Harcol(ActualEletero1)==true)
                Console.WriteLine($"A harcot a {dog1.name} nevű kutya nyerte {iKorokSzama} menetben. Életereje {ActualEletero1}. A legyőzött kutyáé {ActualEletero2}.");
            if (dog2.Harcol(ActualEletero2)==false && dog1.Harcol(ActualEletero1)==false){
                if (ActualEletero1>ActualEletero2)
                    Console.WriteLine($"A harcot a {dog1.name} nevű kutya nyerte {iKorokSzama} menetben. Életereje {ActualEletero1}. A legyőzött kutyáé {ActualEletero2}.");
                if (ActualEletero2>ActualEletero1)
                    Console.WriteLine($"A harcot a {dog2.name} nevű kutya nyerte {iKorokSzama} menetben. Életereje {ActualEletero2}. A legyőzött kutyáé {ActualEletero1}.");
            }
        }
    }
}