using Microsoft.AspNetCore.Mvc;

namespace Tanulok.Entity;
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
        lakcim = new Lakcim();
    }
    public Tanulo()
    { 
        lakcim = new Lakcim();
    }
    
}