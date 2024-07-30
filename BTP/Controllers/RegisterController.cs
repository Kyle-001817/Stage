using Microsoft.AspNetCore.Mvc;
using BTP.Models;

namespace BTP.Controllers;
public class RegisterController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K_Context _context;

    public RegisterController(ILogger<HomeController> logger, K_Context context) 
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult F_Inscription()
    {
        ViewData["genre"] = Genre.GetAllGenre(_context);
        return View();
    }
    public IActionResult InsertProfil(string nom, string prenom, DateTime date_naissance, string adresse, string genre)
    {
        DateTime dateNaissanceUtc = date_naissance.Kind == DateTimeKind.Utc ? date_naissance : date_naissance.ToUniversalTime();
        Profil profil = new()
        {
            Nom = nom,
            Prenom = prenom,
            DateNaissance = dateNaissanceUtc,
            Adresse = adresse,
            Genre = _context.Genre.Find(genre)
        };

        _context.Profil.Add(profil);
        _context.SaveChanges();
        string? id_profil = profil.IdProfil;
        HttpContext.Session.SetString("id_profil", id_profil);
        return RedirectToAction(nameof(F_Utilisateur));
    }
    public IActionResult F_Utilisateur(){
        return View();
    }
    public IActionResult InsertUtilisateur(string email,string mdp_initial,string mdp_final){
        string? id_profil = HttpContext.Session.GetString("id_profil");
        if(Utilisateur.VerificationMdp(mdp_initial,mdp_final) == false)
        {
            TempData["ErrorMessage"] = "Les mots de passe que avez entrer ne sont pas identique, Verifier svp!!!";
            return RedirectToAction("ErrorPage","Error");
        }
        Utilisateur user = new(){
            Email = email,
            Mdp = mdp_initial,
            Profil = _context.Profil.Find(id_profil)
        };
        _context.Utilisateur.Add(user);
        _context.SaveChanges();
        return RedirectToAction("LoginAdmin","Login");
    }

}