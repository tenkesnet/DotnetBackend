namespace Butorgyar
{
    public class ButorlapTipus
    {
        private string name { get; set; }
        public double price { get; set; }

        public double szegelyhossz { get; set; }

        public ButorlapTipus(string nev, double ar, double szegelyhossz)
        {
            name = nev;
            price = ar;
            this.szegelyhossz = szegelyhossz;
        }

        virtual public double getPrice()
        {
            return price + 1.05 * szegelyhossz;
        }

        public double getAmortizacio()
        {
            return price / 4;
        }
        public string getName()
        {
            return name;
        }
    }
}