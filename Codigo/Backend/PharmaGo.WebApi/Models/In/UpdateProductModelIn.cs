using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.In
{
    public class UpdateProductModelIn
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public Product ToEntity()
        {
            return new Product()
            {
                Name = Name,
                Description = Description,
                Price = Price ?? 0
            };
        }
    }
}
