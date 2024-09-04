using Microsoft.AspNetCore.Mvc;
using BTP.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using System.Data;

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
        bool exists = _context.TypeBordereau
        .Any(st => st.Nom == type);
        if (exists)
        {
            ModelState.AddModelError(string.Empty, "Ce Type de Bordereau existe deja");
            return View("F_typeBordereau");
        }
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
            .OrderBy(t => t.IdBdq)
            .Where(t => t.Etat == 1)
            .ToList();
        ViewData["bdq"] = bdq;
        return View();
    }
    public IActionResult DeleteBdq(string idBdq)
    {
        var Bdq = _context.Bdq.Find(idBdq);
        if (Bdq != null)
        {
            Bdq.Etat = 11;
            _context.Bdq.Update(Bdq);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(BDQ));
    }
    public IActionResult V_bdq(string idBdq)
    {
        var bdqs = _context.V_bdq
            .Where(b => b.IdBdq == idBdq)
            .ToList();

        var serieTravauxIds = bdqs.Select(b => b.IdSerieTravaux).Distinct().ToList();

        var serieTravauxList = _context.SerieTravaux
            .Where(st => serieTravauxIds.Contains(st.IdSerieTravaux))
            .ToDictionary(st => st.IdSerieTravaux);

        var groupedBdq = bdqs
            .GroupBy(b => b.IdSerieTravaux)
            .Select(g => new BdqGroupViewModel
            {
                IdSerieTravaux = g.Key,
                SerieTravaux = serieTravauxList.ContainsKey(g.Key) ? serieTravauxList[g.Key] : null,
                Bdqs = g.GroupBy(b => new { b.Designation, b.NomUniteBdq, b.QuantiteBdq })
                        .Select(gg => new BdqViewModel
                        {
                            Designation = gg.Key.Designation,
                            NomUniteBdq = gg.Key.NomUniteBdq,
                            QuantiteBdq = gg.Key.QuantiteBdq,
                            NomMateriaux = gg.Select(x => x.NomMateriaux).ToList(),
                            NomUniteMateriaux = gg.Select(x => x.NomUniteMateriaux).ToList(),
                            QuantiteMateriaux = gg.Select(x => x.QuantiteMateriaux).ToList(),
                            PrixUnitaire = gg.Select(x => x.PrixUnitaire).ToList(),
                            Montant = gg.Select(x => x.Montant).ToList()
                        }).ToList(),
                SousTotal = g.Sum(b => b.Montant)
            })
            .ToList();

        ViewBag.Bdq = _context.Bdq.Find(idBdq) ?? new Bdq();
        ViewData["groupedBdq"] = groupedBdq;
        return View(groupedBdq);
    }


    public IActionResult F_serie_Typebordereau()
    {
        ViewData["type_bordereau"] = _context.TypeBordereau.ToList();
        return View();
    }
    public IActionResult InsertSerie_TypeBordereau(string? id_type, string? serie)
    {
        bool exists = _context.SerieTravaux
            .Any(st => st.IdTypeBordereau == id_type && st.Nom == serie);

        if (exists)
        {
            ModelState.AddModelError(string.Empty, "Vous avez deja insere cette serie dans ce type de bordereau.");
            ViewData["type_bordereau"] = _context.TypeBordereau.ToList();
            return View("F_serie_Typebordereau");
        }

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
        bool exists = _context.TypeMateriel
        .Any(st => st.Nom == materiel);
        if (exists)
        {
            ModelState.AddModelError(string.Empty, "Ce Type de Materiel existe deja.");
            return View("F_typeMateriel");
        }
            TypeMateriel tm = new()
        {
            Nom = materiel
        };
        _context.TypeMateriel.Add(tm);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_typeMateriel));
    }
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
            ViewData["unite"] = _context.Unite.ToList();
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

        // Charger les données nécessaires pour la vue Materiaux
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

        // Charger la liste des unités
        ViewData["unite"] = _context.Unite.ToList();

        // Marquer que le second modal doit rester ouvert
        TempData["ShowSecondModal"] = true;

        // Redirection vers l'action Materiaux pour éviter la répétition de l'insertion sur actualisation
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
    public IActionResult F_detailBdq(string id_serieTravaux)
    {
        if (!string.IsNullOrEmpty(id_serieTravaux))
        {
            HttpContext.Session.SetString("id_serieTravaux", id_serieTravaux);
        }
        else
        {
            id_serieTravaux = HttpContext.Session.GetString("id_serieTravaux");
        }
        string? id_bdq = HttpContext.Session.GetString("id_bdq");
        SerieTravaux ? st = _context.SerieTravaux.Find(id_serieTravaux);
        Bdq? bdq = _context.Bdq.Find(id_bdq);
        ViewBag.st = st;
        ViewBag.bdq = bdq;

        ViewData["unite"] = _context.Unite.ToList();
        return View();
    }
    public IActionResult Insert_detailBdq(string designation,string id_unite, double quantite)
    {
        string? id_serieTravaux = HttpContext.Session.GetString("id_serieTravaux");
        string? idBdq = HttpContext.Session.GetString("id_bdq");
        DetailBdq detailBdq = new() { 
            Designation = designation,
            Quantite = quantite,
            IdSerieTravaux = id_serieTravaux,
            IdBdq = idBdq,
            IdUnite = id_unite
        };
        _context.DetailBdq.Add(detailBdq);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_detailBdq));
    }
    public IActionResult Details() 
    {
        string? id_bdq = HttpContext.Session.GetString("id_bdq");
        List<DetailBdq> a = _context.DetailBdq.Where(d => d.IdBdq == id_bdq).Include(d => d.Unite).OrderBy(d => d.IdSerieTravaux).ToList();
        ViewData["details"] = a;
        return View();
    }
    public IActionResult F_ajoutMateriaux_inBdq(string idDetailBdq)
    {
        if (!string.IsNullOrEmpty(idDetailBdq))
        {
            HttpContext.Session.SetString("id_detailBdq", idDetailBdq);
        }
        else
        {
            idDetailBdq = HttpContext.Session.GetString("id_detailBdq");
        }
        string? idBdq = HttpContext.Session.GetString("id_bdq");
        ViewData["materiaux"] = _context.Materiel.Where(m => m.IdBdq == idBdq).ToList();
        return View();
    }
    public IActionResult Insert_detailMateriel(string materiel, double quantite)
    {
        string? idDetailBdq = HttpContext.Session.GetString("id_detailBdq");
        DetailMateriaux detailMateriaux = new ()
        {
            IdMateriaux = materiel,
            IdDetailBdq = idDetailBdq,
            Quantite = quantite
        };
        _context.DetailMateriaux.Add(detailMateriaux);
        _context.SaveChanges();
        return RedirectToAction(nameof(F_ajoutMateriaux_inBdq));
    }

    public IActionResult Deconnect()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginAdmin", "Login");
    }
    public IActionResult Reset_dba()
    {
        _context.ResetDatabase(_context);
        return RedirectToAction("LoginAdmin", "Login");
    }

}