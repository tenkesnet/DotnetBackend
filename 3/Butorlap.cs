namespace Butorgyar
{
    public class Butorlap
    {
        private double mennyiseg;
        public ButorlapTipus tipus;

        public Butorlap(double mennyiseg, ButorlapTipus tipus)  //Butorlap osztály konstruktora
        {
            this.mennyiseg = mennyiseg;
            this.tipus = tipus;
        }

        public double getAr()       //A felhasznált bútorlapok árának kiszámítását végző metódus 
        {
            double Ar;

            Ar = mennyiseg * tipus.getPrice();

            return Ar;
        }
        public string getName()
        {
            return tipus.getName();
        }
        public double getTipusAr()
        {
            return tipus.getPrice();
        }
        public double getMennysiseg()
        {
            return mennyiseg;
        }
    }
}