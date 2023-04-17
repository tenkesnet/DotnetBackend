
namespace MyApp
{
    class Dog : Animal
    {
        public String fajta { get; set; }
        public int Eletero { get; set; }
        public int Harciero { get; set; }
        public bool HarcolniKepes;
        public int UjEro;
        public Dog(String name, String color, int Eletero, int Harciero) : base(name, color)
        {
            this.Eletero = Eletero;
            this.Harciero = Harciero;
        }
        public bool Harcol(int ActualEletero)
        {
            HarcolniKepes=true;
            if (ActualEletero<=0) HarcolniKepes=false;
            return HarcolniKepes;
        }
        public int Sebzes(int Ero, int HarciEro)
        {
            Random ValosHarciEro=new Random();            
            UjEro=Ero-ValosHarciEro.Next(1,HarciEro);
            return UjEro;
        }
        public new void speak()
        {
            Console.WriteLine("Vau vau a nevem: " + name);
            Console.WriteLine($"{Eletero},{Harciero}");
        }
    }
}
