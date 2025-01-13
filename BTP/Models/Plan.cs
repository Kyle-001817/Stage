using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("plan")]

    public class Plan
    {
        [Key]
        [Column("id_plan")]
        public string? IdPlan { get; set; }

        [Column("titre")]
        public string? Titre { get; set; }

        [Column("emplacement")]
        public string? Emplacement { get; set; }

        [ForeignKey("IdBdq")]
        public virtual Bdq? Bdq { get; set; }

        [Column("id_bdq")]
        public string? IdBdq { get; set; }
    }
}
