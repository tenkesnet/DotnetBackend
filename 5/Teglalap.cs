namespace ex5
{
    public class Teglalap : IAlakzat
    {
        public double a { get; set; }
        public double b { get; set; }
        public Teglalap(double a, double b)
        {
            this.a = a;
            this.b = b;
        }


        public double getKerulet()
        {
            return 2 * a + 2 * b;
        }

        public double getTerulet()
        {
            return a * b;
        }
    }
}