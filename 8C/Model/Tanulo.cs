using Microsoft.AspNetCore.Mvc;

namespace Tanulok.Model;
public class Tanulo :Szemely
{

    public Double TanAtlag { get; set; }
    public Double TanulokOsszesitettAtlaga { get; set; }

    public Tanulo(string Nev, DateTime SzulDatum, String Nem, Double TanAtlag)
    {
        this.Name = Nev;
        this.SzulDatum = SzulDatum;
        this.Nem = Nem;
        this.TanAtlag = TanAtlag;
        Lakcim = new Cim();
        LakcimLista = new List<Cim>();
        LakcimLista.Add(Lakcim);
    }
    public Tanulo()
    {
        Lakcim = new Cim();
        LakcimLista = new List<Cim>();
    }
    
}