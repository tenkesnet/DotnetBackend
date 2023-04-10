
namespace MyApp
{
    class Dog : Animal
    {
        public String fajta { get; set; }
        public int Eletero { get; set; }
        public int Harciero { get; set; }
        public Dog(String name, String color, int Eletero, int Harciero) : base(name, color)
        {
            this.Eletero = Eletero;
            this.Harciero = Harciero;
        }

        public bool Harcol(Dog masik)
        {
            return true;
        }

        public void sebzes(int ero)
        {

        }

        public new void speak()
        {
            Console.WriteLine("Vau vau a nevem: " + name);
        }
    }
}
