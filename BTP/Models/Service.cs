using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("service")]
    public class Service
    {
        [Key]
        [Column("id_service")]
        public string? IdService { get; set; }

        [Column("nom")]
        public string? Nom { get; set; }
    }
}
