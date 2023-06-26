using Microsoft.AspNetCore.Mvc;

namespace Tanulok.Entity;
public class Tanulo :Szemely
{
    public Double tanAtlag { get; set; }
    public Lakcim lakcim { get; set; }
    //public Double TanulokOsszesitettAtlaga { get; set; }
    public Tanulo()
    { 
        //Lakcim lakcim = new Lakcim();
    }
    
}