using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ex7.Models;

namespace ex7.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(long id)
    {
        ViewData["szam"] = id;
        Person a = new Person("Kovács Zsolt");
        ViewData["szemely"] = a;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
