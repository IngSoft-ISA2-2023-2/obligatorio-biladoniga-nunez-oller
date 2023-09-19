using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PharmacyBasicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PharmacyBasicModel(Pharmacy pharmacy)
        {
            Id = pharmacy.Id;
            Name = pharmacy.Name;
        }
    }
}
