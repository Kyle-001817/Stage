using Microsoft.AspNetCore.Mvc;
using BTP.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

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
        List<TypeBordereau> val = _context.TypeBordereau.ToList();
        ViewData["type_bordereau"] = val;
        return View();
    }
    public IActionResult Insert_bdq(string id_typeBordereau,string titre)
    {
        Bdq bdq = new()
        {
            Titre = titre,
            IdTypeBordereau = id_typeBordereau,
        };
        _context.Bdq.Add(bdq);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_bdq));
    }
    public IActionResult F_convention()
    {
        return View();
    }
    public IActionResult F_typeBordereau()
    {
        return View();
    }
    public IActionResult Insert_TypeBordereau(string type) {
        TypeBordereau tb = new() { 
            Nom = type
        };
        _context.TypeBordereau.Add(tb);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_typeBordereau));
    }
    public IActionResult BDQ()
    {
        List<Bdq> bdq = _context.Bdq
            .Include(t =>t.TypeBordereau)
            .ToList();
        ViewData["bdq"] = bdq;
        return View();
    }
    public IActionResult F_serie_Typebordereau()
    {
        ViewData["type_bordereau"] = _context.TypeBordereau.ToList();
        return View();
    }
    public IActionResult InsertSerie_TypeBordereau(string? id_type, string? serie)
    {
        SerieTravaux s = new()
        {
            IdTypeBordereau = id_type,
            Nom = serie
        };
        _context.SerieTravaux.Add(s);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_serie_Typebordereau));
    }
    public IActionResult Materiaux(string idBdq) 
    {
        Bdq? bdqItem = _context.Bdq.Find(idBdq);
        Console.WriteLine(bdqItem?.Titre);        
        return View();
    }

}