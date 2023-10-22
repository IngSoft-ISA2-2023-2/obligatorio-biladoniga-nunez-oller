using PharmaGo.Domain.Entities;

namespace PharmaGo.IBusinessLogic
{
    public interface IProductManager
    {
        void Delete(int id);
        Product Create(Product product, string token);

    }
}
