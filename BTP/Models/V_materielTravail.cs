using Microsoft.EntityFrameworkCore;

namespace BTP.Models
{
    public class V_materielTravail
    {
        public string? IdDetailDbq { get; set; }
        public double? TotalPrixMateriel { get; set; }
        public double? TotalSalairePersonnel { get; set; }
        public double? TotalGeneral { get; set; }
        public double? Benefice { get; set; }
        public double? Montant { get; set; }

        public static V_materielTravail Get_MontanBDE(K_Context _context, double pourcentage, string id_detail_bdq)
        {
            var resultat = (from m in
                                (from mt in _context.MaterielTravail
                                 group mt by mt.IdDetailBdq into grp
                                 select new
                                 {
                                     IdDetailDbq = grp.Key,
                                     TotalPrixMateriel = grp.Sum(mt => mt.Prix)
                                 })
                            join v in _context.V_SalairePersonnel on m.IdDetailDbq equals v.IdDetailBdq into joinedData
                            from v in joinedData.DefaultIfEmpty()
                            where m.IdDetailDbq == id_detail_bdq
                            group new { m, v } by m.IdDetailDbq into grp
                            select new V_materielTravail
                            {
                                IdDetailDbq = grp.Key,
                                TotalPrixMateriel = grp.Select(x => x.m.TotalPrixMateriel).FirstOrDefault(), // Assuming only one total price per group
                                TotalSalairePersonnel = grp.Sum(x => x.v.Salaire ?? 0), // Use null-coalescing operator
                                TotalGeneral = grp.Select(x => x.m.TotalPrixMateriel).FirstOrDefault() + grp.Sum(x => x.v.Salaire ?? 0),
                                Benefice = (grp.Select(x => x.m.TotalPrixMateriel).FirstOrDefault() + grp.Sum(x => x.v.Salaire ?? 0)) * pourcentage / 100,
                                Montant = (grp.Select(x => x.m.TotalPrixMateriel).FirstOrDefault() + grp.Sum(x => x.v.Salaire ?? 0)) +
                                          ((grp.Select(x => x.m.TotalPrixMateriel).FirstOrDefault() + grp.Sum(x => x.v.Salaire ?? 0)) * pourcentage / 100)
                            }).AsNoTracking().FirstOrDefault();

            return resultat;
        }




    }
}
