namespace ex5
{

    public class Kor : IAlakzat
    {
        public double r { get; set; }
        public Kor(double r)
        {
            this.r = r;
        }


        public double getKerulet()
        {
            return 2 * r * Math.PI;
        }

        public double getTerulet()
        {
            return r * r * Math.PI;
        }
    }
}