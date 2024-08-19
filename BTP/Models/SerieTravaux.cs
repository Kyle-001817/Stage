namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("serie_travaux")]
    public class SerieTravaux
    {
        [Key]
        [Column("id_serie_travaux")]
        public string? IdSerieTravaux { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }

        [ForeignKey("IdTypeBordereau")]
        public virtual TypeBordereau? TypeBordereau { get; set; }

        [Column("id_type_bordereau")]
        public string? IdTypeBordereau { get; set; }
    }
}
