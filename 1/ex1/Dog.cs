
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
        public bool Harcol(Dog ellenfel)
        {
            ellenfel.Sebzes(Harciero);
            this.Sebzes(ellenfel.Harciero);

            if (ellenfel.Eletero <= 0 || this.Eletero <= 0)
            {
                return true;
            }
            return false;
        }
        public int Sebzes(int Ero)
        {
            Random ValosHarciEro = new Random();
            Eletero = Eletero - ValosHarciEro.Next(1, Ero);
            return Eletero;
        }
        public new void speak()
        {
            Console.WriteLine("Vau vau a nevem: " + name);
            Console.WriteLine($"{Eletero},{Harciero}");
        }

    }
}
