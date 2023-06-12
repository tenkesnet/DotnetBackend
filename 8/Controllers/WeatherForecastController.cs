using Microsoft.AspNetCore.Mvc;

namespace ex8.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    List<Person> szemelyek = new List<Person>();
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        szemelyek.Add(new Person("Zsolt", DateTime.Parse("1973-02-28")));
        szemelyek.Add(new Person("Robi", DateTime.Parse("1975-02-28")));
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
    public IEnumerable<Person> Peoples()
    {
        return szemelyek;
    }

    [HttpPost]
    public Person setPeoples(Person person)
    {
        person.name +="!";
        person.varosok.Add("Eger");
        return person;

    }

}
