using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("detail_bde")]
    public class Detail_bde
    {
        [Key]
        [Column("id_detail_bde")]
        public string? IdDetailBde { get; set; }

        [Column("prix_unitaire")]
        public double PrixUnitaire { get; set; }

        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }

        [ForeignKey("IdDetailBdq")]
        public virtual DetailBdq? DetailBdq { get; set; }
    }
}
