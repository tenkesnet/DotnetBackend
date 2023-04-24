namespace Butorgyar
{
    public class AjtoTipus : ButorlapTipus
    {
        public double tartozekAr { get; set; }
        public AjtoTipus(double tartozekAr) : base("Ajtó", 7000, 40)
        {
            this.tartozekAr = tartozekAr;
        }

        public override double getPrice()
        {
            return base.getPrice() + tartozekAr;
        }
    }
}