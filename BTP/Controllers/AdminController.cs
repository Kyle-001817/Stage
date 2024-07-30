using Microsoft.AspNetCore.Mvc;
using BTP.Models;

namespace BTP.Controllers;
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K_Context _context;

    public AdminController(ILogger<HomeController> logger, K_Context context) 
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Accueil()
    {
        return View();
    }
    public IActionResult F_bdq()
    {
        return View();
    }
    public IActionResult F_convention()
    {
        return View();
    }
    public IActionResult F_typeBordereau()
    {
        return View();
    }
    public IActionResult BDQ()
    {
        return View();
    }
    public IActionResult F_serie_Typebordereau()
    {
        return View();
    }
}