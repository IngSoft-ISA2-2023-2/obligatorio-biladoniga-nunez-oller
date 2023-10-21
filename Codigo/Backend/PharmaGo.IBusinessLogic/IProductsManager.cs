using PharmaGo.Domain.Entities;

namespace PharmaGo.IBusinessLogic
{
    public interface IProductsManager
    {
        public List<Product> GetProducts();
    }
}
