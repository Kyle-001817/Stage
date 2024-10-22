using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("v_bde")]
    public class V_bde
    {
        [Column("designation")]
        public string? Designation { get; set; }

        [Column("id_unite")]
        public string? IdUnite { get; set; }

        [Column("quantite")]
        public double Quantite { get; set; }

        [Column("prix_unitaire")]
        public double Prix_unitaire { get; set; }

        [Column("id_serie_travaux")]
        public string? IdSerieTravaux { get; set; }

        [Column("id_bdq")]
        public string? IdBdq { get; set; }

        [Column("montant")]
        public double Montant { get; set; }
    }
}
