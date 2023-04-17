
namespace MyApp
{

    public class Animal
    {
        internal String name;
        internal String color;

        // public Animal()
        // {

        // }
        public Animal(String nev, String szin)
        {
            name = nev;
            color = szin;
        }
        public void speak()
        {
            Console.WriteLine("A nevem: " + name);
        }

        public void speak(String hang)
        {
            Console.WriteLine(hang);
        }

        public void speak(int a)
        {
            Console.WriteLine(a * 2);
        }

        public String speak(int b, int a)
        {
            return "";
        }

        // override public String ToString()
        // {
        //     return "A ToString feluldefiniálása miatt. A nevem: " + name;
        // }

    }
}