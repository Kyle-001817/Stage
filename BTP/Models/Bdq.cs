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
    }
}
