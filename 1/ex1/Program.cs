using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog1 = new Dog("Cézár", "Barna", 300, 40);
            Dog dog2 = new Dog("Roti", "Barna", 250, 50);
            Animal cat = new Animal("Cézár", "Barna");
            Animal allat;
            allat = dog1;
            Console.WriteLine(dog1.GetHashCode());
            Console.WriteLine($"{cat}");
            allat.name = "Bodri";
            dog1.speak();
            // cat.speak("Miau");
            // cat.speak(4);
            //dog.Harcol();

            while (dog1.Harcol(dog2))
            {
                /// körök szám és a támadások paramétere
            }
            // ki a győztes
        }
    }
}