namespace Tanulok.Entity
{
    public class Szemely
    {
        public String Name { get; set; }
        public DateTime SzulDatum { get; set; }
        public String Nem { get; set; }
        public Lakcim? lakcim { get; set; }
    }
}
