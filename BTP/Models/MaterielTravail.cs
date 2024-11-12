using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("materiel_travail")]
    public class MaterielTravail
    {
        [Key]
        [Column("id_materiel_travail")]
        public string? IdMaterielTravail { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }

        [Column("quantite")]
        public double? Quantite { get; set; }

        [Column("prix")]
        public double? Prix { get; set; }

        [ForeignKey("IdDetailBdq")]
        public virtual DetailBdq? DetailBdq { get; set; }

        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }
    }
}