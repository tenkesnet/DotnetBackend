namespace _8B.Model
{
    public class Cim
    {
        public string varos { get; set; }
        public string utca { get; set; }
        public int hazszam { get; set; }
        public Cim()
        {
            varos = "Budapest";
            utca = "Liget utca";
            hazszam = 12;
        }
    }
}