namespace BTP.Models
{
    public class BdqViewModel
    {
        public string IdDetailBdq { get; set; }
        public List<BdqViewModel> Bdqs { get; set; }
        public string Designation { get; set; }
        public string IdBdq { get; set; }
        public string IdSerieTravaux { get; set; }
        public string NomUniteBdq { get; set; }
        public double? QuantiteBdq { get; set; }
        public List<string> NomMateriaux { get; set; }
        public List<string> NomUniteMateriaux { get; set; }
        public List<double?> QuantiteMateriaux { get; set; }
        public List<double?> PrixUnitaire { get; set; }    
        public List<double?> Montant { get; set; }
    }
}
