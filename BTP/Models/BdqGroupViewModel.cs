namespace BTP.Models
{
    public class BdqGroupViewModel
    {
        public string IdSerieTravaux { get; set; }
        public SerieTravaux SerieTravaux { get; set; }
        public Bdq Bdq { get; set; }
        public List<BdqViewModel> Bdqs { get; set; }
        public double? SousTotal { get; set; }
    }
}
