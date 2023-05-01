namespace ex5
{
    public class KetDimenzios
    {
        public List<IAlakzat> alakzatok { get; set; }
        public KetDimenzios()
        {
            alakzatok = new List<IAlakzat>();
        }
        public void addAlakzat(IAlakzat a)
        {
            alakzatok.Add(a);
        }

        public void display()
        {
            foreach (var a in alakzatok)
            {
                Console.WriteLine($"{a.GetType().Name} : Kerulet: {a.getKerulet()} Terület: {a.getTerulet()}");

            }
        }
    }
}