namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("bdq")]
    public class Bdq
    {
        [Key]
        [Column("id_bdq")]
        public string? IdBdq { get; set; }

        [Column("titre")]
        public string? Titre { get; set; }

        [ForeignKey("IdTypeBordereau")]
        public virtual TypeBordereau? TypeBordereau { get; set; }

        [Column("id_type_bordereau")]
        public string? IdTypeBordereau { get; set; }

        [Column("etat")]
        public int? Etat { get; set; }

        [ForeignKey("IdProprietaire")]
        public virtual Proprietaire? Proprietaire { get; set; }

        [Column("id_proprietaire")]
        public string? IdProprietaire { get; set; }

        [ForeignKey("IdUtilisateur")]
        public virtual Utilisateur? Utilisateur { get; set; }

        [Column("id_utilisateur")]
        public string? IdUtilisateur { get; set; }
    }
}
