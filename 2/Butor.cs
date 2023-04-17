namespace Butorgyar
{
    public class Butor
    {
        String name;
        public List<Butorlap> butorlapok = new List<Butorlap>();

        public Butor(string nev)
        {
            this.name = nev;
        }
        public void addButorlap(Butorlap butorlap)
        {
            butorlapok.Add(butorlap);
        }

        public double getAr()
        {
            double osszar = 0;
            foreach (Butorlap item in butorlapok)
            {
                osszar = osszar + item.getAr();
            }
            return osszar;
        }
    }
}