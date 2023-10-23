using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    public class UpdateProductModelOut
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public UpdateProductModelOut(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Stock = product.Stock;
        }
    }
}
