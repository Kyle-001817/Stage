namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("type_bordereau")]
    public class TypeBordereau
    {
        [Key]
        [Column("id_type_bordereau")]
        public string? IdTypeBordereau { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }
    }
}
