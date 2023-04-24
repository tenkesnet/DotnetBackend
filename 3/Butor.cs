namespace Butorgyar
{
    public class Butor
    {
        String name;
        public List<Butorlap> ButorlapokLista = new List<Butorlap>();        //Létrehozza a ButorlapokLista nevű listaelemet, amibe a butorlap példányok kerülnek

        public Butor(string nev)            //Butor osztály konstruktora
        {
            this.name = nev;
        }
        public void addButorlap(Butorlap butorlap)          //A metódusnak átadott butorlapot a ButorlapokListaba teszi Add metódussal 
        {
            ButorlapokLista.Add(butorlap);
        }

        public double getAr()                           //Bútor összárának kiszámolását végző getAr metódus
        {
            double osszar = 0;
            foreach (Butorlap item in ButorlapokLista)
            {
                osszar = osszar + item.getAr();
                Console.WriteLine($"{item.getName()} ára : {item.getTipusAr()}*{item.getMennysiseg()}");    //Meghívja az aktuális Bútorlap getAr metódusát
            }
            Console.WriteLine($"Az összár: {osszar:C1}-ba kerül.");
            Console.WriteLine($"A hátlapok száma: {getHatlapSzam()}.");
            return osszar;

        }
        public int getHatlapSzam()
        {
            int hatlapSzam = 0;
            foreach (Butorlap item in ButorlapokLista)
            {
                if (item.tipus is HatlapTipus) hatlapSzam++;
            }
            return hatlapSzam;
        }
        public Boolean valid()
        {
            int ajtoSzam = 0;
            int hatlapSzam = 0;
            foreach (Butorlap item in ButorlapokLista)
            {
                if (item.tipus is HatlapTipus) hatlapSzam++;
                if (item.tipus is AjtoTipus) ajtoSzam++;
            }
            return ajtoSzam >= 2 && hatlapSzam > 1;
        }
    }
}