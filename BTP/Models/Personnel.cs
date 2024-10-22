using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("personnel")]

    public class Personnel
    {
        [Key]
        [Column("id_personnel")]
        public string? IdPersonnel { get; set; }

        [Column("rendement")]
        public double? Rendement { get; set; }

        [Column("jour_travail")]
        public int Jour_travail { get; set; }

        [Column("personnel")]
        public string? Personnels { get; set; }

        [Column("heure_travail")]
        public double? Heure_travail { get; set; }

        [Column("salaire_par_heure")]
        public double? Salaire_parHeure { get; set; }

        [ForeignKey("IdDetailBdq")]
        public virtual DetailBdq? DetailBdq { get; set; }

        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }
    }
}
