using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    public class ProductModelOut
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Pharmacy? Pharmacy { get; set; }

        public ProductModelOut(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }
    }
}
