using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("proprietaire")]
    public class Proprietaire
    {
        [Key]
        [Column("id_proprietaire")]
        public string? IdProprietaire { get; set; }

        [Column("lieu")]
        public string? Lieu { get; set; }

        [Column("client")]
        public string? Client { get; set; }

        [Column("adresse")]
        public string? Adresse { get; set; }

        [Column("telephone")]
        public string? Telephone { get; set; }

        [Column("email")]
        public string? Email { get; set; }
    }
}
