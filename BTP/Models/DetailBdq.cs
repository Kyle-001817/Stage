namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("detail_bdq")]
    public class DetailBdq
    {
        [Key]
        [Column("id_detail_dbq")]
        public string? IdDetailBdq { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("quantite")]
        public double Quantite { get; set; }

        [ForeignKey("IdSerieTravaux")]
        public virtual SerieTravaux? SerieTravaux { get; set; }

        [Column("id_serie_travaux")]
        public string? IdSerieTravaux { get; set; }

        [ForeignKey("IdBdq")]
        public virtual Bdq? Bdq { get; set; }

        [Column("id_bdq")]
        public string? IdBdq { get; set; }

        [ForeignKey("IdUnite")]
        public virtual Unite? Unite { get; set; }

        [Column("id_unite")]
        public string? IdUnite { get; set; }
    }
}
