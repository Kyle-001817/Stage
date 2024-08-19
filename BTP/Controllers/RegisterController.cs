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
    public IActionResult ForgetPass(){
        return View();
    }
    public IActionResult SendCodeConfirmation(string email) {
        HttpContext.Session.SetString("email", email);
        GenerateCode.SendCode(email);
        return View();
    }
    public IActionResult IsCodeValide(int code) 
    {
        if (GenerateCode.IsCode(code) == false)
        {
            TempData["ErrorMessage"] = "Code de confirmation Invalide, Veuillez verifier s'il vous plait";
            return RedirectToAction(nameof(ForgetPass));
        }
        return RedirectToAction(nameof(F_NewCode));
    }
    public IActionResult F_NewCode() {
        return View();
    }
    public IActionResult Update_code(string pass1, string pass2)
    {
        // Vérifiez si les deux mots de passe sont identiques
        if (pass1 == pass2)
        {
            string? email = HttpContext.Session.GetString("email");

            // Rechercher l'utilisateur par son email
            Utilisateur? utilisateurExistant = _context.Utilisateur.FirstOrDefault(u => u.Email == email);

            if (utilisateurExistant != null)
            {
                // Mettre à jour le mot de passe et le profil
                utilisateurExistant.Mdp = pass1;
                utilisateurExistant.Profil = _context.Profil.Find(email);

                // Sauvegarder les changements
                _context.Utilisateur.Update(utilisateurExistant);
                _context.SaveChanges();

                // Rediriger vers la page de connexion après la mise à jour réussie
                return RedirectToAction("LoginAdmin", "Login");
            }
            else
            {
                TempData["ErrorMessage"] = "Utilisateur non trouvé avec cet email";
            }
        }
        else
        {
            // Si les mots de passe ne sont pas identiques, définir un message d'erreur
            TempData["ErrorMessage"] = "Les deux mots de passe ne sont pas identiques";
        }

        // Si une erreur s'est produite, rediriger vers la page F_NewCode
        return RedirectToAction(nameof(F_NewCode));
    }
}