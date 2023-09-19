using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UnitMeasureBasicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UnitMeasureBasicModel(UnitMeasure unitMeasure)
        {
            Id = unitMeasure.Id;
            Name = unitMeasure.Name;
        }
    }
}
