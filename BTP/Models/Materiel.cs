using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTP.Models
{
    [Table("materiaux")]
    public class Materiel
    {
        [Key]
        [Column("id_materiaux")]
        public string? IdMateriel { get; set; }
        [Column("nom")]
        public string? Nom { get; set; }
        [Column("prix_unitaire")]
        public double PrixUnitaire { get; set; }
        [ForeignKey("IdBdq")]
        public virtual Bdq? Bdq { get; set; }
        [Column("id_bdq")]
        public string? IdBdq { get; set; }

        [ForeignKey("IdUnite")]
        public virtual Unite? Unite { get; set; }

        [Column("id_unite")]
        public string? IdUnite { get; set; }

        [ForeignKey("IdTypeMateriel")]
        public virtual TypeMateriel? TypeMateriel { get; set; }

        [Column("id_type_materiel")]
        public string? IdTypeMateriel { get; set; }

        
        public static List<Materiel> GetListTypeMaterielExistant(K_Context _context, string idBdq)
        {
            List<Materiel> ans = _context.Materiel
                .Where(m => m.IdBdq == idBdq)
                .GroupBy(m => m.IdTypeMateriel)
                .Select(g => g.First())
                .ToList();
            return ans;
        }
        public static List<Materiel> GetmaterielsbyIdBdq(K_Context _context, string idBdq, string idTypeMateriel)
        {
            List<Materiel> materiels = _context.Materiel.Where(m => m.IdBdq == idBdq && m.IdTypeMateriel == idTypeMateriel).ToList();
            return materiels;
        }
        public static IQueryable<Materiel> Search(K_Context context, string searchTerm)
        {
            var materiauxQuery = context.Materiel.Include(m => m.TypeMateriel).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                materiauxQuery = materiauxQuery.Where(m =>
                    m.Nom.ToLower().Contains(searchTerm) ||
                    m.IdUnite.ToLower().Contains(searchTerm) ||
                    m.PrixUnitaire.ToString().Contains(searchTerm) ||
                    m.TypeMateriel.Nom.ToLower().Contains(searchTerm));
            }

            return materiauxQuery;
        }


    }
}
