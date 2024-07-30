namespace BTP.Models{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("genre")]
public class Genre {
    [Key]
    [Column("id_genre")]
    public string? IdGenre{ get; set; }
    
    [Column("nom")]
    public string? Nom{ get; set; }

    public static List<Genre> GetAllGenre(K_Context _context){
        List<Genre> ans = [.. _context.Genre];
        return ans;
    }
}
}