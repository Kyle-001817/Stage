namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("detail_materiaux")]
    public class DetailMateriaux
    {
        [Column("id_materiaux")]
        public string? IdMateriaux { get; set; }

        [ForeignKey("IdMateriaux")]
        public virtual Materiel? Materiel { get; set; }

        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }

        [ForeignKey("IdDetailBdq")]
        public virtual DetailBdq? DetailBdq { get; set; }

        [Column("quantite")]
        public double Quantite { get; set; }
    }
}
