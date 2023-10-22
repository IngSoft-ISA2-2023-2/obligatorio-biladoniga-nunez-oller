using PharmaGo.Domain.Entities;

namespace PharmaGo.IBusinessLogic
{
    public interface IProductManager
    {
        void Delete(int id);
        List<Product> GetProducts();
        Product UpdateProduct(int id, Product product);
    }
}
