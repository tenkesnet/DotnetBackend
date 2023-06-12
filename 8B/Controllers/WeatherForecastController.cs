using _8B.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace _8B.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        List<Tanulo> Tanulok = new List<Tanulo>();
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            Tanulok.Add(new Tanulo("Kiss Roland", DateTime.Parse("2009-04-16"),"fiú",4.27));
            Tanulok.Add(new Tanulo("Nagy Emese", DateTime.Parse("2010-07-21"),"lány",4.68));
            Tanulok.Add(new Tanulo("Szabó Tamás", DateTime.Parse("2008-10-06"), "fiú", 3.42));
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
            return Tanulok;
        }
        
        [HttpPost]
        public Tanulo setDiakok(Tanulo tanulo)
        {
            tanulo.Nev += "!";
            tanulo.TanAtlag +=0.2;
            return tanulo;
        }
    }
}