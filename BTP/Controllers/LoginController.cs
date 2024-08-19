using Microsoft.AspNetCore.Mvc;
using BTP.Models;

namespace BTP.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly K_Context _context;

    public LoginController(ILogger<HomeController> logger, K_Context context) 
    {
        _logger = logger;
        _context = context;
    }
    
    public IActionResult LoginAdmin()
    {
        return View();
    }
    public IActionResult TraitementLogin(string email, string mdp)
    {
        Utilisateur admin = new();
        var errors = new Dictionary<string, string>();
        var utilisateur = admin.Login(email, mdp, _context);
        
        if (utilisateur != null)
        {
            var utilisateurId = utilisateur.IdUtilisateur;
            var pro = _context.Utilisateur.SingleOrDefault(p => p.IdUtilisateur == utilisateurId);
            if (pro == null)
            {
                errors.Add("admin", "Admin non trouvé.");
                return Json(new { success = false, errors });
            }
            HttpContext.Session.SetString("id_admin", utilisateurId);
            return Json(new { success = true, redirectUrl = Url.Action("Accueil", "Admin") });
        }
        errors.Add("login", "Email ou mot de passe incorrect.");
        return Json(new { success = false, errors });
    }

}