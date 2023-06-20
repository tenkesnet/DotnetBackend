using Microsoft.AspNetCore.Mvc;
using Tanulok.Entity;
using Tanulok.Service;

namespace Tanulok.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    List<Tanulo> TanulokLista = new List<Tanulo>();
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        TanulokLista.Add(new Tanulo("Kiss Roland", DateTime.Parse("2009-04-16"),"fiú",4.27));
        TanulokLista.Add(new Tanulo("Nagy Emese", DateTime.Parse("2010-07-21"),"lány",4.68));
        TanulokLista.Add(new Tanulo("Szabó Tamás", DateTime.Parse("2008-10-06"), "fiú", 3.42));
        FileDatabase<Tanulo> filedata = new Service.FileDatabase<Tanulo>(@"C:\Users\Kovács Zsolt\Documents\database.txt");
        //filedata.write(new Tanulo("Kiss Roland", DateTime.Parse("2009-04-16"), "fiú", 4.27));
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
        [HttpGet]
        public IEnumerable<Tanulo> Diakok()
        {
        FileDatabase<Tanulo> filedata = new Service.FileDatabase<Tanulo>(@"C:\Users\Kovács Zsolt\Documents\database.txt");
        List<Tanulo> eredmeny = new List<Tanulo>();
        eredmeny.AddRange(filedata.read());
        return eredmeny;
        }
        [HttpGet]
        public IEnumerable<Tanar> Tanarok()
        {
            FileDatabase<Tanar> filedata = new Service.FileDatabase<Tanar>(@"C:\Users\Kovács Zsolt\Documents\database_tanar.txt");
            List<Tanar> eredmeny = new List<Tanar>();
            eredmeny.AddRange(filedata.read());
            return eredmeny;
        }

    [HttpPost]
        public List<Tanulo> setDiakok(Tanulo tanulo)
        {
            double TanulokAtlaganakOsszege=0;
            int TanulokSzama=0;
            Tanulo tan=new Tanulo("Tóth Erika", DateTime.Parse("2009-08-10"), "lány", 3.95);
            //TanulokLista.Add(tanulo);
            TanulokLista.Add(tan);
            foreach (Tanulo tan2 in TanulokLista)
            {
                TanulokSzama++;
                TanulokAtlaganakOsszege+=tan2.TanAtlag;
                tan.TanulokOsszesitettAtlaga=TanulokAtlaganakOsszege/TanulokSzama;
            }
            return TanulokLista;
        }
}
