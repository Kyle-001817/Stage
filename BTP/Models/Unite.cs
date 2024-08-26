using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTP.Models
{
    [Table("unite")]
    public class Unite
    {
        [Key]
        [Column("id_unite")]
        public string? IdUnite { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }
    }
}
