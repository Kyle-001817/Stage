namespace BTP.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("approbation")]
    public class Approbation
    {
        [Key]
        [Column("id_approbation")]
        public string? Id_approbation { get; set; }

        [Column("date_approbation")]
        public DateTime DateApprobation { get; set; }

        [ForeignKey("IdBdq")]
        public virtual Bdq? Bdq { get; set; }

        [Column("id_bdq")]
        public string? IdBdq { get; set; }
    }
}