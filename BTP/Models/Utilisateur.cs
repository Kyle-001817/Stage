namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("utilisateur")]
    public class Utilisateur
    {
        [Key]
        [Column("id_utilisateur")]
        public string? IdUtilisateur { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("mdp")]
        public string? Mdp { get; set; }

        [ForeignKey("id_profil")]
        [Column("id_profil")]
        public Profil? Profil { get; set; }

        public Utilisateur? Login(string email, string mdp, K_Context co)
        {
            Utilisateur? admin = co.Utilisateur.FirstOrDefault(a => a.Email == email && a.Mdp == mdp);
            return admin;
        }
        public static bool VerificationMdp(string mdp_initial,string mdp_final){
            bool mdp = false;
            if(mdp_initial == mdp_final)
            {
                mdp = true;
            }
            return mdp;
        }
    }
}
