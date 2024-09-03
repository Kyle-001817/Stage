using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("v_bdq")]
    public class V_bdq
    {
        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }
        [Column("id_serie_travaux")]
        public string? IdSerieTravaux { get; set; }

        [Column("id_bdq")]
        public string? IdBdq { get; set; }

        [Column("nom_unite_dbq")]
        public string? NomUniteBdq { get; set; }

        [Column("quantite_dbq")]
        public double? QuantiteBdq { get; set; }

        [Column("nom_materiaux")]
        public string? NomMateriaux { get; set; }

        [Column("nom_unite_materiaux")]
        public string? NomUniteMateriaux { get; set; }

        [Column("quantite_materiaux")]
        public double? QuantiteMateriaux { get; set; }

        [Column("prix_unitaire")]
        public double? PrixUnitaire { get; set; }

        [Column("montant")]
        public double? Montant { get; set; }
    }
}
