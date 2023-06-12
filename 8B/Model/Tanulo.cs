using System.Net;

namespace _8B.Model
{
    public class Tanulo
    {
        public String Nev { get; set; }
        public DateTime SzulDatum { get; set; }
        public String Nem { get; set; }
        public Double TanAtlag { get; set; }
        public List<Cim> LakcimLista { get; set; }
        public Cim Lakcim;
        public Tanulo(string Nev, DateTime SzulDatum, String Nem, Double TanAtlag)
        {
            this.Nev = Nev;
            this.SzulDatum = SzulDatum;
            this.Nem = Nem;
            this.TanAtlag = TanAtlag;
            Lakcim= new Cim();
            LakcimLista = new List<Cim>();
            LakcimLista.Add(Lakcim);
        }
    }
}
