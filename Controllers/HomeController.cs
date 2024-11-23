using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MockInterviews.Models;

namespace MockInterviews.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var languages = new Dictionary<string, string>
        {
            {"Java", "card text-center shadow-sm rounded border-primary border-2"},
            {"C#", "card text-center shadow-sm rounded border-success border-2"},
            {"Python", "card text-center shadow-sm rounded border-danger border-2"},
            {"SQL", "card text-center shadow-sm rounded border-warning border-2"},
            {"JavaScript", "card text-center shadow-sm rounded border-info border-2"},
            {"ReactJS", "card text-center shadow-sm rounded border-dark border-2"},
            {"Angular", "card text-center shadow-sm rounded border-secondary border-2"},
            {"HTML/CSS", "card text-center shadow-sm rounded border-primary border-2"}

        };
        return View(languages);
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

