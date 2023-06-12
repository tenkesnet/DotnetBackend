namespace Tanulok.Model
{
    public class Tanar :Szemely
    {

        public string foTantargy { get; set; }
        public Tanar ()
        {
            Lakcim = new Cim();
            LakcimLista = new List<Cim>();
        }
    }
}
