using Microsoft.AspNetCore.Mvc;
using BTP.Models;

namespace BTP.Controllers;
public class ErrorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K_Context _context;

    public ErrorController(ILogger<HomeController> logger, K_Context context) 
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult ErrorPage()
    {
        return View();
    }
}