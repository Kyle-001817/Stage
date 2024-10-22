using CsvHelper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;

namespace BTP.Models
{
    [Table("rendement")]
    public class Rendement
    {
        [Key]
        [Column("num_prix")]
        public string? NumPrix { get; set; }

        [Column("designation")]
        public string? Designation { get; set; }

        [Column("unite")]
        public string? Unite { get; set; }

        [Column("heure_unite")]
        public double Heure_Unite { get; set; }

        [Column("unite_jour")]
        public double Unite_Jour { get; set; }

        public static Rendement MapRendement(CsvReader csv)
        {
            return new Rendement
            {
                NumPrix = csv.GetField<string>("NUM PRIX"),
                Designation = csv.GetField<string>("DESIGNATION DES TRAVAUX"),
                Unite = csv.GetField<string>("UNITE"),
                Heure_Unite = string.IsNullOrWhiteSpace(csv.GetField<string>("RENDEMENT HEURE/UNITE"))
                    ? 0 // Ou une autre valeur par défaut
                    : Convert.ToDouble(csv.GetField<string>("RENDEMENT HEURE/UNITE").Replace(',', '.'), CultureInfo.InvariantCulture),
                Unite_Jour = string.IsNullOrWhiteSpace(csv.GetField<string>("RENDEMENT PAR UNITE/JOUR"))
                    ? 0 // Ou une autre valeur par défaut
                    : Convert.ToDouble(csv.GetField<string>("RENDEMENT PAR UNITE/JOUR").Replace(',', '.'), CultureInfo.InvariantCulture)
            };
        }
    }
}
