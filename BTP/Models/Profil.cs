namespace BTP.Models{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("profil")]
public class Profil {
    [Key]
    [Column("id_profil")]
    public string? IdProfil{ get; set; }

    [Column("nom")]
    public string? Nom{ get; set; }

    [Column("prenom")]
    public string? Prenom{ get; set; }


    [Column("date_naissance")]
    public DateTime DateNaissance{ get; set; }


    [Column("adresse")]
    public string? Adresse{ get; set; }


    [ForeignKey("id_genre")]
    [Column("id_genre")]
    public Genre? Genre{ get; set; }

}
}
