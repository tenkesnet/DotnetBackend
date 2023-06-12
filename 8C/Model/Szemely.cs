namespace Tanulok.Model
{
    public class Szemely
    {
        public String Name { get; set; }
        public DateTime SzulDatum { get; set; }
        public String Nem { get; set; }
        public List<Cim> LakcimLista { get; set; }
        public Cim Lakcim { get; set; }
    }
}
