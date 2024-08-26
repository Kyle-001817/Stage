using Microsoft.AspNetCore.Mvc;
using BTP.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public IActionResult F_typeMateriel() {
        return View();
    }
    public IActionResult Insert_typeMateriel(string materiel) {
        TypeMateriel tm = new()
        {
            Nom = materiel
        };
        _context.TypeMateriel.Add(tm);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_typeMateriel));
    }
    // Dans le contr�leur Materiaux
    public IActionResult Materiaux(string idBdq, string searchTerm, int pageNumber = 1, int pageSize = 10)
    {
        if (!string.IsNullOrEmpty(idBdq))
        {
            HttpContext.Session.SetString("id_bdq", idBdq);
        }
        else
        {
            idBdq = HttpContext.Session.GetString("id_bdq");
        }

        ViewData["type_materiaux"] = _context.TypeMateriel.ToList();

        List<Materiel> typeMat = Materiel.GetListTypeMaterielExistant(_context, idBdq);
        ViewData["type_materiauxbyBdq"] = typeMat;
        var materiauxByType = new Dictionary<string, List<Materiel>>();
        int totalItems = 0;

        for (int i = 0; i < typeMat.Count; i++)
        {
            string idTypeMateriel = typeMat[i].IdTypeMateriel;
            List<Materiel> materiaux = Materiel.GetmaterielsbyIdBdq(_context, idBdq, idTypeMateriel);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                materiaux = materiaux.Where(m =>
                    m.Nom.ToLower().Contains(searchTerm) ||
                    m.IdUnite.ToLower().Contains(searchTerm) ||
                    m.PrixUnitaire.ToString().Contains(searchTerm) ||
                    m.TypeMateriel.Nom.ToLower().Contains(searchTerm)
                ).ToList();
            }

            totalItems += materiaux.Count;
            materiaux = materiaux.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            materiauxByType[idTypeMateriel] = materiaux;
        }

        ViewData["materiaux_byBdq"] = materiauxByType;
        ViewData["searchTerm"] = searchTerm;
        ViewData["pageNumber"] = pageNumber;
        ViewData["pageSize"] = pageSize;
        ViewData["idBdq"] = idBdq;
        ViewData["totalItems"] = totalItems;

        if (TempData["ShowSecondModal"] != null && (bool)TempData["ShowSecondModal"])
        {
            ViewBag.ShowSecondModal = true;
        }

        return View();
    }

    [HttpPost]
    public IActionResult SecondModal(string idmateriel)
    {
        HttpContext.Session.SetString("id_materiel", idmateriel);
        ViewBag.IdMateriel = idmateriel;
        ViewBag.ShowSecondModal = true;
        ViewData["unite"] = _context.Unite.ToList();

        // Charger les donn�es n�cessaires pour la vue Materiaux
        string idBdq = HttpContext.Session.GetString("id_bdq");
        List<Materiel> typeMat = Materiel.GetListTypeMaterielExistant(_context, idBdq);
        ViewData["type_materiauxbyBdq"] = typeMat;
        var materiauxByType = new Dictionary<string, List<Materiel>>();
        for (int i = 0; i < typeMat.Count; i++)
        {
            string idTypeMateriel = typeMat[i].IdTypeMateriel;
            List<Materiel> materiaux = Materiel.GetmaterielsbyIdBdq(_context, idBdq, idTypeMateriel);
            materiauxByType[idTypeMateriel] = materiaux;
        }
        ViewData["materiaux_byBdq"] = materiauxByType;
        return View("Materiaux");
    }

    [HttpPost]
    public IActionResult InsertMateriel(string nom, string unite, double pu)
    {
        string? idMat = HttpContext.Session.GetString("id_materiel");
        string? idBdq = HttpContext.Session.GetString("id_bdq");

        Materiel mtrx = new()
        {
            Nom = nom,
            IdUnite = unite,
            PrixUnitaire = pu,
            IdBdq = idBdq,
            IdTypeMateriel = idMat
        };

        _context.Materiel.Add(mtrx);
        _context.SaveChanges();

        // Marquer que le second modal doit rester ouvert
        TempData["ShowSecondModal"] = true;

        // Redirection vers l'action Materiaux pour �viter la r�p�tition de l'insertion sur actualisation
        return RedirectToAction("Materiaux", new { idBdq = idBdq });
    }
    public IActionResult Suppression(string id)
    {
        var materiel = _context.Materiel.Find(id);
        if (materiel != null)
        {
            _context.Materiel.Remove(materiel);
            _context.SaveChanges();
        }

        string? idBdq = HttpContext.Session.GetString("id_bdq");
        return RedirectToAction("Materiaux", new { idBdq = idBdq });
    }
    public IActionResult Modification(string id)
    {
        var materiel = _context.Materiel.Find(id);
        if (materiel == null)
        {
            return NotFound();
        }
        ViewData["materiel"] = materiel;
        ViewData["unite"] = _context.Unite.ToList();

        return View("Modification");
    }
    [HttpPost]
    public IActionResult UpdateMateriel(string IdMateriel, string Nom, string IdUnite, double PrixUnitaire)
    {
        var materiel = _context.Materiel.Find(IdMateriel);
        if (materiel != null)
        {
            materiel.Nom = Nom;
            materiel.IdUnite = IdUnite;
            materiel.PrixUnitaire = PrixUnitaire;

            _context.Materiel.Update(materiel);
            _context.SaveChanges();
        }

        string? idBdq = HttpContext.Session.GetString("id_bdq");
        return RedirectToAction("Materiaux", new { idBdq = idBdq });
    }

    //BDQ-AJOUT TRAVAUX
    public IActionResult F1_AjoutTravaux(string IdBdq)
    {
        HttpContext.Session.SetString("id_bdq", IdBdq);
        ViewData["serie_travaux"] = _context.SerieTravaux.ToList();
        return View(); 
    }



    public IActionResult Deconnect()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginAdmin", "Login");
    }






}