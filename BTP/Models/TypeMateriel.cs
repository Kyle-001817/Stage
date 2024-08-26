namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("type_materiel")]
    public class TypeMateriel
    {
        [Key]
        [Column("id_type_materiel")]
        public string? IdTypeMateriel { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }
    }
}

